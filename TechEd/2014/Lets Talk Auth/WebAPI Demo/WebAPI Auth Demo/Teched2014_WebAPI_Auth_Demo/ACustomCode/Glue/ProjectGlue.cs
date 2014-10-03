using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Teched2014_WebAPI_Auth_Demo.ACustomCode.Middleware;

namespace Teched2014_WebAPI_Auth_Demo.ACustomCode.Glue
{
    public static class ProjectGlue
    {
        public static IContainer Container { get; private set; }

        public static void Boot()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterModule<ModelModule>();

            builder.RegisterType<TestOwinMiddleware>().InstancePerRequest();

            Container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(Container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

    }

}