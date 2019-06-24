using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageFactory
{
    /// <summary>
    /// 描述:审批消息上下文
    /// 作者：姚栋
    /// 日期：20180619
    /// </summary>
    public class MessageContext
    {

        /// <summary>
        /// 业务编码
        /// </summary>
        public string strBSID { get; set; }
        /// <summary>
        /// 业务系统在接口传入的业务对象ID
        /// </summary>
        public string strBOID { get; set; }
        /// <summary>
        /// 表示创建流程实例是否成功，true为成功，false为创建失败
        /// </summary>
        public bool bSuccess { get; set; }
        /// <summary>
        /// 该业务对象对应的创建的流程实例ID。如果创建失败，则该值无效，置0，在业务系统表中增加一个字段，在进行流程记录查看时需要
        /// </summary>
        public int iProcInstID { get; set; }
        /// <summary>
        /// 为K2接口提供的信息反馈
        /// </summary>
        public string strMessage { get; set; }

        public int ApproveStatus { get; set; }

        /// <summary>
        /// 用户的审批意见备注
        /// </summary>
        public string strComment { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? dtTime { get; set; }
        /// <summary>
        /// 审批时的步骤名称
        /// </summary>
        public string strStepName { get; set; }
        /// <summary>
        /// 用户对流程实例的审批动作
        /// </summary>

        public int eAction { get; set; }
        /// <summary>
        /// 表示审批者用户ID
        /// </summary>

        public string strApproverId { get; set; }
        /// <summary>
        /// 表示流程审批结果
        /// </summary>

        public int eProcessInstanceResult { get; set; }
    }


}
