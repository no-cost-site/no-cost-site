using System;
using System.Threading.Tasks;

namespace NoCostSite.Utils
{
    public static class Assert
    {
        public static void NotNull(object @object)
        {
            if (@object == null)
            {
                throw new NullReferenceException();
            }
        }

        public static void Validate(Func<bool> isValid, string message)
        {
            if (!isValid())
            {
                throw new ValidationException(message);
            }
        }

        public static async Task ValidateAsync(Func<Task<bool>> isValid, string message)
        {
            if (!await isValid())
            {
                throw new ValidationException(message);
            }
        }
    }
}