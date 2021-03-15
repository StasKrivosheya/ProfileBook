using ProfileBook.Services.Settings;

namespace ProfileBook.Services.Theming
{
    class ThemingService : IThemingService
    {
        private readonly ISettingsManager _settingsManager;

        public ThemingService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public bool IsDarkTheme => _settingsManager.RememberedIsDarkTheme;
    }
}
