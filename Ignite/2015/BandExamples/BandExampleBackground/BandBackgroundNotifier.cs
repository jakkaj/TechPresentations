using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Networking.PushNotifications;
using Windows.UI.Notifications;
using Microsoft.Band;
using Microsoft.Band.Notifications;

namespace BandExampleBackground
{
    public sealed class BandBackgroundNotifier : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            RawNotification notification = (RawNotification)taskInstance.TriggerDetails;
            var data = notification.Content;
            NotifyBand(taskInstance.GetDeferral(), data,"title");
        }

        private async void NotifyBand(BackgroundTaskDeferral deferral, string message, string title)
        {
            var pairedBands = await BandClientManager.Instance.GetBandsAsync();
            message = message.Trim();
            title = title.Trim();
            if (pairedBands.Length < 1)
            {

                return;
            }
            try
            {
                using (var bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]))
                {
                    Debug.WriteLine($"Notifying: {message}, {title}");
                    Guid myTileId = new Guid("25508A60-13EB-46C2-9D24-F14BF6A033C6");
                    await
                        bandClient.NotificationManager.SendMessageAsync(myTileId, title, message, DateTimeOffset.Now,
                            MessageFlags.ShowDialog);
                   // await bandClient.NotificationManager.ShowDialogAsync(myTileId, title, message);
                    await Task.Delay(3000);
                }
            }
            catch (BandException ex)
            {
                Debug.WriteLine(ex.Message);
                // handle a Band connection exception }
            }
            deferral.Complete();

            //var tileContent = TileUpdateManager.GetTemplateContent(
            //  TileTemplateType.TileSquareText03);

            //var tileLines = tileContent.SelectNodes("tile/visual/binding/text");

            //var networkStatus =
            //  Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();

            //tileLines[0].InnerText = (networkStatus == null) ?
            //  "No network" :
            //  networkStatus.GetNetworkConnectivityLevel().ToString();

            //tileLines[1].InnerText = DateTime.Now.ToString("MM/dd/yyyy");
            //tileLines[2].InnerText = DateTime.Now.ToString("HH:mm:ss");

            //var notification = new TileNotification(tileContent);

            //var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            //updater.Update(notification);

        }
    }
}
