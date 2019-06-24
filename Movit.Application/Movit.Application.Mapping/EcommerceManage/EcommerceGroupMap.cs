using Movit.Application.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓
    /// 创 建：超级管理员
    /// 日 期：2018-06-19 14:34
    /// 描 述：EcommerceGroup
    /// </summary>
    public class EcommerceGroupMap : EntityTypeConfiguration<EcommerceGroupEntity>
    {
        public EcommerceGroupMap()
        {
            #region 表、主键
            //表
            this.ToTable("T_EcommerceGroup");
            //主键
            this.HasKey(t => t.EcommerceGroupID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}