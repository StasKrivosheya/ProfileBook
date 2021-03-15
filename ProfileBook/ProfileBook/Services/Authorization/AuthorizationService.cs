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

        #region --- Interface Implementation ---

        public bool IsAuthorized => _settingsManager.RememberedUserLogin != default(string);

        public int CurrentUserId => IsAuthorized ? _settingsManager.RememberedUserId : -1;

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

        #endregion
    }
}
