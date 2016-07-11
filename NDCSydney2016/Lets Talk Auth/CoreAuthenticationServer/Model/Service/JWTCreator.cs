using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CoreAuthenticationServer.Model.Contract;
using CoreAuthenticationServer.Model.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoreAuthenticationServer.Model.Service
{
    public class JwtCreator : IJwtCreator
    {
        private readonly IOptions<SigningSettings> _config;

        public JwtCreator(IOptions<SigningSettings> config)
        {
            _config = config;
        }

        public string CreateAuthToken(Guid tokenId, Dictionary<string, string> stringClaims, bool isRefresh = false)
        {
            var tokenType = isRefresh ? "refreshToken" : "authToken";

            var claims = new List<Claim>
            {
                new Claim("central/tokenid", tokenId.ToString()),
                new Claim($"central/{tokenType}", "true")
            };

            claims.AddRange(stringClaims.Select(c => new Claim(c.Key, c.Value)));

            var subject = new ClaimsIdentity(claims.ToArray());

            var now = DateTime.UtcNow;

            var lifeTime = isRefresh
                ? new Lifetime(now.AddDays(1).AddMinutes(-60), now.AddDays(14))
                : new Lifetime(now.AddMinutes(-15), now.AddDays(1));


            return _createToken(subject, lifeTime);

        }

        string _createToken(ClaimsIdentity subject, Lifetime lifeTime)
        {
            using (var privateSigner = new RSACryptoServiceProvider())
            {
                var key = _getKey();
                privateSigner.FromXmlString(key);

                var signingCredentials = new SigningCredentials(new RsaSecurityKey(privateSigner),
                    SecurityAlgorithms.RsaSha256Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Issuer = _getValueIssuer(),
                    Audience = _getValidAudience(),
                    IssuedAt = lifeTime.Created,
                    Expires = lifeTime.Expires,
                    NotBefore = lifeTime.Created,
                    SigningCredentials = signingCredentials,
                };



                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                var tokenString = tokenHandler.WriteToken(token);
                return tokenString;
            }
               
        }

        string _getValueIssuer()
        {
            return _config.Value.TokenValidIssuer;
        }

        string _getValidAudience()
        {
            return _config.Value.TokenAllowedAudience;
        }

        string _getKey()
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(_config.Value.RSAPrivate));
        }
    }
}
