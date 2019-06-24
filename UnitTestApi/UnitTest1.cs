using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movit.Util;
using Movit.Application.Code;
using Movit.Sys.Api;
using Movit.Application.Code;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movit.Data.SQLSugar;
using Movit.Data.Repository;
using Movit.Application.Entity;
using Movit.Application.Busines;
using Movit.Sys.Api.Code.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace UnitTestApi
{

    [TestClass]
    public class UnitTest1
    {

        #region BPM 接口

        /// <summary>
        /// 创建流程结束接口
        /// </summary>
        [TestMethod]
        public void CreateResult()
        {
            string strBSID = "EC_Income";

            string strBOID = "73657f75-09c5-4873-9a60-d733f7289b4e";

            bool bSuccess = true;
            int iProcInstID = 21037;
            string strMessage = "发起";
            Movit_Commerce_Bpm bpm = new Movit_Commerce_Bpm();
            bpm.CreateResult(strBSID, strBOID, bSuccess, iProcInstID, strMessage);
        }
        [TestMethod]
        public void GetInfo()
        {
            string strBSID = "EC_Contract_Add";
            string strBOID = "33373ca9-ce71-41c8-93a6-9fbd83bedc91";
            Movit_Commerce_Bpm bpm = new Movit_Commerce_Bpm();
            string result = bpm.GetInfo(strBSID, strBOID);
        }
        /// <summary>
        /// 退回
        /// </summary>
        [TestMethod]
        public void Rework()
        {
            string strBSID = "EC_Contract_Add";
            string strBOID = "1c71b695-8465-4d86-b340-6bf4fa4f5c81";
            int iProcInstID = 21040;
            string strStepName = "草稿";
            string strApproverId = "admin";
            UserAction eAction = UserAction.Rejected;
            string strComment = "条件阿斯蒂芬";
            DateTime dtTime = DateTime.Now;
            Movit_Commerce_Bpm bpm = new Movit_Commerce_Bpm();
            bpm.Rework(strBSID, strBOID, iProcInstID,
               strStepName, strApproverId, eAction, strComment, dtTime);
        }
        /// <summary>
        /// 退回
        /// </summary>
        [TestMethod]
        public void Close()
        {
            string strBSID = "EC_Income";
            string strBOID = "839d0d19-07c5-4229-a345-b107b164c0f7";

            int iProcInstID = 22494;
            string strStepName = "草稿";
            string strApproverId = "jintao";
            UserAction eAction = UserAction.Rejected;
            ProcessInstanceStatus eProcessInstanceResult = ProcessInstanceStatus.Approved;
            string strComment = "审批通过";
            DateTime dtTime = DateTime.Now;
            Movit_Commerce_Bpm bpm = new Movit_Commerce_Bpm();
            bpm.Close(strBSID, strBOID, iProcInstID,
               strStepName,eProcessInstanceResult, strApproverId, strComment, dtTime);
        }
        #endregion
        #region 共享平台接口
        [TestMethod]
        public void GetModel()
        {

            string res = HttpMethods.HttpGet("http://localhost:30004/api/test/2/");
        }
        [TestMethod]
        public void PostModel()
        {

            string param = "id='12'&age=1";
            string res = HttpMethods.HttpPost("http://localhost:30004/api/SaveModel", param);
        }

        [TestMethod]
        public void PostList()
        {
            string a = "";
            string param = "[{'id':'1',	'age':1},{	'id':'2','age':100}]";
            string res = HttpMethods.PostResponse("http://localhost:30004/api/List/", param, out a);
        }

        [TestMethod]
        public void PostPaymentuse()
        {

            SqlConnection conn = new SqlConnection("Server=.;Initial Catalog=mytest;User ID=sa;Password=123456;Pooling=true;Max Pool Size=1024;Min Pool Size=0;");

            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select * from daorushuju");
            conn.Open();
            SqlCommand comm = new SqlCommand(sqlstr.ToString(), conn);
            SqlDataReader sdr = comm.ExecuteReader();

            Pay_InfoEntity payinfo = new Pay_InfoEntity();
            List<Pay_InfoEntity> payinfolists = new List<Pay_InfoEntity>();
            while (sdr.Read())
            {
                payinfo = new Pay_InfoEntity
                {
                   Project_Name=sdr[2].ToString().Trim(),
                   Electricity_Supplier_Name=sdr[3].ToString().Trim(),
                   Pay_Money=Convert.ToDecimal(sdr[6]),
                   Pay_Info_Id=sdr[10].ToString()
                };
                payinfolists.Add(payinfo);
                if (!sdr.HasRows)
                {
                    payinfo = new Pay_InfoEntity
                    {
                        Project_Name = "",
                    };
                    payinfolists.Add(payinfo);
                }
            }
            sdr.Close();
            conn.Close();

            string datetime = DateTime.Now.ToString("yyyyMMdd");
            string paycode = "History"+datetime+"A00";
            string  i = "1";
            string hetong = "HT-"+datetime+"-00";
            EcommerceBLL ecombll = new EcommerceBLL();
            Base_ProjectInfoBLL bpbbll = new Base_ProjectInfoBLL();
            EcommerceProjectRelationBLL ecomprorelabll = new EcommerceProjectRelationBLL();
            bool isexprot = true;

            foreach (var item in payinfolists)
            {
                var projectdata = bpbbll.GetList("").ToList().Where(t => t.ProjecName == item.Project_Name).ToList();
                var ecomdata = ecombll.GetList("").Where(t => t.EcommerceName == item.Electricity_Supplier_Name).ToList();
                if (ecomdata.Count == 0)
                {
                    isexprot = false;
                    string msg = string.Format("不存在该电商公司：{0}", item.Electricity_Supplier_Name);
                    Info(msg);
                    continue;
                }
                bool isexist = ecomprorelabll.GetProjectAndEcom().Any(t => t.ProjecName == item.Project_Name &&
                    t.EcommerceName == item.Electricity_Supplier_Name);
                if (!isexist)
                {
                    isexprot = false;
                    string msg = string.Format("该电商公司(  {0}  )和项目(  {1}  )不存在", item.Electricity_Supplier_Name, item.Project_Name);
                    Error(msg);
                    continue;
                }
                InputPaymentuse abc = new InputPaymentuse();
                if (i.Length == 1)
                {
                    i = "0" + i;
                }
                abc.pay_info_id = item.Pay_Info_Id;
                abc.pay_info_code = paycode + i;
                abc.electricity_supplier_name = item.Electricity_Supplier_Name;
                abc.electricity_supplier_code = ecomdata[0].EcommerceCode;
                abc.electricity_supplier_id = ecomdata[0].EcommerceID;
                abc.electricity_supplier_ad_id = ecomdata[0].EcommerceGroupID;
                abc.electricity_supplier_ad = ecomdata[0].EcommerceGroupName;
                abc.project_code = projectdata[0].ProjectCode;
                abc.project_id = projectdata[0].ProjectID;
                abc.project_name = item.Project_Name;
                abc.pay_info_type = "EC";
                abc.contract_code = hetong + i;
                abc.contract_name = item.Project_Name + "历史合同数据";
                abc.pay_money = Convert.ToDecimal(item.Pay_Money);
                abc.pay_createtime = Convert.ToDateTime("2018/08/01");
                abc.pay_completetime = Convert.ToDateTime("2018/08/31");
                abc.pay_reason = "导入历史数据";
                abc.approval_status = "SUBMITED";
                abc.url = "https://fssc.yango.com.cn/jiebao-plus/#/app/smart_expense/form/EC/306991952048422912/detail";
                abc.login_name = "金涛";
                abc.login_code = "金涛";
                i = (Convert.ToInt32(i) + 1).ToString();
                string param = abc.ToJson();
                string a = "";
                string paydatadetail = abc.ToJson();
               // data(paydatadetail);
            }
            if(!isexprot)
            {
                 return;
            }
            foreach (var item in payinfolists)
            {
                var projectdata = bpbbll.GetList("").ToList().Where(t => t.ProjecName == item.Project_Name).ToList();
                var ecomdata = ecombll.GetList("").Where(t => t.EcommerceName == item.Electricity_Supplier_Name).ToList();
                if (ecomdata.Count == 0)
                {
                    string msg = string.Format("不存在该电商公司：{0}", item.Electricity_Supplier_Name);
                    Error(msg);
                }
                InputPaymentuse abc = new InputPaymentuse();
                if (i.Length == 1)
                {
                    i = "0" + i;
                }
                abc.pay_info_id = item.Pay_Info_Id;
                abc.pay_info_code = paycode + i;
                abc.electricity_supplier_name = item.Electricity_Supplier_Name;
                abc.electricity_supplier_code = ecomdata[0].EcommerceCode;
                abc.electricity_supplier_id = ecomdata[0].EcommerceID;
                abc.electricity_supplier_ad_id = ecomdata[0].EcommerceGroupID;
                abc.electricity_supplier_ad = ecomdata[0].EcommerceGroupName;
                abc.project_code = projectdata[0].ProjectCode;
                abc.project_id = projectdata[0].ProjectID;
                abc.project_name = item.Project_Name;
                abc.pay_info_type = "EC";
                abc.contract_code = hetong + i;
                abc.contract_name = item.Project_Name + "历史合同数据";
                abc.pay_money = Convert.ToDecimal(item.Pay_Money);
                abc.pay_createtime = Convert.ToDateTime("2018/08/01");
                abc.pay_completetime = Convert.ToDateTime("2018/08/31");
                abc.pay_reason = "导入历史数据";
                abc.approval_status = "AUDITED";
                abc.url = "https://fssc.yango.com.cn/jiebao-plus/#/app/smart_expense/form/EC/306991952048422912/detail";
                abc.login_name = "金涛";
                abc.login_code = "金涛";
                i = (Convert.ToInt32(i) + 1).ToString();
                string param = abc.ToJson();
                string a = "";

                string res = HttpMethods.PostResponse("http://172.19.10.206:8090/api/money/paymentuse", param, out a);
            }
        }
        #endregion
        public static  void Info(string msg)
        {
            string path = "";
            var _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (_config.HasFile)
            {
                path = _config.FilePath;
                path = path.Substring(0, path.LastIndexOf('\\'));
            }
            path = path + "\\Log\\Info\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            StreamWriter sw = new StreamWriter(path + DateTime.Now.ToString("yyyy-MM-dd") + "-info.txt", true);
            sw.WriteLine("记录时间：" + DateTime.Now);
            sw.WriteLine("日志级别：  INFO ");
            sw.WriteLine("错误描述：" + msg);
            sw.WriteLine("\r\n\r\n");
            sw.Close();
        }
        public static void Error(string msg)
        {
            string path = "";
            var _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (_config.HasFile)
            {
                path = _config.FilePath;
                path = path.Substring(0, path.LastIndexOf('\\'));
            }
            path = path + "\\Log\\Error\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            StreamWriter sw = new StreamWriter(path + DateTime.Now.ToString("yyyy-MM-dd") + "-error.txt", true);
            sw.WriteLine("记录时间：" + DateTime.Now);
            sw.WriteLine("日志级别：  Error ");
            sw.WriteLine("错误描述：" + msg);
            sw.WriteLine("\r\n\r\n");
            sw.Close();
        }
        public static void data(string msg)
        {
            string path = "";
            var _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (_config.HasFile)
            {
                path = _config.FilePath;
                path = path.Substring(0, path.LastIndexOf('\\'));
            }
            path = path + "\\Log\\data\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            StreamWriter sw = new StreamWriter(path + DateTime.Now.ToString("yyyy-MM-dd") + "-data.txt", true);
            sw.WriteLine("记录时间：" + DateTime.Now);
            sw.WriteLine("日志级别：  data ");
            sw.WriteLine("数据：" + msg);
            sw.WriteLine("\r\n\r\n");
            sw.Close();
        }
    }



}
