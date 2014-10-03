using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Teched2014_WebAPI_Auth_Demo.ACustomCode.Glue;

[assembly: OwinStartup(typeof(Teched2014_WebAPI_Auth_Demo.Startup))]

namespace Teched2014_WebAPI_Auth_Demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ProjectGlue.Boot();
            app.UseAutofacMiddleware(ProjectGlue.Container);
            app.UseAutofacWebApi(GlobalConfiguration.Configuration);

        }
    }
}
