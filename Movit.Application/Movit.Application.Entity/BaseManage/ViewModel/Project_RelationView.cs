using Movit.Application.Code;
using System;

namespace Movit.Application.Entity.BaseManage.ViewModel
{
    /// <summary>
    /// 作者：durry
    /// 日期：2018年06月21日
    /// </summary>
    public class Project_RelationView
    {
        #region 实体成员
        /// <summary>
        /// ProjectID
        /// </summary>
        /// <returns></returns>
        public string ProjectID { get; set; }
        /// <summary>
        /// ProjecName
        /// </summary>
        /// <returns></returns>
        public string ProjecName { get; set; }
        /// <summary>
        /// DataStatus
        /// </summary>
        /// <returns></returns>
        public int? DataStatus { get; set; }
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
        /// 
        /// </summary>
        public string ProjectGeneralizeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectOfficialName { get; set; }
        public string CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public int? CityID { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }

        ///实际可支配金额
        public decimal ControlAmount { get; set; }

        #endregion
    }
}
