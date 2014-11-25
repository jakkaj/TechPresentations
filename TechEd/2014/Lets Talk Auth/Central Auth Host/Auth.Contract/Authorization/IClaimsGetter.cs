using System.Collections.Generic;

namespace Auth.Contract.Authorization
{
    public interface IClaimsGetter
    {
        bool IsLoggedIn();
        string GetClaim(string claimType);
        Dictionary<string, string> GetClaims();
        string GetNameIdentifier();
    }
}