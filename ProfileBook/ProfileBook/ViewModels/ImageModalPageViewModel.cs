using Prism.Commands;
using Prism.Navigation;

namespace ProfileBook.ViewModels
{
    public class ImageModalPageViewModel : ViewModelBase
    {
        #region --- Private Fields ---

        private string _profileImagePath;

        #endregion

        #region --- Constructors ---

        public ImageModalPageViewModel(INavigationService navigationService) :
            base(navigationService)
        {
        }

        #endregion

        #region --- Public Properties ---

        public string ProfileImagePath
        {
            get => _profileImagePath;
            set => SetProperty(ref _profileImagePath, value);
        }

        public DelegateCommand ImageClickCommand => new DelegateCommand(ExecuteImageClickCommand);

        #endregion

        #region --- Overrides ---

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(nameof(ProfileImagePath), out string profileImagePath))
            {
                ProfileImagePath = profileImagePath;
            }
            else
            {
                ProfileImagePath = Constants.DEFAULT_PROFILE_PIC;
            }
        }

        #endregion

        #region --- Command Handlers ---

        private void ExecuteImageClickCommand()
        {
            NavigationService.GoBackAsync();
        }

        #endregion
    }
}
