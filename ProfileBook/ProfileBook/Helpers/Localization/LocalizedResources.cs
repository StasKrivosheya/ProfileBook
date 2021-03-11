using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using Prism.Mvvm;
using ProfileBook.Services.Settings;
using Xamarin.Forms;

namespace ProfileBook.Helpers.Localization
{
    public class LocalizedResources : BindableBase, INotifyPropertyChanged
    {
        #region --- Fields ---

        private static ISettingsManager _settingsManager;
        private readonly ResourceManager _resourceManager;
        private CultureInfo _currentCultureInfo;

        #endregion

        #region --- Constructors ---

        private LocalizedResources(Type resource, CultureInfo cultureInfo)
        {
            //_currentCultureInfo = cultureInfo;
            CurrentCultureInfo = cultureInfo;
            _resourceManager = new ResourceManager(resource);

            MessagingCenter.Subscribe<object, CultureChangedMessage>(this,
                string.Empty, OnCultureChanged);
        }

        public LocalizedResources(Type resource)
            : this(resource, new CultureInfo(GetCurrentLanguageCode))
        {

        }

        #endregion

        #region --- Private Properties ---

        private static ISettingsManager SettingsManager =>
            _settingsManager ?? (_settingsManager = App.Resolve<ISettingsManager>());

        #endregion

        #region --- Public Properties ---

        private CultureInfo CurrentCultureInfo
        {
            get => _currentCultureInfo;
            set => SetProperty(ref _currentCultureInfo, value);
        }

        public static string GetCurrentLanguageCode
        {
            get
            {
                if (!string.IsNullOrEmpty(SettingsManager.RememberedSelectedLanguage))
                {
                    string rememberedLanguageCode;
                    switch (SettingsManager.RememberedSelectedLanguage)
                    {
                        case "English":
                            rememberedLanguageCode = "en";
                            break;
                        case "Russian":
                            rememberedLanguageCode = "ru";
                            break;
                        default:
                            rememberedLanguageCode = "en";
                            break;
                    }

                    return rememberedLanguageCode;
                }

                var currentLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

                if (Constants.SUPPORTED_LANGUAGES_TWO_LETTER_ISO_NAMES.Contains(currentLanguage))
                {
                    return currentLanguage;
                }

                return Constants.DEFAULT_LANGUAGE_TWO_LETTER_ISO_NAME;
            }
        }

        #endregion

        #region --- Events ---

        public new event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public string this[string key] => _resourceManager.GetString(key, _currentCultureInfo);

        private void OnCultureChanged(object s, CultureChangedMessage ccm)
        {
            _currentCultureInfo = ccm.NewCultureInfo;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
        }
    }
}
