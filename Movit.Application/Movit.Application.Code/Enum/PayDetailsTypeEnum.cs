using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Code
{
    /// <summary>
    /// 付款单流水类型
    /// 作者：姚栋
    /// 日期:20180627
    /// </summary>
    public enum PayDetailsTypeEnum
    {
        /// <summary>
        /// 占用
        /// </summary>
        [Description("占用")]
        Lock = 0,
        /// <summary>
        /// 释放
        /// </summary>
        [Description("释放")]
        UnLock = 1,
        /// <summary>
        /// 消费
        /// </summary>
        [Description("消费")]
        Consumption = 2,

    }
}
