using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.IService.EcommerceContractManage;
using Movit.Data.Repository;
using Movit.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movit.Util;

namespace Movit.Application.Service.EcommerceContractManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：Emily
    /// 日 期：2018-06-19 14:43
    /// 描 述：T_EcommerceDiscountProgram
    /// </summary>
    public class EcommerceDiscountProgramService : RepositoryFactory<EcommerceDiscountProgramEntity>, IEcommerceDiscountProgramService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EcommerceDiscountProgramEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<EcommerceDiscountProgramEntity>();
            expression = expression.And(t => t.DeleteMark == 0);
            var queryParam = queryJson.ToJObject();
            string EcommerceProjectRelationID = queryParam["EcommerceProjectRelationID"].ToString();
            expression = expression.And(t => t.EcommerceProjectRelationID == EcommerceProjectRelationID);
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<EcommerceDiscountProgramEntity> GetEntity(string keyValue)
        {
            var expression = LinqExtensions.True<EcommerceDiscountProgramEntity>();
            expression = expression.And(t => t.EcommerceProjectRelationID == keyValue
                && t.DeleteMark == 0);

            return this.BaseRepository().IQueryable(expression).ToList();

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
        public void SaveForm(string keyValue, EcommerceDiscountProgramEntity entity)
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
