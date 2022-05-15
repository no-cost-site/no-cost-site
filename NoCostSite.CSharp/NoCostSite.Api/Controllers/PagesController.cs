using System.Threading.Tasks;
using NoCostSite.Api.Dto.Pages;
using NoCostSite.BusinessLogic.Pages;
using NoCostSite.Function;

namespace NoCostSite.Api.Controllers
{
    public class PagesController : ControllerBase
    {
        private readonly PagesService _pagesService = new PagesService();

        public async Task<ResultResponse> Upsert(PagesUpsertRequest request)
        {
            var page = new Page
            {
                Id = request.Page.Id,
                TemplateId = request.Page.TemplateId,
                Name = request.Page.Name,
                Url = request.Page.Url,
                Title = request.Page.Title,
                Description = request.Page.Description,
                Keywords = request.Page.Keywords,
                Content = request.Page.Content,
            };

            await _pagesService.Upsert(page);
            return ResultResponse.Ok();
        }
        
        public async Task<PagesReadAllResponse> ReadAll()
        {
            var pages = await _pagesService.ReadAll();
            return PagesReadAllResponse.Ok(pages);
        }

        public async Task<PagesReadResponse> Read(PagesReadRequest request)
        {
            var page = await _pagesService.Read(request.Id);
            return PagesReadResponse.Ok(page);
        }

        public async Task<ResultResponse> Delete(PagesDeleteRequest request)
        {
            await _pagesService.Delete(request.Id);
            return ResultResponse.Ok();
        }
    }
}