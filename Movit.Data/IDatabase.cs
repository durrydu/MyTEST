using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Movit.Data
{
    /// <summary>
    /// 描 述：操作数据库接口
    /// </summary>
    public interface IDatabase
    {
        //Allen Test
        IDatabase BeginTrans();
        int Commit();
        void Rollback();
        void Close();
        int ExecuteBySql(string strSql);
        int ExecuteBySql(string strSql, params DbParameter[] dbParameter);
        int ExecuteByProc(string procName);
        int ExecuteByProc(string procName, DbParameter[] dbParameter);
        int Insert<T>(T entity) where T : class,new();
        int Insert<T>(IEnumerable<T> entities) where T : class,new();
        int Delete<T>() where T : class,new();
        int Delete<T>(T entity) where T : class,new();
        int Delete<T>(IEnumerable<T> entities) where T : class,new();
        int Delete<T>(Expression<Func<T, bool>> condition) where T : class,new();
        int Delete<T>(object KeyValue) where T : class,new();
        int Delete<T>(object[] KeyValue) where T : class,new();
        int Delete<T>(object propertyValue, string propertyName) where T : class,new();
        int Update<T>(T entity) where T : class,new();
        int Update<T>(IEnumerable<T> entities) where T : class,new();
        int Update<T>(Expression<Func<T, bool>> condition) where T : class,new();

        int Update<T>(IEnumerable<T> entities, Expression<Func<T, bool>> UpdateColumns, bool NullUpdate = true) where T : class,new();

        object FindObject(string strSql);
        object FindObject(string strSql, DbParameter[] dbParameter);
        T FindEntity<T>(object KeyValue) where T : class,new();

        T FindEntity<T>(string strSql) where T : class,new();
        T FindEntity<T>(string strSql, DbParameter[] dbParameter) where T : class,new();
        T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class,new();
        IQueryable<T> IQueryable<T>() where T : class,new();
        IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class,new();
        IEnumerable<T> FindList<T>() where T : class,new();
        IEnumerable<T> FindList<T>(Func<T, object> orderby) where T : class,new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class,new();
        IEnumerable<T> FindList<T>(string strSql) where T : class,new();
        IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter) where T : class,new();
        IEnumerable<T> FindList<T>(string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new();
        IEnumerable<T> FindList<T>(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new();
        IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new();

        DataTable FindTable(string strSql);
        DataTable FindTable(string strSql, DbParameter[] dbParameter);
        DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total);
        DataTable FindTable(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total);
    }
}
