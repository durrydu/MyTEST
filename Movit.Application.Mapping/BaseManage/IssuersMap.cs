using Movit.Application.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓
    /// 创 建：毕玉
    /// 日 期：2018-05-30 10:23
    /// 描 述：Issuers
    /// </summary>
    public class IssuersMap : EntityTypeConfiguration<IssuersEntity>
    {
        public IssuersMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Issuers");
            //主键
            this.HasKey(t => t.IssuersId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
