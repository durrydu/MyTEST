using Movit.Application.Busines;
using Movit.Application.Busines.CapitalFlow;
using Movit.Application.Busines.CapitalFlowManage;
using Movit.Application.Busines.EcommerceTransferManage;
using Movit.Application.Code;
using Movit.Application.Entity;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.Entity.EcommerceTransferManage;
using Movit.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinServiceBase;

namespace WinFundStatisticsService
{
    /// <summary>
    /// 描述：统计数据服务
    /// 作者：姚栋
    /// 日期：2018-07-18
    /// </summary>
    public class FundStatisticsServer : ISyncData
    {
        EcommerceProjectRelationBLL eprBll = new EcommerceProjectRelationBLL();
        Transfer_InfoBLL tinfoBll = new Transfer_InfoBLL();
        T_CapitalFlow_NodeBLL cfnBll = new T_CapitalFlow_NodeBLL();
        Pay_InfoBLL payInfoBll = new Pay_InfoBLL();
        T_Funds_DetailsBLL fundsDetailBll = new T_Funds_DetailsBLL();
        #region 属性
        #region 当前日期
        public DateTime dataTimeNow { get; set; }
        public DateTime DateTimeNow
        {

            get { return dataTimeNow; }
        }
        #endregion
        #endregion
        public FundStatisticsServer()
        {
            dataTimeNow = DateTime.Now;

        }
        public void Execute()
        {
            try
            {
                var nowTime = DateTime.Now;
                List<T_Funds_DetailsEntity> insertList = new List<T_Funds_DetailsEntity>();
                T_Funds_DetailsEntity model = null;

                var reuslt = fundsDetailBll.DeleteByData(nowTime);

                //1、获取当天资金池所有的信息
                // 1.1获取所有合同信息
                var contractList = eprBll.GetList().Where(p => p.IsTrunk == 1).ToList();
                // 1.2根据合同信息组装当天的统计信息
                var transferList = tinfoBll.GetList("{'Transfer_DateBegin':'" + nowTime.ToString("yyyy-MM-dd 00:00:00") + "','Transfer_DateEnd':'" + nowTime.ToString("yyyy-MM-dd 23:59:00") + "'}").ToList();
                // 1.3获取当日审批通过的付款单信息
                var payList = payInfoBll.GetList("{'Approval_Status':'" + (int)ApproveStatus.approved + "','Pay_CompletetimeBegin':'" + nowTime.ToString("yyyy-MM-dd 00:00:00") + "','Pay_CompletetimeEnd':'" + nowTime.ToString("yyyy-MM-dd 23:59:00") + "'}").ToList();
                foreach (var item in contractList)
                {
                    model = new T_Funds_DetailsEntity();
                    model.Year = nowTime.Year;
                    model.Month = nowTime.Month;
                    model.Day = nowTime.Day;
                    model.CreateDate = nowTime;
                    model.CreateUserId = "System";
                    model.CreateUserName = "System";
                    model.DeleteMark = 0;
                    model.EcommerceID = item.EcommerceID;
                    model.EcommerceProjectRelationID = item.EcommerceProjectRelationID;
                    model.IsFirstDayOfMonth = Time.IsFirstDayOfMonth(nowTime) == true ? 1 : 0;
                    model.IsFirstDayOfYear = Time.IsFirstDayOfYear(nowTime) == true ? 1 : 0;
                    model.IsLastDayOfMonth = 1;
                    model.IsLastDayOfYear = 1;
                    model.ProjectID = item.ProjectID;
                    model.StatisticalDate = nowTime;
                    model.ActualControlTotalAmount = item.ActualControlTotalAmount;
                    model.FlowNopayTotalAmount = item.FlowNopayTotalAmount;
                    model.ControlTotalAmount = item.ControlTotalAmount;
                    model.CompanyID = item.CompanyId;
                    model.FundsDetailsID = Guid.NewGuid().ToString();
                    model.TotalPayAmount = PayTotal(payList, item);//电商支出合计
                    model.EcommerceGroupID = item.EcommerceGroupID;
                    model.TotalTransferAmount = Transfer(transferList, item);//资金划拨金额
                    insertList.Add(model);
                    model = null;
                }
                fundsDetailBll.BacthInsert(insertList);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 组装统计流水表的资金划拨金额
        /// </summary>
        /// <param name="plist">今日划拨信息</param>
        /// <param name="erpModel">合同信息</param>
        /// <param name="fundsDetailsModel">今日统计流水信息</param>
        public decimal? Transfer(List<Transfer_InfoEntity> plist, EcommerceProjectRelationEntity erpModel)
        {
            decimal? money = 0m;
            var tempTranfer = plist.Where(p => p.CompanyId == erpModel.CompanyId && p.ProjectID == erpModel.ProjectID && p.DeleteMark == 0).ToList();
            if (tempTranfer.Count > 0)
            {
                money = tempTranfer.Sum(p => p.Transfer_Money);//收入合计
            }
            return money;
        }
        /// <summary>
        /// 组装统计流水表的电商支出合计
        /// </summary>
        /// <param name="plist">今日审批通过的付款信息</param>
        /// <param name="erpModel">合同信息</param>
        /// <param name="fundsDetailsModel">今日统计流水信息</param>
        public decimal? PayTotal(List<Pay_InfoEntity> plist, EcommerceProjectRelationEntity erpModel)
        {
            decimal? money = 0m;
            var tempTranfer = plist.Where(p => p.CompanyID == erpModel.CompanyId && p.Project_Id == erpModel.ProjectID && p.DeleteMark == 0).ToList();
            if (tempTranfer.Count > 0)
            {
                money = tempTranfer.Sum(p => p.Pay_Money);//收入合计
            }
            return money;
        }
    }
}
