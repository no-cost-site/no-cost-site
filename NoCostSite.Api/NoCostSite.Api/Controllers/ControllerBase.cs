using NoCostSite.Api.Function;

namespace NoCostSite.Api.Controllers
{
    public abstract class ControllerBase
    {
        public HttpContext Context { get; set; } = null!;
    }
}