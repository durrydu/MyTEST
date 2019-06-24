using Movit.Application.Entity.EcomPartnerCapitalPoolManage;
using Movit.Application.IService.EcomPartnerCapitalPoolManage;
using Movit.Application.Service.EcomPartnerCapitalPoolManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Busines.EcomPartnerCapitalPoolManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-19 15:28
    /// 描 述：T_PartnerCapitalPool
    /// </summary>
    public class T_PartnerCapitalPoolBLL
    {
        private T_PartnerCapitalPoolIService service = new T_PartnerCapitalPoolService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<T_PartnerCapitalPoolEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public T_PartnerCapitalPoolEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, T_PartnerCapitalPoolEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void InsertEntityList(List<T_PartnerCapitalPoolEntity> pCaPoolList){
                service.InsertEntityList(pCaPoolList);
        }
        public void UpdateEntityList(List<T_PartnerCapitalPoolEntity> pCaPoolList)
        {
            service.UpdateEntityList(pCaPoolList);
        }
        public IEnumerable<T_PartnerCapitalPoolEntity> getPaCaPoolList(string keyValue) {
           return service.getPaCaPoolList(keyValue);
        }
        public IEnumerable<T_PartnerCapitalPoolEntity> getAllPaCaPoolList()
        {
            return service.getAllPaCaPoolList();
        }
        #endregion
    }
}
