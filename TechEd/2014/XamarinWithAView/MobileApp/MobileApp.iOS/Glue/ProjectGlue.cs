using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MiniCore.Contract;
using MiniCore.Glue;
using MobileApp.Glue;
using MobileApp.iOS.Framework.Implementation;
using MobileApp.iOS.Framework.Views;
using MobileApp.iOS.Glue.Modules;
using MobileFramework.Shared.Contract;
using MobileFramework.Shared.Factory;
using MobileFramework.Shared.Navigation;
using MobileFramework.Shared.Views;

namespace MobileApp.iOS.Glue
{
    public static class ProjectGlue
    {
        public static IContainer Container { get; set; }

        public static void Init()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<AutoRegistrations>();
            builder.RegisterModule<MiniCoreModule>();

            builder.RegisterType<ViewModelFactory>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<NavigationManager>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ViewManager>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<iOSViewShower>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<iOSViewResolver>().AsImplementedInterfaces().SingleInstance();
            
            builder.RegisterType<iOSViewHost>().AsSelf();

            builder.RegisterType<LocalStorage>().As<ILocalStorage>().SingleInstance();

            Container = builder.Build();
            ContainerHost.Container = Container;
        }
    }
}
