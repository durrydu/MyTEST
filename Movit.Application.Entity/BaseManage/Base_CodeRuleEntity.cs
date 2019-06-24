using System;
using Movit.Application.Code;

using SqlSugar;
namespace Movit.Application.Entity
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-10 09:16
    /// 描 述：编号规则表
    /// </summary>
     [SugarTable(编号规则表)]
    public class Base_CodeRuleEntity
    {
        #region 实体成员
        /// <summary>
        /// 编码规则主键
        /// </summary>
        /// <returns></returns>
        public string RuleId { get; set; }
        /// <summary>
        /// 系统功能Id
        /// </summary>
        /// <returns></returns>
        public string ModuleId { get; set; }
        /// <summary>
        /// 系统功能
        /// </summary>
        /// <returns></returns>
        public string Module { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        /// <returns></returns>
        public string EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
        /// <summary>
        /// 方式（可编辑、自动）
        /// </summary>
        /// <returns></returns>
        public int? Mode { get; set; }
        /// <summary>
        /// 当前流水号
        /// </summary>
        /// <returns></returns>
        public string CurrentNumber { get; set; }
        /// <summary>
        /// 规则格式Json
        /// </summary>
        /// <returns></returns>
        public string RuleFormatJson { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public  void Create()
        {
            this.RuleId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public  void Modify(string keyValue)
        {
            this.RuleId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}