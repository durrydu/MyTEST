using Movit.Application.Code;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.EcommerceTransferManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-06-25 14:48
    /// 描 述：T_Transfer_Info
    /// </summary>
    [SugarTable("T_Transfer_Info")]
    public class Transfer_InfoEntity
    {
        #region 实体成员
        /// <summary>
        /// Transfer_Info_Id
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string Transfer_Info_Id { get; set; }
        /// <summary>
        /// EcommerceID
        /// </summary>
        /// <returns></returns>
        public string EcommerceID { get; set; }
        /// <summary>
        /// CompanyId
        /// </summary>
        /// <returns></returns>
        public string CompanyId { get; set; }
        /// <summary>
        /// Transfer_Code
        /// </summary>
        /// <returns></returns>
        public string Transfer_Code { get; set; }
        /// <summary>
        /// Transfer_Money
        /// </summary>
        /// <returns></returns>
        [DecimalPrecision(18, 6)]
        public decimal? Transfer_Money { get; set; }
        public string EcommerceProjectRelationID { get; set; }
        /// <summary>
        /// Transfer_Title
        /// </summary>
        /// <returns></returns>
        public string Transfer_Title { get; set; }
        public string EcommerceGroupID { get; set; }
        /// <summary>
        /// Transfer_Date
        /// </summary>
        /// <returns></returns>
        public DateTime Transfer_Date { get; set; }
        /// <summary>
        /// Transfer_Balance
        /// </summary>
        /// <returns></returns>
        [DecimalPrecision(18, 6)]
        public decimal? Transfer_Balance { get; set; }
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
        /// Remark
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// ProjectID
        /// </summary>
        /// <returns></returns>
        public string ProjectID { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.Transfer_Info_Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.DeleteMark = 0;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.Transfer_Info_Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}

