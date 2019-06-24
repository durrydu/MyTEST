
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movit.Application.Entity.EcommercePayQueryManage.ViewModel;
using Movit.Util.WebControl;

namespace Movit.Application.IService.EcommercePayQueryManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：durry
    /// 日 期：2018-07-02 09:54
    /// 描 述：EcomPayProject
    public interface IEcomPayProjectService
    {
        #region 获取数据
        IEnumerable<ProjectView> GetAllCompany(Pagination pagination,string queryJson);
        /// <summary>
        /// 获取所有项目的信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<ProjectView> GetAllProject(string queryJson);
        /// <summary>
        /// 获取所有的信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<ProjectView> GetAllList(string queryJson);
        #endregion
    }
}
