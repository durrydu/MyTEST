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
using Movit.Application.Code;
using System.Data;
using Movit.Util.Offices;

namespace BaoLi.Application.Web.Areas.EcommercePayQueryManage.Controllers
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：durry
    /// 日 期：2018-07-02 09:54
    /// 描 述：EcomPayProject
    public class EcomPayProjectController : MvcControllerBase
    {
        EcomPayProjectBLL eppbll = new EcomPayProjectBLL();
        #region 视图功能
        //
        // GET: /EcommercePayQueryManage/EcomPayProject/
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson, int page = 1)
        {
            if(queryJson=="")
            {
                queryJson = null;
            }
            pagination.rows = 10;
            pagination.page = page;
            var watch = CommonHelper.TimerStart();
            var companydata = eppbll.GetAllCompany(pagination,queryJson);
            var projectdata = eppbll.GetAllProject(queryJson);
            if (companydata.ToList().Count == 0)
            {
                return ToJsonResult(companydata);
            }
            var alldata = eppbll.GetAllList(queryJson);

            List<ProjectView> prjectviews = new List<ProjectView>();
            var praentid = "0";
            var currentCompanyid = string.Empty;

            foreach (var item in companydata)
            {
                var projectlist = projectdata.Where(p => p.CompanyID == item.CompanyID).ToList();
                for (int i = 0; i < projectlist.Count; i++)
                {
                    if (i == 0)
                    {
                        praentid = projectlist[i].id;
                        projectlist[i].EcommerceGroupName = "汇总";
                        projectlist[i].Platform = "-";
                        projectlist[i].praentid = "0";
                        prjectviews.Add(projectlist[i]);
                        var secondlist = alldata.Where(p => p.ProjectID == projectlist[i].ProjectID).ToList();
                        for (int j = 0; j < secondlist.Count; j++)
                        {
                            secondlist[j].CompanyName = "";
                            secondlist[j].ProjectName = "";
                            secondlist[j].praentid = praentid;
                        }
                        prjectviews.AddRange(secondlist);
                    }
                    else
                    {
                        praentid = projectlist[i].id;
                        projectlist[i].praentid = "0";
                        projectlist[i].CompanyName = "";
                        projectlist[i].EcommerceGroupName = "汇总";
                        projectlist[i].Platform = "-";
                        prjectviews.Add(projectlist[i]);
                        var secondlist = alldata.Where(p => p.ProjectID == projectlist[i].ProjectID).ToList();
                        for (int j = 0; j < secondlist.Count; j++)
                        {
                            secondlist[j].CompanyName = "";
                            secondlist[j].ProjectName = "";
                            secondlist[j].praentid = praentid;
                        }
                        prjectviews.AddRange(secondlist);
                    }
                }
            }
            var treeList = new List<TreeGridEntity>();

            foreach (ProjectView item in prjectviews)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = prjectviews.Count(t => t.praentid == item.id) == 0 ? false : true;
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
        public ActionResult GetPayProjectForExport(Pagination pagination,string queryJson)
        {
            pagination.rows = 10000;
            pagination.page = 1;
            pagination.sord = "";
            if (queryJson == "")
            {
                queryJson = null;
            }
            var companydata = eppbll.GetAllCompany(pagination,queryJson);
            var projectdata = eppbll.GetAllProject(queryJson);
            var alldata = eppbll.GetAllList(queryJson);

            List<ProjectView> prjectviews = new List<ProjectView>();
            var praentid = "0";
            var currentCompanyid = string.Empty;

            foreach (var item in companydata)
            {
                var projectlist = projectdata.Where(p => p.CompanyID == item.CompanyID).ToList();
                for (int i = 0; i < projectlist.Count; i++)
                {
                    if (i == 0)
                    {
                        praentid = projectlist[i].id;
                        projectlist[i].EcommerceGroupName = "汇总";
                        projectlist[i].Platform = "-";
                        projectlist[i].praentid = "0";
                        prjectviews.Add(projectlist[i]);
                        var secondlist = alldata.Where(p => p.ProjectID == projectlist[i].ProjectID).ToList();
                        for (int j = 0; j < secondlist.Count; j++)
                        {
                            secondlist[j].CompanyName = "";
                            secondlist[j].ProjectName = "";
                            secondlist[j].praentid = praentid;
                        }
                        prjectviews.AddRange(secondlist);
                    }
                    else
                    {
                        praentid = projectlist[i].id;
                        projectlist[i].praentid = "0";
                        projectlist[i].CompanyName = "";
                        projectlist[i].EcommerceGroupName = "汇总";
                        projectlist[i].Platform = "-";
                        prjectviews.Add(projectlist[i]);
                        var secondlist = alldata.Where(p => p.ProjectID == projectlist[i].ProjectID).ToList();
                        for (int j = 0; j < secondlist.Count; j++)
                        {
                            secondlist[j].CompanyName = "";
                            secondlist[j].ProjectName = "";
                            secondlist[j].praentid = praentid;
                        }
                        prjectviews.AddRange(secondlist);
                    }
                }
            }
            for (int i = 0; i < prjectviews.Count;i++ )
            {
                prjectviews[i].IncomeTotal = String.Format("{0:F}", Convert.ToDecimal(prjectviews[i].IncomeTotal));
                prjectviews[i].ClearingTotal = String.Format("{0:F}", Convert.ToDecimal(prjectviews[i].ClearingTotal));
                prjectviews[i].ControllAmount = String.Format("{0:F}", Convert.ToDecimal(prjectviews[i].ControllAmount));
                prjectviews[i].EcommerceExpenseTotal = String.Format("{0:F}", Convert.ToDecimal(prjectviews[i].EcommerceExpenseTotal));
                prjectviews[i].TransfoTotal = String.Format("{0:F}", Convert.ToDecimal(prjectviews[i].TransfoTotal));
                prjectviews[i].PlatformExpensesAmount = String.Format("{0:F}", Convert.ToDecimal(prjectviews[i].PlatformExpensesAmount));
            }
            DataTable dt = DataHelper.ListToDataTable<ProjectView>(prjectviews);
            DataTable dtnew = dt.DefaultView.ToTable(false, new string[] { "CompanyName", "ProjectName", "EcommerceGroupName",
           "IncomeTotal","ClearingTotal","Platform","PlatformExpensesAmount", "ControllAmount","EcommerceExpenseTotal","TransfoTotal"
            });
            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.Title = "电商资金查询（项目）";
            excelconfig.TitleFont = "微软雅黑";
            excelconfig.TitlePoint = 25;
            excelconfig.FileName = "电商资金查询（项目）.xls";
            excelconfig.IsAllSizeColumn = true;
            //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
            List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
            excelconfig.ColumnEntity = listColumnEntity;
            ColumnEntity columnentity = new ColumnEntity();
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "CompanyName", ExcelColumn = "区域公司" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProjectName", ExcelColumn = "项目名称" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "EcommerceGroupName", ExcelColumn = "电商简称" });
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