using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Code.Enum
{
    /// <summary>
    /// 记账类型
    /// </summary>
    public enum AccountingTypeEnum
    {
        /// <summary>
        /// 收入
        /// </summary>
        [Description("收入")]
        Income = 0,
        /// <summary>
        /// 支出
        /// </summary>
        [Description("支出")]
        Expenditure = 1,
    }
}
