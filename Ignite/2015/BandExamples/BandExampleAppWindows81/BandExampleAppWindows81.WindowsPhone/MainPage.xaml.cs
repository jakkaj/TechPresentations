﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BandExampleApp.Model;
using BandExampleAppWindows81.Model;
using BandExampleBackground;

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

        public async void SetProjection(double x, double y, double z)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
             {
                 PlaneProjection.RotationX = x * 100;
                 PlaneProjection.RotationY = y * 100;
                 PlaneProjection.RotationZ = z * 100;
             });

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

        async void _backgrounds()
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                task.Value.Unregister(true);
            }

            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();

            var builder = new BackgroundTaskBuilder { Name = "BandBackroundNotifier", TaskEntryPoint = typeof(BandBackgroundNotifier).FullName };

            var trigger = new PushNotificationTrigger(); 

            builder.SetTrigger(trigger);
            builder.Register();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _backgrounds();

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

        private async void TrackAccel_OnClick(object sender, RoutedEventArgs e)
        {
            TxtThingThatHappened.Text = "Acceleromoeter";
            await BandBits.TrackAccelerometer();
            TxtThingThatHappened.Text = "Finished";
        }

        private async void AddTile_OnClick(object sender, RoutedEventArgs e)
        {
            await BandBits.CreateTile();
        }
    }
}
