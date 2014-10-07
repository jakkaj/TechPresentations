

using System.Windows.Input;
using MiniModel.Entity;
using MobileApp.Views.Home.Clowns;
using MobileApp.Views.Home.Menu;
using MobileFramework.Shared.Base;
using Xamarin.Forms;

namespace MobileApp.Views.Home
{
    public class HomeViewModel : ViewModel
    {
        public string MainText { get; set; }

        public ICommand NextPageCommand { get; set; }
        public ICommand NamesPageCommand { get; set; }
        public ICommand ClownsPageCommand { get; set; }

        public HomeViewModel()
        {
            MainText = "Jordan was ere.";
            NextPageCommand = new Command(_onNextPage);
            NamesPageCommand = new Command(_onNames);
            ClownsPageCommand = new Command(_onClowns);
        }

        void _onClowns()
        {
            NavigateTo<ClownsViewModel>();
        }

        void _onNames()
        {
            NavigateTo<NamesViewModel>();
        }

        void _onNextPage()
        {
            NavigateTo<AnotherPageViewModel>();
        }
    }
}
