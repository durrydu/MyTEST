using Movit.Application.Code;
using SqlSugar;
using System;
namespace Movit.Application.Entity
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓软件(苏州)
    /// 创 建：姚栋
    /// 日 期：2018-05-30 13:49
    /// 描 述：Base_ProjectInfo
    /// </summary>
    [SugarTable("Base_ProjectInfo")]
    public class Base_ProjectInfoEntity
    {
        #region 实体成员
        /// <summary>
        /// ProjectID
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string ProjectID { get; set; }
        public string ProjectCode { get; set; }

        /// <summary>
        /// ProjecName
        /// </summary>
        /// <returns></returns>
        public string ProjecName { get; set; }
        /// <summary>
        /// DataStatus
        /// </summary>
        /// <returns></returns>
        public int? DataStatus { get; set; }
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
        /// 
        /// </summary>
        public string ProjectGeneralizeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectOfficialName { get; set; }
        public string CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public int? CityID { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ProjectID = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.DataStatus = 0;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ProjectID = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}