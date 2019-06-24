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
    /// 日 期：2018-06-19 14:34
    /// 描 述：EcommerceGroup
    /// </summary>
    public class EcommerceGroupBLL
    {
        private IEcommerceGroupService service = new EcommerceGroupService();

        #region 获取数据
        ///<summary>
        ///电商公司(简称)列表
        ///</summary>
        ///<param name="pagination">分页参数</param>
        ///<param name="queryJson">查询参数</param>
        public IEnumerable<EcommerceGroupEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EcommerceGroupEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EcommerceGroupEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        ///<summary>
        ///获取电商集团公司列表
        ///</summary>>
        public IEnumerable<EcommerceGroupEntity> GetEcommerceGroupName(string queryJson)
        {
            return service.GetEcommerceGroupName(queryJson);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 电商集团不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEcommerceGroupName(string EcommerceGroupName, string keyValue)
        {
            return service.ExistEcommerceGroupName(EcommerceGroupName, keyValue);
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
        public void SaveForm(string keyValue, EcommerceGroupEntity entity)
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
    }
}