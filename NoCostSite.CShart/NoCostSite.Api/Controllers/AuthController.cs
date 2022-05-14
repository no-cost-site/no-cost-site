using System.Threading.Tasks;
using NoCostSite.Api.Dto;

namespace NoCostSite.Api.Controllers
{
    public class AuthController : NoCostSiteControllerBase
    {
        public async Task<object> Login(AuthLoginRequest request)
        {
            var token = await _authService.Login(request.Password);
            return AuthLoginResponse.Ok(token);
        }
    }
}