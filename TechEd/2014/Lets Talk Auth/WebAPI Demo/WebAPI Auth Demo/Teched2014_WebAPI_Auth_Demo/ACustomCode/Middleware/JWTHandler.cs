using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace Teched2014_WebAPI_Auth_Demo.ACustomCode.Middleware
{
    public class JWTHandler : AuthenticationHandler<JWTConfig>
    {
        private readonly JWTConfig _config;

        public JWTHandler(JWTConfig config)
        {
            _config = config;
        }

        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            throw new NotImplementedException();
        }
    }
}