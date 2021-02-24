using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace ProfileBook.ViewModels
{
    public class MainListPageViewModel : ViewModelBase
    {
        public MainListPageViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "List View";
        }

        #region tmp

        private string _tmpPrompt = "Default text from MainListPageViewModel";
        public string TmpPrompt
        {
            get => _tmpPrompt;
            set => SetProperty(ref _tmpPrompt, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue("tmp", out string passedValue))
            {
                TmpPrompt = passedValue;
            }
        }

        #endregion
    }
}
