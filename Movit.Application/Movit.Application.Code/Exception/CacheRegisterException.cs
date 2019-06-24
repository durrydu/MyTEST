using System;

namespace Movit.Application.Code
{
    public class CacheRegisterException : MovitInfoException
    {
        public CacheRegisterException() { }

        public CacheRegisterException(string message) : base(message) { }

        public CacheRegisterException(string message, Exception inner) : base(message, inner) { }
    }
}
