using Movit.Application.Entity;
using Movit.Application.Entity.CapitalFlowManage.ViewModel;
using Movit.Application.Entity.EcomPartnerCapitalPoolManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.IService.CapitalFlowManage
{

    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-02 13:40
    /// 描 述：T_Funds_Details
    /// </summary>
    public interface IT_Funds_DetailsService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<T_Funds_DetailsEntity> GetList(string queryJson);
        IEnumerable<T_Funds_DetailsViewModel> GetPieDataList(string queryJson);
        IEnumerable<T_PartnerCapitalPoolViewModel> GetLineDataList(string queryJson);
        
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        T_Funds_DetailsEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取资金池数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        T_Funds_DetailsEntity GetFundStaticJson(string queryJson);
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
        void SaveForm(string keyValue, T_Funds_DetailsEntity entity);

        int DeleteByData(DateTime time);

        void BacthInsert(List<T_Funds_DetailsEntity> entityList);
        #endregion

    }
}