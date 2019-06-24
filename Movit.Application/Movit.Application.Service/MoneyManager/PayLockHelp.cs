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
    /// 付款单占用
    /// 作者:姚栋
    /// 日期:20180627
    public class PayLockHelp : PayToolsBase, InterfacePay
    {
        public PayLockHelp(Pay_InfoEntity inputPayEntity)
            : base(inputPayEntity)
        {


        }

        public bool Check(out string errMsg)
        {
            errMsg = string.Empty;
            //如果没有流水记录，表示是新增付款单所以一定可以可以进行占用操作,或者如果最后一条是释放也可以再次被占用
            if (lastPayInfoDetailsEntity == null || lastPayInfoDetailsEntity.Details_Type == (int)PayDetailsTypeEnum.UnLock)
            {
                //如果是新的付款单才需要 检查余款
                if (EcommerceProjectMoneyCapacity.ActualControlTotalAmount < operationAmount)
                {
                    errMsg = string.Format(@"付款单【{0}】可用金额不足，现有可支配金额{1},需要占用金额{2},占用失败!", inputPayEntity.Pay_Info_Code,
                        EcommerceProjectMoneyCapacity.ActualControlTotalAmount, this.operationAmount);
                    return false;
                }
                return true;
            }

            //判断当前最新的流水类型是占用了，且此次金额不一样，占用失败
            if (lastPayInfoDetailsEntity.Details_Type == (int)PayDetailsTypeEnum.Lock && lastPayInfoDetailsEntity.Amount != operationAmount)
            {
                errMsg = string.Format(@"付款单【{0}】此次占用的金额与上一次不一致，占用失败!", inputPayEntity.Pay_Info_Code);
                return false;
            }
            if (lastPayInfoDetailsEntity.Details_Type == (int)PayDetailsTypeEnum.Consumption)
            {
                errMsg = string.Format(@"付款单【{0}】已被消费，占用失败!", inputPayEntity.Pay_Info_Code);
                return false;
            }


            return true;
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
                        ////如果是第一次调用占用接口 失败了，应该把刚插入的付款单数据删除了 
                        ////判断是不是有过流水记录
                        //if (lastPayInfoDetailsEntity == null)
                        //{
                        new RepositoryFactory().BaseRepository().Delete(this.inputPayEntity);
                        // }


                    }
                    throw new Exception(errMsg);

                }
                //特殊情况判断，因为共享平台会重复调用占用接口，判断最后一条流水是占用状态，且金相同时不用生成流水或调整金额
                if (lastPayInfoDetailsEntity == null || !(lastPayInfoDetailsEntity.Details_Type == (int)PayDetailsTypeEnum.Lock && lastPayInfoDetailsEntity.Amount == operationAmount))
                {
                    AdjustTheFundsPool();
                    GenerateFlowOrders();
                }
              

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
                //1、减去可用的资金池
                EcommerceProjectMoneyCapacity.ActualControlTotalAmount = EcommerceProjectMoneyCapacity.ActualControlTotalAmount - this.operationAmount;
                //2、加上占用金额
                EcommerceProjectMoneyCapacity.FlowNopayTotalAmount = EcommerceProjectMoneyCapacity.FlowNopayTotalAmount + this.operationAmount;
                Transdb.Update(EcommerceProjectMoneyCapacity);
                #endregion
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 生成流水信息
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
                    Details_Name = EnumHelper.ToDescription(PayDetailsTypeEnum.Lock),
                    Details_Type = (int)PayDetailsTypeEnum.Lock,
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
