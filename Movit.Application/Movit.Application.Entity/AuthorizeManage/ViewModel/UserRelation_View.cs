using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Service.AuthorizeManage
{
    public class UserRelation_View
    {
   
        public string UserRelationId { get; set; }
        public string UserId { get; set; }
        public string ObjectId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUserName { get; set; }
        public string NickName { get; set; }
        public string Account { get; set; }
        public string Mobile { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
