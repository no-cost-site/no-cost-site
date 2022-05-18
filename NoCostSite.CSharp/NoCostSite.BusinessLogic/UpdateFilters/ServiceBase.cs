using System;
using System.Threading.Tasks;

namespace NoCostSite.BusinessLogic.UpdateFilters
{
    public class ServiceBase<T>
    {
       protected async Task Upsert(T item, Func<T, Task> action)
        {
            await UpdateFilters.BeforeUpsert(item);
            await action(item);
            await UpdateFilters.AfterUpsert(item);
        }
        
        protected async Task Delete(T item, Func<T, Task> action)
        {
            await UpdateFilters.BeforeDelete(item);
            await action(item);
            await UpdateFilters.AfterDelete(item);
        }
    }
}