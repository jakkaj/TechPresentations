﻿using System.Collections.Generic;

namespace BandExampleApp.Model
{
    public class StandardHttpConfig : IHttpTransferConfig
    {
        public string Url { get; set; }

        public string Verb { get; set; }

        public string BaseUrl { get; set; }

        public string Auth { get; set; }
        public string AuthScheme { get; set; }

        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Cookies { get; set; }

        public string Accept { get; set; }
        public List<string> AcceptEncoding { get; set; }

        public string ContentEncoding { get; set; }

        public bool IsValid { get; set; }

        public bool Gzip { get; set; }

        public bool AllowRedirect { get; set; }
        public int Retries { get; set; }
        public bool RetryOnNonSuccessCode { get; set; }

        public int Timeout { get; set; }
    }
}
