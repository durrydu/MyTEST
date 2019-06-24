
using Movit.Application.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoLi.Application.Web.Controllers
{
    
    [HandlerLogin(LoginMode.Enforce)]
    public class HomeController : MvcControllerBase
    {
      
        /// <summary>
        /// 日期:2018.05.26
        /// 作者:姚栋
        /// 描述：后台框架页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminPretty()
        {
            return View();
        }
        /// <summary>
        /// 日期:2018.05.26
        /// 作者:姚栋
        /// 描述：首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 描述:首页消息中心
        /// 作者:姚栋
        /// 日期:20180719
        /// </summary>
        /// <returns></returns>
        public ActionResult MessageIndex()
        {
            return View();
        }

    }
}