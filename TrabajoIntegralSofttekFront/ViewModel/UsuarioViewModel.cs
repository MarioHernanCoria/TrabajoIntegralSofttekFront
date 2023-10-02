using Data.DTOs;

namespace TrabajoIntegralSofttekFront.ViewModels
{
    public class UsuarioViewModel
    {

        public string Nombre { get; set; }
        public int Dni { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public int CodRol { get; set; }

        public static implicit operator UsuarioViewModel(UsuarioDto usuario)
        {
            var usuarioViewModel = new UsuarioViewModel();
            usuarioViewModel.Nombre = usuario.Nombre;
            usuarioViewModel.Dni = usuario.Dni;
            usuarioViewModel.Email = usuario.Email;
            usuarioViewModel.Clave = usuario.Clave;
            usuarioViewModel.CodRol = usuario.CodRol;
            return usuarioViewModel;
        }
    
    }
}