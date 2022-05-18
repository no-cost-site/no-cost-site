using System.Threading.Tasks;

namespace NoCostSite.BusinessLogic.UpdateFilters
{
    public interface IUpdateFilter<in T>
    {
        Task BeforeUpsert(T item);

        Task AfterUpsert(T item);

        Task BeforeDelete(T item);

        Task AfterDelete(T item);
    }
}