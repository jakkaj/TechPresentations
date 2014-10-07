using System;
using MobileFramework.Shared.Base;
using MobileFramework.Shared.Contract;
using MobileFramework.Shared.Navigation;
using Xamarin.Forms;

namespace MobileFramework.Shared.Views
{
    public class ViewManager : IViewManager
    {
        private readonly INavigationManager _navManager;
        private readonly IViewResolver _viewResolver;
        private readonly IViewShower _viewShower;

        public ViewManager(INavigationManager navManager, 
            IViewResolver viewResolver,
            IViewShower viewShower)
        {
            _navManager = navManager;

            _viewResolver = viewResolver;
            _viewShower = viewShower;

            _navManager.PropertyChanged += _navManager_PropertyChanged;
        }

        void _onNavigated()
        {
            if (_navManager.CurrentContent == null)
            {
                return;
            }

            var view = _viewResolver.Resolve(_navManager.CurrentContent);
            
            var direction = _navManager.NavigationDirection;

            _viewShower.ShowView(view, direction);
        }

        void _navManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentContent")
            {
                _onNavigated();
            }
        }

        
    }
}