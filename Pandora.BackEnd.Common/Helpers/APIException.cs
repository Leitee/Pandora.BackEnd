using System;
using System.Net;
using System.Runtime.Serialization;

namespace Pandora.BackEnd.Common.Helpers
{
    [Serializable]
    public class APIException : Exception
    {

        HttpStatusCode statusCode;
        string reasonPhrase = string.Empty;

        public HttpStatusCode StatusCode
        {
            get { return statusCode; }
            set { statusCode = value; }
        }

        public string ReasonPhrase
        {
            get { return reasonPhrase; }
            set { reasonPhrase = value; }
        }

        public APIException()
          : base()
        {
        }

        public APIException(string message)
            : base(message)
        {
        }

        public APIException(string message, string apiReason, HttpStatusCode statusCode)
          : base(message)
        {
            this.reasonPhrase = apiReason;
            this.statusCode = statusCode;
        }

        public APIException(string message, Exception inner)
            : base(message, inner)
        {
        }
        protected APIException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
