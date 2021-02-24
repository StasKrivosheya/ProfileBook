using ProfileBook.Services.Settings;

namespace ProfileBook.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        public AuthorizationService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        private readonly ISettingsManager _settingsManager;
        
        public bool Authorized => _settingsManager.RememberedUserLogin != string.Empty;
    }
}
