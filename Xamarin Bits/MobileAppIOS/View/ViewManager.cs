using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MobileFramework.Shared.Contract;
using UIKit;

namespace MobileAppIOS.View
{
    public class ViewManager
    {
        private readonly INavigationManager _navManager;

        public ViewManager(INavigationManager navManager)
        {
            _navManager = navManager;

            
        }
    }
}