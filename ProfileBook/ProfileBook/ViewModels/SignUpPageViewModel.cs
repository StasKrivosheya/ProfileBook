using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace ProfileBook.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        public SignUpPageViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Users SignUp";
        }

        private bool _isButtonEnabled;
        public bool IsEnabled
        {
            get => _isButtonEnabled;
            private set => SetProperty(ref _isButtonEnabled, value);
        }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                SetProperty(ref _login, value);

                UpdateSignUpButtonState();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);

                UpdateSignUpButtonState();
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                SetProperty(ref _confirmPassword, value);

                UpdateSignUpButtonState();
            }
        }

        private void UpdateSignUpButtonState()
        {
            bool allEntriesAreFilled = !string.IsNullOrEmpty(_login) &&
                                       !string.IsNullOrEmpty(_password) &&
                                       !string.IsNullOrEmpty(_confirmPassword);

            IsEnabled = allEntriesAreFilled;
        }
    }
}
