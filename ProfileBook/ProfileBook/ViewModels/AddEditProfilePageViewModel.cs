using Prism.Commands;
using System;
using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.ProfileService;
using Xamarin.Essentials;

namespace ProfileBook.ViewModels
{
    public class AddEditProfilePageViewModel : ViewModelBase
    {
        #region --- Private Fields ---

        private readonly IProfileService _profileService;
        private readonly IAuthorizationService _authorizationService;

        private DelegateCommand _saveCommand;
        private DelegateCommand _imageTapCommand;

        private int _profileId;
        private string _profileImagePath;
        private string _name;
        private string _nickName;
        private string _description;
        private DateTime _insertionTime;

        #endregion

        #region --- Constructors ---

        public AddEditProfilePageViewModel(INavigationService navigationService,
            IProfileService profileService,
            IAuthorizationService authorizationService) :
            base(navigationService)
        {
            _profileService = profileService;
            _authorizationService = authorizationService;
        }

        #endregion

        #region --- Public Properties ---

        public DelegateCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand(ExecuteSaveCommand));

        public DelegateCommand ImageTapCommand =>
            _imageTapCommand ?? (_imageTapCommand = new DelegateCommand(ExecuteImageTapCommand));

        public int ProfileId
        {
            get => _profileId;
            set => SetProperty(ref _profileId, value);
        }

        public string ProfileImagePath
        {
            get => _profileImagePath;
            set => SetProperty(ref _profileImagePath, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public DateTime InsertionTime
        {
            get => _insertionTime;
            set => SetProperty(ref _insertionTime, value);
        }

        public bool CanSave => !string.IsNullOrEmpty(Name) &&
                               !string.IsNullOrEmpty(NickName);

        #endregion

        #region --- Overrides ---

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(nameof(ProfileModel), out ProfileModel profile))
            {
                ProfileId = profile?.Id ?? 0;
                ProfileImagePath = profile?.ProfileImagePath ?? "pic_profile.png";
                Name = profile?.Name;
                NickName = profile?.NickName;
                Description = profile?.Description;
                InsertionTime = profile?.InsertionTime ?? DateTime.Now;

                Title = "Edit Profile";
            }
            else
            {
                ProfileImagePath = "pic_profile.png";

                Title = "Add Profile";
            }
        }

        #endregion

        #region --- Command Handlers ---

        private async void ExecuteSaveCommand()
        {
            if (CanSave)
            {
                var profile = new ProfileModel
                {
                    Id = ProfileId,
                    UserId = _authorizationService.CurrentUserId,
                    Description = Description,
                    ProfileImagePath = ProfileImagePath,
                    NickName = NickName,
                    Name = Name,
                    InsertionTime = InsertionTime
                };

                if (profile.Id == 0)
                {
                    profile.InsertionTime = DateTime.Now;
                    await _profileService.InsertItemAsync(profile);
                }
                else
                {
                    await _profileService.UpdateItemAsync(profile);
                }

                await NavigationService.GoBackAsync();
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Type both Name and Nickname!");
            }
        }

        private void ExecuteImageTapCommand()
        {
            UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                .SetTitle("Choose Type")
                .Add("Camera", PickFromCamera, "ic_camera_alt_black.png")
                .Add("Gallery", PickFromGallery, "ic_collections_black.png")
            );
        }

        #endregion

        #region --- Private Helpers ---

        private async void PickFromGallery()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            var photo = await MediaPicker.PickPhotoAsync();

            if (photo != null)
            {
                ProfileImagePath = photo.FullPath;
            }
        }

        private async void PickFromCamera()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.Camera>();
            }

            var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
            {
                Title = $"ProfileBook{DateTime.Now:dd-MM-yyyy_hh.mm.ss}.jpg"
            });

            if (photo != null)
            {
                ProfileImagePath = photo.FullPath;
            }
        }

        #endregion
    }
}
