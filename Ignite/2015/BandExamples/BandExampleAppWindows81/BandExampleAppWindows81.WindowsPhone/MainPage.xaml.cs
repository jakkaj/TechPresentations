using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BandExampleApp.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BandExampleAppWindows81
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Instance { get; set; }
        private YouCore _youCore = new YouCore();
        public MainPage()
        {
            Instance = this;
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }


        public async void DoCommand(string command)
        {

            TxtThingThatHappened.Text = command;

            if (command.ToLower().IndexOf("on") != -1)
            {
                await _youCore.WemoOn();
            }
            else
            {
                await _youCore.WemoOff();
            }

            TxtThingThatHappened.Text += " done";

            Debug.WriteLine(command);
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                return;
            }
            var param = e.Parameter.ToString();
            if (string.IsNullOrWhiteSpace(param))
            {
                return;
            }
            DoCommand(param);

            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }


        private async void OnButton_OnClick(object sender, RoutedEventArgs e)
        {
            await _youCore.WemoOn();
        }

        private async void OffButton_OnClick(object sender, RoutedEventArgs e)
        {
            await _youCore.WemoOff();
        }
    }
}
