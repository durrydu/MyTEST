using Movit.Application.Entity.EcommerceContractManage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Movit.Application.Entity.CapitalFlowManage.ViewModel
{
    [XmlRoot("DATA")]
    public class IncomeView
    {
        #region 实体成员

        //public string CreateUserName { get; set; }
        //public string Job_Number { get; set; }
        //public string DepartmentName { get; set; }
        //public DateTime? CreateDate { get; set; }
        public string OrgName { get; set; }
        public string OrgCode { get; set; }
        //public string IsStamp { get; set; }

        public int? Year { get; set; }
        /// <summary>
        /// Month
        /// </summary>
        /// <returns></returns>
        public int? Month { get; set; }
        public string CapitalFlow_Title { get; set; }
        public string ProjectCodeList { get; set; }
      
        public AccountDetail AccountDetail { get; set; }
        //public string Remark { get; set; }
        public ATTACHMENT ATTACHMENT { get; set; }

        #endregion
    }
    [XmlRoot("ITEM")]
    public class AccountDetail
    {
        [XmlElement]
        public Node[] item { get; set; }
    }
    [XmlRoot("item")]
    public class Node
    {
        //public string CompanyName { get; set; }
        public string ProjectName { get; set; }
        public string ProjCode { get; set; }
        public string EcommerceGroupName { get; set; }
        public decimal? IncomeAmount { get; set; }
        public decimal? ClearingAmount { get; set; }
        public decimal? Proportion { get; set; }
        public decimal? PlatformExpensesAmount { get; set; }
        public decimal? CapitalPoolAdd { get; set; }
    }

    [XmlRoot("ATTACHMENT1")]
    public class ATTACHMENT
    {
        [XmlElement]
        public ATTACHMENT1[] attachment { get; set; }
    }
    [XmlRoot("attachment")]
    public class ATTACHMENT1
    {
        public string FILENAME { get; set; }

        public string URL { get; set; }
    }
}
