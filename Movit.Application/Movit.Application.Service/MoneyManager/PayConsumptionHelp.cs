using Movit.Application.Code;
using Movit.Application.Code.Enum;
using Movit.Application.Entity;
using Movit.Application.Entity.EcomPartnerCapitalPoolManage;
using Movit.Application.Interface;
using Movit.Data.Repository;
using Movit.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Service.MoneyManager
{
    /// <summary>
    /// 付款单消费
    /// 作者:姚栋
    /// 日期:20180627
    public class PayConsumptionHelp : PayToolsBase, InterfacePay
    {
        public PayConsumptionHelp(Pay_InfoEntity inputPayEntity)
            : base(inputPayEntity)
        {


        }
        public bool Check(out string errMsg)
        {
            errMsg = string.Empty;
            //如果没有流水记录，表示是新增付款单所以一定不可以可以进行释放操作
            if (inputPayEntity.Pay_Completetime == null)
            {
                errMsg = string.Format(@"付款单【{0}】审批通过时间错误，消费失败!", inputPayEntity.Pay_Info_Code);
                return false;
            }
            if (lastPayInfoDetailsEntity == null)
            {
                errMsg = string.Format(@"付款单【{0}】未找到需要消费的占用流水记录，消费失败!", inputPayEntity.Pay_Info_Code);
                return false;
            }
            //判断当前最新的流水类型是不是已经处于已消费状态了，如果是则不能再进行 释放

            if (lastPayInfoDetailsEntity.Details_Type == (int)PayDetailsTypeEnum.Consumption)
            {
                errMsg = string.Format(@"付款单【{0}】已被消费，消费失败!", inputPayEntity.Pay_Info_Code);
                return false;
            }
            if (lastPayInfoDetailsEntity.Details_Type == (int)PayDetailsTypeEnum.UnLock)
            {
                errMsg = string.Format(@"付款单【{0}】已被释放，消费失败!", inputPayEntity.Pay_Info_Code);
                return false;
            }
            //最后一条流水是占用才能被消费
            if (lastPayInfoDetailsEntity.Details_Type == (int)PayDetailsTypeEnum.Lock)
            {
                //检查当初占用的金额与现在需要消费的金额是否一致
                if (inputPayEntity.Pay_Money != lastPayInfoDetailsEntity.Amount)
                {
                    errMsg = string.Format(@"付款单【{0}】需要消费的金额为{1}与占用的金额{2}不一致，消费失败!", inputPayEntity.Pay_Info_Code, inputPayEntity.Pay_Money, lastPayInfoDetailsEntity.Amount);
                    return false;
                }
                return true;
            }
            return false;
        }

        public void Execute()
        {

            try
            {
                AdjustTheFundsPool();
                GenerateFlowOrders();
                NoteFlowInformation();
                Transdb.Commit();
            }
            catch (Exception ex)
            {
                Transdb.Rollback();
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 生成记账流水信息
        /// </summary>
        public void NoteFlowInformation()
        {
            #region 生成记账流水信息
            var dateNow = DateTime.Now;
            T_PartnerCapitalPoolEntity payDetailsEntity = new T_PartnerCapitalPoolEntity()
            {
                PartnerCapitalPoolID = Guid.NewGuid().ToString(),
                EcommerceProjectRelationID = EcommerceProjectMoneyCapacity.EcommerceProjectRelationID,
                T_P_PartnerCapitalPoolID = "0",
                OperationTitle = "【" + inputPayEntity.Pay_Info_Code + "】消费支出",
                OperationType = (int)OperationTypeNoteEnum.PayConsumption,
                OperationMoney = this.operationAmount,
                CurrentMoney = EcommerceProjectMoneyCapacity.ControlTotalAmount + this.operationAmount,                // CurrentBalance
                AccountingType = (int)AccountingTypeEnum.Expenditure,
                CurrentBalance = EcommerceProjectMoneyCapacity.ControlTotalAmount,
                DeleteMark = 0,
                CreateDate = dateNow,
                ModifyDate = dateNow,
                CreateUserId = "System",
                CreateUserName = "System",
                ObjectID = inputPayEntity.Pay_Info_Id,
                StatisticalDate = dateNow

            };
            Transdb.Insert(payDetailsEntity);
            #endregion
        }
        /// <summary>
        /// 调整资金池
        /// </summary>
        public void AdjustTheFundsPool()
        {
            string errMsg = string.Empty;
            try
            {
                if (!Check(out errMsg))
                {
                    // 只要这里有错误 必须还原到原来的状态,英文前面已经更新过
                    if (this.existingPayEntity != null)
                    {
                        new RepositoryFactory().BaseRepository().Update(this.existingPayEntity);
                    }
                    else
                    {
                        new RepositoryFactory().BaseRepository().Delete(this.inputPayEntity);
                    }
                    throw new Exception(errMsg);


                }
                #region 调整资金池金额

                //1、减去占用金额
                EcommerceProjectMoneyCapacity.FlowNopayTotalAmount = EcommerceProjectMoneyCapacity.FlowNopayTotalAmount - this.operationAmount;
                //2、总资金池扣掉
                EcommerceProjectMoneyCapacity.ControlTotalAmount = EcommerceProjectMoneyCapacity.ControlTotalAmount - this.operationAmount;
                Transdb.Update(EcommerceProjectMoneyCapacity);
                #endregion
            }
            catch (Exception ex)
            {
                // 只要这里有错误 必须还原到原来的状态,英文前面已经更新过
                if (this.existingPayEntity != null)
                {
                    new RepositoryFactory().BaseRepository().Update(this.existingPayEntity);
                }
                else
                {
                    new RepositoryFactory().BaseRepository().Delete(this.inputPayEntity);
                }
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 生成流水信息,并更新付款单的最新流水信息号
        /// </summary>
        public void GenerateFlowOrders()
        {
            try
            {
                #region 生成流水信息
                T_Pay_Info_DetailsEntity payDetailsEntity = new T_Pay_Info_DetailsEntity()
                {
                    Amount = this.operationAmount,
                    Createtime = DateTime.Now,
                    PayInfoDetailsCode = CodeNumber,
                    Details_Name = EnumHelper.ToDescription(PayDetailsTypeEnum.Consumption),
                    Details_Type = (int)PayDetailsTypeEnum.Consumption,
                    EcommerceGroupID = inputPayEntity.EcommerceGroupID,
                    EcommerceGroupName = this.EcommerceProjectMoneyCapacity.EcommerceGroupName,
                    Electricity_Supplier_Code = inputPayEntity.Electricity_Supplier_Code,
                    Electricity_Supplier_Id = inputPayEntity.EcommerceID,
                    Electricity_Supplier_Name = inputPayEntity.Electricity_Supplier_Name,
                    Pay_Info_Code = inputPayEntity.Pay_Info_Code,
                    Pay_Info_Details_ID = Guid.NewGuid().ToString(),
                    Pay_Info_ID = inputPayEntity.Pay_Info_Id,
                    Project_Code = inputPayEntity.Project_Code,
                    Project_ID = inputPayEntity.Project_Id,
                    Project_Name = inputPayEntity.Project_Name,

                };
                Transdb.Insert(payDetailsEntity);
                inputPayEntity.LastPayInfoDetailsCode = CodeNumber;
                Transdb.Update(inputPayEntity);
                #endregion
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
