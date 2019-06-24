using Movit.Application.Entity;
using Movit.Application.Busines;
using Movit.Util;
using Movit.Util.WebControl;
using System.Web.Mvc;
using Movit.Application.Busines.AuthorizeManage;

namespace BaoLi.Application.Web.Areas.BaseManage.Controllers
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：姚栋
    /// 日 期：2018-05-30 13:49
    /// 描 述：GetListJson
    /// </summary>
    public class Base_ProjectInfoController : MvcControllerBase
    {
        private Base_ProjectInfoBLL base_projectinfobll = new Base_ProjectInfoBLL();
        private AuthorizeBLL authorizebll = new AuthorizeBLL();
        #region 视图功能
        /// <summary>
        /// 项目列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProjectIndex() 
        {
            return View();
        }
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SelectProject()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 详细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }
        #endregion

        #region 获取数据
        ///<summary>
        ///作者：durry
        ///time：2018-06-22 11:20
        ///获取区域公司下拉
        /// </summary>
        [HttpGet]
        public ActionResult GetCompanyNameJson(string queryJson) 
        {
            var data = base_projectinfobll.GetCompanyName(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取区域绑定的项目列表
        /// </summary>
        /// <param name="queryJson">区域id（支持多个区域）</param>
        /// <returns></returns>
        public ActionResult GetListByCompanyid(string queryJson)
        {
            var data = base_projectinfobll.GetListByCompanyid(queryJson);
            return ToJsonResult(data);
        }
        //根据权限获取项目下拉
        [HttpGet]
        public ActionResult GetProjectListByRole(string keyValue) {
            var data = base_projectinfobll.GetListByAuthorize(keyValue);
            return ToJsonResult(data);
        }
        ///<summary>
        ///作者：clare
        ///time：2018-06-26 11:20
        ///根据项目名字获取电商名字
        /// </summary>
        [HttpGet]
        public ActionResult GetEcomGroupNameJson(string queryJson)
        {
            var data = base_projectinfobll.GetEcomGroupNameJson(queryJson);
            return ToJsonResult(data);
        }
        ///<summary>
        ///作者：clare
        ///time：2018-06-26 11:20
        ///根据公司名字获取电商集团
        /// </summary>
        [HttpGet]
        public ActionResult GetEcomGroupNameByEconmJson(string queryJson)
        {
            var data = base_projectinfobll.GetEcomGroupNameByEconmJson(queryJson);
            return ToJsonResult(data);
        }
        ///<summary>
        ///作者：clare
        ///time：2018-06-26 11:20
        ///根据项目名电商名字查询出来资金
        /// </summary>
        [HttpGet]
        public ActionResult GetMoneyByEconmProjectJson(string queryJson, string queryValue)
        {
            var data = base_projectinfobll.GetMoneyByEconmProjectJson(queryJson, queryValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="pagintion">分页</param>>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = base_projectinfobll.GetPageList(pagination, queryJson);
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
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string keyValue)
        {
            var data = base_projectinfobll.GetList(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 根据岗位ID获取已经授权过的项目信息
        /// </summary>
        /// <param name="keyValue">角色ID</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetProjectListByPostId(string keyValue, string queryJson, Pagination pagination)
        {
            var watch = CommonHelper.TimerStart();
            var data = authorizebll.GetProjectListByPostId(keyValue, queryJson, pagination);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 根据岗位ID获取未授权的项目信息
        /// </summary>
        /// <param name="keyValue">角色ID</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetProjectExcept(string keyValue, Pagination pagination, string queryJson)
        {


            var watch = CommonHelper.TimerStart();
            var data = authorizebll.GetProjectExcept(keyValue, pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 获取Sum实际可支配总金额
        /// 作者：durry
        /// time：2018-06-21 19：30
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = base_projectinfobll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        ///<summary>
        ///获取项目带出的区域公司
        ///作者：durry
        ///time：2018-06-21 14：00
        /// </summary>
        [HttpGet]
        public ActionResult GetAreaName(string keyValue)
        {
            var data = base_projectinfobll.GetAreaName(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            base_projectinfobll.RemoveForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 批量删除岗位项目授权数据
        /// </summary>
        /// <param name="authorizeIds">授权ID主键集合</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult BatchRemoveForm(string authorizeIds)
        {
            authorizebll.BatchRemoveForm(authorizeIds);
            return Success("移除成功。");
        }

        /// <summary>
        /// 移除全部岗位项目授权数据
        /// 作者:姚栋
        /// 日期:2018.05.31
        /// </summary>
        /// <param name="keyValue">岗位ID</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult BatchRemoveFormAll(string keyValue)
        {
            authorizebll.BatchRemoveFormAll(keyValue);
            return Success("移除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, Base_ProjectInfoEntity entity)
        {
            base_projectinfobll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}