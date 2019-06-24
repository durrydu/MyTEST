using Movit.Application.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓
    /// 创 建：超级管理员
    /// 日 期：2018-07-23 20:23
    /// 描 述：OP_Parcel
    /// </summary>
    public class OP_ParcelMap : EntityTypeConfiguration<OP_ParcelEntity>
    {
        public OP_ParcelMap()
        {
            #region 表、主键
            //表
            this.ToTable("OP_Parcel");
            //主键
            this.HasKey(t => t.ParcelID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}