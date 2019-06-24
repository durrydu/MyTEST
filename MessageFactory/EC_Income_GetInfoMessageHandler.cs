
using Movit.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using Movit.Application.Cache;
using Movit.Application.Busines.CapitalFlow;
using Movit.Application.Busines.BaseManage;
using Movit.Application.Entity.CapitalFlowManage;
using Movit.Application.Entity.CapitalFlow;
using Movit.Application.Entity.BaseManage;
using Movit.Application.Entity.CapitalFlowManage.ViewModel;

namespace MessageFactory
{
    class EC_Income_GetInfoMessageHandler : MessageHandlerBase
    {
        private T_CapitalFlowBLL cfBll = null;
        private T_CapitalFlow_NodeBLL cfnbll = null;
        private T_AttachmentBLL tabll = null;
        private DataItemCache dataItemCache = new DataItemCache();
        public EC_Income_GetInfoMessageHandler(MessageContext ctx)
            : base(ctx)
        {
            dataItemCache = new DataItemCache();
            cfBll = new T_CapitalFlowBLL();
            cfnbll = new T_CapitalFlow_NodeBLL();
            tabll = new T_AttachmentBLL();

        }

        public override object Execute()
        {
            string CapitalFlowId = this._context.strBOID;
            IEnumerable<IncomeView> capitalFlow = cfBll.GetCFEntity(CapitalFlowId);
            IEnumerable<CapitalFlow_CFNodeView> CapitalFlowNodeList= cfnbll.GetEntityList(CapitalFlowId);
            List<T_AttachmentEntity> attaclist = tabll.GetFormList(CapitalFlowId, "default");
            IncomeView income = new IncomeView();
            List<Node> its = new List<Node>();
            List<ATTACHMENT1> attcs = new List<ATTACHMENT1>();
            income.Year = capitalFlow.First().Year;
            income.Month = capitalFlow.First().Month;
            income.OrgName = capitalFlow.First().OrgName;
            income.OrgCode = capitalFlow.First().OrgCode;
            income.CapitalFlow_Title = capitalFlow.First().CapitalFlow_Title;
            StringBuilder ProjectCodeList=new StringBuilder();
            foreach (var item in CapitalFlowNodeList) 
            {
                Node node = new Node();
                node.CapitalPoolAdd = item.CapitalPoolAdd;
                node.ProjCode = item.ProjectID;
                node.ClearingAmount = item.ClearingAmount;
                node.EcommerceGroupName = item.EcommerceGroupName;
                node.ProjectName = item.ProjectName;
                node.Proportion = item.Proportion;
                node.PlatformExpensesAmount = item.PlatformExpensesAmount;
                node.IncomeAmount = item.IncomeAmount;
                 if (!its.Contains(node))
                    {
                        its.Add(node);
                    }
                 ProjectCodeList.Append(item.ProjectID+",");
            }
            string s = ProjectCodeList.ToString();
            string projeCodeList = s.Substring(0, s.Length - 1);
            income.ProjectCodeList = projeCodeList;
            income.AccountDetail = new AccountDetail();
            income.AccountDetail.item = its.ToArray();
            var geturl = dataItemCache.GetDataItemByCodeAndName("SysConfig", "BPMAttacPath");
            string urlname = geturl.ItemValue;
            foreach (var item in attaclist)
            {
                ATTACHMENT1 attc = new ATTACHMENT1();
                attc.FILENAME = item.AttachmentName;
                attc.URL = string.Format("{0}/{1}", urlname, item.Path);
                if (!attcs.Contains(attc))
                {
                    attcs.Add(attc);
                }
            }
            //income.Remark = capitalFlow.First().Remark;
            income.ATTACHMENT = new ATTACHMENT();
            income.ATTACHMENT.attachment = attcs.ToArray();
            var d = Movit.Util.XmlHelper.Serializer(typeof(IncomeView), income);
            return d;
        }
    }
}
         
