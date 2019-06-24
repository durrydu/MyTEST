using Movit.Application.Busines.EcommerceTransferManage;
using Movit.Application.Busines.SystemManage;
using Movit.Application.Code;
using Movit.Application.Entity.EcommerceTransferManage;
using Movit.Util;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.EcommerceTransferManage.Controllers
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-06-25 14:48
    /// 描 述：T_Transfer_Info
    /// </summary>
    public class Transfer_InfoController : MvcControllerBase
    {
        private Transfer_InfoBLL transfer_infobll = new Transfer_InfoBLL();
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
        /// <summary>
        /// 详情页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(string keyValue)
        {
            ViewBag.keyValue = keyValue;
            return View();
        }
        [HttpGet]
        public ActionResult ReForm(string keyValue, string ProjectID, string Transfer_Money, string EcommerceID)
        {
            ViewBag.keyValue = keyValue;
            ViewBag.ProjectID = ProjectID;
            ViewBag.Transfer_Money = Transfer_Money;
            ViewBag.EcommerceID = EcommerceID;
            return View();
        }
        #endregion

        #region  获取分页
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPageListJson(string keyValue, Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = transfer_infobll.GetPageList(keyValue, pagination, queryJson);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch),
            };
            return Content(JsonData.ToJson());
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = transfer_infobll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 GetProEcomJson
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = transfer_infobll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取详情 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetProEcomJson(string keyValue)
        {
            var data = transfer_infobll.GetProEcomJson(keyValue);
            return ToJsonResult(data);
        }
        [HttpGet]
        public ActionResult GetCodeRole(string keyValue)
        {
             var curretUser = OperatorProvider.Provider.Current();
             var data = codeBll.SetBillCodeByCode(curretUser.UserId, "200");
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
        //[ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult DeleteRemark(string keyValue, string queryJson, string ProjectID, string Transfer_Money, string EcommerceID)
        {
            try
            {
                transfer_infobll.DeleteRemark(keyValue, queryJson, ProjectID, Transfer_Money, EcommerceID);
                return Success("删除成功。");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 删除表单
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
                transfer_infobll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, Transfer_InfoEntity entity, string ProjectID, string EcommerceID, string ActualControlTotalAmount, string Transfer_Code)
        {
            try
            {
                string errMsg = string.Empty;
                transfer_infobll.SaveForm(keyValue, entity, ProjectID, EcommerceID, ActualControlTotalAmount, Transfer_Code, out errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    return Success(errMsg);
                }
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

