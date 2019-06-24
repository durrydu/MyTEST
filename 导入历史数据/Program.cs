using Movit.Application.Busines;
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

namespace 导入历史数据
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExprotContract();
            ExprotPayHistroy();
          
        }

        private static void ExprotPayHistroy()
        {
            SqlConnection conn = new SqlConnection("Server=.;Initial Catalog=E_Commerce_DB;User ID=sa;Password=dq123456");
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select * from Test003 ");
            conn.Open();
            SqlCommand comm = new SqlCommand(sqlstr.ToString(), conn);
            SqlDataReader sdr = comm.ExecuteReader();

            contractEntity list = new contractEntity();
            List<contractEntity> conlists = new List<contractEntity>();
            while (sdr.Read())
            {
                list = new contractEntity
                {
                    projectname = sdr[0].ToString(),
                    ecommercename = sdr[1].ToString(),
                    ecommercegroupname = sdr[2].ToString(),
                    platfromrate = Convert.ToDecimal(sdr[3]),
                    contractname = sdr[4].ToString(),
                    ControllerAmount = Convert.ToDecimal(sdr[5]),
                    FlowAmount = Convert.ToDecimal(sdr[6]),
                    ActualControllerAmount = Convert.ToDecimal(sdr[7])
                };
                conlists.Add(list);
                if (!sdr.HasRows)
                {
                    list = new contractEntity
                    {
                        projectname = "",
                    };
                    conlists.Add(list);
                }
            }
            sdr.Close();
            conn.Close();
        }

        private static void ExprotContract()
        {
            SqlConnection conn = new SqlConnection("Server=.;Initial Catalog=E_Commerce_DB;User ID=sa;Password=dq123456");
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select * from Test003 ");
            conn.Open();
            SqlCommand comm = new SqlCommand(sqlstr.ToString(), conn);
            SqlDataReader sdr = comm.ExecuteReader();

            contractEntity list = new contractEntity();
            List<contractEntity> conlists = new List<contractEntity>();
            while (sdr.Read())
            {
                list = new contractEntity
                {
                    projectname = sdr[0].ToString(),
                    ecommercename = sdr[1].ToString(),
                    ecommercegroupname = sdr[2].ToString(),
                    platfromrate = Convert.ToDecimal(sdr[3]),
                    contractname = sdr[4].ToString(),
                    ControllerAmount = Convert.ToDecimal(sdr[5]),
                    FlowAmount = Convert.ToDecimal(sdr[6]),
                    ActualControllerAmount = Convert.ToDecimal(sdr[7])
                };
                conlists.Add(list);
                if (!sdr.HasRows)
                {
                    list = new contractEntity
                    {
                        projectname = "",
                    };
                    conlists.Add(list);
                }
            }
            sdr.Close();
            conn.Close();

            Base_ProjectInfoBLL bpbbll = new Base_ProjectInfoBLL();
            EcommerceBLL ecombll = new EcommerceBLL();
            EcommerceGroupBLL ecomgroupbll = new EcommerceGroupBLL();
            List<EcommerceGroupEntity> ecomgrouplists = new List<EcommerceGroupEntity>();
            List<EcommerceEntity> ecomlists = new List<EcommerceEntity>();
            List<EcommerceProjectRelationEntity> ecomprorelists = new List<EcommerceProjectRelationEntity>();
            int count = 0;
            foreach (var item in conlists)
            {
                count++;
                var data = bpbbll.GetList("").ToList().Where(t => t.ProjecName == item.projectname).ToList();
                if (data.Count == 0)
                {
                    Console.WriteLine("找不到对应的项目数据'" + item.projectname + "'");
                    Console.ReadKey();
                    return;
                }
                EcommerceProjectRelationEntity ecom = new EcommerceProjectRelationEntity();
                ecom.EcommerceProjectRelationID = Guid.NewGuid().ToString();
                var ecomgroupdata = ecomgroupbll.GetList("").Where(t => t.EcommerceGroupName == item.ecommercegroupname).ToList();
                if (ecomgroupdata.Count == 0)
                {
                    EcommerceGroupEntity ecomgroupentity = new EcommerceGroupEntity();
                    ecomgroupentity.EcommerceGroupID = Guid.NewGuid().ToString();
                    ecomgroupentity.EcommerceGroupName = item.ecommercegroupname;
                    ecomgroupentity.DeleteMark = 0;
                    var t2 = new SqlDatabase("BaseDb").Connection.Insertable(ecomgroupentity).ExecuteCommand();
                    //if (!ecomgrouplists.Any(t => t.EcommerceGroupName == ecomgroupentity.EcommerceGroupName))
                    //{
                    //    ecomgrouplists.Add(ecomgroupentity);
                    //}
                    EcommerceEntity ecomentity = new EcommerceEntity();
                    ecomentity.EcommerceName = item.ecommercename;
                    ecomentity.EcommerceID = Guid.NewGuid().ToString();
                    ecomentity.EcommerceGroupID = ecomgroupentity.EcommerceGroupID;
                    ecomentity.EcommerceCode = count.ToString();
                    ecomentity.EcommerceGroupName = ecomgroupentity.EcommerceGroupName;
                    ecomentity.DeleteMark = 0;
                    ecomentity.EcommerceType = 0;
                    ecomentity.PlatformRate = item.platfromrate;
                    ecomentity.CooperateStartTime = Convert.ToDateTime("2018-01-01");
                    ecomentity.CooperateEndTime = Convert.ToDateTime("2018-06-30");
                    var t3 = new SqlDatabase("BaseDb").Connection.Insertable(ecomentity).ExecuteCommand();
                    //if (!ecomlists.Any(t => t.EcommerceName == ecomentity.EcommerceName))
                    //{
                    //    ecomlists.Add(ecomentity);
                    //}
                    ecom.EcommerceGroupID = ecomgroupentity.EcommerceGroupID;
                    ecom.EcommerceGroupName = ecomgroupentity.EcommerceGroupName;
                    ecom.EcommerceID = ecomentity.EcommerceID;
                    ecom.EcommerceName = ecomentity.EcommerceName;
                    ecom.EcommerceCode = ecomentity.EcommerceCode;
                    ecom.PartyB = ecomentity.EcommerceName;
                }
                else
                {
                    ecom.EcommerceGroupID = ecomgroupdata[0].EcommerceGroupID;
                    ecom.EcommerceGroupName = ecomgroupdata[0].EcommerceGroupName;
                    var ecomdata = ecombll.GetList("").Where(t => t.EcommerceName == item.ecommercename).ToList();
                    if (ecomdata.Count == 0)
                    {
                        EcommerceEntity ecomentity = new EcommerceEntity();
                        ecomentity.EcommerceName = item.ecommercename;
                        ecomentity.EcommerceID = Guid.NewGuid().ToString();
                        ecomentity.EcommerceCode = count.ToString();
                        ecomentity.EcommerceGroupID = ecomgroupdata[0].EcommerceGroupID;
                        ecomentity.EcommerceGroupName = ecomgroupdata[0].EcommerceGroupName;
                        ecomentity.PlatformRate = item.platfromrate;
                        ecomentity.DeleteMark = 0;
                        ecomentity.EcommerceType = 0;
                        ecomentity.CooperateStartTime = Convert.ToDateTime("2018-01-01");
                        ecomentity.CooperateEndTime = Convert.ToDateTime("2018-06-30");
                        var t3 = new SqlDatabase("BaseDb").Connection.Insertable(ecomentity).ExecuteCommand();
                        //if (!ecomlists.Any(t => t.EcommerceName == ecomentity.EcommerceName))
                        //{
                        //    ecomlists.Add(ecomentity);
                        //}
                        ecom.EcommerceGroupID = ecomentity.EcommerceGroupID;
                        ecom.EcommerceGroupName = ecomentity.EcommerceGroupName;
                        ecom.EcommerceID = ecomentity.EcommerceID;
                        ecom.EcommerceName = ecomentity.EcommerceName;
                        ecomentity.EcommerceCode = ecomentity.EcommerceCode;
                        ecom.PartyB = ecomentity.EcommerceName;
                    }
                    else
                    {
                        ecom.EcommerceID = ecomdata[0].EcommerceID;
                        ecom.EcommerceName = ecomdata[0].EcommerceName;
                        ecom.EcommerceCode = ecomdata[0].EcommerceCode;
                        ecom.PartyB = ecomdata[0].EcommerceName;
                    }
                }
                ecom.PlatformRate = item.platfromrate;
                ecom.ContractName = item.contractname;
                ecom.EcommerceType = 0;
                ecom.EcommerceTypeName = EnumHelper.ToDescription((EcommerceTypeEnum)0);
                ecom.Agent = "金涛";
                ecom.CooperateStartTime = Convert.ToDateTime("2018-01-01");
                ecom.CooperateEndTime = Convert.ToDateTime("2018-06-30");
                ecom.ForceContractAmount = 100000;
                ecom.DeleteMark = 0;
                ecom.CityCode = data[0].CityCode;
                ecom.CityID = data[0].CityID;
                ecom.CityName = data[0].CityName;
                ecom.ProjectID = data[0].ProjectID;
                ecom.ProjectCode = data[0].ProjectCode;
                ecom.ProjecName = item.projectname;
                ecom.CompanyId = data[0].CompanyCode;
                ecom.CompanyCode = data[0].CompanyCode;
                ecom.CompanyName = data[0].CompanyName;
                ecom.ProjectType = 0;
                ecom.ApprovalState = 4;
                ecom.FlowNopayTotalAmount = 0;
                ecom.ContractNature = 0;
                ecom.IsStandard = 1;
                ecom.PartyA = "阳光城集团股份有限公司";
                ecom.BiddingMethod = 0;
                ecom.IsStamp = 1;
                ecom.ContractTypeName = "主合同";
                ecom.CreateDate = Convert.ToDateTime("2018-06-30");
                ecom.ControlTotalAmount = item.ControllerAmount * 0;
                ecom.FlowNopayTotalAmount = item.FlowAmount * 10000;
                ecom.ActualControlTotalAmount = item.ActualControllerAmount * 10000;
                ecom.IsTrunk = 1;
                if (!ecomprorelists.Any(t => t.EcommerceID == ecom.EcommerceID && t.ProjectID == ecom.ProjectID))
                {
                    ecomprorelists.Add(ecom);
                }
            }
            var t1 = new SqlDatabase("BaseDb").Connection.Insertable(ecomprorelists).ExecuteCommand();
            //var t2 = new SqlDatabase("BaseDb").Connection.Insertable(ecomgrouplists).ExecuteCommand();
            //var t3 = new SqlDatabase("BaseDb").Connection.Insertable(ecomlists).ExecuteCommand();
            Console.ReadKey();
        }
    }
 
    public class contractEntity
    {
        #region 实体成员
        /// <summary>
        /// projectname
        /// </summary>
        /// <returns></returns>
        public string projectname { get; set; }
        public string ecommercename { get; set; }
        public string ecommercegroupname { get; set; }
        public decimal platfromrate { get; set; }

        public string contractname { get; set; }
        public decimal ControllerAmount { get; set; }
        public decimal FlowAmount { get; set; }
        public decimal ActualControllerAmount { get; set; }

      
        #endregion
    }
    public class PayEntity
    {
        /// <summary>
        /// 区域公司
        /// </summary>
        public string companyname { get; set; }
        /// <summary>
        /// 项目公司
        /// </summary>
        public string projectname { get; set; }
        /// <summary>
        /// 电商公司
        /// </summary>
        public string ecommercename { get; set; }
        public string ecommercegroupname { get; set; }
        public decimal paymoney { get; set; }

        public DateTime faqishijian { get; set; }
        public DateTime shijizhifushijian   { get; set; }
    }
}
