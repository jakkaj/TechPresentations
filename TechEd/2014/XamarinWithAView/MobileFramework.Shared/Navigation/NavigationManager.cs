using System.Collections.Generic;
using MobileFramework.Shared.Base;
using MobileFramework.Shared.Contract;

namespace MobileFramework.Shared.Navigation
{
    public class NavigationManager : NotifyBase, INavigationManager
    {
        private ViewModel _currentContent;
        
        public Stack<ViewModel> BackStack { get; set; }

        public NavigationDirection NavigationDirection { get; set; }

        public NavigationManager()
        {
            BackStack = new Stack<ViewModel>();
        }

        public void NavigateTo<T>(T viewModel)
            where T : ViewModel
        {
            BackStack.Push(viewModel);
            CurrentContent = viewModel;
        }

        public void NavigateBack(bool navigate = true)
        {
            var vm = BackStack.Pop();
            
            if (navigate)
            {
                CurrentContent = vm;    
            }
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

    public enum NavigationDirection
    {
        Forward,
        Backward
    }
}