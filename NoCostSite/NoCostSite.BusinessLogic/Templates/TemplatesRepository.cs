using System;
using System.Linq;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.Repository;

namespace NoCostSite.BusinessLogic.Templates
{
    public class TemplatesRepository
    {
        private readonly IRepository<Storage<Template>> _repository = new ObjectStorageRepository<Storage<Template>>();

        public async Task Upsert(Template template)
        {
            var storage = ToStorage(template);
            await _repository.Upsert(storage);
        }

        public async Task<Template> Read(Guid id)
        {
            var storage =  await _repository.Read(id);
            return ToEntity(storage);
        }

        public async Task<Template[]> ReadAll()
        {
            var storages =  await _repository.ReadAll();
            return storages.Select(ToEntity).ToArray();
        }

        public async Task Delete(Guid id)
        {
            await _repository.Delete(id);
        }

        private Storage<Template> ToStorage(Template entity)
        {
            return new Storage<Template>
            {
                Id = entity.Id,
                Data = entity,
            };
        }

        private Template ToEntity(Storage<Template> storage) => storage.Data;
    }
}