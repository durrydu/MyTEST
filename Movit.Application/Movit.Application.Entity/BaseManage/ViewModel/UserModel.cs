using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.BaseManage.ViewModel
{
    public class UserModel : UserEntity
    {
        public string RoleName { get; set; }

        public string DepartmentName { get; set; }
    }
}
