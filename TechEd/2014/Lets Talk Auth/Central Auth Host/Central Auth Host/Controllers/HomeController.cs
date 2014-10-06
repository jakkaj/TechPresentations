using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Auth.Contract.Service;
using XamlingCore.Portable.Contract.Serialise;

namespace Central_Auth_Host.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IEntitySerialiser _entitySerialiser;

        public HomeController(ITokenService tokenService, IEntitySerialiser entitySerialiser)
        {
            _tokenService = tokenService;
            _entitySerialiser = entitySerialiser;
        }

        [Authorize]
        public string Index()
        {
            var token = _tokenService.GetToken();

            var result = _tokenService.ParseToken(token.Token);

            var tokenstring = token.Token;
            return tokenstring;
        }
    }
}