using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace YouCore.Web.Support.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class InvalidModelStateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                Debug.WriteLine(actionContext.ModelState);
                actionContext.Response = actionContext.Request.CreateResponse(
                   HttpStatusCode.BadRequest,
                   actionContext.ModelState
               );
            }

            else
                base.OnActionExecuting(actionContext);
        }
    }
}