using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using YouCore.NET.Service;

namespace YouCore.NET.Glue
{
    public class NetModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(NotificationService).Assembly)
               .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repo"))
               .AsImplementedInterfaces()
               .InstancePerRequest();
            base.Load(builder);
        }
    }
}
