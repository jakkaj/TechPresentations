using Autofac;
using XamlingCore.Portable.Contract.Config;
using YouCore.Web.Support.Config;
using YouCore.Web.Support.Contract;
using YouCore.Web.Support.Controllers;

namespace YouCore.Web.Support.Glue
{
    public class SupportModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebConfig>().As<IConfig>().SingleInstance();
            builder.RegisterType<OperationResponder>().As<IOperationResponder>().InstancePerRequest();
            base.Load(builder);
        }
    }
}
