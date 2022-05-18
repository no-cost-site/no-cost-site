using System;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.UpdateFilters;

namespace NoCostSite.BusinessLogic.Templates
{
    public class TemplatesService : ServiceBase<Template>
    {
        private readonly TemplatesRepository _repository = new TemplatesRepository();

        public async Task Upsert(Template template)
        {
            await Upsert(template, x => _repository.Upsert(x));
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
            var template = await Read(id);
            await Delete(template, _ => _repository.Delete(id));
        }
    }
}