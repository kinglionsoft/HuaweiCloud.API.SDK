using System;
using System.Runtime.Serialization;

namespace HuaweiCloud.API.SDK
{
    [Serializable]
    public class HuaweiException : Exception
    {
        public HuaweiException()
        {
        }

        public HuaweiException(string message) : base(message)
        {
        }

        public HuaweiException(string message, Exception inner) : base(message, inner)
        {
        }

        protected HuaweiException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}