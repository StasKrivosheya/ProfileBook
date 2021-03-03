using ProfileBook.Services.Settings;

namespace ProfileBook.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ISettingsManager _settingsManager;

        public AuthorizationService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public bool Authorized => _settingsManager.RememberedUserLogin != string.Empty;

        public void Authorize(int id, string login)
        {
            _settingsManager.RememberedUserId = id;
            _settingsManager.RememberedUserLogin = login;
        }

        public void UnAuthorize()
        {
            _settingsManager.RememberedUserId = -1;
            _settingsManager.RememberedUserLogin = string.Empty;
        }
    }
}
