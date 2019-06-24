using System;
using Movit.Application.Code;
using SqlSugar;

namespace Movit.Application.Entity
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓软件(苏州)
    /// 创 建：姚栋
    /// 日 期：2018-06-25 19:32
    /// 描 述：T_Pay_Info_Details
    /// </summary>
    [SugarTable("T_Pay_Info_Details")]
    public class T_Pay_Info_DetailsEntity
    {
        #region 实体成员
        /// <summary>
        /// Pay_Info_Details_ID
        /// </summary>
        /// <returns></returns>
        public string Pay_Info_Details_ID { get; set; }
        /// <summary>
        /// Pay_Info_ID
        /// </summary>
        /// <returns></returns>
        public string Pay_Info_ID { get; set; }

        public string Pay_Info_Code { get; set; }
        /// <summary>
        /// Createtime
        /// </summary>
        /// <returns></returns>
        public DateTime Createtime { get; set; }
        /// <summary>
        /// Details_Name
        /// </summary>
        /// <returns></returns>
        public string Details_Name { get; set; }
        /// <summary>
        /// Details_Type
        /// </summary>
        /// <returns></returns>
        public int Details_Type { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        /// <returns></returns>
         [DecimalPrecision(18, 6)]
        public decimal Amount { get; set; }
        /// <summary>
        /// Project_ID
        /// </summary>
        /// <returns></returns>
        public string Project_ID { get; set; }
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
        /// Electricity_Supplier_Id
        /// </summary>
        /// <returns></returns>
        public string Electricity_Supplier_Id { get; set; }
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
        /// 流水单编号
        /// </summary>
        public string PayInfoDetailsCode { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.Pay_Info_Details_ID = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.Pay_Info_Details_ID = keyValue;
        }
        #endregion
    }
}