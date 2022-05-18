using System;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.UpdateFilters;

namespace NoCostSite.BusinessLogic.Pages
{
    public class PagesService : ServiceBase<Page>
    {
        private readonly PagesRepository _repository = new PagesRepository();
        
        public async Task Upsert(Page page)
        {
            await Upsert(page, x => _repository.Upsert(x));
        }

        public async Task<Page> Read(Guid id)
        {
            return await _repository.Read(id);
        }

        public async Task<Page[]> ReadAll()
        {
            return await _repository.ReadAll();
        }

        public async Task Delete(Guid id)
        {
            var page = await Read(id);
            await Delete(page, _ => _repository.Delete(id));
        }
    }
}