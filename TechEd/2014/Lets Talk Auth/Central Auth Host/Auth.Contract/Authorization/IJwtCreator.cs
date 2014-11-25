using System;

namespace Auth.Contract.Authorization
{
    public interface IJwtCreator
    {
        string CreateAuthToken(Guid tokenId, string uid, string givenName = null, string surname = null, string emailAddress = null, string handle = null);
        string CreateRefreshToken(Guid tokenId, string uid);
    }
}