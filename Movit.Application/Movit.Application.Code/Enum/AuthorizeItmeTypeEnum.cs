using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Code
{
    /// <summary>
    /// 数据类型
    /// 项目类型:1-菜单2-按钮3-视图4-表单5-项目
    /// </summary>
    public enum AuthorizeItmeTypeEnum
    {
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        FuntionInfo = 1,
        /// <summary>
        /// 按钮
        /// </summary>
        [Description("按钮")]
        Button = 2,
        /// <summary>
        /// 视图
        /// </summary>
        [Description("视图")]
        ViewInfo = 3,
        /// <summary>
        /// 表单
        /// </summary>
        [Description("表单")]
        FormInfo = 4,
        /// <summary>
        /// 项目
        /// </summary>
        [Description("项目")]
        ProjectInfo = 5,
   
    }
}
