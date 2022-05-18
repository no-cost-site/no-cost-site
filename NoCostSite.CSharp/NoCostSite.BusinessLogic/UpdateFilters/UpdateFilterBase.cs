using System.Threading.Tasks;

namespace NoCostSite.BusinessLogic.UpdateFilters
{
    public abstract class UpdateFilterBase<T> : IUpdateFilter<T>
    {
        public virtual Task BeforeUpsert(T item) => Task.CompletedTask;

        public virtual Task AfterUpsert(T item) => Task.CompletedTask;

        public virtual Task BeforeDelete(T item) => Task.CompletedTask;

        public virtual Task AfterDelete(T item) => Task.CompletedTask;
    }
}