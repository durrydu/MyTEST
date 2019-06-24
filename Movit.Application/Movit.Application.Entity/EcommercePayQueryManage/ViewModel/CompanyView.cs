using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.EcommercePayQueryManage.ViewModel
{
   public class CompanyView
    {
       //id
       public string id { get; set; }
       //Companyid
       public string Companyid { get; set; }

       //CompanyName
       public string CompanyName { get; set; }
       //ProjectName
       public string ProjectName { get; set; }
       //EcommerceGroupName
       public string EcommerceGroupName { get; set; }
       //ControllAmount
       public decimal ControlTotalAmount { get; set; }
       //FlowNopayTotalAmount
       public decimal FlowNopayTotalAmount { get; set; }
       //ActualControlTotalAmount
       public decimal ActualControlTotalAmount { get; set; }
       public DateTime? StatisticalDate { get; set; }
       //praentid
       public string praentid { get; set; }
    }
}
