using NoCostSite.Api.Controllers;
using NoCostSite.Api.Filters;
using NoCostSite.Function;

namespace NoCostSite.Api
{
    public class EntryPoint : FunctionBase
    {
        protected override IRequestFilter[] Filters { get; } =
        {
            new SetupSettingsFilter(),
            new AuthByDefaultFilter(nameof(AuthController)),
        };
    }
}