using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Web.Extras.Configs;
using XamlingCore.Portable.Contract.Config;

namespace Central_Auth_Host.Glue.Modules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebConfig>().As<IConfig>().SingleInstance();
            base.Load(builder);
        }
    }
}
