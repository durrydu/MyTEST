using Movit.Application.Entity;
using Movit.Application.IService;
using Movit.Application.Service;
using Movit.Util.WebControl;
using System.Collections.Generic;
using System;

namespace Movit.Application.Busines
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-23 20:23
    /// 描 述：OP_Parcel
    /// </summary>
    public class OP_ParcelBLL
    {
        private OP_ParcelIService service = new OP_ParcelService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<OP_ParcelEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OP_ParcelEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public OP_ParcelEntity GetEntityByproid(string proid)
        {
            return service.GetEntityByproid(proid);
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
        public void SaveForm(string keyValue, OP_ParcelEntity entity)
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
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entityList">实体对象</param>
        /// <returns></returns>
        public void BatchInsert(List<OP_ParcelEntity> entityList)
        {
            try
            {
                service.BatchInsert(entityList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entityList">实体对象</param>
        /// <returns></returns>
        public void BatchUpdate(List<OP_ParcelEntity> entityList)
        {
            try
            {
                service.BatchUpdate(entityList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}