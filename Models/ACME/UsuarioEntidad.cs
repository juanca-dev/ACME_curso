using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ACME
{
    public class UsuarioEntidad
    {
        public int UsuarioId { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public int RolId { get; set; }
        public string? NombreRol { get; set; }   // JOIN con Rol
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? UltimoAcceso { get; set; }

        // Propiedad calculada útil en vistas
        public string NombreCompleto => $"{Nombres} {Apellidos}".Trim();
    }
}
