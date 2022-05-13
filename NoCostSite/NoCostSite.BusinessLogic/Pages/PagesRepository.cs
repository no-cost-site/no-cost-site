using System;
using System.Linq;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.Repository;

namespace NoCostSite.BusinessLogic.Pages
{
    public class PagesRepository
    {
        private readonly IRepository<Storage<Page>> _repository = new ObjectStorageRepository<Storage<Page>>();

        public async Task Upsert(Page template)
        {
            var storage = ToStorage(template);
            await _repository.Upsert(storage);
        }

        public async Task<Page> Read(Guid id)
        {
            var storage =  await _repository.Read(id);
            return ToEntity(storage);
        }

        public async Task<Page[]> ReadAll()
        {
            var storages =  await _repository.ReadAll();
            return storages.Select(ToEntity).ToArray();
        }

        public async Task Delete(Guid id)
        {
            await _repository.Delete(id);
        }

        private Storage<Page> ToStorage(Page entity)
        {
            return new Storage<Page>
            {
                Id = entity.Id,
                Data = entity,
            };
        }

        private Page ToEntity(Storage<Page> storage) => storage.Data;
    }
}