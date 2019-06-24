using Movit.Application.Entity;
using Movit.Application.Busines;
using Movit.Util;
using Movit.Util.WebControl;
using System.Web.Mvc;
using BaoLi.Application.Web;
using System;
using System.Data;
using System.Collections.Generic;
using Movit.Util.Offices;
using Movit.Application.Cache;
using System.Linq;
using Movit.Application.Code;

namespace BaoLi.Application.Web.Areas.EcommercePayManage.Controllers
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-06-22 09:54
    /// 描 述：Pay_Info
    /// </summary>
    public class Pay_InfoController : MvcControllerBase
    {
        private Pay_InfoBLL pay_infobll = new Pay_InfoBLL();
        private DataItemCache dataItemCache = new DataItemCache();


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
        #endregion

        #region 获取数据
        ///<summary>
        ///作者：杜强
        ///Time:2018-06-22 13:40
        ///获取分页列表
        /// </summary>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = pay_infobll.GetPageList(pagination, queryJson);
            var geturl = dataItemCache.GetDataItemByCodeAndName("SysConfig", "BPMApplicateRoad");


            foreach (var item in data)
            {
                item.Url = string.Format("{0}?procInstId={1}&userid={2}&key={3}", geturl.ItemValue, item.Procinstid, item.CreateUserId, BpmMD5Helper.GetEnCodeStr(item.Procinstid));
            }

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
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = pay_infobll.GetList(queryJson);
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
            var data = pay_infobll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        ///<summary>
        ///导出Excel
        ///作者：durry
        ///time：2018-07-02 13：27
        /// </summary>
        [HttpGet]
        public ActionResult GetPayListForExport(string queryJson, Pagination pagination)
        {
            var data = pay_infobll.GetList(queryJson).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].Pay_Money = Convert.ToDecimal(String.Format("{0:F}", data[i].Pay_Money));
            }
            DataTable dt = DataHelper.ListToDataTable<Pay_InfoEntity>(data);

            DataTable dtnew = dt.DefaultView.ToTable(false, new string[] { "CompanyName", "Project_Name","EcommerceGroupName", "Electricity_Supplier_Name",
           "Pay_Info_Code","Contract_Name","Pay_Money","Pay_Createtime",
            "Pay_Completetime","Login_Name","Approval_Status"
            });
            for (int i = 0; i < dtnew.Rows.Count; i++)
            {
                dtnew.Rows[i]["Approval_Status"] = EnumHelper.ToDescription((ApproveStatus)Convert.ToInt32(dtnew.Rows[i]["Approval_Status"]));
            }
            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.Title = "付款台账记录";
            excelconfig.TitleFont = "微软雅黑";
            excelconfig.TitlePoint = 25;
            excelconfig.FileName = "付款台账记录.xls";
            excelconfig.IsAllSizeColumn = true;
            //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
            List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
            excelconfig.ColumnEntity = listColumnEntity;
            ColumnEntity columnentity = new ColumnEntity();
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "CompanyName", ExcelColumn = "区域公司" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Project_Name", ExcelColumn = "项目名称" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "EcommerceGroupName", ExcelColumn = "电商简称" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Electricity_Supplier_Name", ExcelColumn = "电商名称" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Pay_Info_Code", ExcelColumn = "支付流序号" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Contract_Name", ExcelColumn = "合同名称" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Pay_Money", ExcelColumn = "付款金额(元)" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Pay_Createtime", ExcelColumn = "支付流程发起时间" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Pay_Completetime", ExcelColumn = "支付流程审批通过时间" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Login_Name", ExcelColumn = "经办人" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Approval_Status", ExcelColumn = "审批状态" });
            //调用导出方法
            ExcelHelper.ExcelDownload(dtnew, excelconfig);
            return Success("");
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
                pay_infobll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, Pay_InfoEntity entity)
        {
            try
            {
                pay_infobll.SaveForm(keyValue, entity);
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