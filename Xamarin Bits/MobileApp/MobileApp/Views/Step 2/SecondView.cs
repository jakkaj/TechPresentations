using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp.Views.Step_2
{
    public class SecondView : ContentView
    {
        public SecondView()
        {
            var l = new Label
            {
                Text = "Second view"
            };

            Content = l;
        }
    }
}
