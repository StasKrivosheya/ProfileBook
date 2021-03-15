using System.Collections.Generic;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.ProfileService;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using ProfileBook.Services.Theming;
using ProfileBook.Services.UserService;
using ProfileBook.Themes;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        private IAuthorizationService _authorizationService;
        private ITheming _themingManager;

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        private IAuthorizationService AuthorizationService =>
            _authorizationService ?? (_authorizationService = Container.Resolve<IAuthorizationService>());

        private ITheming ThemingService => 
        _themingManager ?? (_themingManager = Container.Resolve<ITheming>());

        #region --- Overrides ---

        protected override async void OnInitialized()
        {
            InitializeComponent();

            // setting remembered theme
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                switch (ThemingService.IsDarkTheme)
                {
                    case true:
                        mergedDictionaries.Add(new DarkTheme());
                        break;
                    case false:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
            }

            // navigating to necessary page
            if (AuthorizationService.IsAuthorized)
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}" +
                                                      $"/{nameof(MainListPage)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}" +
                                                      $"/{nameof(SignInPage)}");
            }
            
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // PRISM autogenerated
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            // Services
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<IProfileService>(Container.Resolve<ProfileService>());
            containerRegistry.RegisterInstance<ITheming>(Container.Resolve<Theming>());

            // Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<MainListPage, MainListPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfilePage, AddEditProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
        }

        #endregion

        public static T Resolve<T>() => (Application.Current as App).Container.Resolve<T>();
    }
}
