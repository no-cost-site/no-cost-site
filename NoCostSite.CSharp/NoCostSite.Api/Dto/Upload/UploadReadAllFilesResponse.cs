using System.Linq;
using NoCostSite.BusinessLogic.ObjectStorage;

namespace NoCostSite.Api.Dto.Upload
{
    public class UploadReadAllFilesResponse
    {
        public FileItemDto[] Files { get; set; } = null!;

        public DirectoryDto Directory { get; set; } = null!;

        public static UploadReadAllFilesResponse Ok(ObjectStorageFileInfo[] pages)
        {
            var files = BuildFiles(pages);
            return new UploadReadAllFilesResponse
            {
                Files = files,
                Directory = BuildDirectory(files),
            };
        }

        private static FileItemDto[] BuildFiles(ObjectStorageFileInfo[] pages)
        {
            return pages
                .Select(x => new FileItemDto
                {
                    Id = x.Id,
                    Url = x.Directory.FullPath,
                    Name = x.Name,
                })
                .OrderBy(x => x.Name)
                .ToArray();
        }

        private static DirectoryDto BuildDirectory(FileItemDto[] files)
        {
            var urls = files
                .Select(x => x.Url)
                .ToArray();

            return DirectoryDto.Build(urls);
        }
    }
}