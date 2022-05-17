using System;
using System.Linq;
using NoCostSite.Utils;

namespace NoCostSite.BusinessLogic.ObjectStorage
{
    public class ObjectStorageFileInfo
    {
        public string Id => GetId();
        
        public ObjectStorageDirectory Directory { get; set; } = null!;

        public string Name { get; set; } = null!;

        private string GetId()
        {
            return new[] {Directory.FullPath, Name}
                .Where(x => !string.IsNullOrEmpty(x))
                .Join("-")
                .Replace("/", "-")
                .Replace(".", "-");
        }
        
        public static ObjectStorageFileInfo Parse(string path)
        {
            var arr = path.Replace("\\", "//").Split('/');

            if (!arr.Any())
            {
                throw new Exception("Path is empty");
            }

            return new ObjectStorageFileInfo
            {
                Directory = new ObjectStorageDirectory(arr.Take(arr.Length - 1).ToArray()),
                Name = arr.Last(),
            };
        }
    }
}