using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandExampleApp.Model
{
    public class YouCore
    {
        private Transferrer _transferrer;
        public YouCore()
        {
            _transferrer = new Transferrer();
        }
        public async Task WemoOn()
        {
            await _sendMaker("WemoOn");
        }

        public async Task WemoOff()
        {
            await _sendMaker("WemoOff");
        }

        async Task _sendMaker(string makerName)
        {
            var key = "b81-U_TxfxaNMSzC6sDFG";
            var url = $"https://maker.ifttt.com/trigger/{makerName}/with/key/{key}";

            var result = await _transferrer.Download(key, _getConfig(url));
        }

        IHttpTransferConfig _getConfig(string url, string verb = "GET")
        {
            var config = new StandardHttpConfig
            {
                Accept = "application/json",
                IsValid = true,
                Url = url,
                BaseUrl = url,
                Verb = url,
                Headers = new Dictionary<string, string>(),
                Retries = 2,
                RetryOnNonSuccessCode = false,
                Gzip = true,


            };

            return config;
        }
    }
}
