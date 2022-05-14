using System.Linq;
using NoCostSite.BusinessLogic.Auth;
using NoCostSite.Function;

namespace NoCostSite.Api.Filters
{
    public class AuthByDefaultFilter : IRequestFilter
    {
        private readonly string[] _allowNotAuth;
        private readonly AuthService _authService = new AuthService();

        public AuthByDefaultFilter(params string[] allowNotAuth)
        {
            _allowNotAuth = allowNotAuth
                .Select(x => x.Replace("Controller", ""))
                .ToArray();
        }

        public void Filter(RequestContext context)
        {
            if (_allowNotAuth.Contains(context.Controller))
            {
                return;
            }
            
            _authService.IsAuth(context.Token!);
        }
    }
}