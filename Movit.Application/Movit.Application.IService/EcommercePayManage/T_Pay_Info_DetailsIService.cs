using Movit.Application.Entity;
using Movit.Util.WebControl;
using System.Collections.Generic;

namespace Movit.Application.IService
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：姚栋
    /// 日 期：2018-06-25 19:32
    /// 描 述：T_Pay_Info_Details
    /// </summary>
    public interface T_Pay_Info_DetailsIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<T_Pay_Info_DetailsEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        T_Pay_Info_DetailsEntity GetEntity(string keyValue);

        #region 共享接口
        /// <summary>
        /// 描述：通过付款单编号获取信息
        /// 作者:姚栋
        /// 日期:20180625
        /// </summary>
        /// <param name="pay_info_code"></param>
        /// <returns></returns>
        IEnumerable<T_Pay_Info_DetailsEntity> GetBillingDetails(string pay_info_code);
        #endregion
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
        void SaveForm(string keyValue, T_Pay_Info_DetailsEntity entity);
        #endregion
    }
}