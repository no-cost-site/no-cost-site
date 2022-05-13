using System;
using System.Threading.Tasks;

namespace NoCostSite.BusinessLogic.Repository
{
    public interface IRepository<T> where T : IStorage
    {
        Task Upsert(T item);

        Task<T> Read(Guid id);

        Task<T[]> ReadAll();

        Task Delete(Guid id);
    }
}