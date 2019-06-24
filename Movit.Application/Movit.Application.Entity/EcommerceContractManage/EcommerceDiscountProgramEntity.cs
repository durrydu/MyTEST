using Movit.Application.Code;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.EcommerceContractManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓软件(苏州)
    /// 创 建：Emily
    /// 日 期：2018-06-19 14:43
    /// 描 述：T_EcommerceDiscountProgram
    /// </summary>
    [SugarTable("T_EcommerceDiscountProgram")]
    public class EcommerceDiscountProgramEntity 
    {
        #region 实体成员
        /// <summary>
        /// DiscountProgramID
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string DiscountProgramID { get; set; }
        /// <summary>
        /// EcommerceProjectRelationID
        /// </summary>
        /// <returns></returns>
        public string EcommerceProjectRelationID { get; set; }
        /// <summary>
        /// Format
        /// </summary>
        /// <returns></returns>
        public string Format { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        /// <returns></returns>
        public decimal? Amount { get; set; }
        /// <summary>
        /// Discount
        /// </summary>
        /// <returns></returns>
        public decimal? Discount { get; set; }
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
            this.DiscountProgramID = Guid.NewGuid().ToString();
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
            this.DiscountProgramID = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
