using Movit.Application.Busines;
using Movit.Application.Busines.AuthorizeManage;
using Movit.Application.Busines.BaseManage;
using Movit.Application.Busines.CapitalFlow;
using Movit.Application.Cache;
using Movit.Application.Code;
using Movit.Application.Entity;
using Movit.Application.Entity.BaseManage;
using Movit.Application.Entity.CapitalFlow;
using Movit.Application.Entity.CapitalFlowManage.ViewModel;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Util;
using Movit.Util.Offices;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.CapitalFlowManage.Controllers
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-02 10:55
    /// 描 述：T_CapitalFlow
    /// </summary>
    public class T_CapitalFlowController : MvcControllerBase
    {
        private T_CapitalFlowBLL t_capitalflowbll = new T_CapitalFlowBLL();
        private EcommerceProjectRelationBLL ecombll = new EcommerceProjectRelationBLL();
        private AuthorizeBLL auth = new AuthorizeBLL();
        private DataItemCache dataItemCache = new DataItemCache();
        private T_CapitalFlow_NodeBLL t_capitalflow_nodebll = new T_CapitalFlow_NodeBLL();
        private UserBLL userbll = new UserBLL();
        private DepartmentBLL depart = new DepartmentBLL();
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
        [HttpGet]
        public ActionResult GetYear()
        {
            int Year = 2017;

            DataTable year = new DataTable();
            year.Columns.Add("id", typeof(string));
            year.Columns.Add("value", typeof(string));

            for (int i = Year; i < Year + 15; i++)
            {
                year.Rows.Add(i, i);

            }
            return ToJsonResult(year);
        }
        [HttpGet]
        public ActionResult GetMonth()
        {


            DataTable month = new DataTable();
            month.Columns.Add("id", typeof(string));
            month.Columns.Add("value", typeof(string));

            for (int i = 1; i <= 12; i++)
            {
                month.Rows.Add(i, i);

            }
            return ToJsonResult(month);
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddForm(string keyValue)
        {
            ViewBag.keyValue = keyValue;
            var curretUser = OperatorProvider.Provider.Current();
            ViewBag.EmployeeNum = curretUser.Code;
            ViewBag.Name = curretUser.UserName;
            var departEntity = depart.GetEntity(curretUser.DepartmentId);
            if (departEntity != null)
            {
                ViewBag.DepartmentName = departEntity.FullName;
            }
            else
            {
                ViewBag.DepartmentName = curretUser.DepartmentName;
            }

            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        public ActionResult Form(string keyValue)
        {
            ViewBag.keyValue = keyValue;
            var curretUser = OperatorProvider.Provider.Current();
            ViewBag.EmployeeNum = curretUser.Code;
            ViewBag.Name = curretUser.UserName;
            var departEntity = depart.GetEntity(curretUser.DepartmentId);
            if (departEntity != null)
            {
                ViewBag.DepartmentName = departEntity.FullName;
            }
            else
            {
                ViewBag.DepartmentName = curretUser.DepartmentName;
            }
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        public ActionResult Detail(string keyValue)
        {
            ViewBag.keyValue = keyValue;
            //var curretUser = OperatorProvider.Provider.Current();
            //ViewBag.EmployeeNum = curretUser.Code;
            //ViewBag.Name = curretUser.UserName;
            //ViewBag.DepartmentName = curretUser.DepartmentName;
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
            var data = t_capitalflowbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var geturl = dataItemCache.GetDataItemByCodeAndName("SysConfig", "BPMApplicateRoad");
            string urlname = geturl.ItemValue;
            var data = t_capitalflowbll.GetPageList(pagination, queryJson, urlname);
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
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = t_capitalflowbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        //编辑页面
        public ActionResult GetFormData(string keyValue)
        {
            var data = t_capitalflowbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        //详情页面
        public ActionResult GetDetailData(string keyValue)
        {
            var data = t_capitalflowbll.GetEn(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 是否存在相同表单
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="keyValue">主键</param>
        /// <param name="companyid">区域id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IsExistedS(int year, int month, string keyValue, string companyid)
        {
            //拿到所有的数据
            var data = t_capitalflowbll.GetList("").ToList();
            if (keyValue != null && keyValue != "")
            {
                //找到主键存在的数据
                var item = data.Where(p => p.CapitalFlow_Id.Contains(keyValue)).FirstOrDefault();
                //移除这条数据
                bool flag = data.Remove(item);
            }
            //假设一开始不存在
            bool isexited = false;
            //草稿状态下的数据
            bool drft = data.Any(t => t.Company_Id == companyid && t.Year == year && t.Month == month && t.ApprovalState == 1);
            //审批通过的数据
            bool approve = data.Any(t => t.Company_Id == companyid && t.Year == year && t.Month == month && t.ApprovalState == 4);
            
            if (!drft && !approve)
            {
                //草稿和审批通过都不存在的时候
                isexited = false;
            }
            else
            {
                isexited = true;
            }
            return ToJsonResult(isexited);
        }
        #endregion


        #region 清除excel空行

        protected DataTable clearExcel(DataTable excel)
        {

            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < excel.Rows.Count; i++)
            {
                bool IsNull = true;
                for (int j = 0; j < excel.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(excel.Rows[i][j].ToString().Trim()))
                    {
                        IsNull = false;
                    }
                }
                if (IsNull)
                {
                    removelist.Add(excel.Rows[i]);
                }
            }
            for (int i = 0; i < removelist.Count; i++)
            {
                excel.Rows.Remove(removelist[i]);
            }
            return excel;

        }
        #endregion
        #region 判断列头是否和模板一致
        public string IsColumnTrue(DataTable excel)
        {
            if(!excel.Columns.Contains("项目"))
            {
                return ("Excel中列头【项目】校对失败,请检查后重新导入！");
            }
            if (!excel.Columns.Contains("平台费比例"))
            {
                return ("Excel中列头【平台费比例】校对失败,请检查后重新导入！");
            }
            if (!excel.Columns.Contains("电商公司(全称)"))
            {
                return ("Excel中列头【电商公司(全称)】校对失败,请检查后重新导入！");
            }
            if (!excel.Columns.Contains("收入合计(元)"))
            {
                return ("Excel中列头【收入合计(元)】校对失败,请检查后重新导入！");
            }
            if (!excel.Columns.Contains("结算合计(元)"))
            {
                return ("Excel中列头【结算合计(元)】校对失败,请检查后重新导入！");
            }
            if (!excel.Columns.Contains("平台费支出(元)"))
            {
                return ("Excel中列头【平台费支出(元)】校对失败,请检查后重新导入！");
            }
            if (!excel.Columns.Contains("我司新增可支配金额(元)"))
            {
                return ("Excel中列头【我司新增可支配金额(元)】校对失败,请检查后重新导入！");
            }
            return "";
        }
        #endregion
        #region 提交数据
        /// <summary>
        /// 读取Excel
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult ReadExcel(string url)
        {
            #region 检测文件有效性
            //1、首先检查附件存不存在
            DataTable excel = new DataTable();
            DataTable dw = new DataTable();
            url = DirFileHelper.MapBasePath(url);
            string error = string.Empty;
            if (!DirFileHelper.IsExistFile(url))
            {
                return Error("导入文件不存在！");
            }
            else
            {
                //2、检验上传文件的格式是否正确,其实就是检查列头是否一致
                //2、1检测每列是否和模板一致
                bool isFlag = true;
                excel = ExcelHelper.ExcelImport(url);

                if (excel != null)
                {
                    string msg = IsColumnTrue(excel);
                    if (msg=="")
                    {
                        ///自娱自乐
                            ///查看excel里面内容是否合法
                            foreach (DataRow item in excel.Rows)
                            {
                                float test0 = 0;
                                if (item["平台费比例"].ToString().Trim() != "")
                                {
                                    string plantplat = item["平台费比例"].ToString().Trim().Substring(0, item["平台费比例"].ToString().Trim().Length - 1);
                                    bool test5 = float.TryParse(plantplat, out test0);
                                    if (!test5)
                                    {
                                        return Error("列名【平台费比例】中”" + item["平台费比例"].ToString() + "”数据为非数字类型，导入失败！");
                                    }
                                }
                                if (item["收入合计(元)"].ToString().Trim() != "")
                                {
                                    bool test1 = float.TryParse(item["收入合计(元)"].ToString().Trim(), out test0);
                                    if (!test1)
                                    {
                                        return Error("列名【收入合计(元)】中“" + item["收入合计(元)"].ToString() + "“数据为非数字类型，导入失败！");
                                    }
                                }
                                if (item["结算合计(元)"].ToString().Trim() != "")
                                {
                                    bool test2 = float.TryParse(item["结算合计(元)"].ToString().Trim(), out test0);
                                    if (!test2)
                                    {
                                        return Error("列名【结算合计(元)】中“" + item["结算合计(元)"].ToString() + "”数据为非数字类型，导入失败！");
                                    }
                                }
                                if (item["平台费支出(元)"].ToString().Trim() != "")
                                {
                                    bool test3 = float.TryParse(item["平台费支出(元)"].ToString().Trim(), out test0);
                                    if (!test3)
                                    {
                                        return Error("列名【平台费支出(元)】中”" + item["平台费支出(元)"].ToString() + "“数据为非数字类型，导入失败！");
                                    }
                                }
                                if (item["我司新增可支配金额(元)"].ToString().Trim() != "")
                                {
                                    bool test4 = float.TryParse(item["我司新增可支配金额(元)"].ToString().Trim(), out test0);
                                    if (!test4)
                                    {
                                        return Error("列名【我司新增可支配金额(元)】中”" + item["我司新增可支配金额(元)"].ToString() + "”数据为非数字类型，导入失败！");
                                    }
                                }
                            }
                   
                        //3、获取当前用户所能查看的电商合同信息
                        var UserId = OperatorProvider.Provider.Current().UserId;
                        //清除空行
                        excel = clearExcel(excel);
                        //3.1 首先查询当前用户所关联的岗位，有没有是授权全部的  查试图 view_post_user
                        if (auth.LookAll())
                        {
                            DataTable ss = new DataTable();
                            ss.Columns.Add("column");
                            foreach (DataRow item in excel.Rows)
                            {
                                DataRow z = ss.NewRow();
                                z["column"] = item["项目"].ToString() + item["电商公司(全称)"].ToString();
                                ss.Rows.Add(z);

                            }
                            DataView dv = new DataView(ss);
                            if (dv.Count != dv.ToTable(true, "column").Rows.Count)
                            {
                                isFlag = false;
                                return Error("Excel存在相同的项目和电商公司，请重新导入");
                            }
                            //获取excel 表格中所有的电商公司(全称)类
                            var EcomName = (from myrow in excel.AsEnumerable()
                                            select myrow.Field<string>("电商公司(全称)")).ToArray<string>();
                            for (int i = 0; i < EcomName.Length; i++)
                            {
                                EcomName[i] = EcomName[i].Trim();
                            }
                            string EcomNameStr = string.Join("','", EcomName);
                            ////获取ecxle传入公司所有的合同信息
                            IEnumerable<EcommerceProjectRelationEntity> ecomList = ecombll.GetAllListByEcomNameList(EcomNameStr);
                            excel.Columns.Add("EcomGroupName", typeof(string));
                            excel.Columns.Add("Company_Id", typeof(string));
                            excel.Columns.Add("EcommerceID", typeof(string));
                            excel.Columns.Add("EcommerceGroupID", typeof(string));
                            excel.Columns.Add("ProjectID", typeof(string));
                            excel.Columns.Add("EcommerceProjectRelationID", typeof(string));
                            excel.Columns.Add("CompanyName", typeof(string));

                            //如果有全部权限，直接获取所有的电商合同信息
                            IEnumerable<EcommerceProjectRelationEntity> proEcom = t_capitalflowbll.GetAllListByST();

                            foreach (DataRow item in excel.Rows)
                            {

                                item.BeginEdit();
                                var reuslt = proEcom.Any(p => p.ProjecName == item["项目"].ToString().Trim() && p.EcommerceName == item["电商公司(全称)"].ToString().Trim());
                                var z = ecomList.FirstOrDefault(p => p.ProjecName == item["项目"].ToString().Trim() && p.EcommerceName == item["电商公司(全称)"].ToString().Trim());
                                if (!reuslt)
                                {
                                    isFlag = false;
                                    error = string.Format("(项目){0}和(电商公司){1}不存在合同关系,请检查上传的数据是否正确！", item["项目"].ToString().Trim(), item["电商公司(全称)"].ToString().Trim());
                                    return Error(error);
                                }
                                if (z != null)
                                {
                                    item["EcomGroupName"] = z.EcommerceGroupName;
                                    item["Company_Id"] = z.CompanyCode;
                                    item["EcommerceID"] = z.EcommerceID;
                                    item["EcommerceGroupID"] = z.EcommerceGroupID;
                                    item["ProjectID"] = z.ProjectID;
                                    item["EcommerceProjectRelationID"] = z.EcommerceProjectRelationID;
                                    item["CompanyName"] = z.CompanyName;
                                    item.EndEdit();
                                }
                                else
                                {
                                    isFlag = false;
                                    error = string.Format("导入的Excel(项目){0}和(电商公司){1}不存在合同关系,请检查上传的数据是否正确！", item["项目"].ToString().Trim(), item["电商公司(全称)"].ToString().Trim());
                                    return Error(error);
                                }


                            }
                            var list = excel.AsEnumerable().Select(t => t.Field<string>("Company_Id")).ToList();

                            for (int i = 0; i < list.Count; i++)
                            {
                                if (i < list.Count - 1)
                                {
                                    if (list[i] != list[i + 1])
                                    {
                                        isFlag = false;
                                        return Error("Excel存在不同区域的数据，导入失败");
                                    }
                                }

                            }

                            dw = excel.Clone();
                            excel.DefaultView.Sort = "ProjectID asc";
                            excel = excel.DefaultView.ToTable();
                            string projectName = excel.Rows[0]["项目"].ToString();
                            decimal IncomeAmount = 0;
                            decimal ClearingAmount = 0;
                            decimal PlatformExpensesAmount = 0;
                            decimal CapitalPoolAdd = 0;
                            decimal IncomeAmountSum = 0;
                            decimal ClearingAmountSum = 0;
                            decimal PlatformExpensesAmountSum = 0;
                            decimal CapitalPoolAddSum = 0;
                            DataRow zz = excel.NewRow();

                            excel.Rows.Add(zz);
                            foreach (DataRow item in excel.Rows)
                            {
                                if (projectName == item["项目"].ToString())
                                {
                                    IncomeAmount += Convert.ToDecimal(item["收入合计(元)"].ToString().Trim());
                                    ClearingAmount += Convert.ToDecimal(item["结算合计(元)"].ToString());
                                    PlatformExpensesAmount += Convert.ToDecimal(item["平台费支出(元)"].ToString());
                                    CapitalPoolAdd += Convert.ToDecimal(item["我司新增可支配金额(元)"].ToString());
                                    IncomeAmountSum += Convert.ToDecimal(item["收入合计(元)"].ToString());
                                    ClearingAmountSum += Convert.ToDecimal(item["结算合计(元)"].ToString());
                                    PlatformExpensesAmountSum += Convert.ToDecimal(item["平台费支出(元)"].ToString());
                                    CapitalPoolAddSum += Convert.ToDecimal(item["我司新增可支配金额(元)"].ToString());
                                    DataRow dr = dw.NewRow();
                                    dr["收入合计(元)"] = item["收入合计(元)"].ToString();
                                    dr["结算合计(元)"] = item["结算合计(元)"].ToString();
                                    dr["平台费支出(元)"] = item["平台费支出(元)"].ToString();
                                    dr["我司新增可支配金额(元)"] = item["我司新增可支配金额(元)"].ToString();
                                    dr["项目"] = item["项目"].ToString();
                                    dr["电商公司(全称)"] = item["电商公司(全称)"].ToString();
                                    dr["平台费比例"] = item["平台费比例"].ToString();
                                    dr["EcomGroupName"] = item["EcomGroupName"].ToString();
                                    dr["Company_Id"] = item["Company_Id"].ToString();
                                    dr["EcommerceID"] = item["EcommerceID"].ToString();
                                    dr["EcommerceGroupID"] = item["EcommerceGroupID"].ToString();
                                    dr["ProjectID"] = item["ProjectID"].ToString();
                                    dr["EcommerceProjectRelationID"] = item["EcommerceProjectRelationID"].ToString();
                                    dr["CompanyName"] = item["CompanyName"].ToString();
                                    dw.Rows.Add(dr);
                                }
                                else
                                {
                                    DataRow dr = dw.NewRow();
                                    dr["收入合计(元)"] = IncomeAmount;
                                    dr["结算合计(元)"] = ClearingAmount;
                                    dr["平台费支出(元)"] = PlatformExpensesAmount;
                                    dr["我司新增可支配金额(元)"] = CapitalPoolAdd;
                                    dr["EcomGroupName"] = "项目小计";
                                    dw.Rows.Add(dr);


                                    if (!string.IsNullOrEmpty(item["项目"].ToString()))
                                    {
                                        DataRow dr1 = dw.NewRow();
                                        dr1["收入合计(元)"] = item["收入合计(元)"].ToString();
                                        dr1["结算合计(元)"] = item["结算合计(元)"].ToString();
                                        dr1["平台费支出(元)"] = item["平台费支出(元)"].ToString();
                                        dr1["我司新增可支配金额(元)"] = item["我司新增可支配金额(元)"].ToString();
                                        dr1["项目"] = item["项目"].ToString();
                                        dr1["电商公司(全称)"] = item["电商公司(全称)"].ToString();
                                        dr1["平台费比例"] = item["平台费比例"].ToString();
                                        dr1["EcomGroupName"] = item["EcomGroupName"].ToString();
                                        dr1["Company_Id"] = item["Company_Id"].ToString();
                                        dr1["EcommerceID"] = item["EcommerceID"].ToString();
                                        dr1["EcommerceGroupID"] = item["EcommerceGroupID"].ToString();
                                        dr1["ProjectID"] = item["ProjectID"].ToString();
                                        dr1["EcommerceProjectRelationID"] = item["EcommerceProjectRelationID"].ToString();
                                        dr1["CompanyName"] = item["CompanyName"].ToString();
                                        dw.Rows.Add(dr1);
                                        projectName = item["项目"].ToString();
                                        IncomeAmount = Convert.ToDecimal(item["收入合计(元)"].ToString());
                                        ClearingAmount = Convert.ToDecimal(item["结算合计(元)"].ToString());
                                        PlatformExpensesAmount = Convert.ToDecimal(item["平台费支出(元)"].ToString());
                                        CapitalPoolAdd = Convert.ToDecimal(item["我司新增可支配金额(元)"].ToString());
                                        IncomeAmountSum += Convert.ToDecimal(item["收入合计(元)"].ToString());
                                        ClearingAmountSum += Convert.ToDecimal(item["结算合计(元)"].ToString());
                                        PlatformExpensesAmountSum += Convert.ToDecimal(item["平台费支出(元)"].ToString());
                                        CapitalPoolAddSum += Convert.ToDecimal(item["我司新增可支配金额(元)"].ToString());
                                    }

                                }
                            }
                            DataRow drz = dw.NewRow();
                            drz["收入合计(元)"] = IncomeAmountSum;
                            drz["结算合计(元)"] = ClearingAmountSum;
                            drz["平台费支出(元)"] = PlatformExpensesAmountSum;
                            drz["我司新增可支配金额(元)"] = CapitalPoolAddSum;
                            drz["EcomGroupName"] = "区域合计";
                            dw.Rows.Add(drz);

                        }
                        else
                        {

                            //如果没有全部权限，通过视图view_post_project,找当前有项目的列表,共同项目ID 找到合同信息
                            //UserId = "172665f5-8dfd-4588-a828-fbdd025e989a";

                            IEnumerable<EcommerceProjectRelationEntity> proEcom = t_capitalflowbll.FindProjectByUser(UserId);
                            var EcomName = (from myrow in excel.AsEnumerable()
                                            select myrow.Field<string>("电商公司(全称)")).ToArray<string>();
                            string EcomNameStr = string.Join("','", EcomName);
                            DataTable ss = new DataTable();
                            ss.Columns.Add("column");

                            foreach (DataRow item in excel.Rows)
                            {
                                DataRow z = ss.NewRow();
                                z["column"] = item["项目"].ToString() + item["电商公司(全称)"].ToString();
                                ss.Rows.Add(z);
                            }
                            DataView dv = new DataView(ss);
                            if (dv.Count != dv.ToTable(true, "column").Rows.Count)
                            {
                                isFlag = false;
                                return Error("Excel存在相同的项目和电商公司，请重新导入");
                            }
                            //获取ecxle传入公司所有的合同信息
                            IEnumerable<EcommerceProjectRelationEntity> ecomList = ecombll.GetAllListByEcomNameList(EcomNameStr);
                            excel.Columns.Add("EcomGroupName", typeof(string));
                            excel.Columns.Add("Company_Id", typeof(string));
                            excel.Columns.Add("EcommerceID", typeof(string));
                            excel.Columns.Add("EcommerceGroupID", typeof(string));
                            excel.Columns.Add("ProjectID", typeof(string));
                            excel.Columns.Add("EcommerceProjectRelationID", typeof(string));
                            excel.Columns.Add("CompanyName", typeof(string));

                            foreach (DataRow item in excel.Rows)
                            {
                                item.BeginEdit();
                                var reuslt = proEcom.Any(p => p.ProjecName == item["项目"].ToString().Trim() && p.EcommerceName == item["电商公司(全称)"].ToString().Trim());
                                var z = ecomList.FirstOrDefault(p => p.ProjecName == item["项目"].ToString().Trim() && p.EcommerceName == item["电商公司(全称)"].ToString().Trim());
                                if (!reuslt)
                                {
                                    isFlag = false;
                                    error = string.Format("{0}和{1}不存在合同关系", item["项目"].ToString().Trim(), item["电商公司(全称)"].ToString().Trim());
                                    return Error(error);
                                }
                                if (z != null)
                                {
                                    item["EcomGroupName"] = z.EcommerceGroupName;
                                    item["Company_Id"] = z.CompanyCode;
                                    item["EcommerceID"] = z.EcommerceID;
                                    item["EcommerceGroupID"] = z.EcommerceGroupID;
                                    item["ProjectID"] = z.ProjectID;
                                    item["EcommerceProjectRelationID"] = z.EcommerceProjectRelationID;
                                    item["CompanyName"] = z.CompanyName;
                                    item.EndEdit();
                                }
                                else
                                {
                                    isFlag = false;
                                    error = string.Format("导入的Excel的{0}和{1}不存在合同关系", item["项目"].ToString().Trim(), item["电商公司(全称)"].ToString().Trim());
                                    return Error(error);
                                }

                            }
                            var list = excel.AsEnumerable().Select(t => t.Field<string>("Company_Id")).ToList();

                            for (int i = 0; i < list.Count; i++)
                            {
                                if (i < list.Count - 1)
                                {
                                    if (list[i] != list[i + 1])
                                    {
                                        isFlag = false;
                                        return Error("存在不同区域的数据，导入失败");
                                    }
                                }

                            }

                            dw = excel.Clone();
                            excel.DefaultView.Sort = "ProjectID asc";
                            excel = excel.DefaultView.ToTable();
                            string projectName = excel.Rows[0]["项目"].ToString();
                            decimal IncomeAmount = 0;
                            decimal ClearingAmount = 0;
                            decimal PlatformExpensesAmount = 0;
                            decimal CapitalPoolAdd = 0;
                            decimal IncomeAmountSum = 0;
                            decimal ClearingAmountSum = 0;
                            decimal PlatformExpensesAmountSum = 0;
                            decimal CapitalPoolAddSum = 0;
                            DataRow zz = excel.NewRow();

                            //zz["收入合计"] = "";
                            //zz["结算合计"] = "";
                            //zz["平台费支出"] = "";
                            //zz["我司新增可支配金额"] = "";
                            //zz["项目"] = "";
                            //zz["电商公司(全称)"] = "";
                            //zz["平台费比例"] = "";
                            //zz["EcomGroupName"] = "";
                            //zz["Company_Id"] = "";
                            //zz["EcommerceID"] = "";
                            //zz["EcommerceGroupID"] = "";
                            //zz["ProjectID"] = "";
                            //zz["EcommerceProjectRelationID"] = "";
                            //zz["CompanyName"] = "";
                            excel.Rows.Add(zz);
                            foreach (DataRow item in excel.Rows)
                            {
                                if (projectName == item["项目"].ToString())
                                {
                                    IncomeAmount += Convert.ToDecimal(item["收入合计(元)"].ToString());
                                    ClearingAmount += Convert.ToDecimal(item["结算合计(元)"].ToString());
                                    PlatformExpensesAmount += Convert.ToDecimal(item["平台费支出(元)"].ToString());
                                    CapitalPoolAdd += Convert.ToDecimal(item["我司新增可支配金额(元)"].ToString());
                                    IncomeAmountSum += Convert.ToDecimal(item["收入合计(元)"].ToString());
                                    ClearingAmountSum += Convert.ToDecimal(item["结算合计(元)"].ToString());
                                    PlatformExpensesAmountSum += Convert.ToDecimal(item["平台费支出(元)"].ToString());
                                    CapitalPoolAddSum += Convert.ToDecimal(item["我司新增可支配金额(元)"].ToString());
                                    DataRow dr = dw.NewRow();
                                    dr["收入合计(元)"] = item["收入合计(元)"].ToString();
                                    dr["结算合计(元)"] = item["结算合计(元)"].ToString();
                                    dr["平台费支出(元)"] = item["平台费支出(元)"].ToString();
                                    dr["我司新增可支配金额(元)"] = item["我司新增可支配金额(元)"].ToString();
                                    dr["项目"] = item["项目"].ToString();
                                    dr["电商公司(全称)"] = item["电商公司(全称)"].ToString();
                                    dr["平台费比例"] = item["平台费比例"].ToString();
                                    dr["EcomGroupName"] = item["EcomGroupName"].ToString();
                                    dr["Company_Id"] = item["Company_Id"].ToString();
                                    dr["EcommerceID"] = item["EcommerceID"].ToString();
                                    dr["EcommerceGroupID"] = item["EcommerceGroupID"].ToString();
                                    dr["ProjectID"] = item["ProjectID"].ToString();
                                    dr["EcommerceProjectRelationID"] = item["EcommerceProjectRelationID"].ToString();
                                    dr["CompanyName"] = item["CompanyName"].ToString();
                                    dw.Rows.Add(dr);
                                }
                                else
                                {
                                    DataRow dr = dw.NewRow();
                                    dr["收入合计(元)"] = IncomeAmount;
                                    dr["结算合计(元)"] = ClearingAmount;
                                    dr["平台费支出(元)"] = PlatformExpensesAmount;
                                    dr["我司新增可支配金额(元)"] = CapitalPoolAdd;
                                    dr["EcomGroupName"] = "项目小计";
                                    dw.Rows.Add(dr);


                                    if (!string.IsNullOrEmpty(item["项目"].ToString()))
                                    {
                                        DataRow dr1 = dw.NewRow();
                                        dr1["收入合计(元)"] = item["收入合计(元)"].ToString();
                                        dr1["结算合计(元)"] = item["结算合计(元)"].ToString();
                                        dr1["平台费支出(元)"] = item["平台费支出(元)"].ToString();
                                        dr1["我司新增可支配金额(元)"] = item["我司新增可支配金额(元)"].ToString();
                                        dr1["项目"] = item["项目"].ToString();
                                        dr1["电商公司(全称)"] = item["电商公司(全称)"].ToString();
                                        dr1["平台费比例"] = item["平台费比例"].ToString();
                                        dr1["EcomGroupName"] = item["EcomGroupName"].ToString();
                                        dr1["Company_Id"] = item["Company_Id"].ToString();
                                        dr1["EcommerceID"] = item["EcommerceID"].ToString();
                                        dr1["EcommerceGroupID"] = item["EcommerceGroupID"].ToString();
                                        dr1["ProjectID"] = item["ProjectID"].ToString();
                                        dr1["EcommerceProjectRelationID"] = item["EcommerceProjectRelationID"].ToString();
                                        dr1["CompanyName"] = item["CompanyName"].ToString();
                                        dw.Rows.Add(dr1);
                                        projectName = item["项目"].ToString();
                                        IncomeAmount = Convert.ToDecimal(item["收入合计(元)"].ToString());
                                        ClearingAmount = Convert.ToDecimal(item["结算合计(元)"].ToString());
                                        PlatformExpensesAmount = Convert.ToDecimal(item["平台费支出(元)"].ToString());
                                        CapitalPoolAdd = Convert.ToDecimal(item["我司新增可支配金额(元)"].ToString());
                                        IncomeAmountSum += Convert.ToDecimal(item["收入合计(元)"].ToString());
                                        ClearingAmountSum += Convert.ToDecimal(item["结算合计(元)"].ToString());
                                        PlatformExpensesAmountSum += Convert.ToDecimal(item["平台费支出(元)"].ToString());
                                        CapitalPoolAddSum += Convert.ToDecimal(item["我司新增可支配金额(元)"].ToString());
                                    }

                                }
                            }
                            DataRow drz = dw.NewRow();
                            drz["收入合计(元)"] = IncomeAmountSum;
                            drz["结算合计(元)"] = ClearingAmountSum;
                            drz["平台费支出(元)"] = PlatformExpensesAmountSum;
                            drz["我司新增可支配金额(元)"] = CapitalPoolAddSum;
                            drz["EcomGroupName"] = "区域合计";
                            dw.Rows.Add(drz);


                        }

                    }
                    else
                    {
                        return Error(msg);
                    }

                }
                else
                {
                    return Error("文件错误");
                }
                if (isFlag)
                {

                    dw.Columns["项目"].ColumnName = "Project_Name";
                    dw.Columns["电商公司(全称)"].ColumnName = "EcomName";
                    dw.Columns["收入合计(元)"].ColumnName = "IncomeAmount";
                    dw.Columns["结算合计(元)"].ColumnName = "ClearingAmount";
                    dw.Columns["我司新增可支配金额(元)"].ColumnName = "CapitalPoolAdd";
                    dw.Columns["平台费支出(元)"].ColumnName = "PlatformExpensesAmount";
                    dw.Columns["平台费比例"].ColumnName = "Proportion";
                    return ToJsonResult(dw);
                }
                else
                {
                    return Error("文件格式错误");
                }

            }
            //4、校验数据合法性
            //4.1 循环每条数据，对每一列进行数据校验
            #endregion
            //return null;

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult ReadCFNodeData(string keyValue)
        {
            List<CapitalFlow_CFNodeView> data = t_capitalflow_nodebll.GetEntityList(keyValue).ToList();
            data.Add(new CapitalFlow_CFNodeView()
                {
                    Proportion = 0,
                });
            DataTable excel = new DataTable();
            DataTable dw = new DataTable();
            excel.Columns.Add("Project_Name", typeof(string));
            excel.Columns.Add("EcomName", typeof(string));
            excel.Columns.Add("IncomeAmount", typeof(string));
            excel.Columns.Add("ClearingAmount", typeof(string));
            excel.Columns.Add("CapitalPoolAdd", typeof(string));
            excel.Columns.Add("PlatformExpensesAmount", typeof(string));
            excel.Columns.Add("Proportion", typeof(string));
            excel.Columns.Add("EcomGroupName", typeof(string));
            excel.Columns.Add("Company_Id", typeof(string));
            excel.Columns.Add("EcommerceID", typeof(string));
            excel.Columns.Add("EcommerceGroupID", typeof(string));
            excel.Columns.Add("ProjectID", typeof(string));
            excel.Columns.Add("EcommerceProjectRelationID", typeof(string));
            excel.Columns.Add("CompanyName", typeof(string));

            dw = excel.Clone();
            decimal IncomeAmount = 0;
            decimal ClearingAmount = 0;
            decimal PlatformExpensesAmount = 0;
            decimal CapitalPoolAdd = 0;
            decimal IncomeAmountSum = 0;
            decimal ClearingAmountSum = 0;
            decimal PlatformExpensesAmountSum = 0;
            decimal CapitalPoolAddSum = 0;
            string projectName = data.First().ProjectName;
            foreach (CapitalFlow_CFNodeView item in data)
            {
                if (projectName == item.ProjectName)
                {
                    DataRow zz = excel.NewRow();
                    zz["EcomGroupName"] = item.EcommerceGroupName;
                    zz["Company_Id"] = item.Company_Id;
                    zz["CompanyName"] = item.FullName;
                    zz["EcommerceID"] = item.EcommerceID;
                    zz["EcommerceGroupID"] = item.EcommerceGroupID;
                    zz["ProjectID"] = item.ProjectID;
                    zz["EcommerceProjectRelationID"] = item.EcommerceProjectRelationID;
                    zz["Project_Name"] = item.ProjectName;
                    zz["EcomName"] = item.EcommerceName;
                    zz["IncomeAmount"] = item.IncomeAmount;
                    zz["ClearingAmount"] = item.ClearingAmount;
                    zz["CapitalPoolAdd"] = item.CapitalPoolAdd;
                    zz["PlatformExpensesAmount"] = item.PlatformExpensesAmount;

                    zz["Proportion"] = item.Proportion;
                    IncomeAmount += Convert.ToDecimal(item.IncomeAmount);
                    ClearingAmount += Convert.ToDecimal(item.ClearingAmount);
                    PlatformExpensesAmount += Convert.ToDecimal(item.PlatformExpensesAmount);
                    CapitalPoolAdd += Convert.ToDecimal(item.CapitalPoolAdd);
                    IncomeAmountSum += Convert.ToDecimal(item.IncomeAmount);
                    ClearingAmountSum += Convert.ToDecimal(item.ClearingAmount);
                    PlatformExpensesAmountSum += Convert.ToDecimal(item.PlatformExpensesAmount);
                    CapitalPoolAddSum += Convert.ToDecimal(item.CapitalPoolAdd);
                    excel.Rows.Add(zz);
                }
                else
                {
                    if (item.CapitalFlow_Details_Id != null)
                    {
                        DataRow zz = excel.NewRow();
                        zz["IncomeAmount"] = IncomeAmount;
                        zz["ClearingAmount"] = ClearingAmount;
                        zz["PlatformExpensesAmount"] = PlatformExpensesAmount;
                        zz["CapitalPoolAdd"] = CapitalPoolAdd;
                        zz["EcomGroupName"] = "项目小计";
                        excel.Rows.Add(zz);
                        IncomeAmount = 0;
                        ClearingAmount = 0;
                        PlatformExpensesAmount = 0;
                        CapitalPoolAdd = 0;
                        DataRow SS = excel.NewRow();
                        SS["EcomGroupName"] = item.EcommerceGroupName;
                        SS["Company_Id"] = item.Company_Id;
                        SS["EcommerceID"] = item.EcommerceID;
                        SS["EcommerceGroupID"] = item.EcommerceGroupID;
                        SS["ProjectID"] = item.ProjectID;
                        SS["CompanyName"] = item.FullName;
                        SS["EcommerceProjectRelationID"] = item.EcommerceProjectRelationID;
                        SS["Project_Name"] = item.ProjectName;
                        SS["EcomName"] = item.EcommerceName;
                        SS["IncomeAmount"] = item.IncomeAmount;
                        SS["ClearingAmount"] = item.ClearingAmount;
                        SS["CapitalPoolAdd"] = item.CapitalPoolAdd;
                        SS["PlatformExpensesAmount"] = item.PlatformExpensesAmount;
                        SS["Proportion"] = item.Proportion;

                        excel.Rows.Add(SS);
                        IncomeAmount += Convert.ToDecimal(item.IncomeAmount);
                        ClearingAmount += Convert.ToDecimal(item.ClearingAmount);
                        PlatformExpensesAmount += Convert.ToDecimal(item.PlatformExpensesAmount);
                        CapitalPoolAdd += Convert.ToDecimal(item.CapitalPoolAdd);
                        IncomeAmountSum += Convert.ToDecimal(item.IncomeAmount);
                        ClearingAmountSum += Convert.ToDecimal(item.ClearingAmount);
                        PlatformExpensesAmountSum += Convert.ToDecimal(item.PlatformExpensesAmount);
                        CapitalPoolAddSum += Convert.ToDecimal(item.CapitalPoolAdd);
                        projectName = item.ProjectName;
                    }
                    else
                    {
                        DataRow zz = excel.NewRow();
                        zz["IncomeAmount"] = IncomeAmount;
                        zz["ClearingAmount"] = ClearingAmount;
                        zz["PlatformExpensesAmount"] = PlatformExpensesAmount;
                        zz["CapitalPoolAdd"] = CapitalPoolAdd;
                        zz["EcomGroupName"] = "项目小计";
                        excel.Rows.Add(zz);
                    }


                }

            }
            DataRow drz = excel.NewRow();
            drz["IncomeAmount"] = IncomeAmountSum;
            drz["ClearingAmount"] = ClearingAmountSum;
            drz["PlatformExpensesAmount"] = PlatformExpensesAmountSum;
            drz["CapitalPoolAdd"] = CapitalPoolAddSum;
            drz["EcomGroupName"] = "区域合计";
            excel.Rows.Add(drz);
            if (excel.Rows[0]["CompanyName"].ToString() == "" && excel.Rows[0]["Company_Id"].ToString() == "")
            {
                excel.Clear();
            }
            return ToJsonResult(excel);
        }


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
                t_capitalflowbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, T_CapitalFlowEntity entity)
        {
            try
            {
                t_capitalflowbll.SaveForm(keyValue, entity);
                return Success("操作成功。");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 提交表单为审批中（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult submitFormApp(List<T_CapitalFlow_NodeEntity> entity, List<FileModel> uploadFiles, string year, string month, string keyValue, string CapitalFlow_Title, string Job_Number)
        {
            try
            {
                IEnumerable<T_CapitalFlowEntity> c = t_capitalflowbll.checkCaFLow(entity, year, month);
                if (c.Count() != 0)
                {
                    return Error("已经存在正在审批中的相同数据");
                }
                else
                {
                    if (string.IsNullOrEmpty(keyValue))
                    {
                        keyValue = t_capitalflowbll.submitFormApp(uploadFiles, entity, year, month, keyValue, CapitalFlow_Title, Job_Number);
                    }
                    else
                    {
                        string key = t_capitalflowbll.submitFormApp(uploadFiles, entity, year, month, keyValue, CapitalFlow_Title, Job_Number);
                    }
                    var starturl = dataItemCache.GetDataItemByCodeAndName("SysConfig", "BPMStartProcess");
                    var starturlname = starturl.ItemValue;
                    var data = t_capitalflowbll.GetStartUrl(keyValue, starturlname);
                    return Success("操作成功。", data);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult checkAppro(List<T_CapitalFlow_NodeEntity> entity, List<FileModel> uploadFiles, string year, string month, string keyValue, string CapitalFlow_Title, string Job_Number)
        {

            IEnumerable<T_CapitalFlowEntity> c = t_capitalflowbll.checkCaFLow(entity, year, month);
            return ToJsonResult(c);
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveFormApp(List<T_CapitalFlow_NodeEntity> entity, List<FileModel> uploadFiles, string year, string month, string CapitalFlow_Title, string Job_Number,string keyValue)
        {
            t_capitalflowbll.SaveFormApp(uploadFiles, entity, year, month, keyValue, CapitalFlow_Title, Job_Number);
            return Success("操作成功。");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            t_capitalflow_nodebll.DeleteForm(keyValue);
            return Success("删除成功");
        }
        #endregion
    }
}
