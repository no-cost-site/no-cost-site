using System;
using System.Collections.Generic;

namespace NoCostSite.Utils
{
    public static class EnumerableExtensions
    {
        public static string Join(this IEnumerable<string> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }
}