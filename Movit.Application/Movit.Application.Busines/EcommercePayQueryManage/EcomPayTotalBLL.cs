using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.Entity.EcommercePayQueryManage.ViewModel;
using Movit.Application.IService.EcommercePayQueryManage;
using Movit.Application.Service.EcommercePayQueryManage;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Busines.EcommercePayQueryManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：durry
    /// 日 期：2018-06-25 14:48
    /// </summary>
    public class EcomPayTotalBLL
    {
        private IEcomPayTotalService service = new EcomPayTotalService();
        /// <summary>
        /// 获取关于项目的所有信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<CompanyView> GetAllList(string queryJson)
        {
            return service.GetAllList(queryJson);
        }
        /// <summary>
        /// 获取关于区域公司的所有信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<CompanyView> GetListJson(Pagination pagination,string queryJson)
        {
            return service.GetListJson(pagination,queryJson);
        }
    }
}