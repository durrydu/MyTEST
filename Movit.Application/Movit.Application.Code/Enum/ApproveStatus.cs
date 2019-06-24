using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Code
{
    /// <summary>
    /// 审批状态
    /// </summary>
    public enum ApproveStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [Description("草稿")]
        draft = 1,
        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        deleted = 2,
        /// <summary>
        /// 审批中
        /// </summary>
        [Description("审批中")]
        in_approval = 3,
        /// <summary>
        /// 审批通过
        /// </summary>a
        [Description("审批通过")]
        approved = 4,
        /// <summary>
        /// 审批不通过
        /// </summary>
        [Description("审批不通过")]
        unapproved = 5,
    }
}
