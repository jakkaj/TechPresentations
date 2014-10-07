using System;
using System.Collections.Generic;
using System.Text;
using MobileFramework.Shared.Base;
using MobileFramework.Shared.Contract;
using MonoTouch.UIKit;

namespace MobileApp.iOS.Framework.Views
{
    public class iOSViewHost
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly INavigationManager _navigationManager;

        public iOSViewHost(IViewModelFactory viewModelFactory, 
            INavigationManager navigationManager,
            IViewResolver viewResolver, IViewManager viewManager)
        {
            _viewModelFactory = viewModelFactory;
            _navigationManager = navigationManager;
        }

        public void Init<T>()where T:ViewModel
        {
            var initialVm = _viewModelFactory.Create<T>();
            _navigationManager.NavigateTo(initialVm);

            
        }
    }
}
