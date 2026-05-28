using System.ComponentModel.DataAnnotations;

namespace Models.ACME
{
    public class UsuarioViewModel
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [Display(Name = "Nombre de usuario")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; } = string.Empty;

        [Display(Name = "Contraseña")]
        [MinLength(6, ErrorMessage = "Mínimo 6 caracteres.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Selecciona un rol.")]
        [Display(Name = "Rol")]
        public int RolId { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;

        // Para el <select> de roles
        public IEnumerable<RolEntidad> Roles { get; set; }
            = new List<RolEntidad>();
    }
}