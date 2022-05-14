namespace NoCostSite.Function
{
    public abstract class ControllerBase
    {
        public RequestContext Context { get; set; } = null!;
    }
}