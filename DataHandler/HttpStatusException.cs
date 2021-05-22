using System;
using System.Runtime.Serialization;

namespace DataHandler
{
    [Serializable]
    public class HttpStatusException : Exception
    {
        public HttpStatusException()
        {
        }

        public HttpStatusException(string message) : base(message)
        {
        }

        public HttpStatusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}