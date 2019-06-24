using Movit.Application.Entity.EcommerceContractManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Mapping.EcommerceContractManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓
    /// 创 建：Emily
    /// 日 期：2018-06-19 14:43
    /// 描 述：T_EcommerceDiscountProgram
    /// </summary>
    public class EcommerceDiscountProgramMap : EntityTypeConfiguration<EcommerceDiscountProgramEntity>
    {
        public EcommerceDiscountProgramMap()
        {
            #region 表、主键
            //表
            this.ToTable("T_EcommerceDiscountProgram");
            //主键
            this.HasKey(t => t.DiscountProgramID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
