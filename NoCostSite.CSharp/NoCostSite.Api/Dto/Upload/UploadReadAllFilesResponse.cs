using System;
using System.Collections.Generic;
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
            var paths = files
                .Select(x => x.Url)
                .Distinct()
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Split("/"))
                .ToArray();

            return new DirectoryDto
            {
                Name = "",
                Url = "",
                Child = GetDirectories(paths, Array.Empty<string>()).ToArray(),
            };
        }

        private static IEnumerable<DirectoryDto> GetDirectories(string[][] paths, string[] currentPath)
        {
            var names = paths
                .Where(x => StartWith(x, currentPath))
                .Select(x => x[currentPath.Length])
                .Distinct()
                .OrderBy(x => x);

            foreach (var name in names)
            {
                var directoryPath = currentPath.Append(name).ToArray();
                yield return new DirectoryDto
                {
                    Name = name,
                    Url = string.Join("/", directoryPath),
                    Child = GetDirectories(paths, directoryPath).ToArray(),
                };
            }
        }

        private static bool StartWith(string[] path, string[] start)
        {
            if (path.Length <= start.Length)
            {
                return false;
            }

            for (var i = 0; i < start.Length; i++)
            {
                if (path[i] != start[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}