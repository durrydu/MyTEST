using MessageFactory;
using Movit.Application.Busines.SystemManage;
using Movit.Application.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Movit.Sys.Api
{
    /// <summary>
    /// Movit_Commerce_Bpm 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Movit_Commerce_Bpm : System.Web.Services.WebService
    {

        #region 创建流程结束接口
        [WebMethod]
        //TEST
        public string UpdatePayType(string strJsonData)
        {
            System.Text.StringBuilder dd = new System.Text.StringBuilder(@"<?xml version='1.0'?>
  <DATA xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>   
   <ApplyTheme>申请主题</ApplyTheme>    <Comment>说明</Comment>    <ContractCode>123456</ContractCode>    
   <ContractName>北京地产保理公司+18.0000万元</ContractName>    <ContractDate>2018/7/28 0:00:00</ContractDate> 
      <TotalAccount>18.0000</TotalAccount>   
</DATA>  ");
            return dd.ToString();
        }

        /// <summary>
        /// 描述:创建流程结束接口
        /// 作者:姚栋
        /// 日期:20180619
        /// </summary>
        /// <param name="strBSID">业务编码</param>
        /// <param name="strBOID">业务系统在接口传入的业务对象ID</param>
        /// <param name="bSuccess">表示创建流程实例是否成功，true为成功，false为创建失败</param>
        /// <param name="iProcInstID">该业务对象对应的创建的流程实例ID。如果创建失败，则该值无效，置0，在业务系统表中增加一个字段，在进行流程记录查看时需要</param>
        /// <param name="strMessage">为K2接口提供的信息反馈</param>
        [WebMethod]
        public void CreateResult(string strBSID, string strBOID, bool bSuccess, int iProcInstID, string strMessage)
        {
            strBSID = strBSID + "_" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            string errMsg = string.Empty;
            int reqState = 1;
            try
            {
                if (!bSuccess)
                {
                    reqState = 0;
                    errMsg = "创建流程实例未成功!";
                    return;
                }
                MessageContext ctx = new MessageContext()
                {
                    bSuccess = bSuccess,
                    iProcInstID = iProcInstID,
                    strBOID = strBOID,
                    strBSID = strBSID,
                    strMessage = strMessage,
                    dtTime = DateTime.Now,
                    ApproveStatus = (int)ApproveStatus.in_approval
                };
                MessageHandlerBase handler = MessageHandlerFactory.GetMessageHandler(strBSID, ctx);
                handler.Execute();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                reqState = 0;

            }
            finally
            {
                LogBLL.WriteLogInterface("strBSID:" + strBSID
                    + "  strBOID:" + strBOID
                     + "  bSuccess:" + bSuccess
                      + "  iProcInstID:" + iProcInstID
                       + "  strMessage:" + strMessage,
                        "", "创建流程结束接口",
                      reqState, "BPM", "电商资金", errMsg, "WebService");
            }

        }
        #endregion
        #region 流程审批（撤回，退回）
        /// <summary>
        /// 流程审批（撤回，退回)
        /// 作者:姚栋
        /// 日期:20180619
        /// </summary>
        /// <param name="strBSID">业务编码</param>
        /// <param name="strBOID">业务系统在接口传入的业务对象ID</param>
        /// <param name="iProcInstID">该业务对象对应的创建的流程实例ID。如果创建失败，则该值无效，置0</param>
        /// <param name="strStepName">审批时的步骤名称</param>
        /// <param name="strApproverId">表示审批者用户ID</param>
        /// <param name="eAction">用户对流程实例的审批动作</param>
        /// <param name="strComment">用户的审批意见备注</param>
        /// <param name="dtTime">审批时间</param>
        [WebMethod]
        public void Rework(string strBSID, string strBOID, int iProcInstID,
            string strStepName, string strApproverId,
            UserAction eAction,
            string strComment,
            DateTime dtTime)
        {
            strBSID = strBSID + "_" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            string errMsg = string.Empty;
            int reqState = 1;
            try
            {
                MessageContext ctx = new MessageContext()
                {
                    iProcInstID = iProcInstID,
                    strBOID = strBOID,
                    strBSID = strBSID,
                    strComment = strComment,
                    dtTime = dtTime,
                    strStepName = strStepName,
                    eAction = (int)eAction,
                    ApproveStatus = (int)ApproveStatus.draft,
                    strApproverId = strApproverId
                };
                MessageHandlerBase handler = MessageHandlerFactory.GetMessageHandler(strBSID, ctx);
                handler.Execute();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                reqState = 0;

            }
            finally
            {

                LogBLL.WriteLogInterface("strBSID:" + strBSID
                     + "  strBOID:" + strBOID
                       + "  iProcInstID:" + iProcInstID
                        + "  strStepName:" + strStepName
                         + "  strApproverId:" + strApproverId
                          + "  eAction:" + eAction.ToString()
                           + "  strComment:" + strComment
                             + "  dtTime:" + dtTime.ToString(),
                         "", " 流程审批（撤回，退回)",
                       reqState, "BPM", "电商资金", errMsg, "WebService");
            }
        }
        #endregion
        #region 流程审批结束
        /// <summary>
        /// 作者:姚栋
        /// 日期:20180619
        /// </summary>
        /// <param name="strBSID">业务编码</param>
        /// <param name="strBOID">业务对象ID，主数据系统是GUID数据格式</param>
        /// <param name="iProcInstID">该业务对象对应的创建的流程实例ID。如果创建失败，则该值无效，置0</param>
        /// <param name="strStepName">审批时的步骤名称</param>
        /// <param name="eProcessInstanceResult">表示流程审批结果</param>
        /// <param name="strApproverId">表示审批者用户ID</param>
        /// <param name="strComment">为K2接口提供的信息反馈</param>
        /// <param name="dtTime">审批时间</param>
        [WebMethod]
        public void Close(string strBSID, string strBOID, int iProcInstID,
            string strStepName, ProcessInstanceStatus eProcessInstanceResult,
            string strApproverId, string strComment, DateTime dtTime)
        {
            strBSID = strBSID + "_" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            string errMsg = string.Empty;
            int reqState = 1;
            try
            {
                var _approveStatus = (int)ApproveStatus.approved;
                if (eProcessInstanceResult == ProcessInstanceStatus.Approved)
                {
                    _approveStatus = (int)ApproveStatus.approved;

                }
                else if (eProcessInstanceResult == ProcessInstanceStatus.Denied)
                {
                    _approveStatus = (int)ApproveStatus.unapproved;
                }
                MessageContext ctx = new MessageContext()
                {
                    iProcInstID = iProcInstID,
                    strBOID = strBOID,
                    strBSID = strBSID,
                    strComment = strComment,
                    dtTime = dtTime,
                    strStepName = strStepName,
                    eProcessInstanceResult = (int)eProcessInstanceResult,
                    ApproveStatus = _approveStatus,
                    strApproverId = strApproverId
                };
                MessageHandlerBase handler = MessageHandlerFactory.GetMessageHandler(strBSID, ctx);
                handler.Execute();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                reqState = 0;

            }
            finally
            {

                LogBLL.WriteLogInterface("strBSID:" + strBSID
                       + "  strBOID:" + strBOID
                         + "  iProcInstID:" + iProcInstID
                          + "  strStepName:" + strStepName
                           + "  strApproverId:" + strApproverId
                            + "  eProcessInstanceResult:" + eProcessInstanceResult.ToString()
                             + "  strComment:" + strComment
                               + "  dtTime:" + dtTime.ToString(),
                           "", " 流程审批结束",
                         reqState, "BPM", "电商资金", errMsg, "WebService");
            }
        }
        #endregion
        #region 获取表单数据的接口
        /// <summary>
        /// 主数据平台需提供获取表单数据的接口
        /// 作者:姚栋
        /// 日期:20180619
        /// </summary>
        /// <param name="strBSID">业务系统ID</param>
        /// <param name="strBOID">业务系统在接口传入的业务对象ID</param>
        /// <returns></returns>
        [WebMethod]
        public string GetInfo(string strBSID, string strBOID)
        {
            strBSID = strBSID + "_" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            string errMsg = string.Empty;
            int reqState = 1;
            string result = string.Empty;
            try
            {
                MessageContext ctx = new MessageContext()
                {

                    strBOID = strBOID,
                    strBSID = strBSID

                };
                MessageHandlerBase handler = MessageHandlerFactory.GetMessageHandler(strBSID, ctx);
                result = handler.Execute().ToString();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                reqState = 0;

            }
            finally
            {

                LogBLL.WriteLogInterface("strBSID:" + strBSID
                      + "  strBOID:" + strBOID,
                          result, " 获取表单数据",
                        reqState, "BPM", "电商资金", errMsg, "WebService");
            }
            return result;
        }
        #endregion



    }
}
