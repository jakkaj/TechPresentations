using System.Configuration;
using System.Linq;
using XamlingCore.Portable.Contract.Config;

namespace YouCore.Web.Support.Config
{
    public class WebConfig : IConfig
    {
        private string appendix = null;

        public string ConfigPath { get; set; }

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
                if (ConfigPath != null || (ConfigurationManager.AppSettings.AllKeys.Contains("SpecialConfig")))
                {
                    var cp = ConfigPath;

                    if (string.IsNullOrWhiteSpace(cp))
                    {
                        cp = ConfigurationManager.AppSettings["SpecialConfig"];
                    }  
                                      
                    var c = new ExeConfigurationFileMap();
                    c.ExeConfigFilename = cp;
                    var config = ConfigurationManager.OpenMappedExeConfiguration(c, ConfigurationUserLevel.None);
                    if (config.AppSettings.Settings.AllKeys.Contains(index + appendix))
                    {
                        return config.AppSettings.Settings[index + appendix].Value;
                    }

                    var setting = config.AppSettings.Settings[index];

                    if (setting == null)
                    {
                        return null;
                    }

                    return setting.Value;

                }
                if (ConfigurationManager.AppSettings.AllKeys.Contains(index + appendix))
                {
                    return ConfigurationManager.AppSettings[index + appendix];
                }

                return ConfigurationManager.AppSettings[index];
            }
        }
    }
}
