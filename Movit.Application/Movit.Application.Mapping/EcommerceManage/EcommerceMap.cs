using Movit.Application.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓
    /// 创 建：durry.du
    /// 日 期：2018-06-19 10:50
    /// 描 述：Ecommerce
    /// </summary>
    public class EcommerceMap : EntityTypeConfiguration<EcommerceEntity>
    {
        public EcommerceMap()
        {
            #region 表、主键
            //表
            this.ToTable("T_Ecommerce");
            //主键
            this.HasKey(t => t.EcommerceID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}