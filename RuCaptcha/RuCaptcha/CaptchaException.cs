using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuCaptcha
{
    [System.Serializable]
    public class CaptchaException : Exception
    {
        public CaptchaException() { }
        public CaptchaException(string message) : base(message) { }
        public CaptchaException(string message, Exception inner) : base(message, inner) { }
        protected CaptchaException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

