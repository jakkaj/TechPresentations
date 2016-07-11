using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAuthenticationServer.Model.Contract
{
    public interface IJwtCreator
    {
        string CreateAuthToken(Guid tokenId, Dictionary<string, string> stringClaims, bool isRefresh = false);
      
    }
}
