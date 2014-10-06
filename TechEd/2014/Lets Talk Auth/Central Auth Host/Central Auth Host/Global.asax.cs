using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Central_Auth_Host.Glue;

namespace Central_Auth_Host
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            new ProjectGlue().Init();
        }
    }
}
