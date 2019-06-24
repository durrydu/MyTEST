using Movit.Application.Entity.CapitalFlow;
using Movit.Application.Entity.CapitalFlowManage.ViewModel;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movit.Application.IService.CapitalFlowManage;
using Movit.Application.Entity;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.Entity.BaseManage;
using Movit.Application.Entity.CapitalFlowManage;

namespace Movit.Application.IService.CapitalFlow
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-02 10:55
    /// 描 述：T_CapitalFlow
    /// </summary>
    public interface T_CapitalFlowIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<T_CapitalFlowEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        T_CapitalFlowEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<CapitalFlowViewModel> GetPageList(Pagination pagination, string queryJson, string urlname);
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
        void SaveForm(string keyValue, T_CapitalFlowEntity entity);

        #endregion

        #region 获取权限
          /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        string FindPostByUser(string userId);
          /// <summary>
        /// 获取用户项目列表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        IEnumerable<EcommerceProjectRelationEntity> FindProjectByUser(string userId);
        IEnumerable<EcommerceProjectRelationEntity> GetAllListByST();
        string submitFormApp(List<FileModel> uploadFiles, List<T_CapitalFlow_NodeEntity> entity, string year, string month, string keyValue, string CapitalFlow_Title, string Job_Number);
        IEnumerable<CapitalFlowViewModel> GetStartUrl(string keyValue, string starturlname);
        IEnumerable<IncomeView> GetCFEntity(string keyValue);
        void ApprovalUpdateState(string keyValue, T_CapitalFlowEntity entity);
        IEnumerable<T_CapitalFlowEntity> check(string keyValue, T_CapitalFlowEntity entity);
        void SaveFormApp(List<FileModel> uploadFiles, List<T_CapitalFlow_NodeEntity> entity, string year, string month, string keyValue, string CapitalFlow_Title, string Job_Number);
        IEnumerable<T_CapitalFlowEntity> checkCaFLow(List<T_CapitalFlow_NodeEntity> entity, string year, string month);
       CapitalFlowViewscs GetEn(string keyValue);
       IEnumerable<CapitalFlow_ProRelaView> updateMoney(string keyValue);
      
       void updateOrderNo(int orderNo, string keyValue);
       void updateDeleteMark(string keyValue);
       void updateCapDeleteMark(string keyValue);
        #endregion
    }
}
