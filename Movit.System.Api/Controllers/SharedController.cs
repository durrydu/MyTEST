using Movit.Application.Busines;
using Movit.Application.Busines.SystemManage;
using Movit.Application.Code;
using Movit.Application.Entity;
using Movit.Sys.Api.Code.Entity;
using Movit.Util;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Movit.Sys.Api.Controllers
{
    public class SharedController : BaseApiControl
    {
        private EcommerceProjectRelationBLL ecommerceProjectBll = new EcommerceProjectRelationBLL();
        private T_Pay_Info_DetailsBLL payInfoDetailsBll = new T_Pay_Info_DetailsBLL();
        private Pay_InfoBLL payInfoBll = new Pay_InfoBLL();
        private Base_ProjectInfoBLL projectInfoBLL = new Base_ProjectInfoBLL();
        /// <summary>
        /// 获取资金流水明细
        /// 作者:姚栋
        /// 日期:20180625
        /// </summary>
        /// <param name="pay_info_code">付款单编号</param>
        /// <returns></returns>
        [Route("~/api/money/billingdetails/{pay_info_code}")]
        public async Task<Resp<List<OutPayInfoDetails>>> GetBillingDetails(string pay_info_code)
        {
            string errMsg = string.Empty;
            int reqState = 1;
            List<OutPayInfoDetails> result = new List<OutPayInfoDetails>();

            try
            {
                if (string.IsNullOrEmpty(pay_info_code))
                {
                    reqState = 0;
                    errMsg = "付款单编号缺失!";
                    return Resp.BusinessError<List<OutPayInfoDetails>>(errMsg, result);
                }
                await Task.Run(() =>
                {
                    result = payInfoDetailsBll.GetBillingDetails(pay_info_code);
                });

                return Resp.Success(result);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                reqState = 0;
                return Resp.BusinessError<List<OutPayInfoDetails>>(errMsg, result);
            }
            finally
            {

                string ResponseContent = reqState == 0 ? Resp.BusinessError<List<OutPayInfoDetails>>(errMsg, result).ToJson()
                    : Resp.Success(result).ToJson();
                LogBLL.WriteLogInterface("   pay_info_code:" + pay_info_code, ResponseContent, "获取资金流水明细",
                      reqState, "共享平台", "电商资金", errMsg);
            }
        }

        /// <summary>
        /// 资金余额检查
        /// 作者:姚栋
        /// 日期:20180625
        /// </summary>
        /// <param name="electricity_supplier_id">电商ID</param>
        /// <param name="electricity_supplier_code">电商编码</param>
        /// <param name="locked_amount">占用金额</param>
        /// <param name="project_id">项目ID</param>
        /// <param name="project_code">项目编码</param>
        /// <param name="currency_code">币种编码</param>
        /// <returns></returns>
        [Route("~/api/money/balancecheck/{electricity_supplier_id}/{locked_amount}/{project_id}/{currency_code}")]
        public async Task<Resp<bool>> GetBalanceCheck(string electricity_supplier_id, decimal locked_amount,
            string project_id,
            string currency_code = "CNY")
        {
            string errMsg = string.Empty;
            int reqState = 1;
            var result = true;

            try
            {
                #region 数据验证
                if (string.IsNullOrEmpty(electricity_supplier_id))
                {
                    reqState = 0;
                    errMsg = "电商ID缺失!";
                    return Resp.BusinessError<bool>(errMsg, result);
                }
                //if (string.IsNullOrEmpty(electricity_supplier_code))
                //{
                //    reqState = 0;
                //    errMsg = "电商编码缺失!";
                //    return Resp.BusinessError<bool>("电商编码缺失!", result);
                //}
                if (locked_amount <= 0)
                {
                    reqState = 0;
                    errMsg = "占用金额信息错误!";
                    return Resp.BusinessError<bool>(errMsg, result);
                }
                if (string.IsNullOrEmpty(project_id))
                {
                    reqState = 0;
                    errMsg = "项目ID缺失!";
                    return Resp.BusinessError<bool>(errMsg, result);
                }
                //if (string.IsNullOrEmpty(project_code))
                //{
                //    reqState = 0;
                //    errMsg = "项目编码缺失!";
                //    return Resp.BusinessError<bool>("项目编码缺失!", result);
                //}
                if (string.IsNullOrEmpty(currency_code))
                {
                    reqState = 0;
                    errMsg = "币种编码缺失!";
                    return Resp.BusinessError<bool>(errMsg, result);
                }
                #endregion
                await Task.Run(() =>
               {
                   result = ecommerceProjectBll.BalanceCheck(electricity_supplier_id,
                      "",
                      locked_amount,
                      project_id,
                      "",
                      currency_code);
               });
                return Resp.Success(result);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                reqState = 0;
                return Resp.BusinessError<bool>(errMsg, result);
            }
            finally
            {
                string ResponseContent = reqState == 0 ? Resp.BusinessError<bool>(errMsg, result).ToJson()
                  : Resp.Success(result).ToJson();
                LogBLL.WriteLogInterface("   electricity_supplier_id:" + electricity_supplier_id
                    + "   electricity_supplier_code:" + ""
                     + "   locked_amount:" + locked_amount
                      + "   project_id:" + project_id
                       + "   project_code:" + ""
                        + "   currency_code:" + currency_code,
                    ResponseContent, "资金余额检查",
                      reqState, "共享平台", "电商资金", errMsg);
            }

        }
        /// <summary>
        /// 资金占用情况
        /// 作者:姚栋
        /// 日期:20180625
        /// </summary>
        /// <returns></returns>
        [Route("~/api/money/paymentuse")]
        public async Task<Resp<bool>> PostPaymentuse(InputPaymentuse inputModel)
        {
            string errMsg = string.Empty;
            int reqState = 1;
            bool result = false;
            #region 入参校验
            //验证实体对象属性信息

            #endregion
            try
            {
                var errorResult = GetError();
                if (!string.IsNullOrWhiteSpace(errorResult))
                {
                    errMsg = errorResult;
                    reqState = 0;
                    return Resp.BusinessError(errMsg, result);

                }
                if (inputModel.pay_money <= 0)
                {
                    errMsg = "金额异常!";
                    reqState = 0;
                    return Resp.BusinessError(errMsg, result);

                }
                await Task.Run(() =>
                {
                    var _approval = 0;
                    switch (inputModel.approval_status)
                    {
                        case "AUDITED": // 已审批 消费金额
                            _approval = (int)ApproveStatus.approved;
                            break;
                        case "DRAFT": // DRAFT 草稿 释放
                            _approval = (int)ApproveStatus.draft;
                            break;
                        case "SUBMITED": //SUBMITED 待审批 占用（注意金额的变化，变化是需要修改）
                            _approval = (int)ApproveStatus.in_approval;
                            break;
                        case "DISABLED":// DISABLED 已作废 释放   
                            _approval = (int)ApproveStatus.deleted;
                            break;
                        default:
                            break;
                    }
                    var model = new Pay_InfoEntity()
                    {
                        Approval_Status = _approval.ToString(),
                        Contract_Code = inputModel.contract_code,
                        Contract_Name = inputModel.contract_name,
                        ContractMoney = inputModel.pay_money,
                        CreateUserName = inputModel.login_name,
                        EcommerceGroupID = inputModel.electricity_supplier_ad_id,
                        EcommerceID = inputModel.electricity_supplier_id,
                        Electricity_Supplier_Code = inputModel.electricity_supplier_code,
                        Electricity_Supplier_Name = inputModel.electricity_supplier_name,
                        EcommerceGroupName = inputModel.electricity_supplier_ad,
                        Login_Code = inputModel.login_code,
                        Login_Name = inputModel.login_name,
                        Pay_Completetime = inputModel.pay_completetime,
                        Pay_Createtime = inputModel.pay_createtime,
                        Pay_Info_Code = inputModel.pay_info_code,
                        Pay_Info_Id = inputModel.pay_info_id,
                        Pay_Info_Type = inputModel.pay_info_type,
                        Pay_Money = inputModel.pay_money,
                        Pay_Reason = inputModel.pay_reason,
                        Project_Code = inputModel.project_code,
                        Project_Id = inputModel.project_id,
                        Project_Name = inputModel.project_name,
                        Url = inputModel.url,


                    };
                    payInfoBll.Paymentuse(inputModel.pay_info_id, model);
                });
                result = true;
                return Resp.Success(result);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                reqState = 0;
                return Resp.BusinessError(errMsg, result);
            }
            finally
            {
                string ResponseContent = reqState == 0 ? Resp.BusinessError<bool>(errMsg, result).ToJson()
               : Resp.Success(result).ToJson();
                LogBLL.WriteLogInterface(inputModel.ToJson(),
                   ResponseContent, "资金占用情况",
                      reqState, "共享平台", "电商资金", errMsg);
            }
        }

        /// <summary>
        /// 电商平台选择
        /// 作者:姚栋
        /// 日期:20180625
        /// </summary>
        /// <param name="login_name">当前用户登录名</param>
        /// <param name="currency_code">币种编码</param>
        /// <param name="electricity_supplier_name">电商名称</param>
        /// <param name="electricity_supplier_ad">电商简称</param>
        /// <param name="electricity_supplier_code">电商编码</param>
        /// <param name="available_balance_begin">可用余额(范围-开始)</param>
        /// <param name="available_balance_end">可用余额(范围-结束)</param>
        /// <param name="project_code">项目编码</param>
        /// <param name="project_name">项目名称</param>
        /// <param name="rows">每页行数</param>
        /// <param name="page">当前页</param>
        /// <returns></returns>
        [Route(@"~/api/online_mall_funds/select/{login_name}/{currency_code}/{rows}/{page}")]
        public async Task<Resp<OutOnlieModel>> GetSelect(
            string login_name, string currency_code, int rows, int page)
        {
            string errMsg = string.Empty;
            int reqState = 1;
            Pagination pagination = new Pagination()
            {
                page = page,
                rows = rows,
                sidx = "project_code",
                sord = "asc"
            };
            var electricity_supplier_name = RequestApi["electricity_supplier_name"];
            var electricity_supplier_ad = RequestApi["electricity_supplier_ad"];
            var electricity_supplier_code = RequestApi["electricity_supplier_code"];
            var available_balance_begin = RequestApi["available_balance_begin"] == null ? 0 : Convert.ToDecimal(RequestApi["available_balance_begin"]);
            var available_balance_end = RequestApi["available_balance_end"] == null ? 0 : Convert.ToDecimal(RequestApi["available_balance_end"]);
            var project_code = RequestApi["project_code"];
            var project_name = RequestApi["project_name"];
            OutOnlieModel result = new OutOnlieModel();

            try
            {
                if (string.IsNullOrEmpty(login_name))
                {
                    reqState = 0;
                    errMsg = "登录信息缺失";
                    return Resp.BusinessError<OutOnlieModel>(errMsg, result);
                }
                await Task.Run(() =>
                {
                    result = ecommerceProjectBll.GetOnLineMallList(login_name, pagination, currency_code,
                     electricity_supplier_name,
                     electricity_supplier_ad,
                    electricity_supplier_code,
                     available_balance_begin,
                    available_balance_end,
                     project_code,
                     project_name);
                });
                return Resp.Success(result);
            }
            catch (Exception ex)
            {
                return Resp.BusinessError<OutOnlieModel>(ex.Message, result);
            }
            finally
            {
                string ResponseContent = reqState == 0 ? Resp.BusinessError<OutOnlieModel>(errMsg, result).ToJson()
              : Resp.Success(result).ToJson();
                LogBLL.WriteLogInterface("login_name:" + login_name
                  + "   currency_code:" + currency_code
                   + "   rows:" + rows
                    + "   page:" + page
                     + "   electricity_supplier_name:" + electricity_supplier_name
                      + "   electricity_supplier_ad:" + electricity_supplier_ad
                       + "   electricity_supplier_code:" + electricity_supplier_code
                        + "   available_balance_begin:" + available_balance_begin
                         + "   available_balance_end:" + available_balance_end
                          + "   project_code:" + project_code
                           + "   project_name:" + project_name,
                  ResponseContent, "电商平台选择",
                    reqState, "共享平台", "电商资金", errMsg);
            }
        }
    }
}
