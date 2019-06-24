using Movit.Application.Busines;
using Movit.Application.Entity.EcommerceContractManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageFactory
{
    public class EC_Contract_Add_CloseMessageHandler : MessageHandlerBase
    {
        private EcommerceProjectRelationBLL eprBll = null;
        public EC_Contract_Add_CloseMessageHandler(MessageContext ctx)
            : base(ctx)
        {
            eprBll = new EcommerceProjectRelationBLL();

        }

        public override object Execute()
        {
            var data = eprBll.GetEntity(this._context.strBOID);
            var projectid = data.ProjectID;
            var ecommerceid = data.EcommerceID;
            var istrunk = 1;
            //查询是否存在电商和项目是否存在istrunk=1
            int conunt = eprBll.GetIsTrunkCount(projectid, ecommerceid, istrunk);
            if (conunt > 0)
            {
                istrunk = 0;
            }
                EcommerceProjectRelationEntity model = new EcommerceProjectRelationEntity()
                {
                    IsTrunk = istrunk,
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
