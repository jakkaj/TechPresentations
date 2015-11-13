using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.OData.Query;

namespace YouCore.Web.Support.Controllers
{
    public class ValidateODataQueryOptionsForPagingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            object opts;

            if (actionContext.ActionArguments.TryGetValue("opts", out opts))
            {
                var odata = opts as ODataQueryOptions;

                if (odata == null)
                {
                    return;
                }
                
                var filterResult = OdataQueryOptions.ValidateODataQueryOptionsForPaging(actionContext.Request, odata);

                if (filterResult != null)
                {
                    actionContext.Response = filterResult;
                }
            }
        }
    }
}
