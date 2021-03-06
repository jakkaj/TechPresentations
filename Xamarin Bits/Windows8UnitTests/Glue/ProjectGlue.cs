﻿using System.Globalization;
using System.Reflection;
using Windows8UnitTests.Mock;
using MiniModel.Model.Service;
using XamlingCore.Portable.Glue;
using XamlingCore.Portable.Glue.Glue;
using XamlingCore.Portable.Glue.Locale;
using Autofac;
using Autofac.Core;
using XamlingCore.Portable.Contract.Infrastructure.LocalStorage;
using XamlingCore.Portable.Contract.Network;
using XamlingCore.Portable.Data.Glue;
using XamlingCore.Windows8.Implementations;

namespace Windows8UnitTests.Glue
{
    public class ProjectGlue : GlueBase 
    {
        public override void Init()
        {
            base.Init();

            Builder.RegisterModule<DefaultXCoreModule>();
            Builder.RegisterType<WinMockDeviceNetworkStatus>().As<IDeviceNetworkStatus>().SingleInstance();
            Builder.RegisterType<LocalStorageWindows8>().As<ILocalStorage>().SingleInstance();
            var modelAssmbly = typeof(SampleService).GetTypeInfo().Assembly;

            Builder.RegisterModule(new XAutofacModule(modelAssmbly, "Service"));
            Builder.RegisterModule(new XAutofacModule(modelAssmbly, "Repo"));

            Container = Builder.Build();

            ContainerHost.Container = Container;
        }


    }

}