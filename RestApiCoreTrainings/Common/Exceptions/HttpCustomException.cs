using System;

namespace Common.Exceptions
{
    public class HttpCustomException : ApplicationException
    {
        public int StatusCode { get; }

        public HttpCustomException(int statusCode) : this("Problematic status code", statusCode)
        {
        }

        public HttpCustomException(string message, int statusCode) : this(message, null, statusCode)
        {
        }

        public HttpCustomException(string message, Exception exception, int statusCode) : base(message, exception)
        {
            StatusCode = statusCode;
        }
    }
}
