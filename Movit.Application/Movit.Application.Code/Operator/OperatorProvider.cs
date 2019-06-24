using Movit.Cache.Factory;
using Movit.Util;
using System;
using Movit.Util.Extension;

namespace Movit.Application.Code
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人：姚栋
    /// 日 期：2015.10.10
    /// 描 述：当前操作者回话
    /// </summary>
    public class OperatorProvider : OperatorIProvider
    {
        #region 静态实例
        /// <summary>
        /// 当前提供者
        /// </summary>
        public static OperatorIProvider Provider
        {
            get { return new OperatorProvider(); }
        }
        /// <summary>
        /// 给app调用
        /// </summary>
        public static string AppUserId
        {
            set;
            get;
        }
        #endregion

        /// <summary>
        /// 秘钥
        /// </summary>
        private string LoginUserKey = "LoginUserKey";
        /// <summary>
        /// 登陆提供者模式:Session、Cookie 
        /// </summary>
        private string LoginProvider = Config.GetValue("LoginProvider");
        /// <summary>
        /// 写入登录信息
        /// </summary>
        /// <param name="user">成员信息</param>
        public virtual void AddCurrent(Operator user)
        {
            try
            {
                var LoginUserAuthorizesKey = LoginUserKey + user.UserId;
                if (LoginProvider == "Cookie")
                {
                    #region 解决cookie时，设置数据权限较多时无法登陆的bug
                    CacheFactory.Cache().RemoveCache(LoginUserAuthorizesKey);
                    CacheFactory.Cache().WriteCache(user.DataAuthorize, LoginUserAuthorizesKey, user.LogTime.AddDays(24));
                    user.DataAuthorize = null;
                    #endregion
                    WebHelper.WriteCookie(LoginUserKey, DESEncrypt.Encrypt(user.ToJson()));
                }
                else
                {
                    WebHelper.WriteSession(LoginUserAuthorizesKey, DESEncrypt.Encrypt(user.ToJson()));
                }
                //CacheFactory.Cache().WriteCache(LoginUserKey, user.UserId, user.LogTime.AddHours(12));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 当前用户
        /// </summary>
        /// <returns></returns>
        public virtual Operator Current()
        {
            try
            {

                Operator user = new Operator();
                if (LoginProvider == "Cookie")
                {
                    user = DESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey).ToString()).ToObject<Operator>();

                    if (user != null)
                    {
                        var LoginUserAuthorizesKey = LoginUserKey + user.UserId;
                        #region 解决cookie时，设置数据权限较多时无法登陆的bug
                        AuthorizeDataModel dataAuthorize = CacheFactory.Cache().GetCache<AuthorizeDataModel>(LoginUserAuthorizesKey);
                        user.DataAuthorize = dataAuthorize;
                        #endregion

                        //System.IO.File.AppendAllText("d:\\epclog.txt", "Current=>GetCache:UserID:" + user.UserId + ";CacheCount:" + dataButtonAuthority_View.Count() + "\r\n");
                    }


                }
                else if (LoginProvider == "AppClient")
                {
                    user = CacheFactory.Cache().GetCache<Operator>(AppUserId);
                }
                else
                {
                    user = DESEncrypt.Decrypt(WebHelper.GetSession(LoginUserKey).ToString()).ToObject<Operator>();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 删除登录信息
        /// </summary>
        public virtual void EmptyCurrent()
        {
            Operator user = new Operator();
            if (LoginProvider == "Cookie")
            {
                //user = DESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey).ToString()).ToObject<Operator>();
                //var LoginUserAuthorizesKey = LoginUserKey + user.UserId;
                WebHelper.RemoveCookie(LoginUserKey.Trim());
                //#region 解决cookie时，设置数据权限较多时无法登陆的bug
                //CacheFactory.Cache().RemoveCache(LoginUserAuthorizesKey);
                //#endregion
            }
            else
            {
                WebHelper.RemoveSession(LoginUserKey.Trim());
            }
        }
        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns></returns>
        public virtual bool IsOverdue()
        {
            try
            {
                Operator user = new Operator();
                object str = "";
                //AuthorizeDataModel dataAuthorize = null;
                if (LoginProvider == "Cookie")
                {
                    str = WebHelper.GetCookie(LoginUserKey);
                    user = DESEncrypt.Decrypt(str.ToString()).ToObject<Operator>();
                    if (user == null)
                    {
                        return true;
                    }
                }
                else
                {
                    str = WebHelper.GetSession(LoginUserKey);
                }
                if (str != null && str.ToString() != "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }
        /// <summary>
        /// 是否已登录
        /// </summary>
        /// <returns></returns>
        public virtual int IsOnLine()
        {
            Operator user = new Operator();
            if (LoginProvider == "Cookie")
            {
                user = DESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey).ToString()).ToObject<Operator>();
                if (user != null)
                {
                    return -1;//过期
                }
                var LoginUserAuthorizesKey = LoginUserKey + user.UserId;
                #region 解决cookie时，设置数据权限较多时无法登陆的bug
                AuthorizeDataModel dataAuthorize = CacheFactory.Cache().GetCache<AuthorizeDataModel>(LoginUserAuthorizesKey);
                user.DataAuthorize = dataAuthorize;
                #endregion
            }
            else
            {
                user = DESEncrypt.Decrypt(WebHelper.GetSession(LoginUserKey).ToString()).ToObject<Operator>();
            }
            object token = CacheFactory.Cache().GetCache<string>(user.UserId);
            if (token == null)
            {
                return -1;//过期
            }
            if (user.Token == token.ToString())
            {
                return 1;//正常
            }
            else
            {
                return 0;//已登录
            }
        }


        public virtual string GetLoginUrl()
        {
            var LoginUrl = "/Login/Index";
            bool SsoLogin = Config.GetValue("SsoLogin").ToBool();
            if (SsoLogin)
            {
                LoginUrl = Config.GetValue("SsoLoginPageUrl");
            }
            return LoginUrl;
        }
    }
}
