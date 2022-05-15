namespace NoCostSite.Api.Dto.Upload
{
    public class UploadUpsertFileContentRequest
    {
        public string Url { get; set; } = null!;

        public string FileName { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}