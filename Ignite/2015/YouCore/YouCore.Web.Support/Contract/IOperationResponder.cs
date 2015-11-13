using System.Net.Http;
using System.Web.Http;
using XamlingCore.Portable.Model.Response;

namespace YouCore.Web.Support.Contract
{
    public interface IOperationResponder
    {
        IHttpActionResult ActionResult<T>(XResult<T> result, HttpRequestMessage request);
    }
}