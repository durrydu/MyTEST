using Movit.Application.Entity.EcomPartnerCapitalPoolManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.IService.EcomPartnerCapitalPoolManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-19 15:10
    /// 描 述：T_PartnerCapitalPool
    /// </summary>
    public interface T_PartnerCapitalPoolIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<T_PartnerCapitalPoolEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        T_PartnerCapitalPoolEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, T_PartnerCapitalPoolEntity entity);
        //
        void InsertEntityList(List<T_PartnerCapitalPoolEntity> pCaPoolList);
        void UpdateEntityList(List<T_PartnerCapitalPoolEntity> pCaPoolList);
        
        IEnumerable<T_PartnerCapitalPoolEntity> getPaCaPoolList(string keyValue);
        IEnumerable<T_PartnerCapitalPoolEntity> getAllPaCaPoolList();
        
        #endregion
    }
}
