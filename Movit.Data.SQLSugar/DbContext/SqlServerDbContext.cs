using System;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration;
using System.Reflection;
using System.Linq;
using System.Web;
using System.IO;
using Movit.Data.SQLSugar;

namespace Movit.Data.SQLSugar
{
    /// <summary>
    /// 描 述：数据访问(SqlServer) 上下文
    /// </summary>
    public class SqlServerDbContext : IDbContext
    {
        #region 构造函数
        /// <summary>
        /// 初始化一个 使用指定数据连接名称或连接串 的数据访问上下文类 的新实例
        /// </summary>
        /// <param name="connString"></param>
        public SqlServerDbContext(string connString)
        {
            //this.Configuration.AutoDetectChangesEnabled = false;
            //this.Configuration.ValidateOnSaveEnabled = false;
            //this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
        }
        #endregion

        #region 重载
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    System.Data.Entity.Database.SetInitializer<SqlServerDbContext>(null);
        //    string assembleFileName = Assembly.GetExecutingAssembly().CodeBase.Replace("Movit.Data.EF.DLL", "Movit.Application.Mapping.dll").Replace("file:///", "");
        //    Assembly asm = Assembly.LoadFile(assembleFileName);
        //    var typesToRegister = asm.GetTypes()
        //    .Where(type => !String.IsNullOrEmpty(type.Namespace))
        //    .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
        //    foreach (var type in typesToRegister)
        //    {
        //        dynamic configurationInstance = Activator.CreateInstance(type);
        //        modelBuilder.Configurations.Add(configurationInstance);
        //    }
        //    base.OnModelCreating(modelBuilder);
        //}
        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
