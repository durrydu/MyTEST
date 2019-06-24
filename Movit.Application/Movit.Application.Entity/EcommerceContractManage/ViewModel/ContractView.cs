using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Movit.Application.Entity.EcommerceContractManage.ViewModel
{
    [XmlRoot("DATA")]
    public class ContractView
    {
        public string ParcelCode { get; set; }
        public string ProjCode { get; set; }
        public string ProjectType { get; set; }
        public string ContractName { get; set; }
        public string ContractNature { get; set; }
        public string ContractTypeName { get; set; }
        public string IsStandard { get; set; }
        public string PartyA { get; set; }
        public string PartyB { get; set; }
        public string BiddingMethod { get; set; }
        public  string IsStamp { get; set; }
        public string Agent { get; set; }
        public string CreateDate { get; set; }
        
        public string ProjectName { get; set; }

        public string OrgName { get; set; }
        public string OrgCode { get; set; }

        public string EcommerceName { get; set; }

        public string EcommerceGroupName { get; set; }

        public string EcommerceTypeName { get; set; }

        public decimal? ForceContractAmount { get; set; }

        public decimal? PlatformRate { get; set; }

        public string CooperateStartTime { get; set; }

        public string CooperateEndTime { get; set; }

        public string Remark { get; set; }

        public BDITEM BDITEM { get; set; }

        public ATTACHMENT ATTACHMENT { get; set; }
    }
    [XmlRoot("ITEM")]
    public class BDITEM
    {
        [XmlElement]
        public ITEM[] item { get; set; }
    }
    [XmlRoot("item")]
    public class ITEM
    {
        public string Format { get; set; }

        public decimal? Amount { get; set; }

        public decimal? Discount { get; set; }
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
