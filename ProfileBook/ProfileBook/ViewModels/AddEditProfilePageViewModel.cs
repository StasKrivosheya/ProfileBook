using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Models;

namespace ProfileBook.ViewModels
{
    public class AddEditProfilePageViewModel : ViewModelBase
    {
        #region --- Private Fields ---

        private DelegateCommand _saveCommand;

        private int _profileId;
        private string _profileImagePath;
        private string _name;
        private string _nickName;
        private string _description;

        #endregion

        #region --- Constructors ---

        public AddEditProfilePageViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Add/Edit Profile";
        }

        #endregion

        #region --- Public Properties ---

        public DelegateCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand(ExecuteSaveCommand));

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

        #endregion

        #region --- Overrides ---

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(nameof(ProfileModel), out object obj))
            {
                var profile = obj as ProfileModel;

                ProfileId = profile?.Id ?? -1;
                ProfileImagePath = profile?.ProfileImagePath ?? "pic_profile.png";
                Name = profile?.Name;
                NickName = profile?.NickName;
                Description = profile?.Description;
            }
            else
            {
                ProfileImagePath = "pic_profile.png";
            }
        }

        #endregion

        #region --- Command Handlers ---

        private void ExecuteSaveCommand()
        {
            UserDialogs.Instance.Alert($"id: {ProfileId}\nимя: {Name} {NickName}\n{Description}\n{ProfileImagePath}");
        }

        #endregion
    }
}
