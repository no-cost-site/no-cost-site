namespace NoCostSite.Function
{
    public abstract class ControllerBase
    {
        public HttpContext Context { get; set; } = null!;
    }
}