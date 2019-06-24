using Movit.Application.Busines.CapitalFlow;
using Movit.Application.Entity.CapitalFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageFactory
{
    public class EC_Income_ReworkMessageHandler : MessageHandlerBase
    {
        private T_CapitalFlowBLL cfBll = null;
        public EC_Income_ReworkMessageHandler(MessageContext ctx)
            : base(ctx)
        {
            cfBll = new T_CapitalFlowBLL();

        }

        public override object Execute()
        {
            T_CapitalFlowEntity cf = new T_CapitalFlowEntity()
            {
                CapitalFlow_Id = this._context.strBOID,
                ApprovalState = this._context.ApproveStatus,
                LatestApprover = this._context.strApproverId,
                LatestApprovetime = this._context.dtTime,
                LatestComment = this._context.strComment,
                Procinstid = this._context.iProcInstID.ToString(),
            };
            cfBll.ApprovalUpdateState(this._context.strBOID, cf);

            return true;

          
        }
    }
}
