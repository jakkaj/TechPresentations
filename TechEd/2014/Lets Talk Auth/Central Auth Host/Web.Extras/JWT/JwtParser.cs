using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Auth.Contract.Authorization;
using XamlingCore.Portable.Contract.Config;

namespace Web.Extras.JWT
{
    public class JwtParser : IJwtParser
    {
        private readonly IConfig _config;

        private ClaimsPrincipal _principal;

        private string _failReason;

        public JwtParser(IConfig config)
        {
            _config = config;
        }

        public string GetClaim(string claimType)
        {
            if (_principal == null)
            {
                return null;
            }

            var i = _principal.Identities.FirstOrDefault();

            if (i == null)
            {
                return null;
            }

            var claim = i.Claims.FirstOrDefault(_ => _.Type == claimType);

            return claim == null ? null : claim.Value;
        }

        public Dictionary<string, string> GetClaims()
        {
            if (_principal == null)
            {
                return null;
            }

            var d = _principal.Claims.ToDictionary(item => item.Type, item => item.Value);

            return d;
        }

        public bool LoadToken(string token)
        {
            var publicKey = _getKey();
            var tokenHandler = new JwtSecurityTokenHandler();

            var publicOnly = new RSACryptoServiceProvider();
            publicOnly.FromXmlString(publicKey);

            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningToken = new RsaSecurityToken(publicOnly),
                ValidIssuer = _getValueIssuer(),
                ValidateIssuer = true
            };

            validationParameters.AudienceValidator =
                delegate(IEnumerable<string> audiences, SecurityToken securityToken,
                    TokenValidationParameters parameters)
                {
                    var audience = _getValidAudience();
                    if (parameters.ValidAudience != null)
                    {
                        return parameters.ValidAudience == audience;
                    }
                    return true;
                };

            _failReason = null;

            try
            {
                SecurityToken validated = null;
                _principal = tokenHandler.ValidateToken(token, validationParameters, out validated);
            }
            catch (SecurityTokenValidationException ex)
            {
                _failReason = string.Format("SecurityTokenValidationException: {0}", ex.Message);
            }
            catch (ArgumentException ex)
            {
                _failReason = string.Format("ArgumentException: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                _failReason = string.Format("Exception: {0}", ex.Message);
            }


            return _failReason == null;
        }

        string _getValueIssuer()
        {
            return _config["TokenValidIssuer"];
        }

        string _getValidAudience()
        {
            return _config["TokenAllowedAudience"];
        }

        string _getKey()
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(_config["RSAPublic"]));
        }

    }
}
