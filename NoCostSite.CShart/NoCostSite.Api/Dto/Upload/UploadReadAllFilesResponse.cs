using System.Linq;
using NoCostSite.BusinessLogic.ObjectStorage;

namespace NoCostSite.Api.Dto.Upload
{
    public class UploadReadAllFilesResponse
    {
        public FileItemDto[] Items { get; set; } = null!;

        public static UploadReadAllFilesResponse Ok(ObjectStorageFileInfo[] pages)
        {
            return new UploadReadAllFilesResponse
            {
                Items = pages
                    .Select(x => new FileItemDto
                    {
                        Url = x.Directory.FullPath,
                        Name = x.Name,
                    })
                    .ToArray(),
            };
        }
    }
}