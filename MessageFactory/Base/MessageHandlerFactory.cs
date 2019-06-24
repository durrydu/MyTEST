using Movit.Application.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Movit.Util;

namespace MessageFactory
{
    /// <summary>
    /// 描述:消息处理工厂
    /// 作者:姚栋
    /// 日期:20180619
    /// </summary>
    public class MessageHandlerFactory
    {
        private static readonly Dictionary<string, Func<MessageContext, MessageHandlerBase>> _dict;

        static MessageHandlerFactory()
        {
            _dict = new Dictionary<string, Func<MessageContext, MessageHandlerBase>>();
            _dict[MessageTypeEnum.EC_Contract_Add_CreateResult.ToString()] = p => new EC_Contract_Add_CreateResultMessageHandler(p);
            _dict[MessageTypeEnum.EC_Income_CreateResult.ToString()] = p => new EC_Income_CreateResultMessageHandler(p);
            _dict[MessageTypeEnum.EC_Contract_Add_Rework.ToString()] = p => new EC_Contract_Add_ReworkMessageHandler(p);
            _dict[MessageTypeEnum.EC_Income_Rework.ToString()] = p => new EC_Income_ReworkMessageHandler(p);
            _dict[MessageTypeEnum.EC_Contract_Add_Close.ToString()] = p => new EC_Contract_Add_CloseMessageHandler(p);
            _dict[MessageTypeEnum.EC_Income_Close.ToString()] = p => new EC_Income_CloseMessageHandler(p);
            _dict[MessageTypeEnum.EC_Contract_Add_GetInfo.ToString()] = p => new EC_Contract_Add_GetInfoMessageHandler(p);
            _dict[MessageTypeEnum.EC_Income_GetInfo.ToString()] = p => new EC_Income_GetInfoMessageHandler(p);
            //……其它的后续补充
        }

        public static MessageHandlerBase GetMessageHandler(string MessageType, MessageContext ctx)
        {

            MessageHandlerBase result = null;
            if (_dict.ContainsKey(MessageType))
            {
                result = _dict[MessageType](ctx);
            }
            else
            {
                result = new DefaultMessageHandler(ctx);
            }
            return result;
        }
    }
}