using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        public SignInPageViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Users SignIn";

            _navigationService = navigationService;
        }

        private readonly INavigationService _navigationService;

        private DelegateCommand _navigateCommand;

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigationCommand));

        async void ExecuteNavigationCommand()
        {
            await _navigationService.NavigateAsync("SignUpPage");
        }

        private bool _isButtonEnabled;
        public bool IsButtonEnabled
        {
            get => _isButtonEnabled;
            set => SetProperty(ref _isButtonEnabled, value);
        }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                SetProperty(ref _login, value);
                UpdateSignInButtonState();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                UpdateSignInButtonState();
            }
        }

        private void UpdateSignInButtonState()
        {
            IsButtonEnabled = !string.IsNullOrEmpty(Login) &&
                              !string.IsNullOrEmpty(Password);
        }
    }
}
