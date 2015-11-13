using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamlingCore.Portable.Contract.Config;
using XamlingCore.Portable.Contract.Downloaders;
using XamlingCore.Portable.Net.DownloadConfig;
using XamlingCore.Portable.Net.Service;

namespace YouCore.NET.Config
{
    public class TransferConfigService : HttpTransferConfigServiceBase
    {
       
        public TransferConfigService(IConfig config)
        {
           
        }

        public override async Task<IHttpTransferConfig> GetConfig(string url, string verb)
        {
           

            var config = new StandardHttpConfig
            {
                Accept = "application/json",
                IsValid = true,
                Url = url,
                BaseUrl = url,
                Verb = verb,
                Headers = new Dictionary<string, string>(),
                Retries = 2,
                RetryOnNonSuccessCode = false,
                Gzip = true,


            };

            return config;
        }
    }
}
