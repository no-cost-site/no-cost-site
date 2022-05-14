using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

namespace NoCostSite.BusinessLogic.ObjectStorage
{
    public class ObjectStorageClient : IDisposable
    {
        private readonly ObjectStorageClientConfig _config;
        private AmazonS3Client? _amazonS3Client;

        public ObjectStorageClient(ObjectStorageClientConfig config)
        {
            _config = config;
        }

        public async Task Upsert(ObjectStorageFile file)
        {
            var data = Encoding.Default.GetBytes(file.Content);
            await using var stream = new MemoryStream(data);

            var request = new PutObjectRequest
            {
                BucketName = Bucket.Append(file.Info.Directory).FullPath,
                Key = file.Info.Name,
                InputStream = stream,
            };

            await Client.PutObjectAsync(request);
        }

        public async Task UpsertMany(ObjectStorageFile[] files)
        {
            var tasks = files.Select(Upsert);
            await Task.WhenAll(tasks);
        }

        public async Task Delete(ObjectStorageFileInfo fileInfo)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = Bucket.Append(fileInfo.Directory).FullPath,
                Key = fileInfo.Name,
            };

            await Client.DeleteObjectAsync(request);
        }

        public async Task DeleteMany(ObjectStorageFileInfo[] fileInfos)
        {
            var tasks = fileInfos.Select(Delete);
            await Task.WhenAll(tasks);
        }

        public async Task<ObjectStorageFileInfo[]> List(ObjectStorageDirectory directory)
        {
            return await CatchErrors(async () =>
            {
                var request = new ListObjectsRequest
                {
                    BucketName = Bucket.FullPath,
                    Prefix = directory.FullPath
                };

                var response = await Client.ListObjectsAsync(request);

                return response
                    .S3Objects
                    .Select(x => x.Key)
                    .Select(ObjectStorageFileInfo.Parse)
                    .ToArray();
            });

            async Task<ObjectStorageFileInfo[]> CatchErrors(Func<Task<ObjectStorageFileInfo[]>> action)
            {
                try
                {
                    return await action();
                }
                catch (AmazonS3Exception e) when (e.ErrorCode == "NotFound")
                {
                    return Array.Empty<ObjectStorageFileInfo>();
                }
            }
        }

        public async Task<string> Read(ObjectStorageFileInfo fileInfo)
        {
            var request = new GetObjectRequest
            {
                BucketName = Bucket.Append(fileInfo.Directory).FullPath,
                Key = fileInfo.Name,
            };

            var response = await Client.GetObjectAsync(request);

            await using var responseStream = response.ResponseStream;
            using var reader = new StreamReader(responseStream);

            return await reader.ReadToEndAsync();
        }

        public async Task<string[]> ReadMany(ObjectStorageFileInfo[] fileInfos)
        {
            var tasks = fileInfos.Select(Read);
            return await Task.WhenAll(tasks);
        }

        public async Task<string?> TryRead(ObjectStorageFileInfo fileInfo)
        {
            try
            {
                return await Read(fileInfo);
            }
            catch (AmazonS3Exception e) when (e.ErrorCode == "NotFound")
            {
                return null;
            }
        }

        private AmazonS3Client Client => _amazonS3Client ??= CreateClient();

        private AmazonS3Client CreateClient()
        {
            var config = new AmazonS3Config
            {
                ServiceURL = _config.ServiceUrl,
                AuthenticationRegion = _config.AuthenticationRegion,
            };
            return new AmazonS3Client(_config.AccessKeyId, _config.SecretAccessKey, config);
        }

        private ObjectStorageDirectory Bucket => new ObjectStorageDirectory(_config.BucketName);

        public void Dispose()
        {
            _amazonS3Client?.Dispose();
        }
    }
}