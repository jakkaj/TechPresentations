using MobileFramework.Shared.Navigation;
using Xamarin.Forms;

namespace MobileFramework.Shared.Contract
{
    public interface IViewShower
    {
        void ShowView(Page p, NavigationDirection direction);
    }
}