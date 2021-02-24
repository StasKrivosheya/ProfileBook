using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Views;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        public SignInPageViewModel(INavigationService navigationService,
            IRepositoryService repositoryService) :
            base(navigationService)
        {
            Title = "Users SignIn";

            _repositoryService = repositoryService;
            _repositoryService.CreateTableAsync<UserModel>();
        }

        private readonly IRepositoryService _repositoryService;

        private DelegateCommand _navigateCommand;
        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigationCommand));

        private DelegateCommand _signInCommand;
        public DelegateCommand SignInCommand =>
            _signInCommand ?? (_signInCommand = new DelegateCommand(ExecuteSignInCommand));

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

        private async void ExecuteNavigationCommand()
        {
            await NavigationService.NavigateAsync(nameof(SignUpPage));
        }

        private async void ExecuteSignInCommand()
        {
            var query = await _repositoryService.GetItemAsync<UserModel>(u =>
                u.Login.Equals(Login) && u.Password.Equals(Password));

            if (query != null)
            {
                // todo: navigation to main list page
                // tmp alert
                UserDialogs.Instance.Alert($"Dear {Login}, your login and password are match, so u're in)",
                    "Success");
            }
            else
            {
                UserDialogs.Instance.Alert("Please, check your inputs and retry.\n" +
                                           "Or register if you haven't done it yet!",
                    "Wrong login or password");
            }
        }
    }
}
