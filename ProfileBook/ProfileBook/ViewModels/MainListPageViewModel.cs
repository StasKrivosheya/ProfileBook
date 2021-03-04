using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Prism.Commands;
using Prism.Navigation;
using ProfileBook.Views;
using ProfileBook.Models;
using ProfileBook.Services.Authorization;
using Acr.UserDialogs;

namespace ProfileBook.ViewModels
{
    public class MainListPageViewModel : ViewModelBase
    {
        #region --- Private Fields ---

        private readonly IAuthorizationService _authorizationService;

        private DelegateCommand _logOutCommand;
        private DelegateCommand _settingsTapCommand;
        private DelegateCommand _addCommand;
        private DelegateCommand _editCommand;

        private ObservableCollection<ProfileModel> _profiles;

        private ProfileModel _selectedProfile;

        private bool _isListVisible;
        private bool _isLabelVisible;

        #endregion

        #region --- Constructors ---

        public MainListPageViewModel(INavigationService navigationService,
            IAuthorizationService authorizationService) :
            base(navigationService)
        {
            Title = "List View";

            _authorizationService = authorizationService;

            Profiles = new ObservableCollection<ProfileModel>()
            {
                new ProfileModel()
                {
                    Id = 1,
                    Description = "This is the description of th 1st item.",
                    InsertionTime = DateTime.Now,
                    Name = "Ivan Lollipop",
                    NickName = "Ilol",
                    ProfileImagePath = "pic_profile.png"
                },
                new ProfileModel()
                {
                    Id = 2,
                    Description = "This is the description of th 2nd item.",
                    InsertionTime = DateTime.Now,
                    Name = "Rost Oreo",
                    NickName = "Rostor",
                    ProfileImagePath = "pic_profile.png"
                },
                new ProfileModel()
                {
                    Id = 3,
                    Description = "This is the description of th 3rd item.",
                    InsertionTime = DateTime.Now,
                    Name = "Albert Pie",
                    NickName = "Alpie",
                    ProfileImagePath = "pic_profile.png"
                },
                new ProfileModel()
                {
                    Id = 4,
                    Description = "This is the description of th 4th item.",
                    InsertionTime = DateTime.Now,
                    Name = "Владислав Особый",
                    NickName = "ВладОс",
                    ProfileImagePath = "pic_profile.png"
                }
            };

            EditCommand = new DelegateCommand<ProfileModel>(ExecuteEditCommand,
                (arg) => arg != null);

            DeleteCommand = new DelegateCommand<ProfileModel>(ExecuteDeleteCommand,
                (arg) => arg != null);
        }

        #endregion

        #region --- Public Properties ---

        public DelegateCommand LogOutCommand =>
            _logOutCommand ?? (_logOutCommand = new DelegateCommand(ExecuteLogOutCommand));


        public DelegateCommand SettingsTapCommand =>
            _settingsTapCommand ?? (_settingsTapCommand = new DelegateCommand(ExecuteSettingsTapCommand));

        public DelegateCommand AddCommand =>
            _addCommand ?? (_addCommand = new DelegateCommand(ExecuteAddCommand));

        public DelegateCommand<ProfileModel> EditCommand { get; private set; }

        public DelegateCommand<ProfileModel> DeleteCommand { get; private set; }

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

        #region --- Overrides ---

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            UpdateList();
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

        private async void ExecuteAddCommand()
        {
            await NavigationService.NavigateAsync(nameof(AddEditProfilePage));
        }

        private async void ExecuteEditCommand(object obj)
        {
            var parameters = new NavigationParameters {{nameof(ProfileModel), obj}};

            await NavigationService.NavigateAsync(nameof(AddEditProfilePage), parameters);
        }

        private async void ExecuteDeleteCommand(object obj)
        {
            ConfirmConfig config = new ConfirmConfig()
            { Message = $"Are you sure u want to delete {((ProfileModel)obj).NickName}?",
                CancelText = "No",
                OkText = "Yes" };

            var shouldDelete = await UserDialogs.Instance.ConfirmAsync(config);

            if (shouldDelete)
            {
                Profiles.Remove((ProfileModel)obj);
                UpdateList();
            }
        }

        #endregion

        #region --- Private Helpers ---

        private void UpdateList()
        {
            // todo: get profiles from db

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
    }
}
