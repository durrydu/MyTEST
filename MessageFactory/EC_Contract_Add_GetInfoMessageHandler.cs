using Movit.Application.Busines;
using Movit.Application.Busines.BaseManage;
using Movit.Application.Entity.BaseManage;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.Entity.EcommerceContractManage.ViewModel;
using Movit.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using Movit.Application.Cache;
using Movit.Application.Code;
using Movit.Application.Entity;

namespace MessageFactory
{
    /// <summary>
    ///获取档案信息
    /// </summary>
    public class EC_Contract_Add_GetInfoMessageHandler : MessageHandlerBase
    {
        private EcommerceProjectRelationBLL eprBll = null;
        private EcommerceDiscountProgramBLL edpbll = null;
        private T_AttachmentBLL tabll = null;
        private OP_ParcelBLL op_parcelbll = new OP_ParcelBLL();
        private DataItemCache dataItemCache = new DataItemCache();

        public EC_Contract_Add_GetInfoMessageHandler(MessageContext ctx)
            : base(ctx)
        {
            dataItemCache = new DataItemCache();
            eprBll = new EcommerceProjectRelationBLL();
            edpbll = new EcommerceDiscountProgramBLL();
            op_parcelbll = new OP_ParcelBLL();
            tabll = new T_AttachmentBLL();
        }

        public override object Execute()
        {
            string EProjectRelationId = this._context.strBOID;//合同ID
            EcommerceProjectRelationEntity ecommerceprojectrelationEntity = eprBll.GetEntity(EProjectRelationId);
            OP_ParcelEntity op_parcelEntity = op_parcelbll.GetEntityByproid(ecommerceprojectrelationEntity.ProjectID);
            IEnumerable<EcommerceDiscountProgramEntity> discount = edpbll.GetEntity(EProjectRelationId);
            List<T_AttachmentEntity> attaclist = tabll.GetFormList(EProjectRelationId, "default");
            ContractView cv = new ContractView();
            List<ITEM> its = new List<ITEM>();
            List<ATTACHMENT1> attcs = new List<ATTACHMENT1>();
            cv.ProjectName = ecommerceprojectrelationEntity.ProjecName;
            cv.ProjCode = op_parcelEntity.ParcelCode.Replace("-",".");
            cv.ProjectType = EnumHelper.ToDescription((ProjectTypeEnum) ecommerceprojectrelationEntity.ProjectType);
            cv.OrgName = ecommerceprojectrelationEntity.CompanyName;
            cv.OrgCode = ecommerceprojectrelationEntity.CompanyId;
            cv.EcommerceGroupName = ecommerceprojectrelationEntity.EcommerceGroupName;
            cv.EcommerceTypeName = ecommerceprojectrelationEntity.EcommerceTypeName;
            cv.ContractName = ecommerceprojectrelationEntity.ContractName;
            cv.ContractNature =EnumHelper.ToDescription((ContractNatureEnum)ecommerceprojectrelationEntity.ContractNature);
            cv.ContractTypeName = ecommerceprojectrelationEntity.ContractTypeName;
            cv.IsStandard = ecommerceprojectrelationEntity.IsStandard==0?"否":"是";
            cv.PartyA = ecommerceprojectrelationEntity.PartyA;
            cv.PartyB = ecommerceprojectrelationEntity.PartyB;
            cv.CooperateStartTime = Convert.ToDateTime(ecommerceprojectrelationEntity.CooperateStartTime).ToString("yyyy-MM-dd");
            cv.CooperateEndTime = Convert.ToDateTime(ecommerceprojectrelationEntity.CooperateEndTime).ToString("yyyy-MM-dd");
            cv.BiddingMethod = EnumHelper.ToDescription((BiddingMethodEnum)ecommerceprojectrelationEntity.BiddingMethod);
            cv.IsStamp = ecommerceprojectrelationEntity.IsStamp==0?"否":"是";
            cv.Agent = ecommerceprojectrelationEntity.Agent;
            cv.CreateDate = Convert.ToDateTime(ecommerceprojectrelationEntity.CreateDate).ToString("yyyy-MM-dd");
            cv.ForceContractAmount = ecommerceprojectrelationEntity.ForceContractAmount;
            cv.PlatformRate = ecommerceprojectrelationEntity.PlatformRate;
            foreach (var item in discount)
            {ITEM it=new ITEM();
                it.Format = item.Format;
                it.Amount = item.Amount;
                it.Discount = item.Discount;
                if(!its.Contains(it))
                {
                 its.Add(it);
                }
            }
            cv.BDITEM = new BDITEM();
            cv.BDITEM.item = its.ToArray();

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
            cv.ATTACHMENT = new ATTACHMENT();
            cv.ATTACHMENT.attachment = attcs.ToArray();
            var d = Movit.Util.XmlHelper.Serializer(typeof(ContractView), cv);           
            return d;
            //XmlDocument doc = new XmlDocument();
            //XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-16", null);
            //doc.AppendChild(dec);
            ////创建一个根节点（一级）
            //XmlNode root = doc.CreateElement("DATA");
            //doc.AppendChild(root);
            ////创建节点（二级）
            //XmlElement ITEM = null;
            //XmlElement element = null;
            ////合同编号
            //element = doc.CreateElement("ProjectName");
            //element.SetAttribute("Text", "项目名称");
            //element.InnerText = ecommerceprojectrelationEntity.EcommerceProjectRelationID;
            ////项目名称
            //element = doc.CreateElement("CompanyName");
            //element.SetAttribute("Text", "所属区域");
            //element.InnerText = ecommerceprojectrelationEntity.ProjectName;
            //root.AppendChild(element);

            //ITEM = doc.CreateElement("优化方案Items");
            //ITEM.SetAttribute("ID", "BudgetItem");
            //root.AppendChild(ITEM);
            //foreach (var item in tcollection)
            //{
            //    XmlNode item = doc.CreateElement("item");

            //    //创建节点（三级）
            //    element = null;

            //    //集团/地区公司GUID
            //    element = doc.CreateElement("OrgUnitGUID");
            //    element.SetAttribute("Text", "集团/地区公司");
            //    element.InnerText = "三级节点数据";
            //    item.AppendChild(element);
            //}

            //return XmlHelper.ConvertXmlToString(doc);
        }
    }
}
