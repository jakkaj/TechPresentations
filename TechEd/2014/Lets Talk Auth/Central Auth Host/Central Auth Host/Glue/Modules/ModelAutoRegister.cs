using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Service.Services;
using Autofac;

namespace Central_Auth_Host.Glue.Modules
{
    public class ModelAutoRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(TokenService).Assembly)
             .Where(t => t.Name.EndsWith("Service"))
             .AsImplementedInterfaces()
             .SingleInstance();

            base.Load(builder);
        }
    }
}
