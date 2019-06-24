using Movit.Application.Entity;
using Movit.Application.IService;
using Movit.Data.Repository;
using Movit.Util.Extension;
using Movit.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using Movit.Util;
using System.Data.Common;
using Movit.Data;

namespace Movit.Application.Service
{
    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-06-19 14:34
    /// 描 述：EcommerceGroup
    /// </summary>
    public class EcommerceGroupService : RepositoryFactory<EcommerceGroupEntity>, IEcommerceGroupService
    {
        #region 获取数据
        ///<summary>
        ///电商公司列表
        ///</summary>
        ///<param name="pagination">分页参数</param>
        ///<param name="queryJson">查询参数</param>
        public IEnumerable<EcommerceGroupEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<EcommerceGroupEntity>();
            var queryParam = queryJson.ToJObject();
            expression = expression.And(t => t.DeleteMark == 0);
            if (!queryParam["EcommerceGroupName"].IsEmpty())
            {
                string ecommerceGroupName = queryParam["EcommerceGroupName"].ToString();
                expression = expression.And(t => t.EcommerceGroupName.Contains(ecommerceGroupName));
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EcommerceGroupEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EcommerceGroupEntity GetEntity(string keyValue)
        {
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@EcommerceGroupID", keyValue));
            return this.BaseRepository().FindEntity("select * from T_EcommerceGroup where EcommerceGroupID=@EcommerceGroupID", parameter.ToArray());
        }
        ///<summary>
        ///获取电商集团公司列表
        ///</summary>>
        public IEnumerable<EcommerceGroupEntity> GetEcommerceGroupName(string queryJson)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select * from T_EcommerceGroup where 1=1 and  DeleteMark=0");
            var queryParam = queryJson.ToJObject();
            if (!queryParam["EcommerceGroupID"].IsEmpty())
            {
                string ecommerceGroupid= queryParam["EcommerceGroupID"].ToString();
                sqlstr.AppendFormat(@" and EcommerceGroupID='{0}'", ecommerceGroupid);
            }
            return this.BaseRepository().FindList(sqlstr.ToString());
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 电商列表不能重复
        /// </summary>
        /// <param name="EcommerceGroupName">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEcommerceGroupName(string EcommerceGroupName, string keyValue)
        {
            var expression = LinqExtensions.True<EcommerceGroupEntity>();
            expression = expression.And(t => t.EcommerceGroupName == EcommerceGroupName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.EcommerceGroupID != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
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
                sqlstr.Append(@"update T_EcommerceGroup set DeleteMark='1' where EcommerceGroupID ='" + keyValue + "'");
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
        public void SaveForm(string keyValue, EcommerceGroupEntity entity)
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