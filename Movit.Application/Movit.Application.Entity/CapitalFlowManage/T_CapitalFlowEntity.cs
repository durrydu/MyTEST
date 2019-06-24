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
    /// 日 期：2018-07-02 10:55
    /// 描 述：T_CapitalFlow
    /// </summary>
    [SugarTable("T_CapitalFlow")]
    public class T_CapitalFlowEntity
    {
        #region 实体成员
        /// <summary>
        /// CapitalFlow_Id
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string CapitalFlow_Id { get; set; }
        public string Account { get; set; }
        /// <summary>
        /// Company_Id
        /// </summary>
        /// <returns></returns>
        public string Company_Id { get; set; }
        /// <summary>
        /// Job_Number
        /// </summary>
        /// <returns></returns>
        public string Job_Number { get; set; }
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
        /// Department_Id
        /// </summary>
        /// <returns></returns>
        public string Department_Id { get; set; }
        /// <summary>
        /// ApprovalState
        /// </summary>
        /// <returns></returns>
        public int? ApprovalState { get; set; }
        /// <summary>
        /// Procinstid
        /// </summary>
        /// <returns></returns>
        public string Procinstid { get; set; }
        /// <summary>
        /// LatestApprover
        /// </summary>
        /// <returns></returns>
        public string LatestApprover { get; set; }
        /// <summary>
        /// LatestComment
        /// </summary>
        /// <returns></returns>
        public string LatestComment { get; set; }
        /// <summary>
        /// LatestApprovetime
        /// </summary>
        /// <returns></returns>
        public DateTime? LatestApprovetime { get; set; }
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
        /// CapitalFlow_Title
        /// </summary>
        /// <returns></returns>
        public string CapitalFlow_Title { get; set; }
        public int IsStamp { get; set; }
        public string Remark { get; set; }

        public string TrunkID { get; set; }
        public string ParentID { get; set; }
        public int? OrderNo { get; set; }


        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.CapitalFlow_Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.Department_Id = OperatorProvider.Provider.Current().DepartmentId;
            this.Account = OperatorProvider.Provider.Current().Account;


        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.CapitalFlow_Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}