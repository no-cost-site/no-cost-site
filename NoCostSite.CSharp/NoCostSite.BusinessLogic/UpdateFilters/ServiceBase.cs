using System;
using System.Threading.Tasks;

namespace NoCostSite.BusinessLogic.UpdateFilters
{
    public class ServiceBase<T>
    {
       protected async Task Upsert(T item, Func<T, Task> action)
        {
            await Execute(x => x.BeforeUpsert(item));
            await action(item);
            await Execute(x => x.AfterUpsert(item));
        }
        
        protected async Task Delete(T item, Func<T, Task> action)
        {
            await Execute(x => x.BeforeDelete(item));
            await action(item);
            await Execute(x => x.AfterDelete(item));
        }

        private async Task Execute(Func<IUpdateFilter<T>, Task> action)
        {
            foreach (var filter in UpdateFiltersFactory.CreateFilters<T>())
            {
                await action(filter);
            }
        }
    }
}