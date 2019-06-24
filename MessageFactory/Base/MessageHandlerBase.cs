using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageFactory
{
    /// <summary>
    /// 描述:消息处理基类
    /// 作者:姚栋
    /// 日期:21080619
    /// </summary>
    public abstract class MessageHandlerBase
    {
        protected readonly MessageContext _context;


        public MessageHandlerBase(MessageContext ctx)
        {
            this._context = ctx;
        }

        public abstract object Execute();


    }
}
