using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamlingCore.Portable.Contract.Config;

namespace Web.Extras.Configs
{
    public class WebConfig : IConfig
    {
        private string appendix = null;

        public WebConfig()
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains("RunMode"))
            {
                appendix = ConfigurationManager.AppSettings["RunMode"];
                if (string.IsNullOrWhiteSpace(appendix))
                {
                    appendix = null;
                }
            }
        }
        public string this[string index]
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains(index + appendix))
                {
                    return ConfigurationManager.AppSettings[index + appendix];
                }

                return ConfigurationManager.AppSettings[index];
            }
        }
    }
}
