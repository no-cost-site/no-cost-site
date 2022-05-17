namespace NoCostSite.Api.Dto.Upload
{
    public class UploadUpsertFileResponse
    {
        public string FileId { get; set; } = null!;

        public static UploadUpsertFileResponse Ok(string fileId)
        {
            return new UploadUpsertFileResponse
            {
                FileId = fileId,
            };
        }
    }
}