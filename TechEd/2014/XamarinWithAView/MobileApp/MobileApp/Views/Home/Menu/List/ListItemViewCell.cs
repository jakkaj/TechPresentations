using Xamarin.Forms;

namespace MobileApp.Views.Home.Menu.List
{
    public class ListItemView : ContentView
    {
        public ListItemView()
        {
            var l = new Label();

            l.TextColor = Color.FromHex("000000");
            l.SetBinding(Label.TextProperty, "Item.Name");

            var label = new ContentView
            {
                Padding = new Thickness(10, 0, 0, 5),
                Content = l
            };

            var layout = new StackLayout();

            layout.Children.Add(label);

            Content = layout;
        }
    }
}
