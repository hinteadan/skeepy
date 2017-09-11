using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API
{
    public class SkeepyApiException : InvalidOperationException
    {
        public SkeepyApiException()
        {
        }

        public SkeepyApiException(string message) : base(message)
        {
        }

        public SkeepyApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SkeepyApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
