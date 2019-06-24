using Movit.Application.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓
    /// 创 建：超级管理员
    /// 日 期：2018-07-10 09:16
    /// 描 述：编号规则表
    /// </summary>
    public class Base_CodeRuleMap : EntityTypeConfiguration<Base_CodeRuleEntity>
    {
        public Base_CodeRuleMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_CodeRule");
            //主键
            this.HasKey(t => t.RuleId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
