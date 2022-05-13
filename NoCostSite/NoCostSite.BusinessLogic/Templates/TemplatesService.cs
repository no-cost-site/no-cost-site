using System;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.FilesUpload;
using NoCostSite.BusinessLogic.Pages;
using NoCostSite.Utils;

namespace NoCostSite.BusinessLogic.Templates
{
    public class TemplatesService
    {
        private readonly TemplatesRepository _repository = new TemplatesRepository();
        
        public async Task Upsert(Template template)
        {
            Validate();
            
            await _repository.Upsert(template);

            void Validate()
            {
                Assert.Validate(() => template.Content.Contains(nameof(Page.Content).AsTag()), $"Template must contain tag {nameof(Page.Content).AsTag()}");
            }
        }

        public async Task<Template> Read(Guid id)
        {
            return await _repository.Read(id);
        }

        public async Task<Template[]> ReadAll()
        {
            return await _repository.ReadAll();
        }

        public async Task Delete(Guid id)
        {
            await _repository.Delete(id);
        }
    }
}