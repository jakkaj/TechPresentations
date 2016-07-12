using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AzureFunctionsToolkit.Extensions;
using Microsoft.IdentityModel.Protocols;

namespace JWTNetTestBed
{
    public static class JwtDownloadKeyAndValidator
    {
        public static async Task<TokenResult> Validate(string token, string adConfigName)
        {
            var url =
                $"https://login.microsoftonline.com/jordob2c.onmicrosoft.com/v2.0/.well-known/openid-configuration?p={adConfigName}";

            var result = await url.GetAndParse<OpenIdDiscoveryObject>();

            var keysUrl = result.jwks_uri;

            var keys = await keysUrl.GetRaw();

            return JwtValidator.ValidateWithJwk(token, keys, "https://login.microsoftonline.com/0a7110e8-b2aa-48cf-844f-c43e3533288d/v2.0/", adConfigName);
        }
    }
}
