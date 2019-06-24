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
    /// 创 建：姚栋
    /// 日 期：2018-06-25 19:32
    /// 描 述：T_Pay_Info_Details
    /// </summary>
    public class T_Pay_Info_DetailsService : RepositoryFactory, T_Pay_Info_DetailsIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<T_Pay_Info_DetailsEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable<T_Pay_Info_DetailsEntity>().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public T_Pay_Info_DetailsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<T_Pay_Info_DetailsEntity>(keyValue);
        }

        /// <summary>
        /// 根据流水编号获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public T_Pay_Info_DetailsEntity GetEntityByCode(string Code)
        {
            var expression = LinqExtensions.True<T_Pay_Info_DetailsEntity>();
            expression = expression.And(t => t.PayInfoDetailsCode == Code);

            return this.BaseRepository().FindEntity<T_Pay_Info_DetailsEntity>(expression);
        }
        #region 共享接口
        /// <summary>
        /// 描述：通过付款单编号获取信息
        /// 作者:姚栋
        /// 日期:20180625
        /// </summary>
        /// <param name="pay_info_code"></param>
        /// <returns></returns>
        public IEnumerable<T_Pay_Info_DetailsEntity> GetBillingDetails(string pay_info_code)
        {
            var expression = LinqExtensions.True<T_Pay_Info_DetailsEntity>();
            expression = expression.And(t => t.Pay_Info_Code == pay_info_code);
            return this.BaseRepository().FindList<T_Pay_Info_DetailsEntity>(expression);


        }
        #endregion
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
                this.BaseRepository().Delete<T_Pay_Info_DetailsEntity>(keyValue);
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
        public void SaveForm(string keyValue, T_Pay_Info_DetailsEntity entity)
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
        #endregion
    }
}