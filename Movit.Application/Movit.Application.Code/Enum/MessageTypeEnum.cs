using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Code
{
    public enum MessageTypeEnum
    {
        /// <summary>
        /// 电商合同建立
        /// </summary>
        [Description("电商合同建立-创建结束")]
        EC_Contract_Add_CreateResult = 1,
        /// <summary>
        /// 电商收入
        /// </summary>
        [Description("电商收入-创建结束")]
        EC_Income_CreateResult = 2,

        /// <summary>
        /// 电商合同建立-撤回/退回
        /// </summary>
        [Description("电商合同建立-撤回/退回")]
        EC_Contract_Add_Rework = 3,
        /// <summary>
        /// 电商收入-撤回/退回
        /// </summary>
        [Description("电商收入-撤回/退回")]
        EC_Income_Rework = 4,
        /// <summary>
        /// 电商合同建立-审批结束
        /// </summary>
        [Description("电商合同建立-审批结束")]
        EC_Contract_Add_Close = 5,
        /// <summary>
        /// 电商收入-审批结束
        /// </summary>
        [Description("电商收入-审批结束")]
        EC_Income_Close = 6,

        /// <summary>
        /// 电商合同建立-获取表单信息
        /// </summary>
        [Description("电商合同建立-获取表单信息")]
        EC_Contract_Add_GetInfo = 7,
        /// <summary>
        /// 电商收入获取表单信息
        /// </summary>
        [Description("电商收入-获取表单信息")]
        EC_Income_GetInfo = 8,
    }
}
