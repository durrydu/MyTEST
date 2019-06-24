using Movit.Application.Entity;
using Movit.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓
    /// 创 建：姚栋
    /// 日 期：2018-06-03 18:38
    /// 描 述：T_Attachment
    /// </summary>
    public class T_AttachmentMap : EntityTypeConfiguration<T_AttachmentEntity>
    {
        public T_AttachmentMap()
        {
            #region 表、主键
            //表
            this.ToTable("T_Attachment");
            //主键
            this.HasKey(t => t.AttachmentID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}