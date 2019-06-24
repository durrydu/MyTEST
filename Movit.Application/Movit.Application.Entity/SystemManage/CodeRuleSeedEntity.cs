using Movit.Application.Code;
using SqlSugar;
using System;

namespace Movit.Application.Entity.SystemManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.12.21 16:19
    /// 描 述：编号规则种子
    /// </summary>
    [SugarTable("Base_CodeRuleSeed")]
    public class CodeRuleSeedEntity  
    {
        #region 实体成员
        /// <summary>
        /// 编号规则种子主键
        /// </summary>		
        [SugarColumn(IsPrimaryKey = true)]
        public string RuleSeedId { get; set; }
        /// <summary>
        /// 编码规则主键
        /// </summary>		
        public string RuleId { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>		
        public string UserId { get; set; }
        /// <summary>
        /// 种子值
        /// </summary>		
        public int? SeedValue { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>		
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>		
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>		
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>		
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>		
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.RuleSeedId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.ModifyDate = DateTime.Now;
            this.CreateUserId = "System";
            this.CreateUserName = "System";
        }

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = "System";
            this.ModifyUserName = "System";
        }
        #endregion
    }
}