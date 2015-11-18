using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using YouCore.NET.Service;
using YouCore.Web.Support.Contract;

namespace YouCore.API.Controllers.API.Notification
{
    [RoutePrefix("api/notify")]
    public class NotifyController : ApiController
    {
        private readonly INotificationService _notificationService;
        private readonly IOperationResponder _response;

        public NotifyController(INotificationService notificationService, IOperationResponder response)
        {
            _notificationService = notificationService;
            _response = response;
        }

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Notify(string title, string message)
        {
            var result = await _notificationService.SendWindowsNative(message, title);
            return _response.ActionResult(result, Request);
        }
    }
}
