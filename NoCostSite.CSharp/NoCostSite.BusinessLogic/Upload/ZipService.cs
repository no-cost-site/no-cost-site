using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using NoCostSite.BusinessLogic.ObjectStorage;

namespace NoCostSite.BusinessLogic.Upload
{
    public class ZipService
    {
        public ObjectStorageFile[] UnZip(byte[] zip, ObjectStorageDirectory directory)
        {
            using var stream = new MemoryStream(zip);
            using var archive = new ZipArchive(stream);

            return archive
                .Entries
                .Select(x => UnZip(directory, x))
                .ToArray();
        }

        private ObjectStorageFile UnZip(ObjectStorageDirectory directory, ZipArchiveEntry entry)
        {
            var fileInfo = ObjectStorageFileInfo.Parse(entry.FullName);
            fileInfo.Directory = directory.Append(fileInfo.Directory);

            using var entryStream = entry.Open();
            using var fileStream = new MemoryStream();

            entryStream.CopyTo(fileStream);

            return new ObjectStorageFile
            {
                Info = fileInfo,
                Content = Encoding.Default.GetString(fileStream.ToArray()),
            };
        }
    }
}