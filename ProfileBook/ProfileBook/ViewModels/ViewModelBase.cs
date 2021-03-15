using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Helpers.Localization;

namespace ProfileBook.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        #region --- Fields ---

        private string _title;

        private LocalizedResources _resources;

        protected INavigationService NavigationService { get; private set; }

        #endregion

        #region --- Constructors ---

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;

            Resources = new LocalizedResources(typeof(Resources.Resource));
        }

        #endregion

        #region --- Public Properties ---

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public LocalizedResources Resources
        {
            get => _resources;
            set => SetProperty(ref _resources, value);
        }

        #endregion

        #region --- IInitialize Implementation ---

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        #endregion

        #region --- INavigationAware Implementation ---

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        #endregion

        #region --- IDestructible Implementation ---

        public virtual void Destroy()
        {

        }

        #endregion
    }
}
