using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Code
{
    /// <summary>
    /// 数据权限过滤类型
    /// </summary>
    public enum AuthorizeUserTypeEnum
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Description("用户ID")]
        UserID = 1,
        /// <summary>
        /// 登录名
        /// </summary>
        [Description("登录名")]
        LoginCode = 2,
    }
}
