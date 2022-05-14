using System.Threading.Tasks;

namespace NoCostSite.BusinessLogic.Settings
{
    public class SettingsService
    {
        private readonly SettingsRepository _repository = new SettingsRepository();
        
        public async Task Upsert(Settings settings)
        {
            await _repository.Upsert(settings);
        }

        public async Task<Settings> Read()
        {
            return await _repository.Read();
        }
    }
}