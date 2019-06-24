using Movit.Application.Entity;
using Movit.Application.Busines;
using Movit.Util;
using Movit.Util.WebControl;
using System.Web.Mvc;
using BaoLi.Application.Web;
using System;
using Movit.Application.Code;
using Movit.Application.Busines.SystemManage;

namespace BaoLi.Application.Web.Areas.EcommerceManage.Controllers
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：durry.du
    /// 日 期：2018-06-19 10:50
    /// 描 述：Ecommerce
    /// </summary>
    public class EcommerceController : MvcControllerBase
    {
        private EcommerceBLL ecommercebll = new EcommerceBLL();
        private CodeRuleBLL codeBll = new CodeRuleBLL();

        #region 视图功能
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
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        ///<summary>
        ///详情页面
        /// </summary>
        /// <returns></returns>>
        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取电商平台性质
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [OutputCache(Duration = 30000)]
        public ActionResult GetEcommerceTypeEnum()
        {
            var DesInfo = EnumHelper.ToDescriptionList<EcommerceTypeEnum>();
            return ToJsonResult(DesInfo);
        }
        /// <summary>
        /// 电商公司列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = ecommercebll.GetPageList(pagination, queryJson);
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
        public ActionResult GetListJson(string queryJson)
        {
            var data = ecommercebll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = ecommercebll.GetEntity(keyValue);           
            return ToJsonResult(data);
        }
        [HttpGet]
        public ActionResult GetCodeRole(string keyValue)
        {
            var curretUser = OperatorProvider.Provider.Current();
            var data = codeBll.SetBillCodeByCode(curretUser.UserId, "300");
            return ToJsonResult(data);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 电商公司不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistEcommerceName(string EcommerceName, string keyValue)
        {
            bool IsOk = ecommercebll.ExistEcommerceName(EcommerceName, keyValue);
            return Content(IsOk.ToString());
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
            try
            {
                ecommercebll.RemoveForm(keyValue);
                return Success("删除成功。");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
        public ActionResult SaveForm(string keyValue, EcommerceEntity entity)
        {
            try
            {
                ecommercebll.SaveForm(keyValue, entity);
                return Success("操作成功。");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}