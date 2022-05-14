namespace NoCostSite.Function
{
    public interface IRequestFilter
    {
        void Filter(RequestContext context);
        
    }
}