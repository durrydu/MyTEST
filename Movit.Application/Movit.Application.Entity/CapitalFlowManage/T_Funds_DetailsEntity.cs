using System;
using Movit.Application.Code;

using SqlSugar;
namespace Movit.Application.Entity
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓软件(苏州)
    /// 创 建：姚栋
    /// 日 期：2018-07-18 20:02
    /// 描 述：T_Funds_Details
    /// </summary>
    [SugarTable("T_Funds_Details")]
    public class T_Funds_DetailsEntity
    {
        #region 实体成员
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
         [DecimalPrecision(18, 6)]
        public decimal? TotalCapitalPoolAdd { get; set; }
        /// <summary>
        /// TotalPayAmount
        /// </summary>
        /// <returns></returns>
        [DecimalPrecision(18, 6)]
        public decimal? TotalPayAmount { get; set; }
        /// <summary>
        /// TotalTransferAmount
        /// </summary>
        /// <returns></returns>
         [DecimalPrecision(18, 6)]
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
        [DecimalPrecision(18, 6)]
        public decimal? ControlTotalAmount { get; set; }
    
        /// <summary>
        /// ActualControlTotalAmount
        /// </summary>
        /// <returns></returns>
        [DecimalPrecision(18, 6)]
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.FundsDetailsID = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.FundsDetailsID = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}