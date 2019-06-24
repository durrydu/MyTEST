using Movit.Application.Entity.CapitalFlow;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Mapping.CapitalFlow
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓
    /// 创 建：超级管理员
    /// 日 期：2018-07-02 10:55
    /// 描 述：T_CapitalFlow
    /// </summary>
    public class T_CapitalFlowMap : EntityTypeConfiguration<T_CapitalFlowEntity>
    {
        public T_CapitalFlowMap()
        {
            #region 表、主键
            //表
            this.ToTable("T_CapitalFlow");
            //主键
            this.HasKey(t => t.CapitalFlow_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
