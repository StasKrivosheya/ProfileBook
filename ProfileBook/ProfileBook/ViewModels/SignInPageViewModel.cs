using System.ComponentModel;
using Prism.Commands;
using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.UserService;
using ProfileBook.Views;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        #region --- Private Fields ---

        private readonly IAuthorizationService _authorizationService;
        private readonly IUserService _userService;

        private DelegateCommand _navigateCommand;
        private DelegateCommand _signInCommand;

        private bool _isButtonEnabled;

        private string _login;
        private string _password;

        #endregion

        #region --- Constructors ---

        public SignInPageViewModel(INavigationService navigationService,
            IUserService userService,
            IAuthorizationService authorizationService) :
            base(navigationService)
        {
            Title = "Users SignIn";

            _userService = userService;

            _authorizationService = authorizationService;
        }

        #endregion

        #region --- Public Properties ---

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigationCommand));


        public DelegateCommand SignInCommand =>
            _signInCommand ?? (_signInCommand = new DelegateCommand(ExecuteSignInCommand));


        public bool IsButtonEnabled
        {
            get => _isButtonEnabled;
            set => SetProperty(ref _isButtonEnabled, value);
        }

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        #endregion

        #region --- Overrides ---

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(nameof(Login), out string login))
            {
                Login = login;
                Password = string.Empty;
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Password) ||
                args.PropertyName == nameof(Login))
            {
                UpdateSignInButtonState();
            }
        }

        #endregion

        #region --- Command Handlers ---

        private async void ExecuteNavigationCommand()
        {
            await NavigationService.NavigateAsync(nameof(SignUpPage));
        }

        private async void ExecuteSignInCommand()
        {
            var query = await _userService.GetItemAsync(u =>
                u.Login.Equals(Login) && u.Password.Equals(Password));

            if (query != null)
            {
                _authorizationService.Authorize(query.Id, query.Login);

                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}" +
                                                      $"/{nameof(MainListPage)}");
            }
            else
            {
                UserDialogs.Instance.Alert("Please, check your inputs and retry.\n" +
                                           "Or register if you haven't done it yet!",
                    "Invalid login or password", "OK");
                Password = string.Empty;
            }
        }

        #endregion

        #region --- Private Helpers ---

        private void UpdateSignInButtonState()
        {
            IsButtonEnabled = !string.IsNullOrEmpty(Login) &&
                              !string.IsNullOrEmpty(Password);
        }

        #endregion
    }
}
