

using System.Windows.Input;
using Xamarin.Forms;
using XamlingCore.Portable.View.ViewModel;

namespace MobileApp.Views.Home
{
    public class HomeViewModel : XViewModel
    {
        public string MainText { get; set; }

        public ICommand NextPageCommand { get; set; }
    
        public HomeViewModel()
        {
            MainText = "Jordan was ere.";
            NextPageCommand = new Command(_onNextPage);
        }

        void _onNextPage()
        {
            NavigateTo<AnotherPageViewModel>();
        }
    }
}
