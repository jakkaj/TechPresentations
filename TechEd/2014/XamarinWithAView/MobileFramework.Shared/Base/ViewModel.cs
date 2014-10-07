using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileFramework.Shared.Contract;
using MobileFramework.Shared.Navigation;
using MobileFramework.Shared.Properties;

namespace MobileFramework.Shared.Base
{
    public class ViewModel : NotifyBase, IDisposable
    {
        public IViewModelFactory ViewModelFactory { get; set; }
        public INavigationManager NavigationManager { get; set; } 

        public string Title { get; set; }

        public void NavigateTo<T>(Action<T> initialisedCallback = null)
            where T:ViewModel
        {
            var vm = ViewModelFactory.Create<T>(initialisedCallback);
            NavigationManager.NavigateTo(vm);
        }

        public T Create<T>(Action<T> initialisedCallback = null)
            where T : ViewModel
        {
            var vm = ViewModelFactory.Create<T>(initialisedCallback);
            return vm as T;
        }

        public void NavigateBack()
        {
            NavigationManager.NavigateBack();
        }

        public virtual void Dispose()
        {
           
        }

        public virtual void OnInitiated()
        {
            
        }
    }
}
