using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreAuthenticationServer.Model.Contract;
using CoreAuthenticationServer.Model.Entity;
using Microsoft.CodeAnalysis.CSharp;

namespace CoreAuthenticationServer.Model.Service
{
    public class OurTokenSevice : IOurTokenSevice
    {
        private readonly IJwtCreator _creator;

        public OurTokenSevice(IJwtCreator creator)
        {
            _creator = creator;
        }


        public TokenSet GetToken(IEnumerable<Claim> claims)
        {
            var g = Guid.NewGuid();

            var stringClaims= new Dictionary<string, string>();

            foreach (var claim in claims.Where(claim => !stringClaims.ContainsKey(claim.Type)))
            {
                stringClaims.Add(claim.Type, claim.Value);
            }

            var set = new TokenSet
            {
                Token = _creator.CreateAuthToken(g, stringClaims, false),
                Refresh = _creator.CreateAuthToken(g, stringClaims, true),
            };

            return set;

        }

        string _getClaim(string claimType, IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(_ => _.Type == claimType)?.ToString();
        }
    }
}
