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
    /// 创 建：emily
    /// 日 期：2018-06-19 11:08
    /// 描 述：T_EcommerceProjectRelation
    /// </summary>
    [SugarTable("T_EcommerceProjectRelation")]
    public class EcommerceProjectRelationEntity 
    {
        #region 实体成员
        /// <summary>
        /// EcommerceProjectRelationID
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)] //是主键
        public string EcommerceProjectRelationID { get; set; }
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

        public string EcommerceCode { get; set; }
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
        /// EcommerceTypeName
        /// </summary>
        /// <returns></returns>
        public string EcommerceTypeName { get; set; }
        /// <summary>
        /// Agent
        /// </summary>
        /// <returns></returns>
        public string Agent { get; set; }
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
        /// 预计合同金额
        /// </summary>
        [DecimalPrecision(18, 6)]
        public decimal? ForceContractAmount { get; set; }
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
        /// DeleteMark
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ProjectCode
        /// </summary>
        /// <returns></returns>
        public string ProjectCode { get; set; }
      
        /// <summary>
        /// ProjectGeneralizeName
        /// </summary>
        /// <returns></returns>
        public string ProjectGeneralizeName { get; set; }
        /// <summary>
        /// ProjectOfficialName
        /// </summary>
        /// <returns></returns>
        public string ProjectOfficialName { get; set; }
        /// <summary>
        /// CompanyId
        /// </summary>
        /// <returns></returns>
        public string CompanyId { get; set; }
        /// <summary>
        /// CompanyCode
        /// </summary>
        /// <returns></returns>
        public string CompanyCode { get; set; }
        /// <summary>
        /// CompanyName
        /// </summary>
        /// <returns></returns>
        public string CompanyName { get; set; }
        /// <summary>
        /// CityID
        /// </summary>
        /// <returns></returns>
        public int? CityID { get; set; }
        /// <summary>
        /// CityCode
        /// </summary>
        /// <returns></returns>
        public string CityCode { get; set; }
        /// <summary>
        /// CityName
        /// </summary>
        /// <returns></returns>
        public string CityName { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// PrincipleMan
        /// </summary>
        /// <returns></returns>
        public string PrincipleMan { get; set; }
        /// <summary>
        /// ActualControlTotalAmount
        /// </summary>
        /// <returns></returns>
        [DecimalPrecision(18, 6)]
        public decimal ActualControlTotalAmount { get; set; }

        /// <summary>
        /// ProjectType
        /// </summary>
        /// <returns></returns>
        public int? ProjectType { get; set; }
        /// <summary>
        /// FlowNopayTotalAmount
        /// </summary>
        /// <returns></returns>
        [DecimalPrecision(18, 6)]
        public decimal FlowNopayTotalAmount { get; set; }
         [DecimalPrecision(18, 6)]
        public decimal ControlTotalAmount { get; set; }

        /// <summary>
        /// ApprovalState
        /// </summary>
        /// <returns></returns>
        public int? ApprovalState { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
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
        /// 是否是主干
        /// 0：分支
        /// 1：主干
        /// </summary>
        public int IsTrunk { get; set; }
        /// <summary>
        /// Account
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// ContractName
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// ContractNature
        /// </summary>
        public int? ContractNature { get; set; }
        /// <summary>
        /// IsStandard
        /// </summary>
        public int? IsStandard { get; set; }
        /// <summary>
        /// PartyA
        /// </summary>
        public string PartyA { get; set; }
        /// <summary>
        /// PartyB
        /// </summary>
        public string PartyB { get; set; }
        /// <summary>
        /// BiddingMethod
        /// </summary>
        public int? BiddingMethod { get; set; }
        /// <summary>
        /// IsStamp
        /// </summary>
        public int? IsStamp { get; set; }
        public string ContractTypeName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.EcommerceProjectRelationID = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.Account=OperatorProvider.Provider.Current().Account;
            this.ApprovalState = 1;
            this.DeleteMark = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.EcommerceProjectRelationID = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
