using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using NoCostSite.Utils;

namespace NoCostSite.Function
{
    public abstract class FunctionBase
    {
        protected virtual IRequestFilter[] Filters => Array.Empty<IRequestFilter>();
        
        public async Task<Response> FunctionHandler(Request request)
        {
            if (IsCors(request))
            {
                return CorsOk();
            }
            
            try
            {
                var requestContext = RequestContext.Create(request);
                var executor = new Executor(requestContext);
                
                Filters.ForEach(x => x.Filter(requestContext));
                var result = await executor.ExecuteAsync();

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
    }
}