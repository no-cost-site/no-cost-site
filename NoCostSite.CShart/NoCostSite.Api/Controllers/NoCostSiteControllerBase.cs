using NoCostSite.BusinessLogic.Auth;
using NoCostSite.Function;

namespace NoCostSite.Api.Controllers
{
    public class NoCostSiteControllerBase : ControllerBase
    {
        protected readonly AuthService _authService = new AuthService();

        protected void CheckAuth() => _authService.IsAuth(Context.Token!);
    }
}