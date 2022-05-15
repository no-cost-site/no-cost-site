using System.Collections.Generic;
using System.Linq;
using NoCostSite.BusinessLogic.ObjectStorage;

namespace NoCostSite.Api.Dto.Upload
{
    public class UploadReadAllFilesResponse
    {
        public FileItemDto[] Files { get; set; } = null!;

        public DirectoryDto[] Directories { get; set; } = null!;

        public static UploadReadAllFilesResponse Ok(ObjectStorageFileInfo[] pages)
        {
            var files = BuildFiles(pages);
            return new UploadReadAllFilesResponse
            {
                Files = files,
                Directories = BuildDirectories(files),
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

        private static DirectoryDto[] BuildDirectories(FileItemDto[] files)
        {
            var paths = files
                .Select(x => x.Url)
                .Distinct()
                .Select(x => x.Split("/"))
                .ToArray();

            return GetDirectories(paths, 0).ToArray();
        }

        private static IEnumerable<DirectoryDto> GetDirectories(string[][] paths, int index)
        {
            var indexPath = paths
                .Where(x => x.Length > index)
                .OrderBy(x => x[index]);
            
            foreach (var path in indexPath)
            {
                yield return new DirectoryDto
                {
                    Name = path[index],
                    Url = string.Join("/", path.Take(index + 1)),
                    Child = GetDirectories(paths, index + 1).ToArray(),
                };
            }
        }
    }
}