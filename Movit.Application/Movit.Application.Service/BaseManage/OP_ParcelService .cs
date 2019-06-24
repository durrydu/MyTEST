using Movit.Application.Entity;
using Movit.Application.IService;
using Movit.Data.Repository;
using Movit.Util.Extension;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movit.Application.Service
{
    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-23 20:23
    /// 描 述：OP_Parcel
    /// </summary>
    public class OP_ParcelService : RepositoryFactory<OP_ParcelEntity>, OP_ParcelIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<OP_ParcelEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OP_ParcelEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        public OP_ParcelEntity GetEntityByproid(string proid)
        {
            var expression = LinqExtensions.True<OP_ParcelEntity>();
            expression = expression.And(t => t.ProjectID == proid);
            return this.BaseRepository().FindEntity(expression);
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
                this.BaseRepository().Delete(keyValue);
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
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
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
                this.BaseRepository().Insert(entityList);
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
                this.BaseRepository().Update(entityList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}