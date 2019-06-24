using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.EcommerceTransferManage.ViewModel
{
   public class Transfer_Project_EcommerceView
    {
        #region 实体成员
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        public string EcommerceGroupID { get; set; }
        /// <summary>
        /// Transfer_Code
        /// </summary>
        /// <returns></returns>
        public string Transfer_Code { get; set; }
        /// <summary>
        /// Transfer_Info_Id
        /// </summary>
        /// <returns></returns>
        public string Transfer_Info_Id { get; set; }
        /// <summary>
        /// Transfer_Money
        /// </summary>
        /// <returns></returns>
        public decimal? Transfer_Money { get; set; }
        /// <summary>
        /// Transfer_Date
        /// </summary>
        /// <returns></returns>
        public DateTime? Transfer_Date { get; set; }
        /// <summary>
        /// Transfer_Title
        /// </summary>
        /// <returns></returns>
        public string Transfer_Title { get; set; }
        /// <summary>
        /// EcommerceID
        /// </summary>
        /// <returns></returns>
        /// 
        public string EcommerceID { get; set; }
        /// <summary>
        /// ProjectID
        /// </summary>
        /// <returns></returns>
        /// 
        public string ProjectID { get; set; }
        /// <summary>
        /// CreateUserName
        /// </summary>
        /// <returns></returns>
        /// 
        public string CreateUserName { get; set; }
        /// <summary>
        /// ProjecName
        /// </summary>
        /// <returns></returns>
        public string ProjecName { get; set; }
        /// <summary>
        /// EcommerceName
        /// </summary>
        /// <returns></returns>
        public string EcommerceGroupName { get; set; }
       /// <summary>
        /// CompanyName
        /// </summary>
        /// <returns></returns>
        public string CompanyName { get; set; }
       
        #endregion
    }
}
