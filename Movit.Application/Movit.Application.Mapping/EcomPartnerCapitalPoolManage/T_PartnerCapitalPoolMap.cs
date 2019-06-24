using Movit.Application.Entity.EcomPartnerCapitalPoolManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Mapping.EcomPartnerCapitalPoolMapManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓
    /// 创 建：超级管理员
    /// 日 期：2018-07-19 15:10
    /// 描 述：T_PartnerCapitalPool
    /// </summary>
    public class T_PartnerCapitalPoolMap : EntityTypeConfiguration<T_PartnerCapitalPoolEntity>
    {
        public T_PartnerCapitalPoolMap()
        {
            #region 表、主键
            //表
            this.ToTable("T_PartnerCapitalPool");
            //主键
            this.HasKey(t => t.PartnerCapitalPoolID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}