using System.IO;

namespace NoCostSite.BusinessLogic.ObjectStorage
{
    public class ObjectStorageDirectory
    {
        private readonly string _fullPath;

        public ObjectStorageDirectory(params string[] paths)
        {
            _fullPath = Concat(paths);
        }

        public string FullPath => _fullPath;

        public ObjectStorageDirectory Append(ObjectStorageDirectory objectStorageDirectory)
        {
            return Append(objectStorageDirectory._fullPath);
        }
        
        public ObjectStorageDirectory Append(string path)
        {
            return new ObjectStorageDirectory(_fullPath, path);
        }

        public override string ToString()
        {
            return _fullPath;
        }

        public static ObjectStorageDirectory Root => new ObjectStorageDirectory();

        private static string Concat(params string[] paths)
        {
            return Path.Combine(paths).Replace("\\", "/");
        }
    }
}