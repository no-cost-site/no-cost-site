using System;

namespace NoCostSite.ApiTests.Utils
{
    public class ValidationException : Exception
    {
        public ValidationException()
        {
            
        }

        public ValidationException(string message) : base(message)
        {
            
        }
    }
}