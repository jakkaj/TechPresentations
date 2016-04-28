using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClassicMVC.Startup))]
namespace ClassicMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
