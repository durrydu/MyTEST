using Movit.Application.Busines;
using Movit.Application.Entity.EcommerceContractManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageFactory
{
    /// <summary>
    /// 合同建立档案
    /// </summary>
    public class EC_Contract_Add_CreateResultMessageHandler : MessageHandlerBase
    {
        private EcommerceProjectRelationBLL eprBll = null;
        public EC_Contract_Add_CreateResultMessageHandler(MessageContext ctx)
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
                //LatestApprover = this._context.strApproverId,
                LatestApprovetime = this._context.dtTime,
                LatestComment = this._context.strComment,
                Procinstid = this._context.iProcInstID.ToString(),

            };
            eprBll.ApprovalUpdateState(this._context.strBOID, model);

            return true;
        }
    }
}
