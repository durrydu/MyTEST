using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using Movit.Application.Busines.SystemManage;

namespace Movit.Sys.Api
{
    /// <summary>
    /// API访问授权验证
    /// </summary>
    public class AuthenticateAttribute : ActionFilterAttribute
    {

        #region 公共属性
        #endregion

        #region 公共方法
        /// <summary>
        /// The on action executing.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            try
            {
                bool isAuthenticated = this.IsAuthenticated(actionContext);

                if (!isAuthenticated)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    actionContext.Response = response;
                }
            }
            catch (Exception ex)
            {

                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                actionContext.Response = response;
            }

        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 验证用户请求是否合法
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <returns></returns>
        private bool IsAuthenticated(HttpActionContext actionContext)
        {

            return true;
        }



        #endregion
    }
}