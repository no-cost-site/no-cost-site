using System;
using System.Linq;

namespace NoCostSite.ObjectStorage.Client
{
    public class ObjectStorageFileInfo
    {
        public ObjectStorageDirectory Directory { get; set; } = null!;

        public string Name { get; set; } = null!;

        public static ObjectStorageFileInfo Parse(string path)
        {
            var arr = path.Split('/');

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