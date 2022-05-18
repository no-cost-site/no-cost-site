using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoCostSite.Utils;

namespace NoCostSite.BusinessLogic.UpdateFilters
{
    public static class UpdateFilters
    {
        private static readonly Dictionary<Type, Func<object>[]> Filters = new Dictionary<Type, Func<object>[]>();

        public static void Init(params (Type Type, Func<object> Create)[] filters)
        {
            if (Filters.Any())
            {
                throw new Exception("Filters already configured");
            }

            filters
                .GroupBy(x => x.Type)
                .ForEach(x => Filters[x.Key] = x.Select(f => f.Create).ToArray());
        }

        public static async Task BeforeUpsert<T>(T item) => await Execute<T>(x => x.BeforeUpsert(item));

        public static async Task AfterUpsert<T>(T item) => await Execute<T>(x => x.AfterUpsert(item));

        public static async Task BeforeDelete<T>(T item) => await Execute<T>(x => x.BeforeDelete(item));

        public static async Task AfterDelete<T>(T item) => await Execute<T>(x => x.AfterDelete(item));

        private static async Task Execute<T>(Func<IUpdateFilter<T>, Task> action)
        {
            foreach (var filter in CreateFilters<T>())
            {
                await action(filter);
            }
        }

        private static IUpdateFilter<T>[] CreateFilters<T>()
        {
            return Filters.TryGetValue(typeof(T), out var filter)
                ? filter.Select(x => (IUpdateFilter<T>) x()).ToArray()
                : Array.Empty<IUpdateFilter<T>>();
        }
    }
}