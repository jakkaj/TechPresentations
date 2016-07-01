using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;

namespace JWTNetTestBed
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class JWTDownloadKeyAndValidator
    {
        public JWTDownloadKeyAndValidator()
        {

        }

        public async Task<bool> Validate(string token, string adConfigName)
        {
            var url =
                $"https://login.microsoftonline.com/jordob2c.onmicrosoft.com/v2.0/.well-known/openid-configuration?p={adConfigName}";

            var result = await url.GetAndParse<OpenIdDiscoveryObject>();

            var keysUrl = result.jwks_uri;

            var keys = await keysUrl.GetRaw();

            return _validate(token, keys);
        }

        private bool _validate(string token, string key)
        {
           
            var tokenHandler = new JwtSecurityTokenHandler();

            string failReason = null;

            ClaimsPrincipal principal = null;

            var keyset = new JsonWebKeySet(key);

            var tokens = keyset.GetSigningTokens().LastOrDefault();
         
             
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningToken = tokens,
                ValidIssuer = "https://login.microsoftonline.com/0a7110e8-b2aa-48cf-844f-c43e3533288d/v2.0/",
                ValidateIssuer = true,

            };
            var f = Newtonsoft.Json.Formatting.None;
            validationParameters.AudienceValidator =
                delegate (IEnumerable<string> audiences, SecurityToken securityToken,
                    TokenValidationParameters parameters)
                {
                    var audience = "Aud";
                    if (parameters.ValidAudience != null)
                    {
                        return parameters.ValidAudience == audience;
                    }
                    return true;
                };

            failReason = null;

            try
            {
                SecurityToken validated = null;
                principal = tokenHandler.ValidateToken(token, validationParameters, out validated);
            }
            catch (SecurityTokenValidationException ex)
            {
                failReason = string.Format("SecurityTokenValidationException: {0}", ex.Message);
            }
            catch (ArgumentException ex)
            {
                failReason = string.Format("ArgumentException: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                failReason = string.Format("Exception: {0}", ex.Message);
            }


            return failReason == null;
        }


        public class OpenIdDiscoveryObject
        {
            public string issuer { get; set; }
            public string authorization_endpoint { get; set; }
            public string token_endpoint { get; set; }
            public string end_session_endpoint { get; set; }
            public string jwks_uri { get; set; }
            public List<string> response_modes_supported { get; set; }
            public List<string> response_types_supported { get; set; }
            public List<string> scopes_supported { get; set; }
            public List<string> subject_types_supported { get; set; }
            public List<string> id_token_signing_alg_values_supported { get; set; }
            public List<string> token_endpoint_auth_methods_supported { get; set; }
            public List<string> claims_supported { get; set; }
        }

    }
}
