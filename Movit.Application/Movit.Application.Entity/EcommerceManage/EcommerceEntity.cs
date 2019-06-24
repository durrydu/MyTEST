using System;
using Movit.Application.Code;
using SqlSugar;

namespace Movit.Application.Entity
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓软件(苏州)
    /// 创 建：durry.du
    /// 日 期：2018-06-19 10:50
    /// 描 述：Ecommerce
    /// </summary>
    [SugarTable("T_Ecommerce")]
    public class EcommerceEntity
    {
        #region 实体成员
        /// <summary>
        /// EcommerceID
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string EcommerceID { get; set; }

        /// <summary>
        /// EcommerceCode
        /// </summary>
        public string EcommerceCode { get; set; }
        /// <summary>
        /// T_E_EcommerceGroupID
        /// </summary>
        /// <returns></returns>
        public string T_E_EcommerceGroupID { get; set; }
        /// <summary>
        /// EcommerceGroupName
        /// </summary>
        /// <returns></returns>
        public string EcommerceGroupName { get; set; }
        /// <summary>
        /// EcommerceName
        /// </summary>
        /// <returns></returns>
        public string EcommerceName { get; set; }
        /// <summary>
        /// PlatformRate
        /// </summary>
        /// <returns></returns>
        public decimal? PlatformRate { get; set; }
        /// <summary>
        /// EcommerceType
        /// </summary>
        /// <returns></returns>
        public int? EcommerceType { get; set; }
        /// <summary>
        /// AgentUserID
        /// </summary>
        /// <returns></returns>
        public string AgentUserID { get; set; }
        /// <summary>
        /// CooperateStartTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CooperateStartTime { get; set; }
        /// <summary>
        /// CooperateEndTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CooperateEndTime { get; set; }
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
        /// ApprovalState
        /// </summary>
        /// <returns></returns>
        public int? ApprovalState { get; set; }
        /// <summary>
        /// EcommerceGroupID
        /// </summary>
        /// <returns></returns>
        public string EcommerceGroupID { get; set; }
   
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.DeleteMark = 0;
            this.EcommerceID = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.AgentUserID = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.EcommerceID = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}