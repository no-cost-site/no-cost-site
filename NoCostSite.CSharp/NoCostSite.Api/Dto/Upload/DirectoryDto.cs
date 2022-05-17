using System;
using System.Collections.Generic;
using System.Linq;

namespace NoCostSite.Api.Dto.Upload
{
    public class DirectoryDto
    {
        public string Name { get; set; } = null!;

        public string Url { get; set; } = null!;

        public DirectoryDto[] Child { get; set; } = null!;

        public static DirectoryDto Build(string[] urls)
        {
            var paths = urls
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