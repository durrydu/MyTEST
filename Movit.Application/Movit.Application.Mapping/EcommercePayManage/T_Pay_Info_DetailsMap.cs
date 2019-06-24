using Movit.Application.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓
    /// 创 建：姚栋
    /// 日 期：2018-06-25 19:32
    /// 描 述：T_Pay_Info_Details
    /// </summary>
    public class T_Pay_Info_DetailsMap : EntityTypeConfiguration<T_Pay_Info_DetailsEntity>
    {
        public T_Pay_Info_DetailsMap()
        {
            #region 表、主键
            //表
            this.ToTable("T_Pay_Info_Details");
            //主键
            this.HasKey(t => t.Pay_Info_Details_ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}