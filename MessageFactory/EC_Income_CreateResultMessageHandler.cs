using Movit.Application.Busines;
using Movit.Application.Busines.CapitalFlow;
using Movit.Application.Entity.CapitalFlow;
using Movit.Application.Entity.CapitalFlowManage.ViewModel;
using Movit.Application.Entity.EcommerceContractManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageFactory
{
    /// <summary>
    /// 电商收入
    /// </summary>
    public class EC_Income_CreateResultMessageHandler : MessageHandlerBase
    {
        private T_CapitalFlowBLL cfBll = null;
        private EcommerceProjectRelationBLL ecomBll = null;

        public EC_Income_CreateResultMessageHandler(MessageContext ctx)
            : base(ctx)
        {
            cfBll = new T_CapitalFlowBLL();
            ecomBll = new EcommerceProjectRelationBLL();

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
