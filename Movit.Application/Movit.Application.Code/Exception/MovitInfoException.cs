using Movit.Util.Log;
using System;

namespace Movit.Application.Code
{
    /// <summary>
    /// MovitException 提示异常
    /// </summary>
    public class MovitInfoException : ApplicationException
    {
        Log logg = LogFactory.GetLogger(typeof(MovitInfoException));
        public MovitInfoException()
        {
            
            //logg.Info(this.Message);
        }

        public MovitInfoException(string message)
            : base(message)
        {
            //logg.Info(message);
        }

        public MovitInfoException(string message, Exception inner)
            : base(message, inner)
        {
            //logg.Info(message);
        }

    }
}
