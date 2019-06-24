using Movit.Application.Code;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.EcomPartnerCapitalPoolManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-19 15:10
    /// 描 述：T_PartnerCapitalPool
    /// </summary>
   
    public class T_PartnerCapitalPoolViewModel
    {
        #region 实体成员
        /// <summary>
        /// PartnerCapitalPoolID
        /// </summary>
        /// <returns></returns>
        public string PartnerCapitalPoolID { get; set; }
        /// <summary>
        /// EcommerceProjectRelationID
        /// </summary>
        /// <returns></returns>
        public string EcommerceProjectRelationID { get; set; }
        /// <summary>
        /// T_P_PartnerCapitalPoolID
        /// </summary>
        /// <returns></returns>
        public string T_P_PartnerCapitalPoolID { get; set; }
        /// <summary>
        /// OperationTitle
        /// </summary>
        /// <returns></returns>
        public string OperationTitle { get; set; }
        public string sDate { get; set; }
        /// <summary>
        /// OperationType
        /// </summary>
        /// <returns></returns>
        public int? OperationType { get; set; }
        /// <summary>
        /// OperationMoney
        /// </summary>
        /// <returns></returns>
        public decimal? OperationMoney { get; set; }
        /// <summary>
        /// CurrentMoney
        /// </summary>
        /// <returns></returns>
        public decimal? CurrentMoney { get; set; }
        /// <summary>
        /// CurrentBalance
        /// </summary>
        /// <returns></returns>
        public decimal? CurrentBalance { get; set; }
        /// <summary>
        /// AccountingType
        /// </summary>
        /// <returns></returns>
        public int? AccountingType { get; set; }
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
        public DateTime? StatisticalDate { get; set; }
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
        /// ObjectID
        /// </summary>
        /// <returns></returns>
        public string ObjectID { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.PartnerCapitalPoolID = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.PartnerCapitalPoolID = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
