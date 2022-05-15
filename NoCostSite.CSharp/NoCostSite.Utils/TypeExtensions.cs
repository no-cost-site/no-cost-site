using System;
using System.Linq;

namespace NoCostSite.Utils
{
    public static class TypeExtensions
    {
        public static Type ResolveType(this Type type)
        {
            if (type.IsArray)
            {
                return type.GetElementType()!;
            }

            if (type.IsGenericType)
            {
                return type.GetGenericArguments().Single();
            }

            return type;
        }
    }
}