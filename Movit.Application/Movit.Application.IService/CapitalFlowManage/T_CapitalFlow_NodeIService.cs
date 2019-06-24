using Movit.Application.Entity.CapitalFlow;
using Movit.Application.Entity.CapitalFlowManage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.IService.CapitalFlow
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-02 10:58
    /// 描 述：T_CapitalFlow_Node
    /// </summary>
    public interface T_CapitalFlow_NodeIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<T_CapitalFlow_NodeEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        T_CapitalFlow_NodeEntity GetEntity(string keyValue);
        IEnumerable<CapitalFlow_CFNodeView> GetEntityList(string keyValue);
        IEnumerable<CapitalFlow_CFNodeView> GetCapitalFlowView(string queryJson);
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
        void SaveForm(string keyValue, T_CapitalFlow_NodeEntity entity);
        #endregion

        void DeleteForm(string keyValue);
    }
}
