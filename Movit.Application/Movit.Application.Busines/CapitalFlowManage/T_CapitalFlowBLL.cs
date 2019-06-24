using Movit.Application.Entity;
using Movit.Application.Entity.BaseManage;
using Movit.Application.Entity.CapitalFlow;
using Movit.Application.Entity.CapitalFlowManage;
using Movit.Application.Entity.CapitalFlowManage.ViewModel;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.IService.CapitalFlow;
using Movit.Application.Service.CapitalFlow;
using Movit.Cache.Factory;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Busines.CapitalFlow
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-02 10:55
    /// 描 述：T_CapitalFlow
    /// </summary>
    public class T_CapitalFlowBLL
    {
        private T_CapitalFlowIService service = new T_CapitalFlowService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<T_CapitalFlowEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public T_CapitalFlowEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取页面分页数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<CapitalFlowViewModel> GetPageList(Pagination pagination, string queryJson, string urlname)
        {
            return service.GetPageList(pagination, queryJson, urlname);
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
        public void SaveForm(string keyValue, T_CapitalFlowEntity entity)
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
        #endregion

        #region 获取权限
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <returns></returns>
        public string FindPostByUser(string userId) 
        {
            userId=service.FindPostByUser(userId);
            return userId;
        }
        /// <summary>
        /// 获取项目权限 （根据用户权限）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EcommerceProjectRelationEntity> FindProjectByUser(string userId)
        {
            return service.FindProjectByUser(userId);
        }
        public IEnumerable<EcommerceProjectRelationEntity> GetAllListByST()
        {
            return service.GetAllListByST();
        }
        public string submitFormApp(List<FileModel> uploadFiles, List<T_CapitalFlow_NodeEntity> entity, string year, string month, string keyValue, string CapitalFlow_Title, string Job_Number)
        {
           
            //CacheFactory.Cache().RemoveCache(cacheKey);
            try
            {
                return service.submitFormApp(uploadFiles, entity, year, month, keyValue, CapitalFlow_Title, Job_Number);
               
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveFormApp(List<FileModel> uploadFiles, List<T_CapitalFlow_NodeEntity> entity, string year, string month, string keyValue, string CapitalFlow_Title, string Job_Number)
        {
            service.SaveFormApp(uploadFiles, entity, year, month, keyValue, CapitalFlow_Title, Job_Number);
               
           
        }
        
        public IEnumerable<CapitalFlowViewModel> GetStartUrl(string keyValue, string starturlname)
        {
            return service.GetStartUrl(keyValue, starturlname);
        }
        public IEnumerable<IncomeView> GetCFEntity(string keyValue) {
            return service.GetCFEntity(keyValue);
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void ApprovalUpdateState(string keyValue, T_CapitalFlowEntity entity)
        {
            try
            {
                service.ApprovalUpdateState(keyValue, entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<T_CapitalFlowEntity> check(string keyValue, T_CapitalFlowEntity entity)
        {
            try
            {
                return service.check(keyValue, entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<T_CapitalFlowEntity> checkCaFLow(List<T_CapitalFlow_NodeEntity> entity, string year, string month) {
            return service.checkCaFLow(entity, year, month);
        }
        public CapitalFlowViewscs GetEn(string keyValue)
        { 
         return service.GetEn(keyValue);
        }
        public IEnumerable<CapitalFlow_ProRelaView> updateMoney(string keyValue)
        {
            return service.updateMoney(keyValue);
        }
       
        public void updateOrderNo(int orderNo, string keyValue) {
            service.updateOrderNo(orderNo, keyValue);
        }
        public void updateDeleteMark( string keyValue) {
            service.updateDeleteMark(keyValue);
        }
        public void updateCapDeleteMark(string keyValue) {
            service.updateCapDeleteMark(keyValue);
        }
        #endregion
    }
}
