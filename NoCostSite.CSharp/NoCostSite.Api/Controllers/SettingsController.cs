using System.Threading.Tasks;
using NoCostSite.Api.Dto.Settings;
using NoCostSite.BusinessLogic.Settings;
using NoCostSite.Function;

namespace NoCostSite.Api.Controllers
{
    public class SettingsController : ControllerBase
    {
        private readonly SettingsService _settingsService = new SettingsService();

        public async Task<object> Upsert(SettingsUpsertRequest request)
        {
            var settings = new Settings
            {
                Language = request.Settings.Language!,
            };

            await _settingsService.Upsert(settings);
            return ResultResponse.Ok();
        }

        public async Task<object> Read()
        {
            var settings = await _settingsService.Read();
            return SettingsReadResponse.Ok(settings);
        }
    }
}