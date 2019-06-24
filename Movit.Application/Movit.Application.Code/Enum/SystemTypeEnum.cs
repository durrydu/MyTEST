using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Code
{
    /// <summary>
    /// 请求系统类型
    /// </summary>
    public enum SystemTypeEnum
    {
        /// <summary>
        /// WEB系统
        /// </summary>
        [Description("WEB系统")]
        WebSystem = 1,
        /// <summary>
        /// WebApi
        /// </summary>
        [Description("WebApi")]
        WebApi = 2,
    }
}
