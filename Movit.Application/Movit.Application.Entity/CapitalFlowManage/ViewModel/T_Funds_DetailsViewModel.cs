using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.CapitalFlowManage.ViewModel
{
   public class T_Funds_DetailsViewModel
    {
        
        /// <summary>
        /// FundsDetailsID
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)] //是主键
        public string FundsDetailsID { get; set; }

        /// <summary>
        /// CompanyID
        /// </summary>
        /// <returns></returns>
        public string CompanyID { get; set; }
        public string EcommerceGroupName { get; set; }
        /// <summary>
        /// EcommerceID
        /// </summary>
        /// <returns></returns>
        public string EcommerceID { get; set; }
        /// <summary>
        /// EcommerceProjectRelationID
        /// </summary>
        /// <returns></returns>
        public string EcommerceProjectRelationID { get; set; }
        /// <summary>
        /// ProjectID
        /// </summary>
        /// <returns></returns>
        public string ProjectID { get; set; }
        /// <summary>
        /// Year
        /// </summary>
        /// <returns></returns>
        public int? Year { get; set; }
        /// <summary>
        /// Month
        /// </summary>
        /// <returns></returns>
        public int? Month { get; set; }
        /// <summary>
        /// Day
        /// </summary>
        /// <returns></returns>
        public int? Day { get; set; }
        /// <summary>
        /// StatisticalDate
        /// </summary>
        /// <returns></returns>
        public DateTime? StatisticalDate { get; set; }
        /// <summary>
        /// IsLastDayOfMonth
        /// </summary>
        /// <returns></returns>
        public int? IsLastDayOfMonth { get; set; }
        /// <summary>
        /// IsFirstDayOfMonth
        /// </summary>
        /// <returns></returns>
        public int? IsFirstDayOfMonth { get; set; }

        /// <summary>
        /// TotalCapitalPoolAdd
        /// </summary>
        /// <returns></returns>
        public decimal? TotalCapitalPoolAdd { get; set; }
        /// <summary>
        /// TotalPayAmount
        /// </summary>
        /// <returns></returns>
        public decimal? TotalPayAmount { get; set; }
        /// <summary>
        /// TotalTransferAmount
        /// </summary>
        /// <returns></returns>
        public decimal? TotalTransferAmount { get; set; }
        /// <summary>
        /// DeleteMark
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// CreateUserId
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// CreateUserName
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// ModifyUserId
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// ModifyUserName
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        /// <summary>
        /// FlowNopayTotalAmount
        /// </summary>
        /// <returns></returns>
        public decimal? FlowNopayTotalAmount { get; set; }
        /// <summary>
        /// ControlTotalAmount
        /// </summary>
        /// <returns></returns>
        public decimal? ControlTotalAmount { get; set; }

        /// <summary>
        /// ActualControlTotalAmount
        /// </summary>
        /// <returns></returns>
        public decimal? ActualControlTotalAmount { get; set; }
        /// <summary>
        /// IsLastDayOfYear
        /// </summary>
        /// <returns></returns>
        public int? IsLastDayOfYear { get; set; }
        /// <summary>
        /// IsFirstDayOfYear
        /// </summary>
        /// <returns></returns>
        public int? IsFirstDayOfYear { get; set; }

        public string EcommerceGroupID { get; set; }
    }
}
