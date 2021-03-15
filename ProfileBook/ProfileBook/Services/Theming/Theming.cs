using ProfileBook.Services.Settings;

namespace ProfileBook.Services.Theming
{
    class Theming : ITheming
    {
        private readonly ISettingsManager _settingsManager;

        public Theming(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public bool IsDarkTheme => _settingsManager.RememberedIsDarkTheme;
    }
}
