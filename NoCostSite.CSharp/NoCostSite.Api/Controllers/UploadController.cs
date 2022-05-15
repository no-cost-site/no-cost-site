﻿using System.Threading.Tasks;
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
        private readonly FilesUploader _filesUploader = new FilesUploader();

        public async Task<ResultResponse> UpsertPage(UploadUpsertPageRequest request)
        {
            await _filesUploader.UpsertPage(request.PageId);
            return ResultResponse.Ok();
        }
        
        public async Task<ResultResponse> UpsertFile(UploadUpsertFileRequest request)
        {
            await _filesUploader.UpsertFile(request.Url, request.FileName, request.Data);
            return ResultResponse.Ok();
        }
        
        public async Task<ResultResponse> UpsertTemplate(UploadUpsertTemplateRequest request)
        {
            await _filesUploader.UpsertTemplate(request.TemplateId);
            return ResultResponse.Ok();
        }
        
        public async Task<UploadReadAllFilesResponse> ReadAllFiles()
        {
            var files = await _filesUploader.ReadAllFiles();
            return UploadReadAllFilesResponse.Ok(files);
        }

        public async Task<ResultResponse> DeletePage(UploadDeletePageRequest request)
        {
            await _filesUploader.DeletePage(request.PageId);
            return ResultResponse.Ok();
        }
        
        public async Task<ResultResponse> DeleteFile(UploadDeleteFileRequest request)
        {
            await _filesUploader.DeleteFile(request.Url, request.FileName);
            return ResultResponse.Ok();
        }
    }
}