using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Prism.Commands;
using Prism.Navigation;
using ProfileBook.Views;
using ProfileBook.Models;
using ProfileBook.Services.Authorization;
using Acr.UserDialogs;
using ProfileBook.Enums;
using ProfileBook.Resources;
using ProfileBook.Services.ProfileService;
using ProfileBook.Services.Sorting;

namespace ProfileBook.ViewModels
{
    public class MainListPageViewModel : ViewModelBase
    {
        #region --- Private Fields ---

        private readonly IAuthorizationService _authorizationService;
        private readonly IProfileService _profileService;
        private readonly ISortingService _sortingService;

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
            IProfileService profileService,
            ISortingService sortingService) :
            base(navigationService)
        {
            _authorizationService = authorizationService;

            _profileService = profileService;

            _sortingService = sortingService;

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

        public ICommand ProfileTapCommand => new Command<ProfileModel>(ExecuteProfileTapCommand);

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

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(SelectedProfile))
            {
                ProfileTapCommand.Execute(SelectedProfile);
            }
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
                Message = $"{Resources[nameof(Resource.ProfileDeletionConfirm)]} {profile.NickName}?",
                CancelText = Resources[nameof(Resource.CancelText)],
                OkText = Resources[nameof(Resource.YesText)]
            };

            var shouldDelete = await UserDialogs.Instance.ConfirmAsync(config);

            if (shouldDelete)
            {
                await _profileService.DeleteItemAsync(profile);

                UpdateList();
            }
        }

        private async void ExecuteProfileTapCommand(ProfileModel profile)
        {
            var parameters = new NavigationParameters();
            parameters.Add(nameof(profile.ProfileImagePath), profile.ProfileImagePath);

            await NavigationService.NavigateAsync(nameof(ImageModalPage), parameters);
        }

        #endregion

        #region --- Private Helpers ---

        private async void UpdateList()
        {
            var profiles = await _profileService.GetItemsAsync(
                profile => profile.UserId == _authorizationService.CurrentUserId);

            switch (_sortingService.GetCurrentSortType)
            {
                case SortTypes.ByName:
                    profiles = profiles.OrderBy(profile => profile.Name).ToList();
                    break;
                case SortTypes.ByNickName:
                    profiles = profiles.OrderBy(profile => profile.NickName).ToList();
                    break;
                case SortTypes.ByDate:
                    profiles = profiles.OrderBy(profile => profile.InsertionTime).ToList();
                    break;
            }

            Profiles = new ObservableCollection<ProfileModel>(profiles);

            IsLabelVisible = !Profiles.Any();
            IsListVisible = Profiles.Any();
        }

        #endregion
    }
}
