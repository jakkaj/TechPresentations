using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Central_Auth_Host.Glue.Modules;
using XamlingCore.Portable.Glue;


namespace Central_Auth_Host.Glue
{
    public class ProjectGlue : GlueBase
    {
        public override void Init()
        {
            base.Init();

            Builder.RegisterModule<ModelAutoRegister>();
            Builder.RegisterModule<DefaultXCoreModule>();
            Builder.RegisterModule<WebModule>();
            Builder.RegisterModule<Authorization>();

            Builder.RegisterControllers(typeof(MvcApplication).Assembly);
            Container = Builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}
