using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.CapitalFlowManage.ViewModel
{
    public class CapitalFlowViewModel
    {
        #region 实体成员
        public string Account { get; set; }
        /// <summary>
        /// CapitalFlow_Id
        /// </summary>
        /// <returns></returns>
        public string CapitalFlow_Id { get; set; }
        public decimal? Proportion { get; set; }
       
        /// <summary>
        /// Company_Id
        /// </summary>
        /// <returns></returns>
        public string Company_Id { get; set; }
      
        /// <summary>
        /// FullName
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }

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
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// IncomeAmount
        /// </summary>
        /// <returns></returns>
        public decimal? IncomeAmount { get; set; }
        /// <summary>
        /// ClearingAmount
        /// </summary>
        /// <returns></returns>
        public decimal? ClearingAmount { get; set; }
        /// <summary>
        /// PlatformExpensesAmount
        /// </summary>
        /// <returns></returns>
        public decimal? PlatformExpensesAmount { get; set; }
        /// <summary>
        /// CapitalPoolAdd
        /// </summary>
        /// <returns></returns>
        public decimal? CapitalPoolAdd { get; set; }
        /// <summary>
        /// Job_Number
        /// </summary>
        /// <returns></returns>
        public string Job_Number { get; set; }
   
        /// <summary>
        /// Department_Id
        /// </summary>
        /// <returns></returns>
        public string Department_Id { get; set; }
        /// <summary>
        /// ApprovalState
        /// </summary>
        /// <returns></returns>
        public int? ApprovalState { get; set; }
        /// <summary>
        /// Procinstid
        /// </summary>
        /// <returns></returns>
        public string Procinstid { get; set; }
        /// <summary>
        /// LatestApprover
        /// </summary>
        /// <returns></returns>
        public string LatestApprover { get; set; }
        /// <summary>
        /// LatestComment
        /// </summary>
        /// <returns></returns>
        public string LatestComment { get; set; }
        /// <summary>
        /// LatestApprovetime
        /// </summary>
        /// <returns></returns>
        public DateTime? LatestApprovetime { get; set; }
        /// <summary>
        /// DeleteMark
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
    
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
        /// CapitalFlow_Title
        /// </summary>
        /// <returns></returns>
        public string CapitalFlow_Title { get; set; }
        public string url { get; set; }
        /// <summary>
        /// starturl
        /// </summary>
        public string starturl { get; set; }
        public int IsStamp { get; set; }
        public string Remark { get; set; }
        public string ProjectID { get; set; }
        #endregion
    }
}
