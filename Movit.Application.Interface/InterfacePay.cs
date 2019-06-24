using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Interface
{
    /// <summary>
    /// 付款单支付
    /// </summary>
    public interface InterfacePay
    {
        /// <summary>
        /// 检查是否满足操作条件
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        bool Check(out string errMsg);
        /// <summary>
        /// 进行金额操作
        /// </summary>
        void Execute();
        /// <summary>
        /// 调整资金池
        /// </summary>
        void AdjustTheFundsPool();
        /// <summary>
        /// 生成流水单号
        /// </summary>
        void GenerateFlowOrders();
    }
}
