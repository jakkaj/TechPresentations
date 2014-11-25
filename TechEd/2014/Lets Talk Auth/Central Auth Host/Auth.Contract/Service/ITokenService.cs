using System.Collections.Generic;
using Auth.Entity.DTO.Auth;

namespace Auth.Contract.Service
{
    public interface ITokenService
    {
        Dictionary<string, string> ParseToken(string token);
        TokenGroup GetToken();
    }
}