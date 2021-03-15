using System.Collections.Generic;
using System.ComponentModel;
using Prism.Navigation;
using ProfileBook.Helpers.Localization;
using ProfileBook.Services.Settings;
using ProfileBook.Themes;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        #region --- Private Fields ---

        private readonly ISettingsManager _settingsManager;

        private bool _isSortByName;
        private bool _isSortByNickName;
        private bool _isSortByDate;

        private bool _isDarkTheme;

        private string _selectedLanguage;

        #endregion

        #region --- Constructors ---

        public SettingsPageViewModel(INavigationService navigationService,
            ISettingsManager settingsManager) :
            base(navigationService)
        {
            _settingsManager = settingsManager;

            IsDarkTheme = _settingsManager.RememberedIsDarkTheme;

            IsSortByDate = _settingsManager.RememberedIsSortByDate;
            IsSortByName = _settingsManager.RememberedIsSortByName;
            IsSortByNickName = _settingsManager.RememberedIsSortByNickName;

            // setting default sort
            if (IsSortByName == false &&
                IsSortByNickName == false &&
                IsSortByDate == false)
            {
                IsSortByDate = true;
            }

            // setting necessary language to picker
            if (!string.IsNullOrEmpty(_settingsManager.RememberedSelectedLanguage))
            {
                SelectedLanguage = _settingsManager.RememberedSelectedLanguage;
            }
            else
            {
                switch (LocalizedResources.GetCurrentLanguageCode)
                {
                    case "en":
                        SelectedLanguage = "English";
                        break;
                    case "ru":
                        SelectedLanguage = "Russian";
                        break;
                }
            }
        }

        #endregion

        #region --- Public Properties

        public bool IsSortByName
        {
            get => _isSortByName;
            set => SetProperty(ref _isSortByName, value);
        }

        public bool IsSortByNickName
        {
            get => _isSortByNickName;
            set => SetProperty(ref _isSortByNickName, value);
        }

        public bool IsSortByDate
        {
            get => _isSortByDate;
            set => SetProperty(ref _isSortByDate, value);
        }

        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set => SetProperty(ref _isDarkTheme, value);
        }

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                SetProperty(ref _selectedLanguage, value);
                string langСode = null;
                switch (value)
                {
                    case "English":
                        langСode = "en";
                        break;
                    case "Russian":
                        langСode = "ru";
                        break;
                }
                MessagingCenter.Send<object, CultureChangedMessage>(this, string.Empty, new CultureChangedMessage(langСode));
            }
        }

        #endregion

        #region --- Overrides ---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            // if page haven't been init yet
            if (_settingsManager == null)
                return;

            switch (args.PropertyName)
            {
                case nameof(IsSortByName):
                    _settingsManager.RememberedIsSortByName = IsSortByName;
                    break;
                case nameof(IsSortByNickName):
                    _settingsManager.RememberedIsSortByNickName = IsSortByNickName;
                    break;
                case nameof(IsSortByDate):
                    _settingsManager.RememberedIsSortByDate = IsSortByDate;
                    break;
                case nameof(IsDarkTheme):
                    _settingsManager.RememberedIsDarkTheme = IsDarkTheme;
                    ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                    if (mergedDictionaries != null)
                    {
                        mergedDictionaries.Clear();

                        switch (IsDarkTheme)
                        {
                            case true:
                                mergedDictionaries.Add(new DarkTheme());
                                break;
                            case false:
                                mergedDictionaries.Add(new LightTheme());
                                break;
                        }
                    }
                    break;
                case nameof(SelectedLanguage):
                    _settingsManager.RememberedSelectedLanguage = SelectedLanguage;
                    break;
            }
        }

        #endregion
    }
}
