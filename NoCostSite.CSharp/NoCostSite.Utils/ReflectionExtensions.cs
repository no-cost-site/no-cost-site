using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NoCostSite.Utils
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<Type> GetAllTypes(this Type type)
        {
            return TypesProvider.GetAll(type);
        }

        public static IEnumerable<Type> GetAllTypes(this IEnumerable<Type> types)
        {
            return types.SelectMany(TypesProvider.GetAll);
        }

        private static class TypesProvider
        {
            private static readonly ConcurrentDictionary<Assembly, Type[]> Types =
                new ConcurrentDictionary<Assembly, Type[]>();

            public static Type[] GetAll(Type type)
            {
                return Types.GetOrAdd(type.Assembly, x => x.GetTypes());
            }
        }
    }
}