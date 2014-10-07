using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MiniModel.Repo.Clowns;
using MiniModel.Service.Clowns;
using MobileApp.Views.Home;

namespace MobileApp.iOS.Glue.Modules
{
    public class AutoRegistrations : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof (HomeView).Assembly)
                .Where(t => t.Name.EndsWith("View")).AsSelf();

            builder.RegisterAssemblyTypes(typeof (HomeView).Assembly)
                .Where(t => t.Name.EndsWith("ViewModel")).AsSelf().PropertiesAutowired();


            builder.RegisterAssemblyTypes(typeof (ClownsService).Assembly)
                .Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(ClownRepo).Assembly)
                .Where(t => t.Name.EndsWith("Repo")).AsImplementedInterfaces();


            base.Load(builder);
        }
    }
}
