using System.Linq;
using NoCostSite.Utils;

namespace NoCostSite.BusinessLogic.UpdateFilters
{
    public static class UpdateFiltersFactory
    {
        public static IUpdateFilter<T>[] CreateFilters<T>()
        {
            var type = typeof(IUpdateFilter<>).MakeGenericType(typeof(T));
            return type
                .GetAllTypes()
                .Where(x => x.HasInterface(type))
                .Select(x => x.CreateInstance<IUpdateFilter<T>>())
                .ToArray();
        }
    }
}