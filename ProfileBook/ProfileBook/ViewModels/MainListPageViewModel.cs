using System;
using System.Collections.ObjectModel;
using Prism.Commands;
using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Settings;
using ProfileBook.Views;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListPageViewModel : ViewModelBase
    {
        #region --- Private Fields ---

        private readonly ISettingsManager _settingsManager;
        private readonly IAuthorizationService _authorizationService;

        private DelegateCommand _logOutCommand;
        private DelegateCommand _settingsTapCommand;
        private DelegateCommand _addCommand;

        private ObservableCollection<ProfileModel> _profiles;

        private ProfileModel _selectedProfile;

        private bool _isListVisible;
        private bool _isLabelVisible;

        #endregion

        #region --- Constructors ---

        public MainListPageViewModel(INavigationService navigationService,
            ISettingsManager settingsManager,
            IAuthorizationService authorizationService) :
            base(navigationService)
        {
            Title = "List View";

            _settingsManager = settingsManager;
            _authorizationService = authorizationService;

            Profiles = new ObservableCollection<ProfileModel>()
            {
                new ProfileModel()
                {
                    Description = "This is the description of th 1st item.",
                    InsertionTime = DateTime.Now,
                    Name = "Ivan Lollipop",
                    NickName = "Ilol",
                    ProfileImagePath = "pic_profile.png"
                },
                new ProfileModel()
                {
                    Description = "This is the description of th 2nd item.",
                    InsertionTime = DateTime.Now,
                    Name = "Rost Oreo",
                    NickName = "Rostor",
                    ProfileImagePath = "pic_profile.png"
                },
                new ProfileModel()
                {
                    Description = "This is the description of th 3rd item.",
                    InsertionTime = DateTime.Now,
                    Name = "Albert Pie",
                    NickName = "Alpie",
                    ProfileImagePath = "pic_profile.png"
                }
            };

            if (Profiles.Count < 1)
            {
                IsListVisible = false;
                IsLabelVisible = true;
            }
            else
            {
                IsListVisible = true;
                IsLabelVisible = false;
            }
        }

        #endregion

        #region --- Public Properties ---

        public DelegateCommand LogOutCommand =>
            _logOutCommand ?? (_logOutCommand = new DelegateCommand(ExecuteLogOutCommand));


        public DelegateCommand SettingsTapCommand =>
            _settingsTapCommand ?? (_settingsTapCommand = new DelegateCommand(ExecuteSettingsTapCommand));

        public DelegateCommand AddCommand =>
            _addCommand ?? (_addCommand = new DelegateCommand(ExecuteAddCommand));

        public ObservableCollection<ProfileModel> Profiles
        {
            get => _profiles;
            set => SetProperty(ref _profiles, value);
        }

        public ProfileModel SelectedProfile
        {
            get => _selectedProfile;
            set => SetProperty(ref _selectedProfile, value);
        }

        public bool IsListVisible
        {
            get => _isListVisible;
            set => SetProperty(ref _isListVisible, value);
        }

        public bool IsLabelVisible
        {
            get => _isLabelVisible;
            set => SetProperty(ref _isLabelVisible, value);
        }

        #endregion

        #region --- Command Handlers ---

        private void ExecuteLogOutCommand()
        {
            _authorizationService.UnAuthorize();

            NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
        }

        private void ExecuteSettingsTapCommand()
        {
            UserDialogs.Instance.Alert("Settings page hasn't been implemented yet.", "Oops");
        }

        private void ExecuteAddCommand()
        {
            Profiles.Add(new ProfileModel()
            {
                Name = "New Item",
                NickName = "Newbie",
                InsertionTime = DateTime.Now,
                ProfileImagePath = "pic_profile.png"
            });
        }

        #endregion
    }
}
