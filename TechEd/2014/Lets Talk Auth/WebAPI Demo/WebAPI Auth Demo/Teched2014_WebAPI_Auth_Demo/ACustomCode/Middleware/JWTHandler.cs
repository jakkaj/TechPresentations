using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Teched2014_WebAPI_Auth_Demo.ACustomCode.Jwt;

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

                var tokenValidator = new JwtParser();

                if (!tokenValidator.LoadToken(token))
                {
                    return null;
                }
                var tokenClaims = tokenValidator.GetClaims();
                var authClaims = new ClaimsIdentity(DefaultAuthenticationTypes.ExternalBearer);

                foreach (var item in tokenClaims)
                {
                    authClaims.AddClaim(new Claim(item.Key, item.Value));
                }

                var auth = new AuthenticationTicket(authClaims, new AuthenticationProperties() { });
                return auth;
            }

            return null;
        }
    }
}