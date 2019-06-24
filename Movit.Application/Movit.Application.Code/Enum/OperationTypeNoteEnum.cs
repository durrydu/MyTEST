using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Code.Enum
{
    public enum OperationTypeNoteEnum
    {

        /// <summary>
        /// 资金划拨-回退
        /// </summary>
        [Description("资金划拨-回退")]
        FundTransferRollback = 0,
        /// <summary>
        /// 资金划拨-收入
        /// </summary>
        [Description("资金划拨-收入")]
        FundTransferIncome = 1,
        /// <summary>
        /// 资金导入-回退
        /// </summary>
        [Description("资金导入-回退")]
        FundIntroductionRollback = 2,
        /// <summary>
        /// 资金导入-收入
        /// </summary>
        [Description("资金导入-收入")]
        FundIntroductionIncome = 3,
        /// <summary>
        /// 付款单消费
        /// </summary>
        [Description("付款单消费")]
        PayConsumption = 4,




    }
}
