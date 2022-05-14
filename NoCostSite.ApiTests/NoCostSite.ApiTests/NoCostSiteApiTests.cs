using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var isInit = await AuthIsInit();
            if (!isInit) await Register();
            _token = await Login();

            // Templates
            await ClearTemplates();
            var template1 = await CreateTemplate();
            var template2 = await CreateTemplate();
            await ReadAllTemplates(template1, template2);
            template1 = await UpdateTemplate(template1.Id);
            await DeleteTemplate(template2.Id);
            await ReadAllTemplates(template1);
            // Pages
            // Upload
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

        private static IEnumerable<TestCaseData> ApiWebClients()
        {
            yield return new TestCaseData(
                new ApiWebClient("https://functions.yandexcloud.net/d4e28654q6ombfhfr8nl")
            )
            {
                TestName = "CSharp api"
            };
        }

        private string Password => File.ReadAllText("../../../../../../../settings/Password");
    }
}