using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenIdConnectClientExample.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace OpenIdConnectClientExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize]        
        public async Task<IActionResult> Index()
        {
            // Token içeriğine erişim
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");

            // Token içeriğini işleme
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;

            // JSON içindeki claim'lere erişim
           // var nameClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var emailClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            // Claim'leri kullanma
            // Örneğin, bu claim'leri bir view'e aktarabilir veya iş mantığınızda kullanabilirsiniz
            //ViewBag["Name"] = nameClaim;
            ViewBag.Email = emailClaim.ToString();

            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("OnlineBankamatikCookie");
            await HttpContext.SignOutAsync("oidc");
            
        }
    }
}
