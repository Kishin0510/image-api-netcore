using System.ComponentModel.DataAnnotations;

namespace image_api_netcore.src.DTOs
{
    public class CreatePost
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        public string title { get; set; } = null!;

        [Required(ErrorMessage = "La imagen es obligatoria.")]
        public IFormFile image { get; set; } = null!;

        [Required(ErrorMessage = "El correo del usuario es obligatorio.")]
        public string userEmail { get; set; } = null!;
    }
}