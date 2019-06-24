using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.EcommercePayQueryManage.ViewModel
{
   public class ProjectView
    {
       /// <summary>
       /// id
       /// </summary>
       public string id { get; set; }
       /// <summary>
       /// CompanyID
       /// </summary>
       public string CompanyID { get; set; }
       /// <summary>
       /// CompanyName
       /// </summary>
       public string CompanyName { get; set; }
       /// <summary>
       /// ProjectID
       /// </summary>
       public string ProjectID { get; set; }
       /// <summary>
       /// ProjectName
       /// </summary>
       public string ProjectName { get; set; }
       /// <summary>
       /// EcommerceGroupID
       /// </summary>
       public string EcommerceGroupID { get; set; }
       /// <summary>
       /// EcommerceGroupName
       /// </summary>
       public string EcommerceGroupName { get; set; }
       [DecimalPrecision(18,4)]
       /// <summary>
       /// IncomeTotal
       /// </summary>
       public string IncomeTotal { get; set; }
       /// <summary>
       /// ClearingTotal
       /// </summary>
       public string ClearingTotal { get; set; }
       /// <summary>
       /// Proportion
       /// </summary>
       public string Platform { get; set; }
       /// <summary>
       /// PlatformExpensesAmount
       /// </summary>
       public string PlatformExpensesAmount { get; set; }
       /// <summary>
       /// ControllAmount
       /// </summary>
       public string ControllAmount { get; set; }
       /// <summary>
       /// EcommerceExpenseTotal
       /// </summary>
       public string EcommerceExpenseTotal { get; set; }

       public string TransfoTotal { get; set; }
       /// <summary>
       /// praentid
       /// </summary>
       public string praentid { get; set; }
    }
}
