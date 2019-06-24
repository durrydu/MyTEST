using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Code
{
    #region 流程枚举
    public enum ProcessInstanceStatus
    {
        /// <summary>
        /// 不存在该流程实例
        /// </summary>
        None = 0,
        /// <summary>
        /// 审批中
        /// </summary>
        Active,
        /// <summary>
        /// 已通过
        /// </summary>
        Approved,
        /// <summary>
        /// 已拒绝
        /// </summary>
        Denied,
        /// <summary>
        /// 已删除
        /// </summary>
        Deleted,
    }

    public enum UserAction
    {
        ///审批中(不会在接口中传递此值)
        Active = 0,
        ///同意  (不会在接口中传递此值)
        Approved,
        ///驳回
        Rejected,
        ///撤消
        Cancelled,
    }
    #endregion
}
