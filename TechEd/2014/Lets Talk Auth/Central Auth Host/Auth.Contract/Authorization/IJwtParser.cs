using System.Collections.Generic;

namespace Auth.Contract.Authorization
{
    public interface IJwtParser
    {
        string GetClaim(string claimType);
        bool LoadToken(string token);
        Dictionary<string, string> GetClaims();
    }
}