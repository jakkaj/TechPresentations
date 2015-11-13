using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using YouCore.API.Glue;

[assembly: OwinStartup(typeof(YouCore.API.Startup))]

namespace YouCore.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var g = new ProjectGlue();
            g.Init();
           
            app.UseAutofacMiddleware(g.Container);
            app.UseAutofacWebApi(GlobalConfiguration.Configuration);
        }
    }
}
