using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace CoreAuthenticationServer.Controllers
{
    public class HomeController : Controller
    {
        
        [Authorize]
        public async Task<IActionResult> Index()
        {


            var idToken = await HttpContext.Authentication.GetTokenAsync("id_token");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult SignedIn()
        {
            return View();
        }

    }
}
