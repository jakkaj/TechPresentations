using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;

namespace Teched2014_WebAPI_Auth_Demo.ACustomCode.Middleware
{
    public class TestOwinMiddleware : OwinMiddleware
    {
        public TestOwinMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            var auth = context.Authentication;
            var claims = auth.User.Claims;
            // var user = await _userService.GetCurrentUser();

            Debug.WriteLine("Testing start");
            await Next.Invoke(context);
            Debug.WriteLine("Testing end");

        }
    }
}