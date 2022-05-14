namespace NoCostSite.Api.Dto.Settings
{
    public class SettingsReadResponse
    {
        public SettingsDto Settings { get; set; } = null!;

        public static SettingsReadResponse Ok(BusinessLogic.Settings.Settings settings)
        {
            return new SettingsReadResponse()
            {
                Settings = new SettingsDto
                {
                    Language = settings.Language,
                },
            };
        }
    }
}