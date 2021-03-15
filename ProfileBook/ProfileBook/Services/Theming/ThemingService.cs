using ProfileBook.Services.Settings;

namespace ProfileBook.Services.Theming
{
    class ThemingService : IThemingService
    {
        #region --- Private Fields ---

        private readonly ISettingsManager _settingsManager;

        #endregion

        #region --- Constructors ---

        public ThemingService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        #endregion

        #region ---IThemingService  Implementation ---

        public bool IsDarkTheme => _settingsManager.RememberedIsDarkTheme;

        #endregion
    }
}
