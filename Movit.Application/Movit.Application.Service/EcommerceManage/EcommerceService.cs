using Movit.Application.Entity;
using Movit.Application.IService;
using Movit.Data.Repository;
using Movit.Util.Extension;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using Movit.Util;
using System.Data;
using System.Text;
using Movit.Data.SQLSugar;
using System.Data.Common;
using Movit.Data;

namespace Movit.Application.Service
{
    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-06-19 10:50
    /// 描 述：Ecommerce
    /// </summary>
    public class EcommerceService : RepositoryFactory<EcommerceEntity>, IEcommerceService
    {
        #region 获取数据
        ///<summary>
        ///电商公司列表
        ///</summary>
        ///<param name="pagination">分页参数</param>
        ///<param name="queryJson">查询参数</param>
        public IEnumerable<EcommerceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sqlstr = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            sqlstr.Append(@"select tec.EcommerceID,tecgroup.EcommerceGroupName,tec.EcommerceGroupID,tec.EcommerceName,
                            tec.PlatformRate,tec.EcommerceType,tec.AgentUserID  from T_Ecommerce tec
                            inner join T_EcommerceGroup tecgroup
                            on tec.EcommerceGroupID=tecgroup.EcommerceGroupID where 1=1 
                            and tec.Deletemark=0");
            if (!queryParam["EcommerceGroupID"].IsEmpty())
            {
                sqlstr.Append(" and tec.EcommerceGroupID=@ecommerceGroupID");
                parameter.Add(DbParameters.CreateDbParameter("@ecommerceGroupID", queryParam["EcommerceGroupID"].ToString()));
            }
            if (!queryParam["EcommerceName"].IsEmpty())
            {
                sqlstr.Append(" and tec.EcommerceName like @EcommerceName");
                parameter.Add(DbParameters.CreateDbParameter("@EcommerceName", "%"+queryParam["EcommerceName"].ToString()+"%"));
            }
            return this.BaseRepository().FindList(sqlstr.ToString(), parameter.ToArray(),pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EcommerceEntity> GetList(string queryJson)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select * from T_Ecommerce");
            return this.BaseRepository().FindList(sqlstr.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EcommerceEntity GetEntity(string keyValue)
        {
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@EcommerceID", keyValue));
            return this.BaseRepository().FindEntity(@"SELECT ecom.[EcommerceID]
                          ,ecom.[T_E_EcommerceGroupID],
                          ecomgroup.EcommerceGroupName
                          ,ecom.[EcommerceName]
                          ,ecom.[PlatformRate]
                          ,ecom.[EcommerceType]
                          ,ecom.[AgentUserID]
                          ,ecom.[CooperateStartTime]
                          ,ecom.[CooperateEndTime]
                          ,ecom.[DeleteMark]
                          ,ecom.[CreateDate]
                          ,ecom.[CreateUserId]
                          ,ecom.[CreateUserName]
                          ,ecom.[ModifyDate]
                          ,ecom.[ModifyUserId]
                          ,ecom.[ModifyUserName]
                          ,ecom.[ApprovalState]
                          ,ecom.[EcommerceGroupID]
                          ,ecom.[EcommerceCode]
                      FROM [E_Commerce_DB].[dbo].[T_Ecommerce] ecom
                      inner join T_EcommerceGroup  ecomgroup on ecom.EcommerceGroupID
                      =ecomgroup.EcommerceGroupID where EcommerceID=@EcommerceID", parameter.ToArray());
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
                StringBuilder sqlstr = new StringBuilder();
                sqlstr.Append(@"update T_Ecommerce set DeleteMark='1' where EcommerceID ='" + keyValue + "'");
                this.BaseRepository().ExecuteBySql(sqlstr.ToString());
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

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    new SqlDatabase("BaseDb").Connection.Updateable(entity).IgnoreColumns(it => it == "AgentUserID" || it == "DeleteMark" || it == "CreateDate" || it == "CreateUserId" || it == "CreateUserName").ExecuteCommand();
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

        #region 验证数据
        public bool ExistEcommerceName(string EcommerceName, string keyValue)
        {
            var expression = LinqExtensions.True<EcommerceEntity>();
            expression = expression.And(t => t.EcommerceName == EcommerceName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.EcommerceID != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion
    }
}