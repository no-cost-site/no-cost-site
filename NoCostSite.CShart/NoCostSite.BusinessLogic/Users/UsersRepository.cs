using System;
using System.Linq;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.Repository;

namespace NoCostSite.BusinessLogic.Users
{
    public class UsersRepository
    {
        private readonly IRepository<Storage<User>> _repository = new ObjectStorageRepository<Storage<User>>();

        public async Task Upsert(User user)
        {
            var id = await GetOrCreateId();
            var storage = ToStorage(user, id);
            await _repository.Upsert(storage);
        }

        public async Task<User?> TryRead()
        {
            var storages = await _repository.ReadAll();
            return storages.Any() ? ToEntity(storages.Single()) : null;
        }

        private async Task<Guid> GetOrCreateId()
        {
            var storages = await _repository.ReadAll();
            return storages.Any() ? storages.Single().Id : Guid.NewGuid();
        }

        private Storage<User> ToStorage(User entity, Guid id)
        {
            return new Storage<User>
            {
                Id = id,
                Data = entity,
            };
        }

        private User ToEntity(Storage<User> storage) => storage.Data;
    }
}