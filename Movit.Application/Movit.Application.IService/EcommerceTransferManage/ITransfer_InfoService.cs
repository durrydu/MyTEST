using Movit.Application.Entity.EcommerceTransferManage;
using Movit.Application.Entity.EcommerceTransferManage.ViewModel;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.IService.EcommerceTransferManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-06-25 14:48
    /// 描 述：T_Transfer_Info
    /// </summary>
    public interface ITransfer_InfoService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<Transfer_InfoEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Transfer_InfoEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Transfer_Project_EcommerceView GetProEcomJson(string keyValue);
        /// <summary>
        /// 获取首页分页
        /// </summary>
        /// <returns></returns>
        IEnumerable<Transfer_Project_EcommerceView> GetPageListJson(string keyValue, Pagination pagination, string queryJson);

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void DeleteRemark(string keyValue, string queryJson, string ProjectID, string Transfer_Money, string EcommerceID);
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, Transfer_InfoEntity entity, string ProjectID, string EcommerceID, string ActualControlTotalAmount, string Transfer_Code, out string errMsg);
        #endregion
    }
}
