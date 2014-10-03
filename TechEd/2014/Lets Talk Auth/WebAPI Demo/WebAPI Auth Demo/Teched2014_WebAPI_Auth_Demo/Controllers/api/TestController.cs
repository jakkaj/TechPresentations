using System.Threading.Tasks;
using System.Web.Http;

namespace Teched2014_WebAPI_Auth_Demo.Controllers.api
{
    public class TestController : ApiController
    {
        [Authorize]
        public async Task<IHttpActionResult> Get()
        {
            return Ok("Test");
        }
    }
}
