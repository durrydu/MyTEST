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
    public class EcomPayEcommerceBLL
    {
        private IEcomPayEcommerceService service = new EcomPayEcommerceService();

        public IEnumerable<ProjectView> GetEcommerceGroupList(Pagination pagination,string queryJson)
        {
            return service.GetEcommerceGroupList(pagination,queryJson);
        }

        public IEnumerable<ProjectView> GetCompanyList(string queryJson)
        {
            return service.GetCompanyList(queryJson);
        }
    }
}
