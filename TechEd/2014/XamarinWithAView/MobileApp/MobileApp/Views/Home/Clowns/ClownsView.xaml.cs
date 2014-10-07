using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Controls;
using Xamarin.Forms;

namespace MobileApp.Views.Home.Clowns
{
    public partial class ClownsView : ContentPage
    {
        public ClownsView()
        {
            InitializeComponent();
            _init();
        }

        void _init()
        {
            var list = new XListView();
            list.BackgroundColor = Color.Transparent;

            list.SetBinding(ListView.ItemsSourceProperty, "ClownsList");
            list.SetBinding(ListView.SelectedItemProperty, "SelectedItem");

            var stack = this.FindByName<StackLayout>("MainStack");
            stack.Children.Add(list);
        }
    }
}
