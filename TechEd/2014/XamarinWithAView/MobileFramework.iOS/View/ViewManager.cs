using MobileFramework.Shared.Contract;

namespace MobileFramework.iOS.View
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