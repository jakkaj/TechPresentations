using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoreAuthenticationServer.Model.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace CoreAuthenticationServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOurTokenSevice _tokenService;

        public HomeController(IOurTokenSevice tokenService)
        {
            _tokenService = tokenService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.User;

            var c = user.Claims;

            var tokenSet = _tokenService.GetToken(c);

            //this is not the one we want to return, it is the token that proves we're logged in to B2C. 
            var idToken = await HttpContext.Authentication.GetTokenAsync("id_token");
            Debug.WriteLine($"B2C Token: {idToken}");
            return Ok(tokenSet);
        }

    }
}
