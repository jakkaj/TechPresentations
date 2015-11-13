using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace YouCore.API.Controllers.API
{
    [RoutePrefix("api/ping")]
    public class PingController : ApiController
    {
        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Ping()
        {
            return Ok("Pinged worked");
        }
    }
}

