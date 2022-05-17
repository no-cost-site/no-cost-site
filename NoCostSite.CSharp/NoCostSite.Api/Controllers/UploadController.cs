using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoCostSite.Api.Dto;
using NoCostSite.Api.Dto.Pages;
using NoCostSite.Api.Dto.Templates;
using NoCostSite.Api.Dto.Upload;
using NoCostSite.BusinessLogic.Pages;
using NoCostSite.BusinessLogic.Upload;
using NoCostSite.Function;

namespace NoCostSite.Api.Controllers
{
    public class UploadController : ControllerBase
    {
        private readonly UploadService _uploadService = new UploadService();

        public async Task<ResultResponse> UpsertPage(UploadUpsertPageRequest request)
        {
            await _uploadService.UpsertPage(request.PageId);
            return ResultResponse.Ok();
        }

        public async Task<ResultResponse> UpsertFile(UploadUpsertFileRequest request)
        {
            var data = request.Data.Select(x => (byte)x).ToArray();
            await _uploadService.UpsertFile(request.Url, request.FileName, data);
            return ResultResponse.Ok();
        }

        public async Task<ResultResponse> UpsertFileContent(UploadUpsertFileContentRequest request)
        {
            var data = Encoding.Default.GetBytes(request.Content);
            await _uploadService.UpsertFile(request.Url, request.FileName, data);
            return ResultResponse.Ok();
        }

        public async Task<ResultResponse> UpsertTemplate(UploadUpsertTemplateRequest request)
        {
            await _uploadService.UpsertTemplate(request.TemplateId);
            return ResultResponse.Ok();
        }

        public async Task<UploadReadFileResponse> ReadFile(UploadReadFileRequest request)
        {
            var file = await _uploadService.ReadFile(request.Url, request.FileName);
            return UploadReadFileResponse.Ok(file);
        }

        public async Task<UploadReadAllFilesResponse> ReadAllFiles()
        {
            var files = await _uploadService.ReadAllFiles();
            return UploadReadAllFilesResponse.Ok(files);
        }

        public async Task<ResultResponse> DeletePage(UploadDeletePageRequest request)
        {
            await _uploadService.DeletePage(request.PageId);
            return ResultResponse.Ok();
        }

        public async Task<ResultResponse> DeleteFile(UploadDeleteFileRequest request)
        {
            await _uploadService.DeleteFile(request.Url, request.FileName);
            return ResultResponse.Ok();
        }
    }
}