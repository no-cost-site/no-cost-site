﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
                Content = ToStorage(item),
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

            return ToEntity(result);
        }

        public async Task<T[]> ReadAll()
        {
            using var client = _objectStorageClientFactory.Create(_bucketName);

            var resultList = await client.List(_directory);
            var resultReadMany = await client.ReadMany(resultList);

            return resultReadMany.Select(ToEntity).ToArray();
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

        private string ToStorage(T item)
        {
            var content = item.ToJson();
            return new Storage
            {
                Content = content,
                Signature = GetSignature(content),
            }.ToJson();
        }

        private T ToEntity(string storageJson)
        {
            var storage = storageJson.ToObject<Storage>();
            
            Assert.Validate(() => GetSignature(storage.Content) == storage.Signature, "Signature is not valid");

            return storage.Content.ToObject<T>();
        }
        
        private string Key => SettingsContainer.Current.DataBaseSecureKey;
        
        private string GetSignature(string content)
        {
            return Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.Default.GetBytes($"{content}{Key}")));
        }

        private class Storage
        {
            public string Content { get; set; } = null!;

            public string Signature { get; set; } = null!;
        }
    }
}