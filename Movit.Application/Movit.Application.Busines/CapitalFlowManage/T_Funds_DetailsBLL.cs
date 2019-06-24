
using Movit.Application.Entity;
using Movit.Application.Entity.CapitalFlowManage;
using Movit.Application.Entity.CapitalFlowManage.ViewModel;
using Movit.Application.Entity.EcomPartnerCapitalPoolManage;
using Movit.Application.IService.CapitalFlowManage;
using Movit.Application.Service.CapitalFlowManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Busines.CapitalFlowManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-02 13:40
    /// 描 述：T_Funds_Details
    /// </summary>
    public class T_Funds_DetailsBLL
    {
        private IT_Funds_DetailsService service = new T_Funds_DetailsService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<T_Funds_DetailsEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        public IEnumerable<T_Funds_DetailsViewModel> GetPieDataList(string queryJson)
        {
            return service.GetPieDataList(queryJson);
        }
        public IEnumerable<T_PartnerCapitalPoolViewModel> GetLineDataList(string queryJson)
        {
            return service.GetLineDataList(queryJson);
        }
        
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public T_Funds_DetailsEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
         /// <summary>
        /// 获取资金池数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public T_Funds_DetailsEntity GetFundStaticJson(string queryJson)
        {
            return service.GetFundStaticJson(queryJson);
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
        public void SaveForm(string keyValue, T_Funds_DetailsEntity entity)
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
        public int DeleteByData(DateTime time)
        {

            try
            {
                return service.DeleteByData(time);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void BacthInsert(List<T_Funds_DetailsEntity> entityList)
        {

            try
            {
                service.BacthInsert(entityList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

       
    }
}
