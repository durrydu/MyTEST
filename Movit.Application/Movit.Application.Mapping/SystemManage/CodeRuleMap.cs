using Movit.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping.SystemManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.12.21 16:19
    /// 描 述：编号规则
    /// </summary>
    public class CodeRuleMap : EntityTypeConfiguration<CodeRuleEntity>
    {
        public CodeRuleMap()
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
