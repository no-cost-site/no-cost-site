using NoCostSite.Api.CSharp.Function;

namespace NoCostSite.Api.CSharp.Controllers
{
    public abstract class ControllerBase
    {
        public HttpContext Context { get; set; } = null!;
    }
}