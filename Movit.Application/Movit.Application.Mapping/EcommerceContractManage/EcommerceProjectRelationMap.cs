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
    /// 创 建：emily
    /// 日 期：2018-06-19 11:08
    /// 描 述：T_EcommerceProjectRelation
    /// </summary>
    public class EcommerceProjectRelationMap : EntityTypeConfiguration<EcommerceProjectRelationEntity>
    {
        public EcommerceProjectRelationMap()
        {
            #region 表、主键
            //表
            this.ToTable("T_EcommerceProjectRelation");
            //主键
            this.HasKey(t => t.EcommerceProjectRelationID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
