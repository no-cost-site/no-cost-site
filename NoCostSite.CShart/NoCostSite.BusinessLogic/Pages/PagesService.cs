using System;
using System.Threading.Tasks;

namespace NoCostSite.BusinessLogic.Pages
{
    public class PagesService
    {
        private readonly PagesRepository _repository = new PagesRepository();
        
        public async Task Upsert(Page template)
        {
            await _repository.Upsert(template);
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
            await _repository.Delete(id);
        }
    }
}