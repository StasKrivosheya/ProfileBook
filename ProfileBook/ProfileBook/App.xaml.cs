using System.Collections.Generic;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        private ISettingsManager _settingsManager;

        private ISettingsManager SettingsManager => _settingsManager ?? (_settingsManager = Container.Resolve<ISettingsManager>());

        protected override async void OnInitialized()
        {
            InitializeComponent();

            if (SettingsManager.RememberedUserLogin == string.Empty)
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}" +
                                                      $"/{nameof(SignInPage)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}" +
                                                      $"/{nameof(MainListPage)}");
            }
            
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // PRISM autogenerated
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            // Services
            containerRegistry.RegisterInstance<IRepositoryService>(Container.Resolve<RepositoryService>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());

            // Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<MainListPage, MainListPageViewModel>();
        }
    }
}
