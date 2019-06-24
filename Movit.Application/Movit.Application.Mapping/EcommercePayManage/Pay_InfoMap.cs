using Movit.Application.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓
    /// 创 建：durry
    /// 日 期：2018-06-22 09:54
    /// 描 述：Pay_Info
    /// </summary>
    public class Pay_InfoMap : EntityTypeConfiguration<Pay_InfoEntity>
    {
        public Pay_InfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("T_Pay_Info");
            //主键
            this.HasKey(t => t.Pay_Info_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}