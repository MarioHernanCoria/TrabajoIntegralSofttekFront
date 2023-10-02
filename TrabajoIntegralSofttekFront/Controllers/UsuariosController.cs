using Data.Base;
using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabajoIntegralSofttekFront.ViewModels;

namespace TrabajoIntegralSofttekFront.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {


        private readonly IHttpClientFactory _httpClient;
        public UsuariosController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Usuarios()
        {
            return View();
        }

        public async Task<IActionResult> UsuariosAddPartial([FromBody] UsuarioDto usuario)
        {
            var usuarioViewModel = new UsuarioViewModel();
            if (usuario != null)
            {
                usuarioViewModel = usuario;
            }


                    return PartialView("~/Views/Usuarios/Partial/UsuariosAddPartial.cshtml");
                }

                public IActionResult GuardarUsuario(UsuarioDto usuario)
                {
                    var token = HttpContext.Session.GetString("Token");
                    var baseApi = new BaseApi(_httpClient);
                    var usuarios = baseApi.PostToApi("Usuarios", usuario, token);
                    return View("~/Views/Usuarios/Usuarios.cshtml");
                }

                public async Task<IActionResult> EliminarUsuario(int id)
                {
                    try
                    {
                        var token = HttpContext.Session.GetString("Token");
                        var baseApi = new BaseApi(_httpClient);

                        await baseApi.DeleteFromApi("Usuarios", id.ToString(), token);
                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        return BadRequest($"Error al eliminar el usuario: {ex.Message}");
                    }
                }


            }
        }

