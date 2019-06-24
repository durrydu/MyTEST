using Movit.Application.Entity;
using Movit.Util.WebControl;
using System.Collections.Generic;

namespace Movit.Application.IService
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-06-19 14:34
    /// 描 述：EcommerceGroup
    /// </summary>
    public interface IEcommerceGroupService
    {
        #region 获取数据
        ///<summary>
        ///电商公司(简称)列表
        ///</summary>
        ///<param name="pagination">分页参数</param>
        ///<param name="queryJson">查询参数</param>
        IEnumerable<EcommerceGroupEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<EcommerceGroupEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        EcommerceGroupEntity GetEntity(string keyValue);
        ///<summary>
        ///获取电商集团公司列表
        ///</summary>>
        IEnumerable<EcommerceGroupEntity> GetEcommerceGroupName(string queryJson);
        #endregion

        #region 验证数据
        /// <summary>
        /// 电商列表不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistEcommerceGroupName(string EcommerceGroupName, string keyValue);
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
        void SaveForm(string keyValue, EcommerceGroupEntity entity);
        #endregion
    }
}