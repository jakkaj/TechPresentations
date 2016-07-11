using System.Collections.Generic;
using System.Security.Claims;
using CoreAuthenticationServer.Model.Entity;

namespace CoreAuthenticationServer.Model.Contract
{
    public interface IOurTokenSevice
    {
        TokenSet GetToken(IEnumerable<Claim> claims);
    }
}