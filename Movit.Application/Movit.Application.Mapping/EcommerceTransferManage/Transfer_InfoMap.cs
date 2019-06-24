using Movit.Application.Entity.EcommerceTransferManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Mapping.EcommerceTransferManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓
    /// 创 建：超级管理员
    /// 日 期：2018-06-25 14:48
    /// 描 述：T_Transfer_Info
    /// </summary>
    public class Transfer_InfoMap : EntityTypeConfiguration<Transfer_InfoEntity>
    {
        public Transfer_InfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("T_Transfer_Info");
            //主键
            this.HasKey(t => t.Transfer_Info_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}