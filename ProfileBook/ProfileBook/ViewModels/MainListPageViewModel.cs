using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
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

        #region --- Private Fields ---

        private readonly ISettingsManager _settingsManager;

        private DelegateCommand _logOutCommand;
        private DelegateCommand _settingsTapCommand;

        #endregion

        #region --- Public Properties ---

        public DelegateCommand LogOutCommand =>
            _logOutCommand ?? (_logOutCommand = new DelegateCommand(ExecuteDelegateCommand));


        public DelegateCommand SettingsTapCommand =>
            _settingsTapCommand ?? (_settingsTapCommand = new DelegateCommand(ExecuteSettingsTapCommand));

        #endregion

        #region --- Private Helpers ---

        private void ExecuteDelegateCommand()
        {
            _settingsManager.RememberedUserLogin = string.Empty;
            _settingsManager.RememberedUserId = -1;

            NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
        }

        private void ExecuteSettingsTapCommand()
        {
            UserDialogs.Instance.Alert("Settings page hasn't been implemented yet.", "Oops");
        }

        #endregion

    }
}
