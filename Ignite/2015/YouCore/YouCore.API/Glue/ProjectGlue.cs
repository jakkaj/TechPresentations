using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Xamling.Azure.Glue;
using Xamling.Azure.Table;
using XamlingCore.NET.Glue;
using XamlingCore.Portable.Contract.Downloaders;
using XamlingCore.Portable.Data.Glue;
using XamlingCore.Portable.Glue;
using XamlingCore.Portable.Net.Downloaders;
using YouCore.Model.Services;
using YouCore.NET.Config;
using YouCore.NET.Glue;
using YouCore.Web.Support.Glue;

public class ProjectGlue : GlueBase
{
    public override void Init()
    {
        base.Init(); //ensure you call this first so the builder and container are available

       
        Builder.RegisterAssemblyTypes(typeof(IFTTTService).Assembly)
            .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repo"))
            .AsImplementedInterfaces()
            .InstancePerRequest();
        

        Builder.RegisterModule<SupportModule>();

        Builder.RegisterModule<DefaultXCoreModule>();
        Builder.RegisterModule<DefaultNETModule>();
        Builder.RegisterModule<AzureModule>();

        Builder.RegisterModule<NetModule>();

        Builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

        Builder.RegisterControllers(Assembly.GetExecutingAssembly());

        Builder.RegisterType<TransferConfigService>().As<IHttpTransferConfigService>().InstancePerRequest();
        Builder.RegisterType<HttpClientTransferrer>().As<IHttpTransferrer>().InstancePerRequest();

        Container = Builder.Build();
        ContainerHost.Container = Container;
        DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
    }


}