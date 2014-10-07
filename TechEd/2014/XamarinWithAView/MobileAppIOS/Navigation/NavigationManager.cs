

using System.Collections.Generic;
using MobileFramework.Shared.Base;
using MobileFramework.Shared.Contract;

namespace MobileAppIOS.Navigation
{
    public class NavigationManager : ViewModel, INavigationManager
    {
        private ViewModel _currentContent;
        
        public Stack<ViewModel> BackStack { get; set; }

        public void NavigateTo<T>(T viewModel)
            where T : ViewModel
        {
            BackStack.Push(viewModel);
            CurrentContent = viewModel;
        }

        public void NavigateBack()
        {
            var vm = BackStack.Pop();
            CurrentContent = vm;
        }

        public ViewModel CurrentContent
        {
            get { return _currentContent; }
            set
            {
                _currentContent = value;
                OnPropertyChanged();
            }
        }
    }
}