using System;
using Movit.Application.Code;

using SqlSugar;
namespace Movit.Application.Entity
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-23 20:23
    /// 描 述：OP_Parcel
    /// </summary>
    [SugarTable("OP_Parcel")]
    public class OP_ParcelEntity
    {
        #region 实体成员
        /// <summary>
        /// ParcelID
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)] //是主键
        public string ParcelID { get; set; }
        /// <summary>
        /// ParcelCode
        /// </summary>
        /// <returns></returns>
        public string ParcelCode { get; set; }
        /// <summary>
        /// ParcelName
        /// </summary>
        /// <returns></returns>
        public string ParcelName { get; set; }
        /// <summary>
        /// ParcelShortName
        /// </summary>
        /// <returns></returns>
        public string ParcelShortName { get; set; }
        /// <summary>
        /// FloorPrice
        /// </summary>
        /// <returns></returns>
        public string FloorPrice { get; set; }
        /// <summary>
        /// LandContractNumber
        /// </summary>
        /// <returns></returns>
        public string LandContractNumber { get; set; }
        /// <summary>
        /// ProjectID
        /// </summary>
        /// <returns></returns>
        public string ProjectID { get; set; }
        /// <summary>
        /// ProjectCode
        /// </summary>
        /// <returns></returns>
        public string ProjectCode { get; set; }
        /// <summary>
        /// ProjectName
        /// </summary>
        /// <returns></returns>
        public string ProjectName { get; set; }
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
        /// FCompanyID
        /// </summary>
        /// <returns></returns>
        public string FCompanyID { get; set; }
        /// <summary>
        /// FCompanyCode
        /// </summary>
        /// <returns></returns>
        public string FCompanyCode { get; set; }
        /// <summary>
        /// FCompanyName
        /// </summary>
        /// <returns></returns>
        public string FCompanyName { get; set; }
        /// <summary>
        /// CFCompanyID
        /// </summary>
        /// <returns></returns>
        public string CFCompanyID { get; set; }
        /// <summary>
        /// CFCompanyCode
        /// </summary>
        /// <returns></returns>
        public string CFCompanyCode { get; set; }
        /// <summary>
        /// CFCompanyName
        /// </summary>
        /// <returns></returns>
        public string CFCompanyName { get; set; }
        /// <summary>
        /// ContractTotalPrice
        /// </summary>
        /// <returns></returns>
        public decimal? ContractTotalPrice { get; set; }
        /// <summary>
        /// AcquisitionModeCode
        /// </summary>
        /// <returns></returns>
        public string AcquisitionModeCode { get; set; }
        /// <summary>
        /// AcquisitionMode
        /// </summary>
        /// <returns></returns>
        public string AcquisitionMode { get; set; }
        /// <summary>
        /// Transferor
        /// </summary>
        /// <returns></returns>
        public string Transferor { get; set; }
        /// <summary>
        /// LandUsePropertyCode
        /// </summary>
        /// <returns></returns>
        public string LandUsePropertyCode { get; set; }
        /// <summary>
        /// LandUseProperty
        /// </summary>
        /// <returns></returns>
        public string LandUseProperty { get; set; }
        /// <summary>
        /// LandAcquisitionDate
        /// </summary>
        /// <returns></returns>
        public DateTime? LandAcquisitionDate { get; set; }
        /// <summary>
        /// DeliveryDate
        /// </summary>
        /// <returns></returns>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// RightTime
        /// </summary>
        /// <returns></returns>
        public DateTime? RightTime { get; set; }
        /// <summary>
        /// IsDoRight
        /// </summary>
        /// <returns></returns>
        public string IsDoRight { get; set; }
        /// <summary>
        /// IsAgent
        /// </summary>
        /// <returns></returns>
        public string IsAgent { get; set; }
        /// <summary>
        /// IsCommission
        /// </summary>
        /// <returns></returns>
        public string IsCommission { get; set; }
        /// <summary>
        /// ParcelStatusCode
        /// </summary>
        /// <returns></returns>
        public string ParcelStatusCode { get; set; }
        /// <summary>
        /// ParcelStatus
        /// </summary>
        /// <returns></returns>
        public string ParcelStatus { get; set; }
        /// <summary>
        /// TaxTypeCode
        /// </summary>
        /// <returns></returns>
        public string TaxTypeCode { get; set; }
        /// <summary>
        /// TaxType
        /// </summary>
        /// <returns></returns>
        public string TaxType { get; set; }
        /// <summary>
        /// TraderTypeCode
        /// </summary>
        /// <returns></returns>
        public string TraderTypeCode { get; set; }
        /// <summary>
        /// TraderType
        /// </summary>
        /// <returns></returns>
        public string TraderType { get; set; }
        /// <summary>
        /// ParallelMethodCode
        /// </summary>
        /// <returns></returns>
        public string ParallelMethodCode { get; set; }
        /// <summary>
        /// ParallelMethod
        /// </summary>
        /// <returns></returns>
        public string ParallelMethod { get; set; }
        /// <summary>
        /// AccountingName
        /// </summary>
        /// <returns></returns>
        public string AccountingName { get; set; }
        /// <summary>
        /// DevelopStatusCode
        /// </summary>
        /// <returns></returns>
        public string DevelopStatusCode { get; set; }
        /// <summary>
        /// DevelopStatus
        /// </summary>
        /// <returns></returns>
        public string DevelopStatus { get; set; }
        /// <summary>
        /// ParcelAddress
        /// </summary>
        /// <returns></returns>
        public string ParcelAddress { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// AccountBookID
        /// </summary>
        /// <returns></returns>
        public string AccountBookID { get; set; }
        /// <summary>
        /// AccountBookCode
        /// </summary>
        /// <returns></returns>
        public string AccountBookCode { get; set; }
        /// <summary>
        /// AccountBookName
        /// </summary>
        /// <returns></returns>
        public string AccountBookName { get; set; }
        /// <summary>
        /// DataStatus2
        /// </summary>
        /// <returns></returns>
        public int? DataStatus2 { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// CreateUser
        /// </summary>
        /// <returns></returns>
        public string CreateUser { get; set; }
        /// <summary>
        /// UpdateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// UpdateUser
        /// </summary>
        /// <returns></returns>
        public string UpdateUser { get; set; }
        /// <summary>
        /// SyncTime
        /// </summary>
        /// <returns></returns>
        public DateTime? SyncTime { get; set; }
        /// <summary>
        /// SyncSource
        /// </summary>
        /// <returns></returns>
        public string SyncSource { get; set; }
        /// <summary>
        /// SourceID
        /// </summary>
        /// <returns></returns>
        public string SourceID { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ParcelID = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ParcelID = keyValue;
        }
        #endregion
    }
}