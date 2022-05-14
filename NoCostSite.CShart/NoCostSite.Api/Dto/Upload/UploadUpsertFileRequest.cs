namespace NoCostSite.Api.Dto.Upload
{
    public class UploadUpsertFileRequest
    {
        public string Url { get; set; } = null!;

        public string FileName { get; set; } = null!;

        public byte[] Data { get; set; } = null!;
    }
}