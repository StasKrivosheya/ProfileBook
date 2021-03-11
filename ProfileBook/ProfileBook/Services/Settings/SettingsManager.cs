using Xamarin.Essentials;

namespace ProfileBook.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
        #region --- ISettingsManager Implementation ---

        public int RememberedUserId
        {
            get => Preferences.Get(nameof(RememberedUserId), default(int));
            set => Preferences.Set(nameof(RememberedUserId), value);
        }

        public string RememberedUserLogin
        {
            get => Preferences.Get(nameof(RememberedUserLogin), default(string));
            set => Preferences.Set(nameof(RememberedUserLogin), value);
        }

        public bool RememberedIsSortByName
        {
            get => Preferences.Get(nameof(RememberedIsSortByName), default(bool));
            set => Preferences.Set(nameof(RememberedIsSortByName), value);

        }

        public bool RememberedIsSortByNickName
        {
            get => Preferences.Get(nameof(RememberedIsSortByNickName), default(bool));
            set => Preferences.Set(nameof(RememberedIsSortByNickName), value);
        }

        public bool RememberedIsSortByDate
        {
            get => Preferences.Get(nameof(RememberedIsSortByDate), default(bool));
            set => Preferences.Set(nameof(RememberedIsSortByDate), value);
        }

        public bool RememberedIsDarkTheme
        {
            get => Preferences.Get(nameof(RememberedIsDarkTheme), default(bool));
            set => Preferences.Set(nameof(RememberedIsDarkTheme), value);
        }

        public string RememberedSelectedLanguage
        {
            get => Preferences.Get(nameof(RememberedSelectedLanguage), default(string));
            set => Preferences.Set(nameof(RememberedSelectedLanguage), value);
        }

        #endregion
    }
}
