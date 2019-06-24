using Movit.Application.Busines.SystemManage;
using Movit.Application.Code;
using Movit.Application.Entity.SystemManage;
using Movit.Util;
using Movit.Util.Attributes;
using Movit.Util.Extension;
using Movit.Util.Log;
using Movit.Util.WebControl;
using System;
using System.Web;
using System.Web.Mvc;

namespace BaoLi.Application.Web
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人：姚栋 
    /// 日 期：2018.11.9 10:45
    /// 描 述：错误日志（Controller发生异常时会执行这里） 
    /// </summary>
    public class HandlerErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 控制器方法中出现异常，会调用该方法捕获异常
        /// </summary>
        /// <param name="context">提供使用</param>
        public override void OnException(ExceptionContext context)
        {
            var errType = context.Exception.GetType();
            var errMsg = context.Exception.Message; ;
            if (typeof(MovitInfoException) != errType)
            {
                errMsg = "系统异常请联系管理员!";
                WriteLog(context);
            }
            base.OnException(context);
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = 200;
            context.Result = new ContentResult { Content = new AjaxResult { type = ResultType.error, message = errMsg }.ToJson() };
        }
        protected Exception GerInnerException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return this.GerInnerException(ex.InnerException);
            }
            return ex;
        }
        /// <summary>
        /// 写入日志（log4net）
        /// </summary>
        /// <param name="context">提供使用</param>
        private void WriteLog(ExceptionContext context)
        {
            string userCode = string.Empty;
            string userId = string.Empty;
            if (context == null)
            {
                return;
            }

            if (OperatorProvider.Provider.IsOverdue())
            {
                userCode = "System";
                userId = "System";
            }
            else
            {
                userCode = OperatorProvider.Provider.Current().Account + "（" + OperatorProvider.Provider.Current().UserName + "）";
                userId = OperatorProvider.Provider.Current().UserId;
            }
            var log = LogFactory.GetLogger(context.Controller.ToString());
            Exception Error = GerInnerException(context.Exception);
            LogMessage logMessage = new LogMessage();
            logMessage.OperationTime = DateTime.Now;
            logMessage.Url = HttpContext.Current.Request.RawUrl;
            logMessage.Class = context.Controller.ToString();
            logMessage.Ip = Net.Ip;
            logMessage.Host = Net.Host;
            logMessage.Browser = Net.Browser;
            logMessage.UserName = userCode;
            if (Error.InnerException == null)
            {
                logMessage.ExceptionInfo = Error.Message;
            }
            else
            {
                logMessage.ExceptionInfo = Error.InnerException.Message;
            }
            logMessage.ExceptionSource = Error.Source;
            logMessage.ExceptionRemark = Error.StackTrace;
            string strMessage = new LogFormat().ExceptionFormat(logMessage);
            log.Error(strMessage);

            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 4;
            logEntity.OperateTypeId = ((int)OperationType.Exception).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Exception);
            logEntity.OperateAccount = logMessage.UserName;
            logEntity.OperateUserId = userId;
            logEntity.ExecuteResult = -1;
            logEntity.ExecuteResultJson = strMessage;
            logEntity.WriteLog();
            //LogBLL.SendMail(strMessage);

        }
      
    }
}