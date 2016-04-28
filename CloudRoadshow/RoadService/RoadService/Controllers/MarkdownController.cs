using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RoadService.Demo;

namespace RoadService.Controllers
{
    public class MarkdownController : ApiController
    {
        // POST api/values
        public async Task<IHttpActionResult> Post([FromBody]MarkdownStuff.UrlModel value)
        {
            var result = await MarkdownStuff.GetMarkdown(value.Url);
            value.Result = result;
            return Content(HttpStatusCode.OK, value);

        }
    }
}
