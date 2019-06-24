using Movit.Application.Code;
using Movit.Application.Entity.AuthorizeManage;
using Movit.Application.Entity.BaseManage;
using Movit.Application.IService.AuthorizeManage;
using Movit.Data;
using Movit.Data.Repository;
using Movit.Util.Extension;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Service.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2018-2016  
    /// 创建人：ivan.yao
    /// 日 期：2016.03.29 22:35
    /// 描 述：授权认证
    /// </summary>
    public class AuthorizeService<T> : RepositoryFactory<T>, IAuthorizeService<T> where T : class,new()
    {
        private IRepository db = new RepositoryFactory().BaseRepository();
        private AuthorizeService authorizeService = new AuthorizeService();
        #region 带权限的数据源查询
        //public IQueryable<T> IQueryable()
        //{
        //    if (GetReadUserId() == "")
        //    {
        //        return this.BaseRepository().IQueryable();
        //    }
        //    else
        //    {
        //        var parameter = Expression.Parameter(typeof(T), "t");
        //        var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
        //        var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
        //        return this.BaseRepository().IQueryable(lambda);
        //    }
        //}
        public IQueryable<T> IQueryable(string authorizeKeyName = "ProjectId")
        {
            if (LookAll())
            {
                return this.BaseRepository().IQueryable();
            }
            else
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadProjectId()).Call("Contains", parameter.Property(authorizeKeyName));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                return this.BaseRepository().IQueryable(lambda);
            }


        }
        //public IQueryable<T> IQueryable(Expression<Func<T, bool>> condition)
        //{
        //    if (GetReadUserId() != "")
        //    {
        //        var parameter = Expression.Parameter(typeof(T), "t");
        //        var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
        //        var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
        //        condition = condition.And(lambda);
        //    }
        //    return db.IQueryable<T>(condition);
        //}

        public IQueryable<T> IQueryable(Expression<Func<T, bool>> condition, string authorizeKeyName = "ProjectId")
        {
            if (LookAll())
            {
                return this.BaseRepository().IQueryable(condition);
            }
            else
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadProjectId()).Call("Contains", parameter.Property(authorizeKeyName));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                condition = condition.And(lambda);
                return this.BaseRepository().IQueryable(condition);
            }

        }

        //public IEnumerable<T> FindList(Pagination pagination)
        //{
        //    if (GetReadUserId() == "")
        //    {
        //        return this.BaseRepository().FindList(pagination);
        //    }
        //    else
        //    {
        //        var parameter = Expression.Parameter(typeof(T), "t");
        //        var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
        //        var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
        //        return this.BaseRepository().FindList(lambda, pagination);
        //    }
        //}

        public IEnumerable<T> FindList(Pagination pagination, string authorizeKeyName = "ProjectId")
        {
            if (LookAll())
            {
                return this.BaseRepository().FindList(pagination);
            }
            else
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadProjectId()).Call("Contains", parameter.Property(authorizeKeyName));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                return this.BaseRepository().FindList(lambda, pagination);
            }
        }
        //public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination)
        //{
        //    if (GetReadUserId() != "")
        //    {
        //        var parameter = Expression.Parameter(typeof(T), "t");
        //        var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
        //        var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
        //        condition = condition.And(lambda);
        //    }
        //    return this.BaseRepository().FindList(condition, pagination);
        //}

        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination, string authorizeKeyName = "ProjectId")
        {
            if (LookAll())
            {
                return this.BaseRepository().FindList(condition, pagination);
            }
            else
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadProjectId()).Call("Contains", parameter.Property(authorizeKeyName));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                condition = condition.And(lambda);
            }
            return this.BaseRepository().FindList(condition, pagination);
        }
        #region sql 按照项目过滤权限
        public IEnumerable<T> FindList(string strSql, string authorizeKeyName = "projectid")
        {
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                if (!LookAll())
                {
                    strSql = string.Format(@"select *from ({0}) pinfo
                                    inner join 
                                    (
                                    SELECT ItemId,UserId FROM view_post_project 
                                    where UserId='{1}'
                                   ) as post_project
                                    on pinfo." + authorizeKeyName + "= post_project.ItemId", strSql, SystemInfo.CurrentUserId);
                }
            }


            return this.BaseRepository().FindList(strSql);
        }
        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, string authorizeKeyName = "projectid")
        {

            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                if (!LookAll())
                {
                    strSql = string.Format(@"select *from ({0}) pinfo
                                    inner join 
                                    (
                                    SELECT ItemId,UserId FROM view_post_project 
                                    where UserId='{1}'
                                   ) as post_project
                                    on pinfo." + authorizeKeyName + "= post_project.ItemId", strSql, SystemInfo.CurrentUserId);
                }
            }
            return this.BaseRepository().FindList(strSql, dbParameter);
        }
        /// <summary>
        /// 检查是否有全部权限 
        /// </summary>
        /// <returns></returns>
        public bool LookAll()
        {
            object _obj = null;
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                var parameter = new List<DbParameter>();
                string strSql = string.Format(@"
                                    SELECT * from view_post_user 
                                    where UserId='{0}'  and AuthorizationMethod={1}", SystemInfo.CurrentUserId, (int)AuthorizationMethodEnum.AllPorject);
                _obj = this.BaseRepository().FindEntity(strSql, parameter.ToArray());
            }
            else
            {

                return true;
            }
            return _obj == null ? false : true;
        }
        public IEnumerable<T> FindList(string strSql, Pagination pagination, string authorizeKeyName = "projectid")
        {
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                if (!LookAll())
                {
                    strSql = string.Format(@"select *from ({0}) pinfo
                                    inner join 
                                    (
                                    SELECT ItemId,UserId FROM view_post_project 
                                    where UserId='{1}'
                                   ) as post_project
                                    on pinfo." + authorizeKeyName + "= post_project.ItemId", strSql, SystemInfo.CurrentUserId);
                }
            }
            return this.BaseRepository().FindList(strSql, pagination);
        }
        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, Pagination pagination, string authorizeKeyName = "projectid")
        {
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                if (!LookAll())
                {
                    strSql = string.Format(@"select *from ({0}) pinfo
                                    inner join 
                                    (
                                    SELECT ItemId,UserId FROM view_post_project 
                                    where UserId='{1}'
                                   ) as post_project
                                    on pinfo." + authorizeKeyName + "= post_project.ItemId", strSql, SystemInfo.CurrentUserId);
                }
            }
            return this.BaseRepository().FindList(strSql, dbParameter, pagination);
        }
        #endregion
        #region sql 按照项目过滤权限 扩展方法,基本是给API 使用的
        public IEnumerable<T> FindList(string strSql, AuthorizeUserTypeEnum authorizeUserType,
            SystemTypeEnum systemType,
            string loginKey = null, Pagination pagination = null, string authorizeKeyName = "projectid")
        {

            StringBuilder sqlString = new StringBuilder();
            if (systemType == SystemTypeEnum.WebSystem && OperatorProvider.Provider.Current().IsSystem)
            {

                sqlString.Append(strSql);
            }
            else
            {
                if (!LookAll(authorizeUserType, systemType, loginKey))
                {
                    if (authorizeUserType == AuthorizeUserTypeEnum.UserID && string.IsNullOrEmpty(loginKey))
                    {
                        loginKey = SystemInfo.CurrentUserId;
                    }
                    sqlString.AppendLine(string.Format(@"select *from ({0}) pinfo
                                    inner join 
                                    (
                                    SELECT ItemId,UserId FROM view_post_project 
                                    where 1=1 ", sqlString));
                    sqlString.AppendLine(authorizeUserType == AuthorizeUserTypeEnum.UserID ?
                        string.Format(" and UserId='{0}'", loginKey) : string.Format(" and Account='{0}'", loginKey));
                    sqlString.AppendLine(@" ) as post_project
                                    on pinfo." + authorizeKeyName + "= post_project.ItemId");

                }
                else
                {
                    sqlString.Append(strSql);
                }
            }
            return pagination == null ? this.BaseRepository().FindList(sqlString.ToString())
              : this.BaseRepository().FindList(sqlString.ToString(), pagination);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <param name="authorizeUserType">授权验证的方式</param>
        /// <param name="systemType">请求系统类型</param>
        /// <param name="loginKey">用户ID 或者 登录名</param>
        /// <returns></returns>
        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter,
            AuthorizeUserTypeEnum authorizeUserType,
            SystemTypeEnum systemType,
            string loginKey = null, Pagination pagination = null, string authorizeKeyName = "projectid")
        {

            StringBuilder sqlString = new StringBuilder();
            if (systemType == SystemTypeEnum.WebSystem && OperatorProvider.Provider.Current().IsSystem)
            {

                sqlString.Append(strSql);
            }
            else
            {
                if (!LookAll(authorizeUserType, systemType, loginKey))
                {
                    if (authorizeUserType == AuthorizeUserTypeEnum.UserID && string.IsNullOrEmpty(loginKey))
                    {
                        loginKey = SystemInfo.CurrentUserId;
                    }
                    sqlString.AppendLine(string.Format(@"select *from ({0}) pinfo
                                    inner join 
                                    (
                                    SELECT ItemId,UserId FROM view_post_project 
                                    where 1=1 ", strSql));
                    sqlString.AppendLine(authorizeUserType == AuthorizeUserTypeEnum.UserID ?
                        string.Format(" and UserId='{0}'", loginKey) : string.Format(" and Account='{0}'", loginKey));
                    sqlString.AppendLine(@" ) as post_project
                                    on pinfo." + authorizeKeyName + "= post_project.ItemId");

                }
                else
                {
                    sqlString.Append(strSql);
                }
            }

            return pagination == null ? this.BaseRepository().FindList(sqlString.ToString(), dbParameter)
                : this.BaseRepository().FindList(sqlString.ToString(), dbParameter, pagination);
        }
        /// <summary>
        /// 检查是否有全部权限 
        /// </summary>
        /// <returns></returns>
        public bool LookAll(AuthorizeUserTypeEnum authorizeUserType,
             SystemTypeEnum systemType,
            string loginKey = null)
        {
            object _obj = null;
            StringBuilder sqlString = new StringBuilder();
            if (systemType == SystemTypeEnum.WebSystem && OperatorProvider.Provider.Current().IsSystem)
            {

                _obj = true;
            }
            else
            {
                if (authorizeUserType == AuthorizeUserTypeEnum.UserID && string.IsNullOrEmpty(loginKey))
                {
                    loginKey = SystemInfo.CurrentUserId;
                }
                var parameter = new List<DbParameter>();
                sqlString.AppendLine(@"
                                    SELECT * from view_post_user 
                                    where 1=1");
                sqlString.AppendLine(authorizeUserType == AuthorizeUserTypeEnum.UserID ?
                        string.Format(" and UserId='{0}' and AuthorizationMethod={1}", loginKey, (int)AuthorizationMethodEnum.AllPorject) :
                        string.Format(" and Account='{0}' and AuthorizationMethod={1}", loginKey, (int)AuthorizationMethodEnum.AllPorject));
                _obj = this.BaseRepository().FindEntity(sqlString.ToString(), parameter.ToArray());
            }
            return _obj == null ? false : true;
        }


        #endregion
        //public IEnumerable<T> FindList(string strSql)
        //{
        //    strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
        //    return this.BaseRepository().FindList(strSql);
        //}
        //public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter)
        //{
        //    strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
        //    return this.BaseRepository().FindList(strSql, dbParameter);
        //}
        //public IEnumerable<T> FindList(string strSql, Pagination pagination)
        //{
        //    strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
        //    return this.BaseRepository().FindList(strSql, pagination);
        //}
        //public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, Pagination pagination)
        //{
        //    strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
        //    return this.BaseRepository().FindList(strSql, dbParameter, pagination);
        //}
        #endregion

        #region 取数据权限用户
        private string GetReadUserId()
        {
            return OperatorProvider.Provider.Current().DataAuthorize.ReadAutorizeUserId;
        }
        private string GetReadSql()
        {
            return OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize;
        }
        public string GetReadProjectId()
        {
            return OperatorProvider.Provider.Current().DataAuthorize.GetReadProjectId;


        }
        #endregion


    }
}
