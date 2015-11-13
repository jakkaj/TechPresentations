using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using YouCore.Contract.Services;
using YouCore.Entity.IFTTT;
using YouCore.Model.Services;
using YouCore.Web.Support.Contract;

namespace YouCore.API.Controllers.API.IFTTT
{
    [RoutePrefix("api/IFTTT")]
    public class IFTTTController : ApiController
    {
        private readonly IOperationResponder _responder;
        private readonly IIFTTTService _iftttService;

        public IFTTTController(IOperationResponder responder, IIFTTTService iftttService)
        {
            _responder = responder;
            _iftttService = iftttService;
        }

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Ping()
        {
            return Ok("Ping");
        }

        [Route("routemaker")]
        [HttpPost]
        public async Task<IHttpActionResult> RouteMaker(IFTTTPushRequest request)
        {
            var result = await _iftttService.RouteRequest(request);

            return _responder.ActionResult(result, Request);
        }
    }
}
