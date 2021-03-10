using System.ComponentModel;
using Prism.Navigation;
using ProfileBook.Resources;

namespace ProfileBook.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        #region --- Private Fields ---

        private bool _isSortByName;
        private bool _isSortByNickName;
        private bool _isSortByDate;

        private bool _isDarkTheme;

        private string _selectedLanguage;

        #endregion

        #region --- Constructors ---

        public SettingsPageViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = Resource.Settings;
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
            set => SetProperty(ref _selectedLanguage, value);
        }

        #endregion

        #region --- Overrides ---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(IsSortByName):
                    
                    break;
                case nameof(IsSortByNickName):
                    
                    break;
                case nameof(IsSortByDate):
                    
                    break;
                case nameof(IsDarkTheme):

                    break;
                case nameof(SelectedLanguage):

                    break;
            }
        }

        #endregion
    }
}
