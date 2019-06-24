
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.Entity.EcommercePayQueryManage.ViewModel;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.IService.EcommercePayQueryManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-06-25 14:48
    /// 描 述：T_Transfer_Info
    /// </summary>
    public interface IEcomPayTotalService
    {
        #region 获取数据
        /// <summary>
        /// 获取项目的所有信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<CompanyView> GetAllList(string queryJson);
        /// <summary>
        /// 获取区域的所有信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<CompanyView> GetListJson(Pagination pagination,string queryJson);
        #endregion
    }
}
