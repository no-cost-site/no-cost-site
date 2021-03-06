using System;
using System.Linq;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.Config;
using NoCostSite.BusinessLogic.ObjectStorage;
using NoCostSite.BusinessLogic.Pages;
using NoCostSite.BusinessLogic.Templates;

namespace NoCostSite.BusinessLogic.Upload
{
    public class UploadService
    {
        private readonly ObjectStorageClientFactory _objectStorageClientFactory = new ObjectStorageClientFactory();
        private readonly PagesService _pagesService = new PagesService();
        private readonly TemplatesService _templatesService = new TemplatesService();
        private readonly ZipService _zipService = new ZipService();

        public async Task UpsertPage(Guid pageId)
        {
            var page = await _pagesService.Read(pageId);
            var template = await _templatesService.Read(page.TemplateId);

            var file = CreateFile(page, template);

            using var client = _objectStorageClientFactory.Create(BucketName);
            await client.Upsert(file);
        }

        public async Task<string> UpsertFile(string url, string fileName, byte[] data)
        {
            var file = new ObjectStorageFileData
            {
                Info = new ObjectStorageFileInfo
                {
                    Directory = new ObjectStorageDirectory(url),
                    Name = fileName,
                },
                Data = data,
            };

            using var client = _objectStorageClientFactory.Create(BucketName);
            await client.Upsert(file);

            return file.Info.Id;
        }
        
        public async Task<string> UpsertFile(string url, string fileName, string content)
        {
            var file = new ObjectStorageFile
            {
                Info = new ObjectStorageFileInfo
                {
                    Directory = new ObjectStorageDirectory(url),
                    Name = fileName,
                },
                Content = content,
            };

            using var client = _objectStorageClientFactory.Create(BucketName);
            await client.Upsert(file);

            return file.Info.Id;
        }

        public async Task UpsertZip(string url, byte[] zip)
        {
            var directory = new ObjectStorageDirectory(url);
            var files = _zipService.UnZip(zip, directory);
            
            using var client = _objectStorageClientFactory.Create(BucketName);
            await client.UpsertMany(files);
        }

        public async Task UpsertTemplate(Guid templateId)
        {
            var pages = await _pagesService.ReadAll();
            var template = await _templatesService.Read(templateId);

            var files = pages
                .Where(x => x.TemplateId == templateId)
                .Select(x => CreateFile(x, template))
                .ToArray();

            using var client = _objectStorageClientFactory.Create(BucketName);
            await client.UpsertMany(files);
        }

        public async Task DeletePage(Guid pageId)
        {
            var page = await _pagesService.Read(pageId);

            var fileInfo = CreateFileInfo(page);

            using var client = _objectStorageClientFactory.Create(BucketName);
            await client.Delete(fileInfo);
        }
        
        public async Task DeleteFile(string url, string fileName)
        {
            var fileInfo = new ObjectStorageFileInfo
            {
                Directory = new ObjectStorageDirectory(url),
                Name = fileName,
            };

            using var client = _objectStorageClientFactory.Create(BucketName);
            await client.Delete(fileInfo);
        }
        
        public async Task<ObjectStorageFile> ReadFile(string url, string fileName)
        {
            var fileInfo = new ObjectStorageFileInfo
            {
                Directory = new ObjectStorageDirectory(url),
                Name = fileName,
            };

            using var client = _objectStorageClientFactory.Create(BucketName);
            var content = await client.Read(fileInfo);

            return new ObjectStorageFile
            {
                Info = fileInfo,
                Content = content,
            };
        }

        public async Task<ObjectStorageFileInfo[]> ReadAllFiles()
        {
            using var client = _objectStorageClientFactory.Create(BucketName);
            return await client.List(ObjectStorageDirectory.Root);
        }

        private ObjectStorageFile CreateFile(Page page, Template template)
        {
            return new ObjectStorageFile
            {
                Info = CreateFileInfo(page),
                Content = PagesBuilder.Build(page, template),
            };
        }

        private ObjectStorageFileInfo CreateFileInfo(Page page)
        {
            return new ObjectStorageFileInfo
            {
                Directory = new ObjectStorageDirectory(page.Url),
                Name = "index.html"
            };
        }
        
        private string BucketName => ConfigContainer.Current.PublicBucketName;
    }
}