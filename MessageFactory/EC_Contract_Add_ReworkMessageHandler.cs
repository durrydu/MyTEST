using Movit.Application.Busines;
using Movit.Application.Entity.EcommerceContractManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageFactory
{
    public class EC_Contract_Add_ReworkMessageHandler : MessageHandlerBase
    {
        private EcommerceProjectRelationBLL eprBll = null;
        public EC_Contract_Add_ReworkMessageHandler(MessageContext ctx)
            : base(ctx)
        {

            eprBll = new EcommerceProjectRelationBLL();
        }

        public override object Execute()
        {
            EcommerceProjectRelationEntity model = new EcommerceProjectRelationEntity()
            {

                EcommerceProjectRelationID = this._context.strBOID,
                ApprovalState = this._context.ApproveStatus,
                LatestApprover = this._context.strApproverId,
                LatestApprovetime = this._context.dtTime,
                LatestComment = this._context.strComment,
                Procinstid = this._context.iProcInstID.ToString()

            };
            eprBll.ApprovalUpdateState(this._context.strBOID, model);
            return true;

        }
    }
}
