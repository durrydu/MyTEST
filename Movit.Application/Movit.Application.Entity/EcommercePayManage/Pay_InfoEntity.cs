using System;
using Movit.Application.Code;
using SqlSugar;

namespace Movit.Application.Entity
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓软件(苏州)
    /// 创 建：durry
    /// 日 期：2018-06-22 09:54
    /// 描 述：Pay_Info
    /// </summary>
    [SugarTable("T_Pay_Info")]
    public class Pay_InfoEntity
    {
        #region 实体成员
        /// <summary>
        /// Pay_Info_Id
        /// </summary>
        /// <returns></returns>
       [SugarColumn(IsPrimaryKey = true)]
        public string Pay_Info_Id { get; set; }
        /// <summary>
        /// Pay_Info_Code
        /// </summary>
        /// <returns></returns>
        public string Pay_Info_Code { get; set; }
        /// <summary>
        /// EcommerceGroupID
        /// </summary>
        /// <returns></returns>
        public string EcommerceGroupID { get; set; }
        /// <summary>
        /// EcommerceID
        /// </summary>
        /// <returns></returns>
        public string EcommerceID { get; set; }
        /// <summary>
        /// Project_Id
        /// </summary>
        /// <returns></returns>
        public string Project_Id { get; set; }
        /// <summary>
        /// Electricity_Supplier_Name
        /// </summary>
        /// <returns></returns>
        public string Electricity_Supplier_Name { get; set; }
        /// <summary>
        /// Electricity_Supplier_Code
        /// </summary>
        /// <returns></returns>
        public string Electricity_Supplier_Code { get; set; }
        /// <summary>
        /// Project_Code
        /// </summary>
        /// <returns></returns>
        public string Project_Code { get; set; }
        /// <summary>
        /// Project_Name
        /// </summary>
        /// <returns></returns>
        public string Project_Name { get; set; }
        /// <summary>
        /// Pay_Reason
        /// </summary>
        /// <returns></returns>
        public string Pay_Reason { get; set; }
        /// <summary>
        /// Pay_Info_Type
        /// </summary>
        /// <returns></returns>
        public string Pay_Info_Type { get; set; }
        /// <summary>
        /// Contract_Name
        /// </summary>
        /// <returns></returns>
        public string Contract_Name { get; set; }
        /// <summary>
        /// Contract_Code
        /// </summary>
        /// <returns></returns>
        public string Contract_Code { get; set; }
        /// <summary>
        /// Pay_Money
        /// </summary>
        /// <returns></returns>
        [DecimalPrecision(18, 6)]
        public decimal? Pay_Money { get; set; }
        /// <summary>
        /// Pay_Createtime
        /// </summary>
        /// <returns></returns>
        public DateTime? Pay_Createtime { get; set; }
        /// <summary>
        /// Pay_Completetime
        /// </summary>
        /// <returns></returns>
        public DateTime? Pay_Completetime { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        /// <returns></returns>
        public string Url { get; set; }
        /// <summary>
        /// Login_Name
        /// </summary>
        /// <returns></returns>
        public string Login_Name { get; set; }
        /// <summary>
        /// Login_Code
        /// </summary>
        /// <returns></returns>
        public string Login_Code { get; set; }
        /// <summary>
        /// Approval_Status
        /// </summary>
        /// <returns></returns>
        public string Approval_Status { get; set; }
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
        /// ContractMoney
        /// </summary>
        /// <returns></returns>
        [DecimalPrecision(18, 6)]
        public decimal? ContractMoney { get; set; }

        /// <summary>
        /// CompanyName
        /// </summary>
        /// <returns></returns>
        public string CompanyName { get; set; }
        public string CompanyID { get; set; }

        /// <summary>
        /// 最新流水处理编号
        /// </summary>
        public string LastPayInfoDetailsCode { get; set; }
        /// <summary>
        /// Procinstid
        /// </summary>
        public string Procinstid { get; set; }
        /// <summary>
        /// LatestApprover
        /// </summary>
        public string LatestApprover { get; set; }
        /// <summary>
        /// LatestComment
        /// </summary>
        public string LatestComment { get; set; }
        /// <summary>
        /// LatestApprovetime
        /// </summary>
        public DateTime? LatestApprovetime { get; set; }
        /// <summary>
        /// EcommerceGroupName
        /// </summary>
        public string EcommerceGroupName { get; set; }

        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            //this.Pay_Info_Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.DeleteMark = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.Pay_Info_Id = keyValue;
            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}