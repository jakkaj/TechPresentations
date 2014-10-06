using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Auth.Contract.Authorization;
using Microsoft.AspNet.Identity;

namespace Central_Auth_Host.Auth
{
    public class AspNetIdentityClaimsGetter : IClaimsGetter
    {
        public string GetNameIdentifier()
        {
            var c = _getClaims();
            var id = c.Identity.GetUserId();

            return id;
        }

        public string GetClaim(string claimType)
        {
            return _getClaim(claimType);
        }

        public Dictionary<string, string> GetClaims()
        {
            var c = _getClaims();

            if (c == null)
            {
                return null;
            }

            var i = c.Identities.FirstOrDefault();

            if (i == null)
            {
                return null;
            }

            return i.Claims.ToDictionary(item => item.Type, item => item.Value);
        }

        string _getClaim(string claimType)
        {
            var c = _getClaims();

            if (c == null)
            {
                return null;
            }

            var i = c.Identities.FirstOrDefault();

            if (i == null)
            {
                return null;
            }

            var claim = i.Claims.FirstOrDefault(_ => _.Type == claimType);

            if (claim == null)
            {
                return null;
            }

            return claim.Value;
        }

        private ClaimsPrincipal _getClaims()
        {
            return HttpContext.Current.GetOwinContext().Authentication.User;
        }

        public bool IsLoggedIn()
        {
            return _getClaims() != null;
        }
    }
}
