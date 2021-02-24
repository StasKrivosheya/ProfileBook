using Xamarin.Essentials;

namespace ProfileBook.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
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
    }
}
