using Movit.Application.Busines.EcommercePayQueryManage;
using Movit.Application.Entity.EcommercePayQueryManage.ViewModel;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movit.Util;
using Movit.Util.Extension;
using System.Data;
using Movit.Util.Offices;

namespace BaoLi.Application.Web.Areas.EcommercePayQueryManage.Controllers
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：durry
    /// 日 期：2018-07-02 09:54
    /// 描 述：Pay_Info
    public class EcomPayTotalController : MvcControllerBase
    {
        EcomPayTotalBLL eptbll = new EcomPayTotalBLL();
        #region 视图功能
        //
        // GET: /EcommercePayQueryManage/EcomPayTotal/
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取资金总览列表数据
        /// 作者：durry
        /// time：2018-07-06
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public ActionResult GetListJson(Pagination pagination, string queryJson, int page = 1)
        {
            pagination.rows = 10;
            pagination.page = page;
            var watch = CommonHelper.TimerStart();
            //找到所有的区域公司
            var companydata = eptbll.GetListJson(pagination, queryJson).ToList();
            //找不到区域的话不拼成树
            if (companydata.Count == 0)
            {
                return ToJsonResult(companydata);
            }
           //找到所有的数据
            var alllist = eptbll.GetAllList(queryJson);
            //ViewBag.StatisticalDate = companydata[0].StatisticalDate;
            List<CompanyView> lists = new List<CompanyView>();
            var praentid = "0";
            var currentProjctName = string.Empty;
            foreach (var item in companydata)
            {
                ///拼接头
                lists.Add(new CompanyView()
                {
                    id = Guid.NewGuid().ToString(),
                    Companyid = item.Companyid,
                    CompanyName = item.CompanyName,
                    ProjectName = "汇总",
                    EcommerceGroupName = "",
                    ControlTotalAmount = item.ControlTotalAmount,
                    FlowNopayTotalAmount = item.FlowNopayTotalAmount,
                    ActualControlTotalAmount = item.ActualControlTotalAmount,
                    StatisticalDate = item.StatisticalDate,
                    praentid = praentid,
                });
                //
                var sencondlist = alllist.Where(p => p.Companyid == item.Companyid).ToList();
                for (int i = 0; i < sencondlist.Count; i++)
                {
                    if (currentProjctName != sencondlist[i].ProjectName)
                    {

                        currentProjctName = sencondlist[i].ProjectName;
                        praentid = sencondlist[i].id;
                        sencondlist[i].CompanyName = "";
                        sencondlist[i].praentid = lists[0].praentid;
                    }
                    else
                    {
                        sencondlist[i].CompanyName = "";
                        sencondlist[i].ProjectName = "";
                        sencondlist[i].praentid = praentid;
                    }
                    currentProjctName = sencondlist[i].ProjectName;
                }
                lists.AddRange(sencondlist);
                praentid = "0";
            }
            var treeList = new List<TreeGridEntity>();

            foreach (CompanyView item in lists)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = lists.Count(t => t.praentid == item.id) == 0 ? false : true;
                tree.id = item.id;
                if (item.praentid == "0")
                {
                    tree.parentId = "0";
                }
                else
                {
                    tree.parentId = item.praentid;
                }
                tree.expanded = true;
                tree.hasChildren = hasChildren;
                string itemJson = item.ToJson();
                itemJson = itemJson.Insert(1, "\"Sort\":\"xmmc\",");
                tree.entityJson = itemJson;
                treeList.Add(tree);
            }
            //var JsonData = new
            //{
            //    rows = treeList.TreeJson(),
            //    total = pagination.total,
            //    page = pagination.page,
            //    records = pagination.records,
            //    costtime = CommonHelper.TimerEnd(watch)
            //};

            return Content(treeList.TreeJson(pagination.total, pagination.page, pagination.records, CommonHelper.TimerEnd(watch)));


        }
        #endregion

        #region 提交数据
        #endregion

        #region 导出数据
        [HttpGet]

        public ActionResult GetPayToalForExport(Pagination pagination, string queryJson)
        {
            pagination.rows = 10000;
            pagination.page = 1;
            pagination.sord = "";
            var companydata = eptbll.GetListJson(pagination, queryJson);
            var alllist = eptbll.GetAllList(queryJson);

            List<CompanyView> lists = new List<CompanyView>();
            var praentid = "0";
            var currentProjctName = string.Empty;
            foreach (var item in companydata)
            {
                lists.Add(new CompanyView()
                {
                    id = Guid.NewGuid().ToString(),
                    Companyid = item.Companyid,
                    CompanyName = item.CompanyName,
                    ProjectName = "汇总",
                    EcommerceGroupName = "",
                    ControlTotalAmount = item.ControlTotalAmount,
                    FlowNopayTotalAmount = item.FlowNopayTotalAmount,
                    ActualControlTotalAmount = item.ActualControlTotalAmount,
                    praentid = praentid,
                });
                var sencondlist = alllist.Where(p => p.Companyid == item.Companyid).ToList();
                for (int i = 0; i < sencondlist.Count; i++)
                {
                    if (currentProjctName != sencondlist[i].ProjectName)
                    {

                        currentProjctName = sencondlist[i].ProjectName;
                        praentid = sencondlist[i].id;
                        sencondlist[i].CompanyName = "";
                        sencondlist[i].praentid = lists[0].praentid;
                    }
                    else
                    {
                        sencondlist[i].CompanyName = "";
                        sencondlist[i].ProjectName = "";
                        sencondlist[i].praentid = praentid;
                    }
                    currentProjctName = sencondlist[i].ProjectName;
                }
                lists.AddRange(sencondlist);
                praentid = "0";
            }
            for (int i = 0; i < lists.Count; i++)
            {
                lists[i].ActualControlTotalAmount = Convert.ToDecimal(String.Format("{0:F}", lists[i].ActualControlTotalAmount));
                lists[i].FlowNopayTotalAmount = Convert.ToDecimal(String.Format("{0:F}", lists[i].FlowNopayTotalAmount));
                lists[i].ControlTotalAmount = Convert.ToDecimal(String.Format("{0:F}", lists[i].ControlTotalAmount));
            }
            //导出
            DataTable dt = DataHelper.ListToDataTable<CompanyView>(lists);
            DataTable dtnew = dt.DefaultView.ToTable(false, new string[] { "CompanyName", "ProjectName", "EcommerceGroupName",
           "ControlTotalAmount","FlowNopayTotalAmount","ActualControlTotalAmount",
            });

            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.Title = "电商资金查询总览";
            excelconfig.TitleFont = "微软雅黑";
            excelconfig.TitlePoint = 25;
            excelconfig.FileName = "电商资金查询总览.xls";
            excelconfig.IsAllSizeColumn = true;
            //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
            List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
            excelconfig.ColumnEntity = listColumnEntity;
            ColumnEntity columnentity = new ColumnEntity();
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "CompanyName", ExcelColumn = "区域公司" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProjectName", ExcelColumn = "项目名称" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "EcommerceGroupName", ExcelColumn = "电商简称" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ControlTotalAmount", ExcelColumn = "我司可支配金额(元)" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "FlowNopayTotalAmount", ExcelColumn = "流程中未付款金额(元)" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ActualControlTotalAmount", ExcelColumn = "实际可支配总金额(元)" });
            //调用导出方法
            ExcelHelper.ExcelDownload(dtnew, excelconfig);
            return Success("");
        }
        #endregion
    }
}