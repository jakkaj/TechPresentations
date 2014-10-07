using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MobileFramework.Shared.Contract;
using MobileFramework.Shared.Navigation;
using MonoTouch.UIKit;
using Xamarin.Forms;

namespace MobileApp.iOS.Framework.Views
{
    public class iOSViewShower : IViewShower
    {
        private readonly INavigationManager _navigationManager;
        private UIWindow _window;
        public UIViewController RootView { get; set; }

        private NavigationPage _navigationPage;

        public iOSViewShower(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public async void ShowView(Page p, NavigationDirection direction)
        {
            if (_window == null)
            {
                await _init(p);
            }
            else
            {
                if (direction == NavigationDirection.Forward)
                {
                    await _navigationPage.PushAsync(p);
                }
                else
                {
                    await _navigationPage.PopAsync();
                }
            }
        }

        async Task _init(Page p)
        {
            _window = new UIWindow(UIScreen.MainScreen.Bounds);
            _navigationPage = new NavigationPage();

            _navigationPage.Popped += _navigationPage_Popped;
            await _navigationPage.PushAsync(p);

            RootView = _navigationPage.CreateViewController();

            _window.RootViewController = RootView;

            _window.MakeKeyAndVisible();
        }

        void _navigationPage_Popped(object sender, NavigationEventArgs e)
        {
            _navigationManager.NavigateBack(false);
        }
    }
}
