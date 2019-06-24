using Movit.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping.BaseManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.11.02 14:27
    /// 描 述：机构管理
    /// </summary>
    public class OrganizeMap : EntityTypeConfiguration<OrganizeEntity>
    {
        public OrganizeMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Organize");
            //主键
            this.HasKey(t => t.OrganizeId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
