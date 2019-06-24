using Movit.Application.Entity;
using Movit.Util.WebControl;
using System.Collections.Generic;

namespace Movit.Application.IService
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：durry
    /// 日 期：2018-06-22 09:54
    /// 描 述：Pay_Info
    /// </summary>
    public interface IPay_InfoService
    {
        #region 获取数据
        ///<summary>
        ///作者：杜强
        ///Time:2018-06-22 13:40
        ///获取分页列表
        IEnumerable<Pay_InfoEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<Pay_InfoEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Pay_InfoEntity GetEntity(string keyValue);
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
        void SaveForm(string keyValue, Pay_InfoEntity entity);
        /// <summary>
        /// 共享平台推送付款单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void Paymentuse(string keyValue, Pay_InfoEntity entity);
        #endregion

    }
}