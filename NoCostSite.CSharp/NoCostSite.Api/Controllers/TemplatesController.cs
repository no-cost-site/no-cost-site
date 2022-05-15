using System.Threading.Tasks;
using NoCostSite.Api.Dto.Templates;
using NoCostSite.BusinessLogic.Templates;
using NoCostSite.Function;

namespace NoCostSite.Api.Controllers
{
    public class TemplatesController : ControllerBase
    {
        private readonly TemplatesService _templatesService = new TemplatesService();

        public async Task<ResultResponse> Upsert(TemplatesUpsertRequest request)
        {
            var page = new Template
            {
                Id = request.Template.Id,
                Name = request.Template.Name,
                Content = request.Template.Content,
            };

            await _templatesService.Upsert(page);
            return ResultResponse.Ok();
        }
        
        public async Task<TemplatesReadAllResponse> ReadAll()
        {
            var pages = await _templatesService.ReadAll();
            return TemplatesReadAllResponse.Ok(pages);
        }

        public async Task<TemplatesReadResponse> Read(TemplatesReadRequest request)
        {
            var page = await _templatesService.Read(request.Id);
            return TemplatesReadResponse.Ok(page);
        }

        public async Task<ResultResponse> Delete(TemplatesDeleteRequest request)
        {
            await _templatesService.Delete(request.Id);
            return ResultResponse.Ok();
        }
    }
}