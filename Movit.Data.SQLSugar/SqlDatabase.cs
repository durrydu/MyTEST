using SqlSugar;
using Movit.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Movit.Util.Ioc;
using Microsoft.Practices.Unity;
using Movit.Data;
using System.Linq;
using Movit.Util.Extension;
namespace Movit.Data.SQLSugar
{
    /// <summary>
    /// 描 述：操作数据库
    /// </summary>
    public class SqlDatabase : IDatabase
    {
        #region 构造函数
        /// <summary>
        /// 构造方法
        /// </summary>
        ///// <summary>
        ///// 构造方法
        ///// </summary>
        public SqlDatabase(string connString)
        {
            DbHelper.DbType = DatabaseType.SqlServer;
            connectionString = Config.GetConnectionValue(connString);
        }
        #endregion

        #region 属性


        /// <summary>
        /// 获取 数据库连接串
        /// </summary>
        public string connectionString { get; set; }
        public SqlSugarClient Connection
        {
            get
            {
                SqlSugarClient sqlsugar = new SqlSugarClient(new ConnectionConfig
                {
                    ConnectionString = connectionString,// "Server=.;User ID=sa;Password=1qaz2wsx~;database=CCM_Base",//必填, 数据库连接字符串
                    DbType = SqlSugar.DbType.SqlServer,     　//必填, 数据库类型
                    IsAutoCloseConnection = true,       //默认false, 时候知道关闭数据库连接, 设置为true无需使用using或者Close操作
                    InitKeyType = InitKeyType.Attribute,    //默认SystemTable, 字段信息读取, 如：该属性是不是主键，是不是标识列等等信息
                    IsShardSameThread = false

                });
                if (Config.GetValue("IsLog").ToBool())
                {
                    //sqlsugar.Aop.OnLogExecuted = (sql, pars) => //SQL执行完事件
                    //{
                    //    System.Diagnostics.Trace.WriteLine(sql);
                    //};
                    sqlsugar.Aop.OnLogExecuting = (sql, pars) => //SQL执行前事件
                    {
                        System.Diagnostics.Trace.WriteLine(sql);
                    };
                    sqlsugar.Aop.OnError = (exp) =>//执行SQL 错误事件
                    {
                        System.Diagnostics.Trace.WriteLine(exp.Message);
                    };
                }
                return sqlsugar;
            }
        }

        private bool _isusetran = false;
        /// <summary>
        /// 事务对象
        /// </summary>
        public bool IsUseTransaction
        {
            get
            {
                return this._isusetran;
            }
            set
            {
                this._isusetran = value;
            }
        }

        #endregion

        #region 事物提交
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public IDatabase BeginTrans()
        {
            _isusetran = true;


            Connection.Ado.BeginTran();
            return this;
        }
        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        public int Commit()
        {
            try
            {
                if (_isusetran == true)
                {
                    Connection.Ado.CommitTran();
                    _isusetran = false;
                    this.Close();
                }

                return 1;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = ex.InnerException.InnerException as SqlException;
                    string msg = ExceptionMessage.GetSqlExceptionMessage(sqlEx.Number);
                    throw DataAccessException.ThrowDataAccessException(sqlEx, msg);
                }
                throw;
            }
            finally
            {
                if (_isusetran == false)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 把当前操作回滚成未提交状态
        /// </summary>
        public void Rollback()
        {
            if (_isusetran == true)
            {
                Connection.Ado.RollbackTran();
                this.Close();
                _isusetran = false;
            }

        }
        /// <summary>
        /// 关闭连接 内存回收
        /// </summary>
        public void Close()
        {
            if (Connection != null)
            {
                //Connection.Dispose();
                Connection.Close();
            }

        }
        #endregion

        #region 执行 SQL 语句
        public int ExecuteBySql(string strSql)
        {
            return Connection.Ado.ExecuteCommand(strSql);
        }

        public int ExecuteBySql(string strSql, params DbParameter[] dbParameter)
        {
            List<SugarParameter> sugarParameter = AutoCopy(dbParameter);
            if (sugarParameter.Count() > 0)
            {
                return Connection.Ado.ExecuteCommand(strSql, sugarParameter);
            }
            return ExecuteBySql(strSql);
        }

        /// <summary>
        /// TODO:实现（未测试）
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public int ExecuteByProc(string procName)
        {
            StringBuilder strSql = new StringBuilder("exec " + procName);
            return Connection.Ado.UseStoredProcedure().GetInt(strSql.ToString());
        }
        /// <summary>
        /// TODO:实现（未测试）
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public int ExecuteByProc(string procName, params DbParameter[] dbParameter)
        {
            if (dbParameter == null || dbParameter.Count() == 0)
            {
                return ExecuteByProc(procName);
            }
            List<SugarParameter> sugarParameter = AutoCopy(dbParameter);

            StringBuilder strSql = new StringBuilder("exec " + procName);
            return Connection.Ado.UseStoredProcedure().GetInt(strSql.ToString(), sugarParameter);
        }
        #endregion

        #region 对象实体 添加、修改、删除
        /// <summary>
        /// 单实体插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert<T>(T entity) where T : class,new()
        {
            return Connection.Insertable(entity).Where(true, true).ExecuteCommand();
        }
        /// <summary>
        /// 多实体插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Insert<T>(IEnumerable<T> entities) where T : class,new()
        {
            if (entities.Count() <= 0) { return 0; }
            return Connection.Insertable<T>(entities.ToArray()).ExecuteCommand();
        }
        public int Delete<T>() where T : class,new()
        {
            return Connection.Deleteable<T>().ExecuteCommand();
        }
        public int Delete<T>(T entity) where T : class,new()
        {
            return Connection.Deleteable<T>(entity).ExecuteCommand();
        }
        public int Delete<T>(IEnumerable<T> entities) where T : class,new()
        {
            int result = 0;
            if (entities.Count() <= 0) { return result; }
            return result = Connection.Deleteable<T>(entities).ExecuteCommand();
        }
        public int Delete<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return Connection.Deleteable<T>().Where(condition).ExecuteCommand();
        }
        public int Delete<T>(object keyValue) where T : class,new()
        {
            return Connection.Deleteable<T>(keyValue).ExecuteCommand();
        }
        public int Delete<T>(object[] keyValue) where T : class,new()
        {
            return Connection.Deleteable<T>(keyValue).ExecuteCommand();
        }

        public int Delete<T>(object propertyValue, string propertyName) where T : class,new()
        {
            var str = string.Format("{0}='{1}'", propertyName, propertyValue);
            return Connection.Deleteable<T>().Where(str).ExecuteCommand();
        }
        public int Update<T>(T entity) where T : class,new()
        {
            return Connection.Updateable<T>(entity).Where(true).ExecuteCommand();
        }
        public int Update<T>(IEnumerable<T> entities) where T : class,new()
        {
            if (entities.Count() <= 0) { return 0; }
            return Connection.Updateable<T>(entities).Where(true).ExecuteCommand();
        }
        public int Update<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return Connection.Updateable<T>().Where(condition).Where(true).ExecuteCommand();
        }
        public int Update<T>(IEnumerable<T> entities, Expression<Func<T, bool>> UpdateColumns, bool NullUpdate = true) where T : class, new()
        {

            return Connection.Updateable<T>(entities).UpdateColumns(UpdateColumns).Where(!NullUpdate).ExecuteCommand();
        }
        #endregion

        #region 对象实体 查询
        public T FindEntity<T>(object keyValue) where T : class,new()
        {
            return Connection.Queryable<T>().InSingle(keyValue);
        }

        public T FindEntity<T>(string strSql) where T : class,new()
        {
            return Connection.Ado.SqlQuery<T>(strSql).FirstOrDefault();
        }
        public T FindEntity<T>(string strSql, DbParameter[] dbParameter) where T : class,new()
        {

            List<SugarParameter> sugarparameter = AutoCopy(dbParameter);
            return Connection.Ado.SqlQuery<T>(strSql, sugarparameter).FirstOrDefault();
        }
        public T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return Connection.Queryable<T>().Where(condition).First();
        }
        public IQueryable<T> IQueryable<T>() where T : class,new()
        {
            return Connection.Queryable<T>().ToList().AsQueryable<T>();
        }
        public IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return Connection.Queryable<T>().Where(condition).ToList().AsQueryable<T>();
        }
        public IEnumerable<T> FindList<T>() where T : class,new()
        {
            return Connection.Queryable<T>().ToList();
        }

        /// <summary>
        /// TODO:实现未测试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Func<T, object> keySelector) where T : class,new()
        {
            Expression<Func<T, object>> keyex = t => keySelector;
            return Connection.Queryable<T>().OrderBy(keyex).ToList();
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return Connection.Queryable<T>().Where(condition).ToList();
        }
        public IEnumerable<T> FindList<T>(string strSql) where T : class,new()
        {
            return Connection.Ado.SqlQuery<T>(strSql).ToList();
        }
        public IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter) where T : class,new()
        {
            List<SugarParameter> sugarparameter = AutoCopy(dbParameter);
            return Connection.Ado.SqlQuery<T>(strSql, sugarparameter).ToList();
        }
        public IEnumerable<T> FindList<T>(string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new()
        {
            return FindList<T>(t => 1 == 1, orderField, isAsc, pageSize, pageIndex, out total);
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new()
        {
            string[] _orderField = orderField.Split(',');
            string ascstr = isAsc ? " asc" : " desc";
            int totalNumber = 0;

            var query = Connection.Queryable<T>().Where(condition);

            if (!string.IsNullOrEmpty(orderField))
            {
                //string[] _orderField = orderField.Split(',');
                if (orderField.ToUpper().IndexOf("ASC") <= -1 && orderField.ToUpper().IndexOf("DESC") <= -1 && !string.IsNullOrEmpty(orderField))
                {
                    orderField = orderField.TrimEnd(',') + ascstr;
                    query = query.OrderBy(orderField);
                }
            }
            var data = query.ToPageList(pageIndex, pageSize, ref totalNumber);
            total = totalNumber;
            return data;
        }
        public IEnumerable<T> FindList<T>(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new()
        {
            return FindList<T>(strSql, null, orderField, isAsc, pageSize, pageIndex, out total);
        }


        public IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new()
        {
            // 获取数据并排序
            List<SugarParameter> sugarparamter = AutoCopy(dbParameter);

            string ascstr = isAsc ? " asc" : " desc";
            int totalNumber = 0;

            var query = Connection.SqlQueryable<T>(strSql).AddParameters(sugarparamter);

            if (!string.IsNullOrEmpty(orderField))
            {
                //string[] _orderField = orderField.Split(',');
                if (orderField.ToUpper().IndexOf("ASC") <= -1 && orderField.ToUpper().IndexOf("DESC") <= -1 && !string.IsNullOrEmpty(orderField))
                {
                    orderField = orderField.TrimEnd(',') + ascstr;
                    query = query.OrderBy(orderField);
                }
            }

            var data = query.ToPageList(pageIndex, pageSize, ref totalNumber);
            total = totalNumber;
            return data;
        }
        #endregion

        #region 数据源查询
        public DataTable FindTable(string strSql)
        {
            return FindTable(strSql, null);
        }
        public DataTable FindTable(string strSql, DbParameter[] dbParameter)
        {
            List<SugarParameter> sugarParmeter = AutoCopy(dbParameter);
            return Connection.Ado.GetDataTable(strSql, sugarParmeter);
        }
        public DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            return FindTable(strSql, null, orderField, isAsc, pageSize, pageIndex, out total);
        }

        /// <summary>
        /// TODO:未测试
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <param name="orderField"></param>
        /// <param name="isAsc"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public DataTable FindTable(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            List<SugarParameter> sugarParameter = AutoCopy(dbParameter);

            //var query = this.Connection.SqlQueryable<DataTable>(strSql).AddParameters(dbParameter).ToDataTablePage(pageIndex, pageSize);
            ////var data = Connection.Ado.GetDataTable(strSql, sugarParameter);

            //string[] _orderField = orderField.Split(',');
            //string ascstr = isAsc ? " asc" : " desc";
            //if (orderField.ToUpper().IndexOf("ASC") <= -1 && orderField.ToUpper().IndexOf("DESC") <= -1 && !string.IsNullOrEmpty(orderField))
            //{
            //    orderField = orderField.TrimEnd(',') + ascstr;
            //}
            //data.DefaultView.Sort = orderField;
            //data = data.DefaultView.ToTable();
            //total = data.Rows.Count;
            //return SplitDataTable(data, pageIndex, pageSize);
            string ascstr = isAsc ? " asc" : " desc";
            int totalNumber = 0;
            var query = Connection.SqlQueryable<DataTable>(strSql).AddParameters(sugarParameter);

            if (!string.IsNullOrEmpty(orderField))
            {
                //string[] _orderField = orderField.Split(',');
                if (orderField.ToUpper().IndexOf("ASC") <= -1 && orderField.ToUpper().IndexOf("DESC") <= -1 && !string.IsNullOrEmpty(orderField))
                {
                    orderField = orderField.TrimEnd(',') + ascstr;
                    query = query.OrderBy(orderField);
                }
            }

            var data = query.ToDataTablePage(pageIndex, pageSize, ref totalNumber);
            total = totalNumber;
            return data;
        }
        public object FindObject(string strSql)
        {
            return FindObject(strSql, null);
        }
        public object FindObject(string strSql, DbParameter[] dbParameter)
        {
            List<SugarParameter> sugarParameter = AutoCopy(dbParameter);
            var data = Connection.Ado.GetDataTable(strSql, sugarParameter);
            return data;
        }
        #endregion

        #region SugarParameter => DbParameter
        private List<SugarParameter> AutoCopy(params DbParameter[] dbParameter)
        {
            List<SugarParameter> SugarParamterList = new List<SugarParameter>();
            if (dbParameter != null)
            {
                foreach (var item in dbParameter)
                {
                    SugarParameter sugarParamter = new SugarParameter(item.ParameterName, item.Value);
                    SugarParamterList.Add(sugarParamter);
                }
            }
            return SugarParamterList;
        }
        #endregion

        #region 根据索引和pagesize返回记录

        private static DataTable SplitDataTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0)
                return dt;
            DataTable newdt = dt.Clone();
            //newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }

            return newdt;
        }
        #endregion






    }
}
