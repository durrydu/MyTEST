using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageFactory
{
    /// <summary>
    /// 
    /// </summary>
    internal class DefaultMessageHandler : MessageHandlerBase
    {
        public DefaultMessageHandler(MessageContext ctx) : base(ctx) { }

        public override object Execute()
        {
            return true;
        }

    }
}