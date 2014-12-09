using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MonoTouch.UIKit;

namespace MobileApp.iOS.Step_1
{
    public class iOSAlert : IAlertDisplayer
    {
        public void DisplayAlert(string title, string message)
        {
            var alert = new UIAlertView(title, message, null, "Cancel",
                                   "Ok");
            alert.Clicked += (sender, args) => Debug.WriteLine(args.ButtonIndex);
            alert.Show();
        }
    }
}
