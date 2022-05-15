using System.IO;

namespace NoCostSite.TypeScript
{
    internal class FilesWriter
    {
        private readonly string _output;

        private FilesWriter(string output)
        {
            _output = output;
        }

        private void Flush()
        {
            if (Directory.Exists(_output))
            {
                Directory.Delete(_output, true);
            }
        }

        internal void Write(string dir, string fileName, string content)
        {
            CreateDirectory(dir);
            
            var path = Path.Combine(_output, dir, $"{fileName}.ts");
            File.WriteAllText(path, content);
        }

        private void CreateDirectory(string dir)
        {
            var path = Path.Combine(_output, dir);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        internal static FilesWriter Init(string output)
        {
            var filesWriter = new FilesWriter(output);
            filesWriter.Flush();
            return filesWriter;
        }
    }
}