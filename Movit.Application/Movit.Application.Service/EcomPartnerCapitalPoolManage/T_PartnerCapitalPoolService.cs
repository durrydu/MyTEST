using Movit.Application.Entity.EcomPartnerCapitalPoolManage;
using Movit.Application.IService.EcomPartnerCapitalPoolManage;
using Movit.Data.Repository;
using Movit.Data.SQLSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Service.EcomPartnerCapitalPoolManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-19 15:10
    /// 描 述：T_PartnerCapitalPool
    /// </summary>
    public class T_PartnerCapitalPoolService : RepositoryFactory, T_PartnerCapitalPoolIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<T_PartnerCapitalPoolEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable<T_PartnerCapitalPoolEntity>().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public T_PartnerCapitalPoolEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<T_PartnerCapitalPoolEntity>(keyValue);
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
                this.BaseRepository().Delete<T_PartnerCapitalPoolEntity>(keyValue);
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
        public void SaveForm(string keyValue, T_PartnerCapitalPoolEntity entity)
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
        //插入数据库数组
        public void InsertEntityList(List<T_PartnerCapitalPoolEntity> pCaPoolList) {
            this.BaseRepository().Insert(pCaPoolList);

        }
        public void UpdateEntityList(List<T_PartnerCapitalPoolEntity> pCaPoolList)
        {
            new SqlDatabase("BaseDb").Connection.Updateable(pCaPoolList).UpdateColumns(p => new { p.DeleteMark})
               .ExecuteCommand();

        }
       
        public IEnumerable<T_PartnerCapitalPoolEntity> getPaCaPoolList(string keyValue)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select * from T_PartnerCapitalPool where ObjectID='" + keyValue+"'");
            return  this.BaseRepository().FindList<T_PartnerCapitalPoolEntity>(sqlStr.ToString());
         
        }
        public IEnumerable<T_PartnerCapitalPoolEntity> getAllPaCaPoolList()
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select * from T_PartnerCapitalPool");
            return this.BaseRepository().FindList<T_PartnerCapitalPoolEntity>(sqlStr.ToString());

        }
        #endregion
    }
}
