using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movit.Util;
using Movit.Util.Extension;
using Movit.Application.Busines.EcommercePayQueryManage;
using Movit.Application.Entity.EcommercePayQueryManage.ViewModel;
using System.Data;
using Movit.Util.Offices;

namespace BaoLi.Application.Web.Areas.EcommercePayQueryManage.Controllers
{
    public class EcomPayEcommerceController : MvcControllerBase
    {
        private EcomPayEcommerceBLL epebll = new EcomPayEcommerceBLL();
        #region 视图功能
        //
        // GET: /EcommercePayQueryManage/EcomPayEcommerce/
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPageListJson(Pagination pagination, string queryJson, int page = 1)
        {
            if (queryJson == "")
            {
                queryJson = null;
            }
            pagination.rows = 10;
            pagination.page = page;
            var watch = CommonHelper.TimerStart();
            var ecommmerceList = epebll.GetEcommerceGroupList(pagination,queryJson);
            if(ecommmerceList.ToList().Count==0)
            {
                return ToJsonResult(ecommmerceList);
            }
            var companyList = epebll.GetCompanyList(queryJson);

            List<ProjectView> showlists = new List<ProjectView>();

            var praentid = "0";
            foreach(var item in ecommmerceList)
            {
                showlists.Add(new ProjectView
                {
                    id=item.id,
                    EcommerceGroupID=item.EcommerceGroupID,
                    EcommerceGroupName=item.EcommerceGroupName,
                    CompanyName="汇总",
                    IncomeTotal=item.IncomeTotal,
                    ClearingTotal=item.ClearingTotal,
                    Platform="-",
                    PlatformExpensesAmount=item.PlatformExpensesAmount,
                    ControllAmount=item.ControllAmount,
                    EcommerceExpenseTotal=item.EcommerceExpenseTotal,
                    TransfoTotal=item.TransfoTotal,
                    praentid = praentid,
                });
                var secondlist = companyList.Where(p => p.EcommerceGroupID == item.EcommerceGroupID).ToList();
                for (int i = 0; i < secondlist.Count; i++)
                {
                    secondlist[i].EcommerceGroupName = "";
                    secondlist[i].praentid = item.id;
                }
                showlists.AddRange(secondlist);
                praentid = "0";
            }
            var treeList = new List<TreeGridEntity>();

            foreach (ProjectView item in showlists)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = showlists.Count(t => t.praentid == item.id) == 0 ? false : true;
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
            return Content(treeList.TreeJson(pagination.total, pagination.page, pagination.records, CommonHelper.TimerEnd(watch)));
        }
        #endregion
        #region 导出数据
        public ActionResult GetPayEcommerceGroupForExport(Pagination pagination,string queryJson)
        {
            pagination.rows = 10000;
            pagination.page = 1;
            pagination.sord = "";
            if (queryJson == "")
            {
                queryJson = null;
            }
            var ecommmerceList = epebll.GetEcommerceGroupList(pagination,queryJson);
            var companyList = epebll.GetCompanyList(queryJson);

            List<ProjectView> showlists = new List<ProjectView>();

            var praentid = "0";
            foreach (var item in ecommmerceList)
            {
                showlists.Add(new ProjectView
                {
                    id = item.id,
                    EcommerceGroupID = item.EcommerceGroupID,
                    EcommerceGroupName = item.EcommerceGroupName,
                    CompanyName = "汇总",
                    IncomeTotal = item.IncomeTotal,
                    ClearingTotal = item.ClearingTotal,
                    Platform = "-",
                    PlatformExpensesAmount = item.PlatformExpensesAmount,
                    EcommerceExpenseTotal=item.EcommerceExpenseTotal,
                    TransfoTotal = item.TransfoTotal,
                    ControllAmount = item.ControllAmount,
                    praentid = praentid,
                });
                var secondlist = companyList.Where(p => p.EcommerceGroupID == item.EcommerceGroupID).ToList();
                for (int i = 0; i < secondlist.Count; i++)
                {
                    secondlist[i].EcommerceGroupName = "";
                    secondlist[i].praentid = item.id;
                }
                showlists.AddRange(secondlist);
                praentid = "0";
            }
            for (int i = 0; i < showlists.Count; i++)
            {
                showlists[i].IncomeTotal = String.Format("{0:F}", Convert.ToDecimal(showlists[i].IncomeTotal));
                showlists[i].ClearingTotal = String.Format("{0:F}", Convert.ToDecimal(showlists[i].ClearingTotal));
                showlists[i].ControllAmount = String.Format("{0:F}", Convert.ToDecimal(showlists[i].ControllAmount));
                showlists[i].EcommerceExpenseTotal = String.Format("{0:F}", Convert.ToDecimal(showlists[i].EcommerceExpenseTotal));
                showlists[i].TransfoTotal = String.Format("{0:F}", Convert.ToDecimal(showlists[i].TransfoTotal));
                showlists[i].PlatformExpensesAmount = String.Format("{0:F}", Convert.ToDecimal(showlists[i].PlatformExpensesAmount));
            }
            DataTable dt = DataHelper.ListToDataTable<ProjectView>(showlists);
            DataTable dtnew = dt.DefaultView.ToTable(false, new string[] { "EcommerceGroupName", "CompanyName", "IncomeTotal",
           "ClearingTotal","Platform","PlatformExpensesAmount", "ControllAmount","EcommerceExpenseTotal","TransfoTotal"
            });
            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.Title = "电商资金查询（电商简称）";
            excelconfig.TitleFont = "微软雅黑";
            excelconfig.TitlePoint = 25;
            excelconfig.FileName = "电商资金查询（电商简称）.xls";
            excelconfig.IsAllSizeColumn = true;
            //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
            List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
            excelconfig.ColumnEntity = listColumnEntity;
            ColumnEntity columnentity = new ColumnEntity();
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "EcommerceGroupName", ExcelColumn = "电商简称" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "CompanyName", ExcelColumn = "区域公司" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "IncomeTotal", ExcelColumn = "收入合计(元)" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ClearingTotal", ExcelColumn = "结算合计(元)" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Platform", ExcelColumn = "平台费比例(%)" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "PlatformExpensesAmount", ExcelColumn = "平台费支出(元)" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ControllAmount", ExcelColumn = "我司可支配金额(元)" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "EcommerceExpenseTotal", ExcelColumn = "电商支出合计(元)" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "TransfoTotal", ExcelColumn = "划拨金额(元)" });
            //调用导出方法
            ExcelHelper.ExcelDownload(dtnew, excelconfig);
            return Success("");
        }
        #endregion
    }
}