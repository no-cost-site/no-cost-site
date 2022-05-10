using System;

namespace NoCostSite.Api.CSharp.Function
{
    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }

        public Error(Exception exception)
        {
            Message = exception.Message;
            StackTrace = exception.StackTrace;
        }

        public string? Message { get; }
        
        public string? StackTrace { get; }
    }
}