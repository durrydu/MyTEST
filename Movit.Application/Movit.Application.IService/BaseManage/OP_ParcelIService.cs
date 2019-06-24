using Movit.Application.Entity;
using Movit.Util.WebControl;
using System.Collections.Generic;

namespace Movit.Application.IService
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-23 20:23
    /// 描 述：OP_Parcel
    /// </summary>
    public interface OP_ParcelIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<OP_ParcelEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        OP_ParcelEntity GetEntity(string keyValue);
        OP_ParcelEntity GetEntityByproid(string proid);
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
        void SaveForm(string keyValue, OP_ParcelEntity entity);
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entityList">实体对象List</param>
        /// <returns></returns>
        void BatchInsert(List<OP_ParcelEntity> entityList);
        /// <summary>
        /// 批量更更新
        /// </summary>
        /// <param name="entityList">实体对象List</param>
        /// <returns></returns>
        void BatchUpdate(List<OP_ParcelEntity> entityList);
        #endregion

        
    }
}