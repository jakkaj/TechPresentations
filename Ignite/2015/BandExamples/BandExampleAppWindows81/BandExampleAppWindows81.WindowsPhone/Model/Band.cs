using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Band;
using Microsoft.Band.Notifications;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;

namespace BandExampleAppWindows81.Model
{
    public static class BandBits
    {
        public static async Task TrackAccelerometer()
        {
            var pairedBands = await BandClientManager.Instance.GetBandsAsync();

            if (pairedBands.Length < 1)
            {
                return;
            }
            try
            {
                using (var bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]))
                {
                    var fwVersion = await bandClient.GetFirmwareVersionAsync();
                    var hwVersion = await bandClient.GetHardwareVersionAsync();

                    Debug.WriteLine($"FW: {fwVersion}");
                    Debug.WriteLine($"HW: {hwVersion}");

                    var supportedIntervals =
                        bandClient.SensorManager.Accelerometer.SupportedReportingIntervals.ToList();
                    //foreach (var ri in supportedHeartBeatReportingIntervals)
                    //{
                    //    // do work with each reporting interval (i.e., add them to a list in the UI)
                    //}
                    //bandClient.SensorManager.Accelerometer.ReportingInterval =
                    //    supportedIntervals.LastOrDefault();

                    bandClient.SensorManager.Accelerometer.ReadingChanged += (sender, args) =>
                    {
                        var x = args.SensorReading.AccelerationX;
                        var y = args.SensorReading.AccelerationY;
                        var z = args.SensorReading.AccelerationZ;

                        MainPage.Instance.SetProjection(x, y, z);  
                    };

                    await bandClient.SensorManager.Accelerometer.StartReadingsAsync();

                    await Task.Delay(TimeSpan.FromSeconds(25));
                    await bandClient.SensorManager.Accelerometer.StopReadingsAsync();
                    await bandClient.NotificationManager.VibrateAsync(VibrationType.NotificationOneTone);
                }
            }
            catch (BandException ex)
            {
                Debug.WriteLine(ex.Message);
                // handle a Band connection exception }
            }
        }

        public static async Task CreateTile()
        {
            //await Task.Delay(3000);
            var pairedBands = await BandClientManager.Instance.GetBandsAsync();

            if (pairedBands.Length < 1)
            {

                return;
            }
            try
            {
                using (var bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]))
                {
                    // do work after successful connect     } } 
                    var fwVersion = await bandClient.GetFirmwareVersionAsync();
                    var hwVersion = await bandClient.GetHardwareVersionAsync();

                    Debug.WriteLine($"FW: {fwVersion}");
                    Debug.WriteLine($"HW: {hwVersion}");

                    Guid myTileId = new Guid("13408A60-13EB-46C2-9D24-F14BF6A033C6");
                    BandTile myTile = new BandTile(myTileId)
                    {
                        Name = "Demo Tile",
                        TileIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconLarge.png"),
                        SmallIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconSmall.png")
                    };
                    TextButton button = new TextButton() { ElementId = 1, Rect = new PageRect(10, 10, 200, 90) };
                    FilledPanel panel = new FilledPanel(button) { Rect = new PageRect(0, 0, 220, 150) };
                    myTile.PageLayouts.Add(new PageLayout(panel));

                    // Remove the Tile from the Band, if present. An application won't need to do this everytime it runs. 
                    // But in case you modify this sample code and run it again, let's make sure to start fresh.
                    await bandClient.TileManager.RemoveTileAsync(myTileId);

                    // Create the Tile on the Band.
                    await bandClient.TileManager.AddTileAsync(myTile);

                    var pOff = new Guid("6F5FD06E-BD37-4B71-B36C-3ED9D721F200");
                    var pOn = new Guid("5F5FD06E-BD37-4B71-B36C-3ED9D721F200");

                    await bandClient.TileManager.SetPagesAsync(myTileId,
                        new PageData(pOn, 0, new TextButtonData(1, "Wemo On")),
                        new PageData(pOff, 0, new TextButtonData(1, "Wemo Off")));


                    // Subscribe to Tile events.
                    int buttonPressedCount = 0;
                    TaskCompletionSource<bool> closePressed = new TaskCompletionSource<bool>();
                    var dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
                    bandClient.TileManager.TileButtonPressed += (s, args) =>
                    {

                        var a = dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            () =>
                            {
                                if (args.TileEvent.PageId == pOn)
                                {
                                    Debug.WriteLine("On");
                                }
                                else
                                {
                                    Debug.WriteLine("Off");
                                }
                                buttonPressedCount++;
                                Debug.WriteLine($"{args.TileEvent.PageId} - {args.TileEvent.ElementId}");
                            }
                        );
                    };
                    bandClient.TileManager.TileClosed += (s, args) =>
                    {
                        Debug.WriteLine("Close");
                        closePressed.TrySetResult(true);
                    };

                    await bandClient.TileManager.StartReadingsAsync();

                    // Receive events until the Tile is closed.


                    await closePressed.Task;

                    // Stop listening for Tile events.
                    await bandClient.TileManager.StopReadingsAsync();




                    ////if (bandClient.SensorManager.HeartRate.GetCurrentUserConsent() != UserConsent.Granted)
                    ////{
                    ////    // user hasn’t consented, request consent  
                    ////    await bandClient.SensorManager.HeartRate.RequestUserConsentAsync();
                    ////}
                    //// get a list of available reporting intervals 
                    //var supportedIntervals =
                    //    bandClient.SensorManager.Accelerometer.SupportedReportingIntervals.ToList();
                    ////foreach (var ri in supportedHeartBeatReportingIntervals)
                    ////{
                    ////    // do work with each reporting interval (i.e., add them to a list in the UI)
                    ////}
                    ////bandClient.SensorManager.Accelerometer.ReportingInterval =
                    ////    supportedIntervals.LastOrDefault();

                    //bandClient.SensorManager.Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;

                    //await bandClient.SensorManager.Accelerometer.StartReadingsAsync();

                    //await Task.Delay(TimeSpan.FromSeconds(25));
                    //await bandClient.SensorManager.Accelerometer.StopReadingsAsync();
                    //await bandClient.NotificationManager.VibrateAsync(VibrationType.NotificationOneTone);
                }

            }
            catch (BandException ex)
            {
                Debug.WriteLine(ex.Message);
                // handle a Band connection exception }
            }
        }
        private static async Task<BandIcon> LoadIcon(string uri)
        {
            StorageFile imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uri));

            using (IRandomAccessStream fileStream = await imageFile.OpenAsync(FileAccessMode.Read))
            {
                WriteableBitmap bitmap = new WriteableBitmap(1, 1);
                await bitmap.SetSourceAsync(fileStream);
                return bitmap.ToBandIcon();
            }
        }
    }
}
