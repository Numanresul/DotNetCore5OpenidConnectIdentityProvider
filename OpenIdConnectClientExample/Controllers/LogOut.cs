using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenIdConnectClientExample.Controllers
{
    public class LogOut : Controller
    {
        private readonly IConfiguration _configuration;

        public LogOut(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public async Task<IActionResult> SignOut()
        {

            await HttpContext.SignOutAsync("OnlineBankamatikCookie"); // Client1 den çıkış yapıyoruz
            await HttpContext.SignOutAsync("oidc"); // OpenId Connect'ten yani Identity Server'dan çıkış yapıyoruz. Bunu yorum haline getirip kapatırsak Identity Server otomatik anasayfaya yönlendirme yapamayacağından anasayfaya dönmek için return view() yazmamız gerekecek.
            return RedirectToAction("Index", "Home");                                      //burada geriye view dnmüyoruz çünkü zaten Identity Serverdan çıkış yapıp tekrar geri gelecek
       
        }
    }

}
