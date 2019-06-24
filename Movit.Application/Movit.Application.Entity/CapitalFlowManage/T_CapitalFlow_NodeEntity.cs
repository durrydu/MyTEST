using Movit.Application.Code;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.CapitalFlow
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-02 10:58
    /// 描 述：T_CapitalFlow_Node
    /// </summary>
    [SugarTable("T_CapitalFlow_Node")]
    public class T_CapitalFlow_NodeEntity
    {
        #region 实体成员
        /// <summary>
        /// CapitalFlow_Details_Id
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string CapitalFlow_Details_Id { get; set; }
        /// <summary>
        /// Company_Id
        /// </summary>
        /// <returns></returns>
        public string Company_Id { get; set; }
        /// <summary>
        /// EcommerceID
        /// </summary>
        /// <returns></returns>
        public string EcommerceID { get; set; }
        public DateTime? UploadDate { get; set; }
        /// <summary>
        /// EcommerceName
        /// </summary>
        /// <returns></returns>
        public string EcommerceName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        /// <summary>
        /// EcommerceGroupID
        /// </summary>
        /// <returns></returns>
        public string EcommerceGroupID { get; set; }
        /// <summary>
        /// EcommerceGroupName
        /// </summary>
        /// <returns></returns>
        public string EcommerceGroupName { get; set; }
        /// <summary>
        /// CapitalFlow_Id
        /// </summary>
        /// <returns></returns>
        public string CapitalFlow_Id { get; set; }
        /// <summary>
        /// ProjectID
        /// </summary>
        /// <returns></returns>
        public string ProjectID { get; set; }
        /// <summary>
        /// EcommerceProjectRelationID
        /// </summary>
        /// <returns></returns>
        public string EcommerceProjectRelationID { get; set; }
        /// <summary>
        /// ProjectName
        /// </summary>
        /// <returns></returns>
        public string ProjectName { get; set; }
        /// <summary>
        /// IncomeAmount
        /// </summary>
        /// <returns></returns>
         [DecimalPrecision(18, 6)]
        public decimal? IncomeAmount { get; set; }
        /// <summary>
        /// ClearingAmount
        /// </summary>
        /// <returns></returns>
        [DecimalPrecision(18, 6)]
        public decimal? ClearingAmount { get; set; }
        /// <summary>
        /// Proportion
        /// </summary>
        /// <returns></returns>
        public decimal? Proportion { get; set; }
        /// <summary>
        /// PlatformExpensesAmount
        /// </summary>
        /// <returns></returns>
        [DecimalPrecision(18, 6)]
        public decimal? PlatformExpensesAmount { get; set; }
        /// <summary>
        /// CapitalPoolAdd
        /// </summary>
        /// <returns></returns>
        /// 
         [DecimalPrecision(18, 6)]
        public decimal? CapitalPoolAdd { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.CapitalFlow_Details_Id = Guid.NewGuid().ToString();
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
            this.CapitalFlow_Details_Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}

