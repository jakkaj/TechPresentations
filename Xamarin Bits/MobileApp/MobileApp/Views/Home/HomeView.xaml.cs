using Xamarin.Forms;
using XamlingCore.XamarinThings.Content.Lists;

namespace MobileApp.Views.Home
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            Title = "Home";


            var c = Content as StackLayout;

            var x = new XListView();
            x.SetBinding(ListView.ItemsSourceProperty, "Items");

            c.Children.Add(x);
        }
    }
}
