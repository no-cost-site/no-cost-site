using System;
using System.Threading.Tasks;

namespace NoCostSite.BusinessLogic.Templates
{
    public class TemplatesService
    {
        private readonly TemplatesRepository _repository = new TemplatesRepository();
        
        public async Task Upsert(Template template)
        {
            await _repository.Upsert(template);
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