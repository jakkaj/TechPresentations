using Microsoft.Owin.Security;

namespace Teched2014_WebAPI_Auth_Demo.ACustomCode.Middleware
{
    public class JWTConfig : AuthenticationOptions
    {
        public JWTConfig(string authenticationType = "Bearer") : base(authenticationType)
        {
        }
    }
}