namespace NoCostSite.TypeScript
{
    internal class ControllerAction
    {
        internal string Name { get; set; } = null!;

        internal ControllerType? Request { get; set; }

        internal ControllerType Response { get; set; } = null!;
        
        internal ControllerType[] AllTypes { get; set; } = null!;
    }
}