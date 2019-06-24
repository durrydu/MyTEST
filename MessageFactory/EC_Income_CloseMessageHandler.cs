using Movit.Application.Busines;
using Movit.Application.Busines.CapitalFlow;
using Movit.Application.Busines.EcomPartnerCapitalPoolManage;
using Movit.Application.Code;
using Movit.Application.Entity.CapitalFlow;
using Movit.Application.Entity.CapitalFlowManage.ViewModel;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.Entity.EcomPartnerCapitalPoolManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageFactory
{
    public class EC_Income_CloseMessageHandler : MessageHandlerBase
    {
        private T_CapitalFlowBLL cfBll = null;
        private EcommerceProjectRelationBLL ecomBll = null;
        private T_PartnerCapitalPoolBLL pCaPoolBll = null;
        public EC_Income_CloseMessageHandler(MessageContext ctx)
            : base(ctx)
        {
            cfBll = new T_CapitalFlowBLL();
            ecomBll = new EcommerceProjectRelationBLL();
            pCaPoolBll = new T_PartnerCapitalPoolBLL();
        }

        public override object Execute()
        {


            T_CapitalFlowEntity cf = new T_CapitalFlowEntity()
           {
               CapitalFlow_Id = this._context.strBOID,
               LatestApprovetime = this._context.dtTime,
               ApprovalState = this._context.ApproveStatus,
               LatestComment = this._context.strComment,
               Procinstid = this._context.iProcInstID.ToString(),
           };
            cfBll.ApprovalUpdateState(this._context.strBOID, cf);
            if (this._context.ApproveStatus == (int)ApproveStatus.approved)
            {
                /// <summary>
                ///  1.根据CapitalFlow_Id 查询
                ///     1.1如果orderNo为空 是第一个
                ///           1.1.1根据CapitalFlow_Id查询出子表数据
                ///           1.1.2循环子表数据 根据每一个子表数据中的电商id和项目id 查询出来合同表
                ///           1.1.3然后将子表中资金和合同表中资金相加 然后更新合同表
                ///           1.1.4更新当前CapitalFlow的orderNo为0
                ///     1.2如果不为空
                ///           1.2.1根据查询出来orderNo最大的一条CapitalFlow记录
                ///           1.2.2根据CapitalFlow_Id查询出子表数据
                ///           1.2.3循环子表数据 根据每一个子表数据中的电商id和项目id 查询出来合同表
                /// </summary>
                //根据区域id 年份 月份 获取orderno最大的一条
                List<T_CapitalFlowEntity> cfEntity = cfBll.check(this._context.strBOID, cf).ToList();
                //判断是否为第一次收入

                if (cfEntity[0].OrderNo == null)
                {
                    //第一笔收入
                    //获取子表
                    List<CapitalFlow_ProRelaView> cfNodeList = cfBll.updateMoney(this._context.strBOID).ToList();
                    //新建一个合同list 便于update
                    List<EcommerceProjectRelationEntity> ecomList = new List<EcommerceProjectRelationEntity>();
                    //新建一个流水list 便于insert
                    List<T_PartnerCapitalPoolEntity> pCaPoolList = new List<T_PartnerCapitalPoolEntity>();
                    CapitalFlow_ProRelaView ecomEntity = new CapitalFlow_ProRelaView();
                    foreach (CapitalFlow_ProRelaView c in cfNodeList)
                    {
                        EcommerceProjectRelationEntity ecomProReEntity = new EcommerceProjectRelationEntity();
                        T_PartnerCapitalPoolEntity pcEntity = new T_PartnerCapitalPoolEntity();
                        //根据项目id和电商id查询合同表
                        //ecomEntity = ecomBll.GetTrunkEntity(c.ProjectID, c.EcommerceID);
                        ecomEntity = cfNodeList.FirstOrDefault(p => p.ProjectID == c.ProjectID && p.EcommerceID == c.EcommerceID);
                        if (ecomEntity != null)
                        {
                            //List<EcommerceProjectRelationEntity> list= ecomBll.searchActAmount(ecomEntity.EcommerceID,ecomEntity.ProjectID).ToList();
                            decimal aca = (decimal)(ecomEntity.ActualControlTotalAmount + c.CapitalPoolAdd);
                            decimal ca = (decimal)(ecomEntity.ControlTotalAmount + c.CapitalPoolAdd);
                            //合同表
                            ecomProReEntity.EcommerceProjectRelationID = c.EcommerceProjectRelationID;
                            ecomProReEntity.ActualControlTotalAmount = aca;
                            ecomProReEntity.ControlTotalAmount = ca;
                            ecomList.Add(ecomProReEntity);
                            //流水表
                            pcEntity.OperationTitle = "资金导入";
                            pcEntity.OperationType = 3;
                            pcEntity.T_P_PartnerCapitalPoolID = null;
                            pcEntity.OperationMoney = c.CapitalPoolAdd;
                            pcEntity.CurrentMoney = ecomEntity.ControlTotalAmount;
                            pcEntity.CurrentBalance = ca;
                            pcEntity.AccountingType = 0;
                            pcEntity.EcommerceProjectRelationID = c.EcommerceProjectRelationID;
                            pcEntity.PartnerCapitalPoolID = Guid.NewGuid().ToString();
                            pcEntity.CreateDate = DateTime.Now;
                            pcEntity.CreateUserId = c.CreateUserId;
                            pcEntity.CreateUserName = c.CreateUserName;
                            pcEntity.ObjectID = c.CapitalFlow_Details_Id;
                            pcEntity.DeleteMark = 0;
                            pcEntity.StatisticalDate = c.UploadDate;
                            pCaPoolList.Add(pcEntity);
                        }
                    }
                    //更新钱
                    ecomBll.updateMoneyAA(ecomList);
                    pCaPoolBll.InsertEntityList(pCaPoolList);
                    //更新orderno
                    cfBll.updateOrderNo(0, this._context.strBOID);
                }
                else
                {
                    //获取流水表的明细
                    IEnumerable<T_PartnerCapitalPoolEntity> paCaPoolList = pCaPoolBll.getAllPaCaPoolList();
                    //获取上一个通过的记录
                    List<CapitalFlow_ProRelaView> cfNodeListBefor = cfBll.updateMoney(cfEntity[0].CapitalFlow_Id).ToList();
                    //获取当前记录
                    List<CapitalFlow_ProRelaView> cfNodeListNow = cfBll.updateMoney(this._context.strBOID).ToList();
                    //新建一个资金池list 更新资金池
                    List<EcommerceProjectRelationEntity> ecomListBefor = new List<EcommerceProjectRelationEntity>();
                    //新建一个流水list 便于insert
                    List<T_PartnerCapitalPoolEntity> pCaPoolListBefor = new List<T_PartnerCapitalPoolEntity>();
                    //新建一个流水list 便于update deletemark=1
                    List<T_PartnerCapitalPoolEntity> pCaPoolListDel = new List<T_PartnerCapitalPoolEntity>();
                    //循环上一个通过的记录
                    foreach (CapitalFlow_ProRelaView c in cfNodeListBefor)
                    {
                        EcommerceProjectRelationEntity ecomProReEntity = new EcommerceProjectRelationEntity();
                        CapitalFlow_ProRelaView ecomEntity = new CapitalFlow_ProRelaView();
                        CapitalFlow_ProRelaView ecomEntityZ = new CapitalFlow_ProRelaView();
                        T_PartnerCapitalPoolEntity pcEntity = new T_PartnerCapitalPoolEntity();
                        T_PartnerCapitalPoolEntity pcZEntity = new T_PartnerCapitalPoolEntity();
                        //根据项目id和电商id查询合同表
                        ecomEntity = cfNodeListBefor.FirstOrDefault(p => p.ProjectID == c.ProjectID && p.EcommerceID == c.EcommerceID);
                        ecomEntityZ = cfNodeListNow.FirstOrDefault(p => p.ProjectID == c.ProjectID && p.EcommerceID == c.EcommerceID);
                        pcZEntity = paCaPoolList.FirstOrDefault(p => p.ObjectID == c.CapitalFlow_Details_Id && p.OperationType == 3);
                        pcZEntity.DeleteMark = 1;
                        pCaPoolListDel.Add(pcZEntity);
                        if (ecomEntity != null)
                        {
                            decimal aca = (decimal)(ecomEntity.ActualControlTotalAmount - c.CapitalPoolAdd);
                            decimal ca = (decimal)(ecomEntity.ControlTotalAmount - c.CapitalPoolAdd);
                            ecomProReEntity.EcommerceProjectRelationID = c.EcommerceProjectRelationID;
                            ecomProReEntity.ActualControlTotalAmount = aca;
                            ecomProReEntity.ControlTotalAmount = ca;
                            ecomListBefor.Add(ecomProReEntity);
                            //流水表
                            pcEntity.OperationTitle = "资金导入";
                            pcEntity.OperationType = 2;
                            pcEntity.OperationMoney = c.CapitalPoolAdd;
                            pcEntity.CurrentMoney = ecomEntity.ControlTotalAmount;
                            pcEntity.CurrentBalance = ca;
                            pcEntity.AccountingType = 1;
                            
                            pcEntity.EcommerceProjectRelationID = c.EcommerceProjectRelationID;
                            pcEntity.PartnerCapitalPoolID = Guid.NewGuid().ToString();
                            pcEntity.CreateDate = DateTime.Now;
                            pcEntity.T_P_PartnerCapitalPoolID = pcZEntity.PartnerCapitalPoolID;
                            if (ecomEntityZ != null)
                            {
                                pcEntity.ObjectID = ecomEntityZ.CapitalFlow_Details_Id;
                            }
                            else
                            {
                                pcEntity.ObjectID = ecomEntity.CapitalFlow_Details_Id;
                            }
                            pcEntity.StatisticalDate = c.UploadDate;
                            pcEntity.DeleteMark = 1;
                            pcEntity.CreateUserId = cfNodeListNow.First().CreateUserId;
                            pcEntity.CreateUserName = cfNodeListNow.First().CreateUserName;
                            pCaPoolListBefor.Add(pcEntity);
                        }
                    }
                    pCaPoolBll.InsertEntityList(pCaPoolListBefor);
                    ecomBll.updateMoneyAA(ecomListBefor);
                    pCaPoolBll.UpdateEntityList(pCaPoolListDel);
                    cfBll.updateDeleteMark(cfEntity[0].CapitalFlow_Id);
                    cfBll.updateCapDeleteMark(cfEntity[0].CapitalFlow_Id);
                    //获取当前的记录
                    paCaPoolList = pCaPoolBll.getAllPaCaPoolList();
                    cfNodeListNow = cfBll.updateMoney(this._context.strBOID).ToList();
                    List<EcommerceProjectRelationEntity> ecomListNow = new List<EcommerceProjectRelationEntity>();
                    //新建一个流水list 便于insert
                    List<T_PartnerCapitalPoolEntity> pCaPoolListNow = new List<T_PartnerCapitalPoolEntity>();
                    foreach (CapitalFlow_ProRelaView c in cfNodeListNow)
                    {
                        EcommerceProjectRelationEntity ecomProReEntity = new EcommerceProjectRelationEntity();

                        CapitalFlow_ProRelaView ecomEntity = new CapitalFlow_ProRelaView();
                        T_PartnerCapitalPoolEntity pcEntity = new T_PartnerCapitalPoolEntity();
                        T_PartnerCapitalPoolEntity pcZEntity = new T_PartnerCapitalPoolEntity();
                        //根据项目id和电商id查询合同表
                        ecomEntity = cfNodeListNow.FirstOrDefault(p => p.ProjectID == c.ProjectID && p.EcommerceID == c.EcommerceID);
                        pcZEntity = paCaPoolList.FirstOrDefault(p => p.ObjectID == c.CapitalFlow_Details_Id);
                        if (ecomEntity != null)
                        {
                            decimal aca = (decimal)(ecomEntity.ActualControlTotalAmount + c.CapitalPoolAdd);
                            decimal ca = (decimal)(ecomEntity.ControlTotalAmount + c.CapitalPoolAdd);
                            ecomProReEntity.EcommerceProjectRelationID = c.EcommerceProjectRelationID;
                            ecomProReEntity.ActualControlTotalAmount = aca;
                            ecomProReEntity.ControlTotalAmount = ca;
                            ecomListNow.Add(ecomProReEntity);
                            //流水表
                            if (pcZEntity != null)
                            {
                                pcEntity.OperationTitle = "资金导入";
                                pcEntity.OperationType = 3;
                                pcEntity.OperationMoney = c.CapitalPoolAdd;
                                pcEntity.CurrentMoney = ecomEntity.ControlTotalAmount;
                                pcEntity.CurrentBalance = ca;
                                pcEntity.AccountingType = 0;
                                pcEntity.EcommerceProjectRelationID = c.EcommerceProjectRelationID;
                                pcEntity.PartnerCapitalPoolID = Guid.NewGuid().ToString();
                                pcEntity.CreateDate = DateTime.Now;
                                pcEntity.CreateUserId = c.CreateUserId;
                                pcEntity.T_P_PartnerCapitalPoolID = pcZEntity.PartnerCapitalPoolID;
                                pcEntity.CreateUserName = c.CreateUserName;
                                pcEntity.ObjectID = c.CapitalFlow_Details_Id;
                                pcEntity.StatisticalDate = c.UploadDate;
                                pcEntity.DeleteMark = 0;
                            }
                            else
                            {
                                pcEntity.OperationTitle = "资金导入";
                                pcEntity.OperationType = 3;
                                pcEntity.OperationMoney = c.CapitalPoolAdd;
                                pcEntity.CurrentMoney = ecomEntity.ControlTotalAmount;
                                pcEntity.CurrentBalance = ca;
                                pcEntity.AccountingType = 0;
                                pcEntity.EcommerceProjectRelationID = c.EcommerceProjectRelationID;
                                pcEntity.PartnerCapitalPoolID = Guid.NewGuid().ToString();
                                pcEntity.CreateDate = DateTime.Now;
                                pcEntity.CreateUserId = c.CreateUserId;
                                pcEntity.T_P_PartnerCapitalPoolID = null;
                                pcEntity.CreateUserName = c.CreateUserName;
                                pcEntity.ObjectID = c.CapitalFlow_Details_Id;
                                pcEntity.StatisticalDate = c.UploadDate;
                                pcEntity.DeleteMark = 0;
                            }


                            pCaPoolListNow.Add(pcEntity);
                        }
                    }
                    ecomBll.updateMoneyAA(ecomListNow);
                    pCaPoolBll.InsertEntityList(pCaPoolListNow);
                    int a = cfEntity[0].OrderNo ?? 0;
                    cfBll.updateOrderNo(a + 1, this._context.strBOID);
                }
            }

            return true;

        }
        #region 更新统计表
        //public bool UpdateTongji(Dictionary<string, string> contact)
        //{

        //    //1、
        //}
        #endregion
    }
}
