using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Auth.Contract.Authorization;
using XamlingCore.Portable.Contract.Config;

namespace Web.Extras.JWT
{
    public class JwtCreator : IJwtCreator
    {
        private readonly IConfig _config;

        public JwtCreator(IConfig config)
        {
            _config = config;
        }

        public string CreateAuthToken(Guid tokenId, string uid, string givenName = null, string surname = null, string emailAddress = null, string handle = null)
        {
            handle = "somehanlefromotherserver";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, uid),
                new Claim("projecttripod/tokenid", tokenId.ToString()),
                new Claim("projecttripod/authToken", "true")
            };

            if (givenName != null)
            {
                claims.Add(new Claim(ClaimTypes.GivenName, givenName));
            }
            if (surname != null)
            {
                claims.Add(new Claim(ClaimTypes.Surname, givenName));
            }
            if (emailAddress != null)
            {
                claims.Add(new Claim(ClaimTypes.Email, givenName));
            }
            if (handle != null)
            {
                claims.Add(new Claim("projecttripod/handle", handle));
            }

            var subject = new ClaimsIdentity(claims.ToArray());

            var now = DateTime.UtcNow;

            return _createToken(subject, new Lifetime(now.AddMinutes(-15), now.AddDays(1)));

        }

        public string CreateRefreshToken(Guid tokenId, string uid)
        {
            var subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, uid),
                new Claim("projecttripod/tokenid", tokenId.ToString()),
                new Claim("projecttripod/refreshToken", "true")
            });

            var now = DateTime.UtcNow;

            return _createToken(subject, new Lifetime(now.AddDays(1).AddMinutes(-60), now.AddDays(14)));

        }

        string _createToken(ClaimsIdentity subject, Lifetime lifeTime)
        {
            var privateSigner = new RSACryptoServiceProvider();
            privateSigner.FromXmlString(_getKey());

            var signingCredentials = new SigningCredentials(new RsaSecurityKey(privateSigner),
                SecurityAlgorithms.RsaSha256Signature, SecurityAlgorithms.Sha256Digest);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                TokenIssuerName = _getValueIssuer(),
                AppliesToAddress = _getValidAudience(),
                Lifetime = lifeTime,
                SigningCredentials = signingCredentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
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
            return Encoding.UTF8.GetString(Convert.FromBase64String(_config["RSAPrivate"]));
        }
    }
}
