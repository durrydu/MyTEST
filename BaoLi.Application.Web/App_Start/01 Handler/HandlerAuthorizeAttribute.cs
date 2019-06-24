﻿using Movit.Application.Busines.AuthorizeManage;
using Movit.Application.Code;
using Movit.Util;
using Movit.Util.Extension;
using System.Web;
using System.Web.Mvc;

namespace BaoLi.Application.Web
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人：姚栋 
    /// 日 期：2015.11.9 10:45
    /// 描 述：（权限认证+安全）拦截组件
    /// </summary>
    public class HandlerAuthorizeAttribute : ActionFilterAttribute
    {
        private PermissionMode _customMode;
        /// <summary>默认构造</summary>
        /// <param name="Mode">认证模式</param>
        public HandlerAuthorizeAttribute(PermissionMode Mode)
        {
            _customMode = Mode;
        }
        /// <summary>
        /// 权限认证
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //是否超级管理员
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return;
            }
            //是否忽略
            if (_customMode == PermissionMode.Ignore)
            {
                return;
            }
           
            ////认证执行
            //if (!this.ActionAuthorize(filterContext))
            //{
            //    ContentResult Content = new ContentResult();
            //    Content.Content = "<script type='text/javascript'>alert('很抱歉！您的权限不足，访问被拒绝！');top.Loading(false);</script>";
            //    filterContext.Result = Content;
            //    return;
            //}
        }
      
        /// <summary>
        /// 执行权限认证
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool ActionAuthorize(ActionExecutingContext filterContext)
        {
            string currentUrl = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            return new AuthorizeBLL().ActionAuthorize(SystemInfo.CurrentUserId, SystemInfo.CurrentModuleId, currentUrl);
        }
    }
}