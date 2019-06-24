using Movit.Application.Entity;
using Movit.Application.Entity;
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
    /// 日 期：2018-07-02 13:40
    /// 描 述：T_Funds_Details
    /// </summary>
    public class T_Funds_DetailsMap : EntityTypeConfiguration<T_Funds_DetailsEntity>
    {
        public T_Funds_DetailsMap()
        {
            #region 表、主键
            //表
            this.ToTable("T_Funds_Details");
            //主键
            this.HasKey(t => t.FundsDetailsID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}