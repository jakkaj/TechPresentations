using Owin;
using Teched2014_WebAPI_Auth_Demo.ACustomCode.Middleware;

namespace Teched2014_WebAPI_Auth_Demo
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseJwtToken(new JWTConfig());
        }
    }
}
