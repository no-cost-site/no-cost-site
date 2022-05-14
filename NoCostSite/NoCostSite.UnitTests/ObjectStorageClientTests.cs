using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using NoCostSite.BusinessLogic.ObjectStorage;
using NUnit.Framework;

namespace NoCostSite.UnitTests
{
    public class ObjectStorageClientTests
    {
        [Test]
        [Explicit]
        public async Task Upsert_Should_BeCreated_When_NewFile()
        {
            var file = new ObjectStorageFile
            {
                Info = new ObjectStorageFileInfo
                {
                    Directory = new ObjectStorageDirectory(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                    Name = Guid.NewGuid().ToString(),
                },
                Content = Guid.NewGuid().ToString(),
            };

            using var client = CreateClient();
            await client.Upsert(file);

            var actual = await client.Read(file.Info);

            actual.Should().BeEquivalentTo(file.Content);
        }

        [Test]
        [Explicit]
        public async Task Upsert_Should_BeCreated_When_ReplaceFile()
        {
            var file = new ObjectStorageFile
            {
                Info = new ObjectStorageFileInfo
                {
                    Directory = new ObjectStorageDirectory(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                    Name = Guid.NewGuid().ToString(),
                },
                Content = Guid.NewGuid().ToString(),
            };

            using var client = CreateClient();
            await client.Upsert(file);

            file.Content = Guid.NewGuid().ToString();
            await client.Upsert(file);

            var actual = await client.Read(file.Info);

            actual.Should().BeEquivalentTo(file.Content);
        }

        [Test]
        [Explicit]
        public async Task Upsert_Should_BeCreated_When_RootDirectory()
        {
            var file = new ObjectStorageFile
            {
                Info = new ObjectStorageFileInfo
                {
                    Directory = ObjectStorageDirectory.Root,
                    Name = Guid.NewGuid().ToString(),
                },
                Content = Guid.NewGuid().ToString(),
            };

            using var client = CreateClient();
            await client.Upsert(file);

            var actual = await client.Read(file.Info);

            actual.Should().BeEquivalentTo(file.Content);
        }

        [Test]
        [Explicit]
        public async Task Delete_Should_BeDeleted_When_SubDirectory()
        {
            var directory = new ObjectStorageDirectory(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            var file = new ObjectStorageFile
            {
                Info = new ObjectStorageFileInfo
                {
                    Directory = directory,
                    Name = Guid.NewGuid().ToString(),
                },
                Content = Guid.NewGuid().ToString(),
            };

            var file2 = new ObjectStorageFile
            {
                Info = new ObjectStorageFileInfo
                {
                    Directory = directory,
                    Name = Guid.NewGuid().ToString(),
                },
                Content = Guid.NewGuid().ToString(),
            };

            using var client = CreateClient();
            await client.UpsertMany(new[] {file, file2});
            await client.Delete(file.Info);

            var actual = await client.List(file.Info.Directory);

            actual.Should().BeEquivalentTo(new[] {file2.Info});
        }

        [Test]
        [Explicit]
        public async Task Delete_Should_BeDeleted_When_RootDirectory()
        {
            var file = new ObjectStorageFile
            {
                Info = new ObjectStorageFileInfo
                {
                    Directory = ObjectStorageDirectory.Root,
                    Name = Guid.NewGuid().ToString(),
                },
                Content = Guid.NewGuid().ToString(),
            };

            using var client = CreateClient();
            await client.Upsert(file);
            await client.Delete(file.Info);

            var actual = await client.TryRead(file.Info);

            actual.Should().BeNull();
        }

        [Test]
        [Explicit]
        public async Task List_Should_ListFiles_When_HasFiles()
        {
            var directory = new ObjectStorageDirectory(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            var file = new ObjectStorageFile
            {
                Info = new ObjectStorageFileInfo
                {
                    Directory = directory,
                    Name = Guid.NewGuid().ToString(),
                },
                Content = Guid.NewGuid().ToString(),
            };

            var file2 = new ObjectStorageFile
            {
                Info = new ObjectStorageFileInfo
                {
                    Directory = directory,
                    Name = Guid.NewGuid().ToString(),
                },
                Content = Guid.NewGuid().ToString(),
            };

            using var client = CreateClient();
            await client.UpsertMany(new[] {file, file2});

            var actual = await client.List(directory);

            actual.Should().BeEquivalentTo(new[] {file.Info, file2.Info});
        }

        [Test]
        [Explicit]
        public async Task List_Should_ListFiles_When_NotFiles()
        {
            var directory = new ObjectStorageDirectory(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            using var client = CreateClient();
            var actual = await client.List(directory);

            actual.Should().BeEmpty();
        }

        private ObjectStorageClient CreateClient()
        {
            return new ObjectStorageClient(new ObjectStorageClientConfig
            {
                AccessKeyId = ReadFile("AccessKeyId"),
                SecretAccessKey = ReadFile("SecretAccessKey"),
                ServiceUrl = ReadFile("ServiceUrl"),
                AuthenticationRegion = ReadFile("Region"),
                BucketName = "no-cost-site-test",
            });

            string ReadFile(string fileName)
            {
                var filePath = Path.Combine("../../../../../../../tokens", fileName);
                return File.ReadAllText(filePath);
            }
        }
    }
}