using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Code
{
    /// <summary>
    /// 描述:授权方式
    /// 作者:姚栋
    /// 日期:2018-06-15
    /// </summary>
    public enum AuthorizationMethodEnum
    {
        /// <summary>
        /// 全部项目 
        /// </summary>
        [Description("全部项目")]
        AllPorject = 1,
        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义")]
        CustomizeProject = 2,
    }
}
