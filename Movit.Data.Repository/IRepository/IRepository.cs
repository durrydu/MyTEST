﻿using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Movit.Data.Repository
{
    /// <summary>
    /// 描 述：定义仓储模型中的数据标准操作接口
    /// </summary>
    public interface IRepository
    {
        IRepository BeginTrans();
        void Commit();
        void Rollback();

        int ExecuteBySql(string strSql);
        int ExecuteBySql(string strSql, params DbParameter[] dbParameter);
        int ExecuteByProc(string procName);
        int ExecuteByProc(string procName, params DbParameter[] dbParameter);

        int Insert<T>(T entity) where T : class,new();
        int Insert<T>(List<T> entity) where T : class, new();
        int Delete<T>() where T : class, new();
        int Delete<T>(T entity) where T : class, new();
        int Delete<T>(List<T> entity) where T : class, new();
        int Delete<T>(Expression<Func<T, bool>> condition) where T : class,new();
        int Delete<T>(object keyValue) where T : class, new();
        int Delete<T>(object[] keyValue) where T : class, new();
        int Delete<T>(object propertyValue, string propertyName) where T : class, new();
        int Update<T>(T entity) where T : class, new();
        int Update<T>(List<T> entity) where T : class, new();
        int Update<T>(Expression<Func<T, bool>> condition) where T : class,new();
        int Update<T>(IEnumerable<T> entities, Expression<Func<T, bool>> UpdateColumns, bool NullUpdate = true) where T : class,new();
        T FindEntity<T>(object keyValue) where T : class, new();
        T FindEntity<T>(string strSql, DbParameter[] dbParameter) where T : class,new();
        T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class,new();
        IQueryable<T> IQueryable<T>() where T : class,new();
        IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class,new();
        IEnumerable<T> FindList<T>(string strSql) where T : class, new();
        IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter) where T : class, new();
        IEnumerable<T> FindList<T>(Pagination pagination) where T : class,new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Pagination pagination) where T : class,new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class,new();
        IEnumerable<T> FindList<T>(string strSql, Pagination pagination) where T : class, new();
        IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter, Pagination pagination) where T : class, new();

        DataTable FindTable(string strSql);
        DataTable FindTable(string strSql, DbParameter[] dbParameter);
        DataTable FindTable(string strSql, Pagination pagination);
        DataTable FindTable(string strSql, DbParameter[] dbParameter, Pagination pagination);
        object FindObject(string strSql);
        object FindObject(string strSql, DbParameter[] dbParameter);
    }
}
