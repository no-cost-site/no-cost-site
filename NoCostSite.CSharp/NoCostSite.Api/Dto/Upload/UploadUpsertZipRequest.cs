namespace NoCostSite.Api.Dto.Upload
{
    public class UploadUpsertZipRequest
    {
        public string Url { get; set; } = null!;

        public int[] Data { get; set; } = null!;
    }
}