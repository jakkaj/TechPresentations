using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;

namespace Teched2014_WebAPI_Auth_Demo.ACustomCode.Jwt
{
    public class JWTConfig : AuthenticationOptions
    {
        public JWTConfig(string authenticationType = "Token") : base(authenticationType)
        {
        }
    }
}