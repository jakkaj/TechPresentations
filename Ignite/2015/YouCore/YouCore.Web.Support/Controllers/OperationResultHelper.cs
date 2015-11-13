using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Xamling.Azure.Portable.Contract;
using XamlingCore.Portable.Model.Response;
using YouCore.Web.Support.Contract;

namespace YouCore.Web.Support.Controllers
{
    public class OperationResponder : IOperationResponder
    {
        private readonly ILogService _logService;

        public OperationResponder(ILogService logService)
        {
            _logService = logService;
        }

        public IHttpActionResult ActionResult<T>(XResult<T> result, HttpRequestMessage request)
        {
            _logService.TrackOperation(result);
            var op = new OperationActionResult<T>(result, request);
            return op;
        }
    }


    public static class OperationActionResult
    {
        public static IHttpActionResult ActionResult<T>(this XResult<T> result, HttpRequestMessage request)
        {
            return new OperationActionResult<T>(result, request);
        }
    }

    public class OperationActionResult<T> : IHttpActionResult
    {
        private readonly XResult<T> _result;
        private readonly HttpRequestMessage _request;

        public OperationActionResult(XResult<T> result, HttpRequestMessage request)
        {
            _result = result;
            _request = request;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            HttpStatusCode code;

            switch (_result.ResultCode)
            {
                case OperationResults.Success:
                    code = HttpStatusCode.OK;
                    _result.Id = null;
                    break;
                case OperationResults.Failed:
                     code = HttpStatusCode.InternalServerError;
                    break;
                case OperationResults.Exception:
                    code = HttpStatusCode.InternalServerError;
                    break;
                case OperationResults.NotFound:
                    code = HttpStatusCode.NotFound;
                    break;
                case OperationResults.NotAuthorised:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case OperationResults.BadRequest:
                    code = HttpStatusCode.BadRequest;
                    break;
                default:
                    code = HttpStatusCode.OK;
                    break;
            }

#if !DEBUG
            _result.CallerInfo = null; 
            _result.CallerInfoHistory = null;
#endif

            response = _request.CreateResponse(code, _result);


            return response;
        }
    }
}
