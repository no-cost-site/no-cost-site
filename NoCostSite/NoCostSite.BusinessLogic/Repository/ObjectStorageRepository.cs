using System;
using System.Linq;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.Settings;
using NoCostSite.ObjectStorage.Client;
using NoCostSite.Utils;

namespace NoCostSite.BusinessLogic.Repository
{
    public class ObjectStorageRepository<T> : IRepository<T> where T : IStorage
    {
        private readonly string _bucketName;
        private readonly ObjectStorageDirectory _directory;
        private readonly ObjectStorageClientFactory _objectStorageClientFactory = new ObjectStorageClientFactory();

        public ObjectStorageRepository()
        {
            _bucketName = SettingsContainer.Current.PrivateBucketName;
            _directory = new ObjectStorageDirectory(typeof(T).Name);
        }

        public async Task Upsert(T item)
        {
            var file = new ObjectStorageFile
            {
                Info = new ObjectStorageFileInfo
                {
                    Directory = _directory,
                    Name = item.Id.ToString(),
                },
                Content = item.ToJson(),
            };

            using var client = _objectStorageClientFactory.Create(_bucketName);
            await client.Upsert(file);
        }

        public async Task<T> Read(Guid id)
        {
            var fileInfo = new ObjectStorageFileInfo
            {
                Directory = _directory,
                Name = id.ToString(),
            };

            using var client = _objectStorageClientFactory.Create(_bucketName);
            var result = await client.Read(fileInfo);

            return result.ToObject<T>();
        }

        public async Task<T[]> ReadAll()
        {
            using var client = _objectStorageClientFactory.Create(_bucketName);

            var resultList = await client.List(_directory);
            var resultReadMany = await client.ReadMany(resultList);

            return resultReadMany.Select(x => x.ToObject<T>()).ToArray();
        }

        public async Task Delete(Guid id)
        {
            var fileInfo = new ObjectStorageFileInfo
            {
                Directory = _directory,
                Name = id.ToString(),
            };

            using var client = _objectStorageClientFactory.Create(_bucketName);
            await client.Delete(fileInfo);
        }
    }
}