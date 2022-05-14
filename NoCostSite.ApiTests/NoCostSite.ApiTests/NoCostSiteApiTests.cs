using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NoCostSite.ApiTests.Client;
using NoCostSite.ApiTests.Dto;
using NUnit.Framework;

namespace NoCostSite.ApiTests
{
    public class NoCostSiteApiTests
    {
        private ApiWebClient _apiWebClient = null!;

        [TestCaseSource(nameof(ApiWebClients))]
        [Explicit]
        public async Task EndToEndTest(ApiWebClient apiWebClient)
        {
            _apiWebClient = apiWebClient;

            // Auth
            var isInit = await AuthIsInit();
            if (!isInit) await Register();
            var token = await Login();

            // Templates
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