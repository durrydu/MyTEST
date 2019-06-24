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
    public class EcomPayProjectBLL
    {
        private IEcomPayProjectService service = new EcomPayProjectService();

        #region 获取数据
        public IEnumerable<ProjectView> GetAllCompany(Pagination pagination,string queryJson)
        {
            return service.GetAllCompany(pagination,queryJson);
        }
        /// <summary>
        /// 获取所有项目的信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<ProjectView> GetAllProject(string queryJson)
        {
              return  service.GetAllProject(queryJson);
        }
        /// <summary>
        /// 获取所有的信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<ProjectView> GetAllList(string queryJson)
        {
            return service.GetAllList(queryJson);
        }
        #endregion

    }
}