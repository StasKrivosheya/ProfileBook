using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using ProfileBook.Services.Settings;
using ProfileBook.Views;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListPageViewModel : ViewModelBase
    {
        public MainListPageViewModel(INavigationService navigationService,
            ISettingsManager settingsManager) :
            base(navigationService)
        {
            Title = "List View";

            _settingsManager = settingsManager;
        }

        private readonly ISettingsManager _settingsManager;

        #region tmp

        private DelegateCommand _logOutCommand;
        public DelegateCommand LogOutCommand => _logOutCommand ??
                                                (_logOutCommand = new DelegateCommand(ExecuteDelegateCommand));

        private string _tmpPrompt = "Default text from MainListPageViewModel";
        public string TmpPrompt
        {
            get => _tmpPrompt;
            set => SetProperty(ref _tmpPrompt, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (_settingsManager.RememberedUserLogin != string.Empty)
            {
                TmpPrompt = $"Hello, dear {_settingsManager.RememberedUserLogin}. Press button below to log out.";
            }
        }

        private void ExecuteDelegateCommand()
        {
            _settingsManager.RememberedUserLogin = string.Empty;
            _settingsManager.RememberedUserId = -1;

            NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
        }

        #endregion
    }
}
