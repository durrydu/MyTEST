using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.BaseManage
{
    public class User_View
    {
        /// <summary>
        /// 用户主键
        /// </summary>		
        public string UserId { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>		
        public string EnCode { get; set; }
        /// <summary>
        /// 登录账户
        /// </summary>		
        public string Account { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>		
        public string Password { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>		
        public string RealName { get; set; }
        /// <summary>
        /// 呢称
        /// </summary>		
        public string NickName { get; set; }

        /// <summary>
        /// 部门主键
        /// </summary>		
        public string DepartmentId { get; set; }

        /// <summary>
        /// 部门主键
        /// </summary>		
        public string DepartmentName { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>		
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? EnabledMark { get; set; }

    }
}
