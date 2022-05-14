using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using NoCostSite.ApiTests.Client;
using NoCostSite.ApiTests.Dto;
using NUnit.Framework;

namespace NoCostSite.ApiTests
{
    public class NoCostSiteApiTests
    {
        private readonly IFixture _fixture = new Fixture();
        private ApiWebClient _apiWebClient = null!;
        private string _token = null!;

        [TestCaseSource(nameof(ApiWebClients))]
        [Explicit]
        public async Task EndToEndTest(ApiWebClient apiWebClient)
        {
            _apiWebClient = apiWebClient;

            // Auth
            await CheckAuthFalse();
            var isInit = await AuthIsInit();
            if (!isInit) await Register();
            _token = await Login();

            // Clear
            await ClearFiles();
            await ClearPages();
            await ClearTemplates();
            
            // Settings
            await CreateSettings();
            await UpdateSettings();

            // Templates
            var template1 = await CreateTemplate();
            var template2 = await CreateTemplate();
            await ReadAllTemplates(template1, template2);
            template1 = await UpdateTemplate(template1.Id);
            await DeleteTemplate(template2.Id);
            await ReadAllTemplates(template1);

            // Pages
            var page1 = await CreatePage(template1.Id);
            var page2 = await CreatePage(template1.Id);
            await ReadAllPages(page1, page2);
            page1 = await UpdatePage(page1.Id, template1.Id);
            await DeletePage(page2.Id);
            await ReadAllPages(page1);

            // Upload
            var page3 = await CreatePage(template1.Id);
            var file1 = await UploadPage(page1, template1);
            var file2 = await UploadPage(page3, template1);
            var file3 = await UploadFile();
            var file4 = await UploadFile();
            await UploadTemplate(template1.Id);
            await ReadAllFiles(file1, file2, file3, file4);
            await DeletePageFile(page3.Id);
            await DeleteFile(file4);
            await ReadAllFiles(file1, file3);
        }

        #region Auth

        private async Task CheckAuthFalse()
        {
            await SendWithException(x => x
                    .WithController("Pages")
                    .WithAction("ReadAll"),
                HttpStatusCode.Unauthorized
            );
        }

        private async Task<bool> AuthIsInit()
        {
            var response = await _apiWebClient.Send<AuthIsInitResponse>(x => x
                .WithController("Auth")
                .WithAction("IsInit")
            );

            return response.IsInit;
        }

        private async Task Register()
        {
            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Auth")
                .WithAction("Register")
                .WithBody(new {Password, PasswordConfirm = Password})
            );
        }

        private async Task<string> Login()
        {
            var response = await _apiWebClient.Send<AuthLoginResponse>(x => x
                .WithController("Auth")
                .WithAction("Login")
                .WithBody(new {Password})
            );

            return response.Token;
        }

        #endregion

        #region Settings

        private async Task CreateSettings()
        {
            var settings = _fixture.Create<SettingsDto>();
            
            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Settings")
                .WithAction("Upsert")
                .WithToken(_token)
                .WithBody(new {Settings = settings})
            );
            
            var actual = await _apiWebClient.Send<SettingsReadResponse>(x => x
                .WithController("Settings")
                .WithAction("Read")
                .WithToken(_token)
            );

            actual.Settings.Should().BeEquivalentTo(settings);
        }

        private async Task UpdateSettings()
        {
            var settings = _fixture.Create<SettingsDto>();
            
            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Settings")
                .WithAction("Upsert")
                .WithToken(_token)
                .WithBody(new {Settings = settings})
            );
            
            var actual = await _apiWebClient.Send<SettingsReadResponse>(x => x
                .WithController("Settings")
                .WithAction("Read")
                .WithToken(_token)
            );

            actual.Settings.Should().BeEquivalentTo(settings);
        }

        #endregion

        #region Templates

        private async Task ClearTemplates()
        {
            var response = await _apiWebClient.Send<TemplatesReadAllResponse>(x => x
                .WithController("Templates")
                .WithAction("ReadAll")
                .WithToken(_token)
            );

            var tasks = response.Items.Select(x => DeleteTemplate(x.Id));
            await Task.WhenAll(tasks);
        }

        private async Task<TemplateDto> CreateTemplate()
        {
            var template = _fixture
                .Build<TemplateDto>()
                .With(x => x.Content, $"{Guid.NewGuid().ToString()}<!-- Content -->{Guid.NewGuid().ToString()}")
                .Create();

            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Templates")
                .WithAction("Upsert")
                .WithToken(_token)
                .WithBody(new {Template = template})
            );

            var response = await _apiWebClient.Send<TemplatesReadResponse>(x => x
                .WithController("Templates")
                .WithAction("Read")
                .WithToken(_token)
                .WithBody(new {template.Id})
            );

            response.Template.Should().BeEquivalentTo(template);

            return template;
        }

        private async Task<TemplateDto> UpdateTemplate(Guid templateId)
        {
            var template = _fixture.Build<TemplateDto>()
                .With(x => x.Id, templateId)
                .With(x => x.Content, $"{Guid.NewGuid().ToString()}<!-- Content -->{Guid.NewGuid().ToString()}")
                .Create();

            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Templates")
                .WithAction("Upsert")
                .WithToken(_token)
                .WithBody(new {Template = template})
            );

            var response = await _apiWebClient.Send<TemplatesReadResponse>(x => x
                .WithController("Templates")
                .WithAction("Read")
                .WithToken(_token)
                .WithBody(new {template.Id})
            );

            response.Template.Should().BeEquivalentTo(template);

            return template;
        }

        private async Task ReadAllTemplates(params TemplateDto[] templates)
        {
            var response = await _apiWebClient.Send<TemplatesReadAllResponse>(x => x
                .WithController("Templates")
                .WithAction("ReadAll")
                .WithToken(_token)
            );

            var items = templates
                .Select(x => new TemplateItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToArray();

            response.Items.Should().BeEquivalentTo(items);
        }

        private async Task DeleteTemplate(Guid templateId)
        {
            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Templates")
                .WithAction("Delete")
                .WithToken(_token)
                .WithBody(new {Id = templateId})
            );
        }

        #endregion

        #region Pages

        private async Task ClearPages()
        {
            var response = await _apiWebClient.Send<PagesReadAllResponse>(x => x
                .WithController("Pages")
                .WithAction("ReadAll")
                .WithToken(_token)
            );

            var tasks = response.Items.Select(x => DeletePage(x.Id));
            await Task.WhenAll(tasks);
        }

        private async Task<PageDto> CreatePage(Guid templateId)
        {
            var page = _fixture
                .Build<PageDto>()
                .With(x => x.TemplateId, templateId)
                .Create();

            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Pages")
                .WithAction("Upsert")
                .WithToken(_token)
                .WithBody(new {Page = page})
            );

            var response = await _apiWebClient.Send<PagesReadResponse>(x => x
                .WithController("Pages")
                .WithAction("Read")
                .WithToken(_token)
                .WithBody(new {page.Id})
            );

            response.Page.Should().BeEquivalentTo(page);

            return page;
        }

        private async Task<PageDto> UpdatePage(Guid pageId, Guid templateId)
        {
            var page = _fixture.Build<PageDto>()
                .With(x => x.Id, pageId)
                .With(x => x.TemplateId, templateId)
                .Create();

            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Pages")
                .WithAction("Upsert")
                .WithToken(_token)
                .WithBody(new {Page = page})
            );

            var response = await _apiWebClient.Send<PagesReadResponse>(x => x
                .WithController("Pages")
                .WithAction("Read")
                .WithToken(_token)
                .WithBody(new {page.Id})
            );

            response.Page.Should().BeEquivalentTo(page);

            return page;
        }

        private async Task ReadAllPages(params PageDto[] pages)
        {
            var response = await _apiWebClient.Send<PagesReadAllResponse>(x => x
                .WithController("Pages")
                .WithAction("ReadAll")
                .WithToken(_token)
            );

            var items = pages
                .Select(x => new PageItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToArray();

            response.Items.Should().BeEquivalentTo(items);
        }

        private async Task DeletePage(Guid pageId)
        {
            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Pages")
                .WithAction("Delete")
                .WithToken(_token)
                .WithBody(new {Id = pageId})
            );
        }

        #endregion

        #region Upload

        private async Task ClearFiles()
        {
            var response = await _apiWebClient.Send<UploadReadAllFilesResponse>(x => x
                .WithController("Upload")
                .WithAction("ReadAllFiles")
                .WithToken(_token)
            );

            var tasks = response.Items.Select(DeleteFile);
            await Task.WhenAll(tasks);
        }

        private async Task<FileItemDto> UploadPage(PageDto page, TemplateDto template)
        {
            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Upload")
                .WithAction("UpsertPage")
                .WithToken(_token)
                .WithBody(new {PageId = page.Id})
            );

            var actual = await _apiWebClient.Download(page.Url, "");

            actual.Should().Be(template.Content.Replace("<!-- Content -->", page.Content));

            return new FileItemDto
            {
                Url = page.Url,
                Name = "index.html"
            };
        }

        private async Task<FileItemDto> UploadFile()
        {
            var fileItem = _fixture.Create<FileItemDto>();
            var data = Encoding.Default.GetBytes(_fixture.Create<string>());

            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Upload")
                .WithAction("UpsertFile")
                .WithToken(_token)
                .WithBody(new {fileItem.Url, FileName = fileItem.Name, Data = data})
            );

            return fileItem;
        }

        private async Task UploadTemplate(Guid templateId)
        {
            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Upload")
                .WithAction("UpsertTemplate")
                .WithToken(_token)
                .WithBody(new {TemplateId = templateId})
            );
        }

        private async Task ReadAllFiles(params FileItemDto[] files)
        {
            var response = await _apiWebClient.Send<UploadReadAllFilesResponse>(x => x
                .WithController("Upload")
                .WithAction("ReadAllFiles")
                .WithToken(_token)
            );

            response.Items.Should().BeEquivalentTo(files);
        }

        private async Task DeletePageFile(Guid pageId)
        {
            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Upload")
                .WithAction("DeletePage")
                .WithToken(_token)
                .WithBody(new {PageId = pageId})
            );
        }

        private async Task DeleteFile(FileItemDto file)
        {
            await _apiWebClient.Send<ResultResponse>(x => x
                .WithController("Upload")
                .WithAction("DeleteFile")
                .WithToken(_token)
                .WithBody(new {file.Url, FileName = file.Name})
            );
        }

        #endregion

        #region Private

        private static IEnumerable<TestCaseData> ApiWebClients()
        {
            yield return new TestCaseData(
                new ApiWebClient(FunctionUrl, BucketName)
            )
            {
                TestName = "CSharp api"
            };
        }

        private async Task SendWithException(Func<RequestBuilder, RequestBuilder> build, HttpStatusCode code)
        {
            try
            {
                await _apiWebClient.Send<ResultResponse>(build);
                throw new Exception($"Not throw exception with code {code}");
            }
            catch (ApiException e) when (e.Code == code)
            {
            }
        }

        private string Password => File.ReadAllText("../../../../../../../settings/Password");

        private static string FunctionUrl => File.ReadAllText("../../../../../../../settings/FunctionUrl");

        private static string BucketName => File.ReadAllText("../../../../../../../settings/PublicBucketName");

        #endregion
    }
}