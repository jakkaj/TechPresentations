using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Contract.Authorization;
using Auth.Contract.Service;
using Auth.Entity.DTO.Auth;

namespace Auth.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IJwtCreator _tokenCreator;
        private readonly IJwtParser _jwtParser;
        private readonly IClaimsGetter _claimsGetter;

        public TokenService(IJwtCreator tokenCreator, IJwtParser jwtParser, IClaimsGetter claimsGetter)
        {
            _tokenCreator = tokenCreator;
            _jwtParser = jwtParser;
            _claimsGetter = claimsGetter;
        }

        public Dictionary<string, string> ParseToken(string token)
        {
            var parsed = _jwtParser.LoadToken(token);

            if (!parsed)
            {
                return null;
            }

            return _jwtParser.GetClaims();
        }

        public TokenGroup GetToken()
        {
            var tokenid = Guid.NewGuid();

            var token = _tokenCreator.CreateAuthToken(
                tokenid,
                _claimsGetter.GetClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"),
                _claimsGetter.GetClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"),
                _claimsGetter.GetClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"),
                _claimsGetter.GetClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"),
                _claimsGetter.GetClaim("projecttripod/handle")
                );

            var refresh =
                _tokenCreator.CreateRefreshToken(
                    tokenid,
                    _claimsGetter.GetClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                    );

            return new TokenGroup { Refresh = refresh, Token = token };
        }

    }
}
