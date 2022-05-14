using System;

namespace NoCostSite.BusinessLogic.Config
{
    public static class ConfigContainer
    {
        private static Config? _current;

        public static Config Current
        {
            get => _current ?? throw new Exception($"Need configure {nameof(Config)} by {nameof(ConfigContainer)}");
            set => _current = value;
        }
    }
}