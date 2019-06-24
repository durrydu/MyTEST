using Movit.Util;
using Movit.Util.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Movit.Util.Extension;

namespace BaoLi.Application.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {


        public static int MaxInterval = Config.GetValue("MaxInterval").ToInt();
        private Log _logger;
        /// <summary>
        /// 日志操作
        /// </summary>
        public Log Logger
        {
            get { return _logger ?? (_logger = LogFactory.GetLogger(this.GetType().ToString())); }
        }
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleTable.EnableOptimizations = true;
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        /// <summary>
        /// 应用程序错误处理
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
        }
        #region 性能查询 姚栋 20180531
#if DEBUG
        protected DateTime dt;
        protected void Application_BeginRequest(Object sender, EventArgs E)
        {
            dt = DateTime.Now;

        }
        protected void Application_EndRequest(Object sender, EventArgs E)
        {
            DateTime dt2 = DateTime.Now;
            TimeSpan ts = dt2 - dt;
            if (ts.TotalMilliseconds >= MaxInterval)//秒以上的慢页面进行记录
            {
                var msg = "\r\n" + dt2.ToString("yyyy-MM-dd hh:mm:ss fff") + ":[当前请求URL：" + HttpContext.Current.Request.Url + "；请求的参数为：" + HttpContext.Current.Request.QueryString + "；页面加载的时间：" + ts.TotalMilliseconds.ToString() + " 毫秒]";
                Logger.Debug(msg);
            }

        }
#endif
        /// <summary>
        /// 性能查询
        /// </summary>
        /*使用说明
            #if DEBUG
            [ActionPerformance]
            #endif
            public ActionResult Detail(string id = "", long partnerid=0)
        */
        public class ActionPerformance : ActionFilterAttribute
        {
            public static int MaxInterval = Config.GetValue("MaxInterval").ToInt();
            private Log _logger;
            /// <summary>
            /// 日志操作
            /// </summary>
            public Log Logger
            {
                get { return _logger ?? (_logger = LogFactory.GetLogger(this.GetType().ToString())); }
            }
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                GetTimer(filterContext, "action").Stop();

                base.OnActionExecuted(filterContext);
            }

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {

                GetTimer(filterContext, "action").Start();

                base.OnActionExecuting(filterContext);
            }



            public override void OnResultExecuted(ResultExecutedContext filterContext)
            {


                var renderTimer = GetTimer(filterContext, "render");
                renderTimer.Stop();

                var actionTimer = GetTimer(filterContext, "action");
                var response = filterContext.HttpContext.Response;

                if (response.ContentType == "text/html")
                {
                    if (actionTimer.ElapsedMilliseconds >= MaxInterval || renderTimer.ElapsedMilliseconds >= MaxInterval)
                    {
                        string result = String.Format(
                             "\r\n{0}.{1}, Action执行时间: {2}毫秒, View执行时间: {3}毫秒.",
                             filterContext.RouteData.Values["controller"],
                             filterContext.RouteData.Values["action"],
                             actionTimer.ElapsedMilliseconds,
                             renderTimer.ElapsedMilliseconds
                         );
                        Logger.Debug(result);
                    }
                }

                base.OnResultExecuted(filterContext);
            }

            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {
                GetTimer(filterContext, "render").Start();

                base.OnResultExecuting(filterContext);
            }

            private Stopwatch GetTimer(ControllerContext context, string name)
            {
                string key = "__timer__" + name;
                if (context.HttpContext.Items.Contains(key))
                {
                    return (Stopwatch)context.HttpContext.Items[key];
                }

                var result = new Stopwatch();
                context.HttpContext.Items[key] = result;
                return result;
            }

        }
        #endregion
    }
}
