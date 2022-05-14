using System;

namespace NoCostSite.BusinessLogic.Token
{
    public static class TokenConfigContainer
    {
        private static TokenConfig? _config;

        public static TokenConfig Config
        {
            get => _config ?? throw new Exception($"{nameof(TokenConfig)} should be configured by {nameof(TokenConfigContainer)}");
            set => _config = value;
        }
    }
}