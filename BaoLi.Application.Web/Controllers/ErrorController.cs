using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoLi.Application.Web.Controllers
{
   
    /// </summary>
    public class ErrorController : Controller
    {
        #region 视图功能
        /// <summary>
        /// 错误页面（异常页面）
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ActionResult ErrorMessage(string message)
        {
           
            Dictionary<string, string> modulesError = (Dictionary<string, string>)HttpContext.Application["error"];
            ViewData["Message"] = modulesError;
            return View();
        }
        /// <summary>
        /// 错误页面（错误路径404）
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorPath404()
        {
            return View();
        }
        /// <summary>
        /// 错误页面（升级浏览器软件）
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorBrowser()
        {
            return View();
        }
        #endregion

        #region 提交数据
        #endregion
    }
}
