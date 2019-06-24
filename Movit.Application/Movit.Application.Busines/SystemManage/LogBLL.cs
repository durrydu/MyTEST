using Movit.Application.Code;
using Movit.Application.Entity.SystemManage;
using Movit.Application.IService.SystemManage;
using Movit.Application.Service.SystemManage;
using Movit.Util;
using Movit.Util.Log;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using Movit.Util.Extension;

namespace Movit.Application.Busines.SystemManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2016.1.8 9:56
    /// 描 述：系统日志
    /// </summary>
    public static class LogBLL
    {
        private static ILogService service = new LogService();


        #region 获取数据
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public static IEnumerable<LogEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 日志实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public static LogEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 发送邮件
        /// </summary>
        public static void SendMail(string body)
        {
            //try
            //{
            //    bool ErrorToMail = Config.GetValue("ErrorToMail").ToBool();
            //    if (ErrorToMail == true)
            //    {
            //        string SystemName = Config.GetValue("SystemName");//系统名称
            //        string ReceiverMail = Config.GetValue("ReceiverMail");//接收人地址

            //        MailHelper.Send(ReceiverMail, SystemName + " - 发生异常", body.Replace("-", ""));

            //    }
            //}
            //catch (Exception)
            //{
                
           
            //}
        }

        /// <summary>
        /// 接口调用日志
        /// </summary>
        /// <param name="RequestContent">入参</param>
        /// <param name="ResponseContent">出参</param>
        /// <param name="InterfaceName">接口名称</param>
        /// <param name="type">1:成功 -1失败</param>
        /// <param name="ReqisetSystem">调用系统</param>
        /// <param name="ReceivingSystem">接收系统</param>
        /// <param name="ErrMsg">错误信息</param>
        /// <param name="Url">请求地址</param>
        public static void WriteLogInterface(string RequestContent,
            string ResponseContent,
            string InterfaceName,
            int type,
            string ReqisetSystem,
            string ReceivingSystem,
            string ErrMsg,
            string Method = "",
            string Url = ""
            )
        {
            try
            {
                if (string.IsNullOrEmpty(Url))
                {
                    Url = Net.Url;
                }

                LogEntity logEntity = new LogEntity();
                logEntity.Module = InterfaceName;
                logEntity.CategoryId = 6;
                logEntity.IPAddress = Url;
                logEntity.Host = Method;//作为请求方式
                logEntity.OperateTime = DateTime.Now;
                logEntity.OperateTypeId = ((int)OperationType.Interface).ToString();
                logEntity.OperateType = EnumHelper.ToDescription(OperationType.Interface);
                logEntity.ExecuteResult = type;//1:成功 2:失败
                logEntity.ExecuteResultJson = RequestContent;//请求内容
                logEntity.IPAddressName = ResponseContent;//这里暂时作为  返回内容
                logEntity.Description = ErrMsg;
                logEntity.DeleteMark = 0;
                logEntity.SourceObjectId = ReqisetSystem;//请求系统
                logEntity.SourceContentJson = ReceivingSystem;//目标系统
                service.WriteLogInterface(logEntity);
                LogMessage logMessage = new LogMessage();
                logMessage.Url = Url;
                logMessage.Browser = ReqisetSystem;
                logMessage.Host = ReceivingSystem;
                logMessage.RemarkOne = RequestContent;
                logMessage.RemarkTwo = ResponseContent;
                logMessage.ExceptionRemark = ErrMsg;
                //if (type == 2)
                //{
                //    string strMessage = new LogFormat().InterfaceFormat(logMessage);
                //    SendMail(strMessage);
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="categoryId">日志分类Id</param>
        /// <param name="keepTime">保留时间段内</param>
        public static void RemoveLog(int categoryId, string keepTime)
        {
            try
            {
                service.RemoveLog(categoryId, keepTime);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logEntity">对象</param>
        public static void WriteLog(this LogEntity logEntity)
        {
            try
            {
                service.WriteLog(logEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        #endregion
    }
}
