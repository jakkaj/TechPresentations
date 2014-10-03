using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;

namespace Teched2014_WebAPI_Auth_Demo.ACustomCode.Middleware
{
    public class JWTMiddleware : AuthenticationMiddleware<JWTConfig>
    {
        private readonly JWTConfig _options;

        public JWTMiddleware(OwinMiddleware next, JWTConfig options) : base(next, options)
        {
            _options = options;
        }

        protected override AuthenticationHandler<JWTConfig> CreateHandler()
        {
            return new JWTHandler(_options);
        }
    }
}