using Movit.Application.Code;
using Movit.Application.Entity.AuthorizeManage;
using Movit.Application.Entity.AuthorizeManage.ViewModel;
using Movit.Application.Entity.BaseManage;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.IService.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2018-2016  
    /// 创建人： 姚栋
    /// 日 期：20180709
    /// 描 述：授权认证
    /// </summary>
    public interface IAuthorizeService<T>
    {
        IQueryable<T> IQueryable(string ProjectIdName = "ProjectId");
        IQueryable<T> IQueryable(Expression<Func<T, bool>> condition, string ProjectIdName = "ProjectId");
        IEnumerable<T> FindList(Pagination pagination, string ProjectIdName = "ProjectId");
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination, string ProjectIdName = "ProjectId");
        IEnumerable<T> FindList(string strSql, string authorizeKeyName = "projectid");
        IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, string authorizeKeyName = "projectid");
        IEnumerable<T> FindList(string strSql, Pagination pagination, string authorizeKeyName = "projectid");
        IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, Pagination pagination, string authorizeKeyName = "projectid");
        IEnumerable<T> FindList(string strSql, AuthorizeUserTypeEnum authorizeUserType,
           SystemTypeEnum systemType,
           string loginKey = null, Pagination pagination = null, string authorizeKeyName = "projectid");
        IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter,
           AuthorizeUserTypeEnum authorizeUserType,
           SystemTypeEnum systemType,
           string loginKey = null, Pagination pagination = null, string authorizeKeyName = "projectid");
        bool LookAll(AuthorizeUserTypeEnum authorizeUserType,
         SystemTypeEnum systemType,
        string loginKey = null);
        bool LookAll();
        string GetReadProjectId();
    }
}
