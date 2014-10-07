using System.Collections.Generic;
using System.ComponentModel;
using MobileFramework.Shared.Base;
using MobileFramework.Shared.Navigation;

namespace MobileFramework.Shared.Contract
{
    public interface INavigationManager
    {
        Stack<ViewModel> BackStack { get; set; }
        ViewModel CurrentContent { get; set; }
        NavigationDirection NavigationDirection { get; set; }

        void NavigateTo<T>(T viewModel)
            where T : ViewModel;

        void NavigateBack(bool navigate = true);
        event PropertyChangedEventHandler PropertyChanged;
    }
}