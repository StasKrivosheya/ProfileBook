using System.ComponentModel;
using Prism.Commands;
using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Resources;
using ProfileBook.Services.UserService;
using ProfileBook.Validators;

namespace ProfileBook.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        #region --- Private Fields ---

        private readonly IUserService _userService;

        private DelegateCommand _signUpCommand;

        private bool _isButtonEnabled;

        private string _login;
        private string _password;
        private string _confirmPassword;

        #endregion

        #region --- Constructors ---

        public SignUpPageViewModel(INavigationService navigationService,
            IUserService userService) :
            base(navigationService)
        {
            Title = Resource.SignUpTitle;

            _userService = userService;
        }

        #endregion

        #region --- Public Properties

        public DelegateCommand SignUpCommand =>
            _signUpCommand ?? (_signUpCommand = new DelegateCommand(ExecuteSignUpCommand));

        public bool IsEnabled
        {
            get => _isButtonEnabled;
            private set => SetProperty(ref _isButtonEnabled, value);
        }

        public string Login
        {
            get => _login;
            set
            {
                SetProperty(ref _login, value);

                UpdateSignUpButtonState();
            }
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        #endregion

        #region --- Overrides ---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Password) ||
                args.PropertyName == nameof(ConfirmPassword) ||
                args.PropertyName == nameof(Login))
            {
                UpdateSignUpButtonState();
            }
        }

        #endregion

        #region --- Command Handlers ---

        private async void ExecuteSignUpCommand()
        {
            if (Password == ConfirmPassword)
            {
                var loginValid = StringValidator.Validate(Login, StringValidator.Login);
                var passwordValid = StringValidator.Validate(Password, StringValidator.Password);

                if (!loginValid)
                {
                    UserDialogs.Instance.Alert(Resource.InvalidLoginMessage,
                        Resource.InvalidLoginTitle, Resource.OkText);
                }
                else if (!passwordValid)
                {
                    UserDialogs.Instance.Alert(Resource.InvalidPasswordMessage,
                        Resource.InvalidPasswordTitle, Resource.OkText);
                }
                else
                {
                    var userModel = new UserModel
                    {
                        Login = Login,
                        Password = Password
                    };

                    var result = await _userService.InsertItemAsync(userModel);

                    if (result == -1)
                    {
                        UserDialogs.Instance.Alert(Resource.RegisterFailedMessage,
                            Resource.RegisterFailedTitle, Resource.OkText);
                    }
                    else
                    {
                        var parameters = new NavigationParameters { { nameof(Login), Login } };
                        await NavigationService.GoBackAsync(parameters);
                    }
                }
            }
            else
            {
                UserDialogs.Instance.Alert(Resource.DifferentPasswordError,
                    Resource.DifferentPasswordError, Resource.OkText);
            }
        }

        #endregion

        #region --- Private Helpers ---

        private void UpdateSignUpButtonState()
        {
            bool allEntriesAreFilled = !string.IsNullOrEmpty(_login) &&
                                       !string.IsNullOrEmpty(_password) &&
                                       !string.IsNullOrEmpty(_confirmPassword);

            IsEnabled = allEntriesAreFilled;
        }

        #endregion
    }
}
