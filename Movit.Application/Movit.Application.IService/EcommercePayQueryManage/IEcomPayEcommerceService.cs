
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movit.Application.Entity.EcommercePayQueryManage.ViewModel;
using Movit.Util.WebControl;

namespace Movit.Application.IService.EcommercePayQueryManage
{
    public interface IEcomPayEcommerceService
    {
        IEnumerable<ProjectView> GetEcommerceGroupList(Pagination pagination,string queryJson);

        IEnumerable<ProjectView> GetCompanyList(string queryJson);
    }
}
