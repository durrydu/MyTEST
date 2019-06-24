using Movit.Application.Busines.SystemManage;
using Movit.Application.Code;
using Movit.Util;
using Movit.Util.WebControl;
using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-2016   
    /// 创建人：姚栋
    /// 日 期：2015.11.18 9:56
    /// 描 述：系统日志
    /// </summary>
    public class LogController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 日志详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Details()
        {
            return View();
        }
        /// <summary>
        /// 日志管理-登陆日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 日志管理-异常日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult AppErrorIndex()
        {
            return View();
        }
        /// <summary>
        /// 日志管理-接口日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult InterfaceIndex()
        {
            return View();
        }
        /// <summary>
        /// 日志管理-操作日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult OperationIndex()
        {
            return View();
        }
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveLog()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = LogBLL.GetEntity(keyValue);

            return Content(data.ToJson());
        }
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = LogBLL.GetPageList(pagination, queryJson);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="categoryId">日志分类Id</param>
        /// <param name="keepTime">保留时间段内</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveLog(int categoryId, string keepTime)
        {
            LogBLL.RemoveLog(categoryId, keepTime);
            return Success("清空成功。");
        }
        #endregion
    }
}
