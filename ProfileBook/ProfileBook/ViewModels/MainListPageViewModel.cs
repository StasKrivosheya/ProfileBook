using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Prism.Commands;
using Prism.Navigation;
using ProfileBook.Views;
using ProfileBook.Models;
using ProfileBook.Services.Authorization;
using Acr.UserDialogs;
using ProfileBook.Resources;
using ProfileBook.Services.ProfileService;

namespace ProfileBook.ViewModels
{
    public class MainListPageViewModel : ViewModelBase
    {
        #region --- Private Fields ---

        private readonly IAuthorizationService _authorizationService;
        private readonly IProfileService _profileService;

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
            IAuthorizationService authorizationService,
            IProfileService profileService) :
            base(navigationService)
        {
            Title = Resource.ListViewTitle;

            _authorizationService = authorizationService;

            _profileService = profileService;

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

        private async void ExecuteSettingsTapCommand()
        {
            await NavigationService.NavigateAsync(nameof(SettingsPage));
        }

        private async void ExecuteAddCommand()
        {
            await NavigationService.NavigateAsync(nameof(AddEditProfilePage));
        }

        private async void ExecuteEditCommand(ProfileModel profile)
        {
            var parameters = new NavigationParameters {{nameof(ProfileModel), profile } };

            await NavigationService.NavigateAsync(nameof(AddEditProfilePage), parameters);
        }

        private async void ExecuteDeleteCommand(ProfileModel profile)
        {
            ConfirmConfig config = new ConfirmConfig
            { 
                Message = $"{Resource.ProfileDeletionConfirm} {profile.NickName}?",
                CancelText = Resource.CancelText,
                OkText = Resource.YesText
            };

            var shouldDelete = await UserDialogs.Instance.ConfirmAsync(config);

            if (shouldDelete)
            {
                await _profileService.DeleteItemAsync(profile);

                UpdateList();
            }
        }

        #endregion

        #region --- Private Helpers ---

        private async void UpdateList()
        {
            var profiles = await _profileService.GetItemsAsync(
                profile => profile.UserId == _authorizationService.CurrentUserId);

            Profiles = new ObservableCollection<ProfileModel>(profiles);

            IsLabelVisible = !Profiles.Any();
            IsListVisible = Profiles.Any();
        }

        #endregion
    }
}
