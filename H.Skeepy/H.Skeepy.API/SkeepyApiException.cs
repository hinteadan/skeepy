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
        private string displayMessage;

        public string DisplayMessage { get { return displayMessage; } }

        public SkeepyApiException()
        {
        }

        public SkeepyApiException(string message) : base(message)
        {
            WithDisplayMessage(message);
        }

        public SkeepyApiException(string message, Exception innerException) : base(message, innerException)
        {
            WithDisplayMessage(message);
        }

        protected SkeepyApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public SkeepyApiException WithDisplayMessage(string message)
        {
            displayMessage = message;
            return this;
        }
    }
}
