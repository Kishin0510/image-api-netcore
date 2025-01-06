using System.ComponentModel.DataAnnotations;

namespace image_api_netcore.src.DTOs
{
    public class LoginUserDTO
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string email { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]

        public string password { get; set; } = null!;
    }
}