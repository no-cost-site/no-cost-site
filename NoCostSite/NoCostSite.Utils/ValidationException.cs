using System;

namespace NoCostSite.Utils
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