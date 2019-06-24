using Movit.Application.Code;
using Movit.Application.Entity;
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
    /// 付款单释放
    /// 作者:姚栋
    /// 日期:20180627
    public class PayUnLockHelp : PayToolsBase, InterfacePay
    {
        public PayUnLockHelp(Pay_InfoEntity inputPayEntity)
            : base(inputPayEntity)
        {


        }
        public bool Check(out string errMsg)
        {
            errMsg = string.Empty;
            //如果没有流水记录，表示是新增付款单所以一定不可以可以进行释放操作
            if (lastPayInfoDetailsEntity == null)
            {
                errMsg = string.Format(@"付款单【{0}】未找到需要释放的占用流水记录，释放失败!", inputPayEntity.Pay_Info_Code);
                return false;
            }

            //判断当前最新的流水类型是不是已经处于已消费状态了，如果是则不能再进行 释放

            if (lastPayInfoDetailsEntity.Details_Type == (int)PayDetailsTypeEnum.Consumption)
            {
                errMsg = string.Format(@"付款单【{0}】已被消费，释放失败!", inputPayEntity.Pay_Info_Code);
                return false;
            }
            if (lastPayInfoDetailsEntity.Details_Type == (int)PayDetailsTypeEnum.UnLock)
            {
                errMsg = string.Format(@"付款单【{0}】已被释放，释放失败!", inputPayEntity.Pay_Info_Code);
                return false;
            }
            //如果最后一条是锁定才能被释放
            if (lastPayInfoDetailsEntity.Details_Type == (int)PayDetailsTypeEnum.Lock)
            {
                //检查释放的金额和当初占用的金额与现在需要释放的金额是否一致
                if (inputPayEntity.Pay_Money != lastPayInfoDetailsEntity.Amount)
                {
                    errMsg = string.Format(@"付款单【{0}】需要释放的金额为{1}与占用的金额{2}不一致，释放失败!", inputPayEntity.Pay_Info_Code, inputPayEntity.Pay_Money, lastPayInfoDetailsEntity.Amount);
                    return false;
                }
                return true;
            }
            return false;
        }


        public void Execute()
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
                AdjustTheFundsPool();
                GenerateFlowOrders();

                Transdb.Commit();
            }
            catch (Exception ex)
            {
                Transdb.Rollback();
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 调整资金池
        /// </summary>
        public void AdjustTheFundsPool()
        {
            try
            {
                #region 调整资金池金额
                //1、将占用的资金还给可用的资金池
                EcommerceProjectMoneyCapacity.ActualControlTotalAmount = EcommerceProjectMoneyCapacity.ActualControlTotalAmount + this.operationAmount;
                //2、减去占用金额
                EcommerceProjectMoneyCapacity.FlowNopayTotalAmount = EcommerceProjectMoneyCapacity.FlowNopayTotalAmount - this.operationAmount;
                Transdb.Update(EcommerceProjectMoneyCapacity);
                #endregion
            }
            catch (Exception ex)
            {

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
                    Details_Name = EnumHelper.ToDescription(PayDetailsTypeEnum.UnLock),
                    Details_Type = (int)PayDetailsTypeEnum.UnLock,
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
