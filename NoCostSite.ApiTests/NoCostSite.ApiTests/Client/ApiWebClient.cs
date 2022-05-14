using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using NoCostSite.ApiTests.Utils;

namespace NoCostSite.ApiTests.Client
{
    public class ApiWebClient
    {
        private readonly string _apiUrl;

        public ApiWebClient(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public async Task<TResult> Send<TResult>(Func<RequestBuilder, RequestBuilder> build)
        {
            var request = build(new RequestBuilder()).Build();
            var url = BuildUrl(request);

            using var wc = CreateWebClient(request);
            var response = await SendWithCatchExceptionAndLog(wc, url, request.Body);

            return response.ToObject<TResult>();
        }

        private WebClient CreateWebClient(Request request)
        {
            var wc = new WebClient();

            wc.Headers["Content-Type"] = "application/json";

            foreach (var header in request.Headers) wc.Headers[header.Key] = header.Value;

            return wc;
        }

        private async Task<string> SendWithCatchExceptionAndLog(WebClient wc, string url, string body)
        {
            try
            {
                Log(url, body, wc.Headers.ToJson());

                var (response, executeTime) = await WithTiming(async () =>
                    await wc.UploadStringTaskAsync(url, body)
                );
                Log(response, executeTime.ToString());

                return response;
            }
            catch (WebException exception)
            {
                var response = await ExtractResponse(exception);
                var code = ((HttpWebResponse) exception.Response).StatusCode;

                Log($"{code} - {response}");
                throw new ApiException(response, code);
            }

            void Log(params string[] messages)
            {
                messages.ForEach(Console.WriteLine);
                Console.WriteLine();
            }

            async Task<(string Result, TimeSpan Elapsed)> WithTiming(Func<Task<string>> action)
            {
                var sw = Stopwatch.StartNew();

                var result = await action();

                sw.Stop();

                return (result, sw.Elapsed);
            }

            async Task<string> ExtractResponse(WebException x)
            {
                using var reader = new StreamReader(x.Response.GetResponseStream()!);
                return await reader.ReadToEndAsync();
            }
        }

        private string BuildUrl(Request request)
        {
            return $"{_apiUrl}?Controller={request.Controller}&Action={request.Action}";
        }
    }
}