using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Protocols;

namespace TestJWTFunctionClass
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class JWTValidator
    {
        public JWTValidator()
        {

        }

        public bool Validate(string token, string key)
        {
            var publicKey = _getKey(key);
            var tokenHandler = new JwtSecurityTokenHandler();

            string failReason = null;

            ClaimsPrincipal principal = null;

            var publicOnly = new RSACryptoServiceProvider();
            
//            publicOnly.FromXmlString(publicKey);
            var keyset = new JsonWebKeySet(key);

            var tokens = keyset.GetSigningTokens().LastOrDefault();
         
             
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningToken = tokens,
                ValidIssuer = "https://login.microsoftonline.com/0a7110e8-b2aa-48cf-844f-c43e3533288d/v2.0/",
                ValidateIssuer = true,

            };

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


        string _getKey(string key)
        {
            return key;
            return Encoding.UTF8.GetString(Convert.FromBase64String(key));
        }
    }
}
