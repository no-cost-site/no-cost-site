using System;

namespace NoCostSite.BusinessLogic.Settings
{
    public static class SettingsContainer
    {
        private static Settings? _current;

        public static Settings Current
        {
            get => _current ?? throw new Exception($"Need configure {nameof(Current)} by {nameof(SettingsContainer)}");
            set => _current = value;
        }
    }
}