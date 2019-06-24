using System;
using System.Data.Entity.Infrastructure;

namespace Movit.Data.EF
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人：姚栋
    /// 日 期：2016.04.07
    /// 描 述：数据库连接接口 
    /// </summary>
    public interface IDbContext: IDisposable, IObjectContextAdapter
    {
    }
}
