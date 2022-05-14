using System;
using System.Linq;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.Repository;

namespace NoCostSite.BusinessLogic.Settings
{
    public class SettingsRepository
    {
        private readonly IRepository<Storage<Settings>> _repository = new ObjectStorageRepository<Storage<Settings>>();

        public async Task Upsert(Settings settings)
        {
            var id = await GetOrCreateId();
            var storage = ToStorage(settings, id);
            await _repository.Upsert(storage);
        }

        public async Task<Settings> Read()
        {
            var storages = await _repository.ReadAll();
            return ToEntity(storages.Single());
        }

        private async Task<Guid> GetOrCreateId()
        {
            var storages = await _repository.ReadAll();
            return storages.Any() ? storages.Single().Id : Guid.NewGuid();
        }

        private Storage<Settings> ToStorage(Settings entity, Guid id)
        {
            return new Storage<Settings>
            {
                Id = id,
                Data = entity,
            };
        }

        private Settings ToEntity(Storage<Settings> storage) => storage.Data;
    }
}