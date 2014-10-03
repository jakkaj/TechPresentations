using System.Web;
using System.Web.Mvc;

namespace Teched2014_WebAPI_Auth_Demo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
