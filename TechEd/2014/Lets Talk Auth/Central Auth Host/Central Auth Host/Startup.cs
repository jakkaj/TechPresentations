using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Central_Auth_Host.Startup))]
namespace Central_Auth_Host
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
