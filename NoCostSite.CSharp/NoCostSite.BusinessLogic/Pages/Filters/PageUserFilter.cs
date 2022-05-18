using System;
using System.Linq;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.Templates;
using NoCostSite.BusinessLogic.UpdateFilters;
using NoCostSite.BusinessLogic.Upload;
using NoCostSite.BusinessLogic.Users;

namespace NoCostSite.BusinessLogic.Pages.Filters
{
    public class PageUserFilter : UpdateFilterBase<User>
    {
        private readonly PagesService _pagesService = new PagesService();
        private readonly TemplatesService _templatesService = new TemplatesService();
        private readonly UploadService _uploadService = new UploadService();
        
        public override Task AfterUpsert(User item) => CreateDefaultPageIfNoPages();

        private async Task CreateDefaultPageIfNoPages()
        {
            var pagesCount = await _pagesService.Count();
            if (pagesCount > 0)
            {
                return;
            }

            var templateId = await GetOrCreateTemplateId();
            
            var page = new Page
            {
                Id = Guid.NewGuid(),
                TemplateId = templateId,
                Name = "index.html",
                Url = "",
                Title = "",
                Description = "",
                Keywords = "",
                Content = "",
            };
            
            await _pagesService.Upsert(page);
            await _uploadService.UpsertPage(page.Id);
        }

        private async Task<Guid> GetOrCreateTemplateId()
        {
            var templates = await _templatesService.ReadAll();
            if (templates.Any())
            {
                return templates.First().Id;
            }
            
            var template = new Template
            {
                Id = Guid.NewGuid(),
                Name = "Default template",
                Content = "<!-- Content -->",
            };
            
            await _templatesService.Upsert(template);
            return template.Id;
        }
    }
}