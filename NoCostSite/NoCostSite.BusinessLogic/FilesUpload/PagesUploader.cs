using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.Pages;
using NoCostSite.BusinessLogic.Settings;
using NoCostSite.BusinessLogic.Templates;
using NoCostSite.ObjectStorage.Client;

namespace NoCostSite.BusinessLogic.FilesUpload
{
    public class PagesUploader
    {
        private readonly string _bucketName;
        private readonly ObjectStorageClientFactory _objectStorageClientFactory = new ObjectStorageClientFactory();
        private readonly PagesService _pagesService = new PagesService();
        private readonly TemplatesService _templatesService = new TemplatesService();

        public PagesUploader()
        {
            _bucketName = SettingsContainer.Current.PublicBucketName;
        }

        public async Task Upsert(Guid pageId)
        {
            var page = await _pagesService.Read(pageId);
            var template = await _templatesService.Read(page.TemplateId);

            var file = CreateFile(page, template);

            using var client = _objectStorageClientFactory.Create(_bucketName);
            await client.Upsert(file);
        }

        public async Task Upsert(string url, string fileName, byte[] data)
        {
            var file = new ObjectStorageFile
            {
                Info = new ObjectStorageFileInfo
                {
                    Directory = new ObjectStorageDirectory(url),
                    Name = fileName,
                },
                Content = Encoding.Default.GetString(data),
            };

            using var client = _objectStorageClientFactory.Create(_bucketName);
            await client.Upsert(file);
        }

        public async Task UpsertTemplate(Guid templateId)
        {
            var pages = await _pagesService.ReadAll();
            var template = await _templatesService.Read(templateId);

            var files = pages
                .Where(x => x.TemplateId == templateId)
                .Select(x => CreateFile(x, template))
                .ToArray();

            using var client = _objectStorageClientFactory.Create(_bucketName);
            await client.UpsertMany(files);
        }

        public async Task Delete(Guid pageId)
        {
            var page = await _pagesService.Read(pageId);

            var fileInfo = CreateFileInfo(page);

            using var client = _objectStorageClientFactory.Create(_bucketName);
            await client.Delete(fileInfo);
        }

        public async Task<ObjectStorageFileInfo[]> ReadAll()
        {
            using var client = _objectStorageClientFactory.Create(_bucketName);
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
    }
}