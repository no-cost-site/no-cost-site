using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using NoCostSite.Utils;

namespace NoCostSite.Function
{
    public abstract class FunctionBase
    {
        public async Task<Response> FunctionHandler(Request request)
        {
            if (IsCors(request))
            {
                return CorsOk();
            }
            
            try
            {
                var requestBody = ExtractBody(request.body).ToObject<RequestBody>();
                var executor = new Executor(requestBody.Controller, requestBody.Action);
                var httpContext = new HttpContext(request);

                var result = await executor.ExecuteAsync(httpContext, requestBody.Body);
                return Response.Ok(result);
            }
            catch (ValidationException validationException)
            {
                return Response.Validation(validationException.Message);
            }
            catch (UnauthorizedException)
            {
                return Response.Unauthorized();
            }
            catch (NotFoundException)
            {
                return Response.NotFound();
            }
            catch (Exception exception)
            {
                return Response.Error(exception);
            }
        }

        private Response CorsOk()
        {
            return Response.Ok(new NoContent());
        }

        private bool IsCors(Request request)
        {
            return request.httpMethod == "OPTIONS";
        }

        private string ExtractBody(string body)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(body));
            }
            catch (Exception)
            {
                return body;
            }
        }
    }
}