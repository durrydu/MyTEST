using Movit.Application.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓
    /// 创 建：姚栋
    /// 日 期：2018-05-30 13:49
    /// 描 述：Base_ProjectInfo
    /// </summary>
    public class Base_ProjectInfoMap : EntityTypeConfiguration<Base_ProjectInfoEntity>
    {
        public Base_ProjectInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_ProjectInfo");
            //主键
            this.HasKey(t => t.ProjectID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}