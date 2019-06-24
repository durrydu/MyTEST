
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Movit.Sys.Api
{
    [Authenticate]
    [RoutePrefix("api")]
    public class BaseApiControl : ApiController
    {
        #region 请求上下文
        public HttpRequestBase RequestApi
        {
            get
            {
                HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                HttpRequestBase request = context.Request;//定义传统request对象
                return request;
            }


        }
        #endregion
        #region 获得字段错误信息提示
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetError()
        {
            string strError = string.Empty;
            if (!ModelState.IsValid)
            {
                List<string> sb = new List<string>();
                //获取所有错误的Key
                List<string> Keys = ModelState.Keys.ToList();
                //获取每一个key对应的ModelStateDictionary
                foreach (var key in Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    //将错误描述添加到sb中
                    foreach (var error in errors)
                    {
                        sb.Add(error.ErrorMessage);
                    }
                }
                strError = sb[0].ToString();//取第一条错误
            }
            return strError;
        }

        #endregion
    }
}