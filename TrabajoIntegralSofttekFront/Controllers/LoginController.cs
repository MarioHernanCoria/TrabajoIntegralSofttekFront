using Microsoft.AspNetCore.Mvc;
using Data.DTOs;
using Data.Base;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using TrabajoIntegralSofttekFront.ViewModel;

namespace TrabajoIntegralSofttekFront.Controllers
{
    public class LoginController : Controller
    {

        private readonly IHttpClientFactory _httpClient;
        public LoginController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }


        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Ingresar(LoginDto login)
        {

            var baseApi = new BaseApi(_httpClient);
            var token = await baseApi.PostToApi("Login", login);
            var resultadoLogin = token as OkObjectResult;
            var resultadoObjeto = JsonConvert.DeserializeObject<Models.Login>(resultadoLogin.Value.ToString());

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            Claim claimRole = new(ClaimTypes.Role, "Administrador");

            identity.AddClaim(claimRole);

            var claimPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(1),
            });

            HttpContext.Session.SetString("Token", resultadoObjeto.Token);

            var homeViewModel = new HomeViewModel();
            homeViewModel.Token = resultadoObjeto.Token;


            return View("~/Views/Home/Index.cshtml", homeViewModel);
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}