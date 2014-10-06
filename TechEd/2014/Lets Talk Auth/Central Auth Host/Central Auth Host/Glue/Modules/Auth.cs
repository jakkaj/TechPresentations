using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Central_Auth_Host.Auth;
using Web.Extras.JWT;

namespace Central_Auth_Host.Glue.Modules
{
    public class Authorization : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtCreator>().AsImplementedInterfaces();
            builder.RegisterType<JwtParser>().AsImplementedInterfaces();
            builder.RegisterType<AspNetIdentityClaimsGetter>().AsImplementedInterfaces();
            base.Load(builder);
        }
    }
}
