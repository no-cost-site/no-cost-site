using NoCostSite.BusinessLogic.ObjectStorage;

namespace NoCostSite.Api.Dto.Upload
{
    public class UploadReadFileResponse
    {
        public FileDto File { get; set; } = null!;

        public static UploadReadFileResponse Ok(ObjectStorageFile file)
        {
            return new UploadReadFileResponse
            {
                File = new FileDto
                {
                    Id = file.Info.Id,
                    Url = file.Info.Directory.FullPath,
                    Name = file.Info.Name,
                    Content = file.Content,
                }
            };
        }
    }
}