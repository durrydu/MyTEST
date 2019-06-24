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
    /// 创 建：durry.du
    /// 日 期：2018-06-19 10:50
    /// 描 述：Ecommerce
    /// </summary>
    public class EcommerceBLL
    {
        private IEcommerceService service = new EcommerceService();

        #region 获取数据
        ///<summary>
        ///电商公司列表
        ///</summary>
        ///<param name="pagination">分页参数</param>
        ///<param name="queryJson">查询参数</param>
        public IEnumerable<EcommerceEntity> GetPageList(Pagination pagination, string queryJson) 
        {
            return service.GetPageList(pagination,queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EcommerceEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EcommerceEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
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
        public void SaveForm(string keyValue, EcommerceEntity entity)
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

        #region 验证数据
        public bool ExistEcommerceName(string EcommerceName, string keyValue)
        {
            return service.ExistEcommerceName(EcommerceName, keyValue);
        }
        #endregion
    }
}