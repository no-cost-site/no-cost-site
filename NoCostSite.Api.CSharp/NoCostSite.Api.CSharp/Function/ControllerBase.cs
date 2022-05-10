namespace NoCostSite.Api.CSharp.Function
{
    public abstract class ControllerBase
    {
        public HttpContext Context { get; set; } = null!;
    }
}