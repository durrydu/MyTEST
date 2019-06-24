using Movit.Application.Busines.CapitalFlowManage;
using Movit.Application.Entity;
using Movit.Application.Entity.EcomPartnerCapitalPoolManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.CapitalFlowManage
{

    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-02 13:40
    /// 描 述：T_Funds_Details
    /// </summary>
    public class T_Funds_DetailsController : MvcControllerBase
    {
        private T_Funds_DetailsBLL t_funds_detailsbll = new T_Funds_DetailsBLL();

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
        public ActionResult T_Funds_DetailsForm()
        {
            return View();
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
            var data = t_funds_detailsbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        [HttpGet]
        public ActionResult GetPieDataList(string queryJson)
        {
            var data = t_funds_detailsbll.GetPieDataList(queryJson);
            return ToJsonResult(data);
        }
        [HttpGet]
        public ActionResult GetLineDataList(string queryJson)
        {
            List<T_PartnerCapitalPoolViewModel> result = new List<T_PartnerCapitalPoolViewModel>();
            T_PartnerCapitalPoolViewModel rcModel = null;
            var data = t_funds_detailsbll.GetLineDataList(queryJson);
            var month = string.Empty;
            for (int i = 1; i <= 12; i++)
            {
                month = i.ToString();
                //发现是单数补全一位
                if (i.ToString().Length == 1)
                {
                    month = "0" + i.ToString();
                }
                #region 组装收入信息
                //收入
                var modelShouru = data.FirstOrDefault(p => p.sDate.Substring(5, 2) == month && p.AccountingType == 0);
                rcModel = new T_PartnerCapitalPoolViewModel();
                rcModel.CurrentBalance = (modelShouru != null ? modelShouru.CurrentBalance : 0);
                rcModel.sDate = i.ToString() + "月";
                rcModel.AccountingType = 0;
                result.Add(rcModel);
                rcModel = null;
                #endregion
                #region 组装支出信息
                //收支出
                var modechu = data.FirstOrDefault(p => p.sDate.Substring(5, 2) == month && p.AccountingType == 1);
                rcModel = new T_PartnerCapitalPoolViewModel();
                rcModel.CurrentBalance = (modechu != null ? modechu.CurrentBalance : 0);
                rcModel.sDate = i.ToString() + "月";
                rcModel.AccountingType = 1;
                result.Add(rcModel);
                rcModel = null;
                #endregion
            }
            return ToJsonResult(result);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = t_funds_detailsbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取资金池数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFundStaticJson(string queryJson)
        {
            var data = t_funds_detailsbll.GetFundStaticJson(queryJson);
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
            try
            {
                t_funds_detailsbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, T_Funds_DetailsEntity entity)
        {
            try
            {
                t_funds_detailsbll.SaveForm(keyValue, entity);
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

