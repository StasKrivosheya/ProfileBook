using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Validators;

namespace ProfileBook.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        public SignUpPageViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "Users SignUp";
        }

        private DelegateCommand _signUpCommand;
        public DelegateCommand SignUpCommand =>
            _signUpCommand ?? (_signUpCommand = new DelegateCommand(ExecuteSignUpCommand));

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

        private void ExecuteSignUpCommand()
        {
            if (Password == ConfirmPassword)
            {
                var loginValid = StringValidator.Validate(Login, StringValidator.Login);
                var passwordValid = StringValidator.Validate(Password, StringValidator.Password);

                if (!loginValid)
                {
                    UserDialogs.Instance.Alert("First letter must be non-digit.\n" +
                                               "Login length must be from 4 to 16 characters", "Invalid login!");
                }
                else if (!passwordValid)
                {
                    UserDialogs.Instance.Alert("Use at least 1 digit.\n" +
                                               "Use at least 1 lowercase letter.\n" +
                                               "Use at least 1 uppercase letter.\n" +
                                               "Password length must be from 8 to 16 characters.", "Invalid password!");
                    Password = ConfirmPassword = "";
                }
                else
                {
                    UserDialogs.Instance.Alert("Everything is ok so far. Wait for implementing repository", "Nice job");
                }

                // todo: check if login is already taken

                // todo: save to db and navigate to main list view

            }
            else
            {
                UserDialogs.Instance.Alert("Passwords are different!");
            }
        }
    }
}
