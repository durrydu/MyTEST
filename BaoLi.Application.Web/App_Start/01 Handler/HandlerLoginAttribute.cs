using System.Web.Mvc;
using Movit.Application.Code;
using Movit.Util;
using System.Web;
using Movit.Util.Extension;
using YANGO.SSO.Utility;
using System;
using Movit.Application.Entity.SystemManage;
using Movit.Util.Attributes;
using Movit.Application.Busines.AuthorizeManage;
using Movit.Application.Busines.SystemManage;
using Movit.Application.Busines.BaseManage;
using System.Collections.Generic;

namespace BaoLi.Application.Web
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人：fwq
    /// 日 期：2015.11.9 10:45
    /// 描 述：登录认证（会话验证组件）
    /// </summary>
    public class HandlerLoginAttribute : AuthorizeAttribute
    {
        private LoginMode _customMode;
        /// <summary>默认构造</summary>
        /// <param name="Mode">认证模式</param>
        public HandlerLoginAttribute(LoginMode Mode)
        {
            _customMode = Mode;
        }
        /// <summary>
        /// 响应前执行登录验证,查看当前用户是否有效 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //登录拦截是否忽略
            if (_customMode == LoginMode.Ignore)
            {
                return;
            }
            #region 单点登录
            bool SsoLogin = Config.GetValue("SsoLogin").ToBool();
            if (SsoLogin)
            {
                if (!CheckLogin_SSO(filterContext))
                {
                    return;
                }
            }
            #endregion
            //登录是否过期
            if (OperatorProvider.Provider.IsOverdue())
            {
                var isRequestAjax = filterContext.HttpContext.Request.IsAjaxRequest();
                if (isRequestAjax)//如果是ajax请求
                {
                    filterContext.HttpContext.Response.StatusCode = 200;
                    filterContext.HttpContext.Response.StatusDescription = "TimeOut";
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    WebHelper.WriteCookie("movit_login_error", "Overdue");//登录已超时,请重新登录
                    RedirectToLogin(filterContext);
                }

                return;
            }
            //是否已登录
            //var OnLine = OperatorProvider.Provider.IsOnLine();
            //if (OnLine == 0)
            //{
            //    bool checkOnLine = Config.GetValue("CheckOnLine").ToBool();//是否允许重复登录
            //    if (!checkOnLine)
            //    {
            //        WebHelper.WriteCookie("movit_login_error", "OnLine");//您的帐号已在其它地方登录,请重新登录
            //        RedirectToLogin(filterContext);
            //        return;
            //    }
            //}
            //else if (OnLine == -1)
            //{
            //    WebHelper.WriteCookie("movit_login_error", "-1");//缓存已超时,请重新登录
            //    filterContext.Result = new RedirectResult("~/Login/Default");
            //    return;
            //}
        }
        private bool RedirectToLogin(AuthorizationContext authorizationContext)
        {

            var content = new ContentResult();
            content.Content = "<script type='text/javascript'>top.window.location.href='/Login/Index'</script>";
            authorizationContext.Result = content;
            return false;
        }
        private bool RedirectToError(AuthorizationContext authorizationContext, Dictionary<string, string> errorMsg)
        {
            authorizationContext.HttpContext.Application["error"] = errorMsg;
            var content = new ContentResult();
            content.Content = "<script type='text/javascript'>top.window.location.href='/Error/ErrorMessage'</script>";
            authorizationContext.Result = content;
            return false;


        }
        #region SSO模拟登录

        public bool CheckLogin_SSO(AuthorizationContext filterContext)
        {
            string LoginName = OptionMananger.Instance.GetLoginName();
            if (!string.IsNullOrEmpty(LoginName))
            {
                //获取当前用户信息
                var currentUser = OperatorProvider.Provider.Current();
                //模拟登录
                AnalogLogon(filterContext, LoginName, currentUser);
            }
            else
            {
                Dictionary<string, string> errorMsg = new Dictionary<string, string>();
                errorMsg.Add("非法用户", "未授权的用户禁止使用本系统!");

                //TODO 跳转到错误页面，并携带错误信息过去
                return RedirectToError(filterContext, errorMsg);
            }

            return true;


        }
        private bool IsSsoResquest()
        {
            //判断是否是从SSO登录过来的 
            if (!string.IsNullOrEmpty(Net.RequstUrl) && Net.RequstUrl.ToLower().IndexOf("sso") > -1)
            {
                return true;

            }
            return false;
        }
        #region 登出系统
        private void LoginOut(AuthorizationContext filterContext)
        {
            #region 先进行登出系统

            filterContext.RequestContext.HttpContext.Session.Abandon();                                          //清除当前会话
            filterContext.RequestContext.HttpContext.Session.Clear();                                            //清除当前浏览器所有Session
            OperatorProvider.Provider.EmptyCurrent(); ;                  //清除登录者信息
            WebHelper.RemoveCookie("movit_autologin");                  //清除自动登录
            #endregion

        }
        #endregion
        #region 模拟登录
        private bool AnalogLogon(AuthorizationContext filterContext, string LoginName, Operator Current)
        {
            bool needLogin = false;
            //当前用户未登录且SSO登录用户信息不为空,需要进行模拟登录
            if (Current == null && !string.IsNullOrEmpty(LoginName))
            {

                needLogin = true;
            }
            //判断当前用户是否登录如果登录了，检查当前登录的用户是否和SSO用户不一致，如果不一致就进行重写登录
            else if (Current != null && (Current.Account != LoginName))
            {
                LoginOut(filterContext);
                needLogin = true;
            }
            // 当前用户已经登录且用户相同，不需要登录
            else if (Current != null && (Current.Account == LoginName))
            {

                needLogin = false;
            }
            #region  模拟登录

            if (needLogin)
            {
                var userEntity = new UserBLL().CheckLogin(LoginName);
                if (userEntity != null)
                {
                    AuthorizeBLL authorizeBLL = new AuthorizeBLL();
                    Operator operators = new Operator();
                    operators.UserId = userEntity.UserId;
                    operators.Account = userEntity.Account;
                    operators.UserName = userEntity.RealName;
                    operators.DepartmentId = userEntity.DepartmentId;
                    //operators.IPAddress = Net.Ip;
                    //operators.IPAddressName = IPLocation.GetLocation(Net.Ip);
                    operators.LogTime = DateTime.Now;
                    operators.DepartmentName = userEntity.DepartmentName;
                    //写入当前用户数据权限
                    AuthorizeDataModel dataAuthorize = new AuthorizeDataModel();
                    //dataAuthorize.ReadAutorize = authorizeBLL.GetDataAuthor(operators);
                    dataAuthorize.GetReadProjectId = authorizeBLL.GetReadProjectId(operators);
                    //dataAuthorize.WriteAutorize = authorizeBLL.GetDataAuthor(operators, true);
                    //dataAuthorize.WriteAutorizeUserId = authorizeBLL.GetDataAuthorUserId(operators, true);
                    operators.DataAuthorize = dataAuthorize;

                    OperatorProvider.Provider.AddCurrent(operators);
                    #region 写入登录日志
                    LogEntity logEntity = new LogEntity();
                    logEntity.CategoryId = 1;
                    logEntity.OperateTypeId = ((int)OperationType.Login).ToString();
                    logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Login);
                    logEntity.OperateAccount = userEntity.RealName;
                    logEntity.OperateUserId = userEntity.UserId;
                    logEntity.Module = "SSOLogin=>System";
                    //写入日志
                    logEntity.ExecuteResult = 1;
                    logEntity.ExecuteResultJson = "登录成功";
                    logEntity.WriteLog();
                    #endregion
                }
            }
            #endregion
            return true;
        }
        #endregion
        #endregion
    }
}