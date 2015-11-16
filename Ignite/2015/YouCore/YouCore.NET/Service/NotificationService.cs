using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;
using XamlingCore.Portable.Contract.Config;
using XamlingCore.Portable.Model.Response;

namespace YouCore.NET.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IConfig _config;

        public NotificationService(IConfig config)
        {
            _config = config;
        }

        public async Task<XResult<bool>> SendWindowsNative(string message, string title)
        {
            var key = _config["NotificationHubKey"];

            var hub = NotificationHubClient
        .CreateClientFromConnectionString(key, "jordocore");
            //   var toast = $"<toast><visual><binding template=\"ToastText02\"><text id=\"1\">{title}</text><text id=\"2\">{message}</text></binding></visual></toast>";
            //     var result = await hub.SendWindowsNativeNotificationAsync(message, new List<string>() { "JordoMain" });

            var wins = new WindowsNotification($"{message}|{title}");

            wins.Headers.Add("X-WNS-Type", "wns/raw");
            var result = await hub.SendNotificationAsync(wins, new List<string>() { "JordoMain" });
           
            return new XResult<bool>(true);
        }
    }
}
