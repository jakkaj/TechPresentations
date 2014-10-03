using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
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

        protected async override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            string[] headers;
            if (Request.Headers.TryGetValue("Authorization", out headers))
            {
                var bearer = headers[0];
                
                if (!bearer.StartsWith("Token"))
                {
                    return null;
                }
                
                var token = bearer.Replace("Token", "").Trim();

                var authClaims = new ClaimsIdentity(DefaultAuthenticationTypes.ExternalBearer);
                
                authClaims.AddClaim(new Claim(ClaimTypes.NameIdentifier, "jordanwasere"));
                authClaims.AddClaim(new Claim(ClaimTypes.Name, "jordanwasere"));
                authClaims.AddClaim(new Claim(ClaimTypes.Role, "User"));

                var auth = new AuthenticationTicket(authClaims, new AuthenticationProperties() { });
                return auth;
            }

            return null;
        }
    }
}