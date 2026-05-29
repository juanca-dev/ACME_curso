using Dapper;
using Microsoft.Data.SqlClient;
using Models.ACME;

namespace DataAccess.ACME
{
    public class UsuarioDA
    {
        private readonly string _cadenaConexion;

        public UsuarioDA()
        {
            string? cadenaConexion = Environment.GetEnvironmentVariable("ACME_cc");
            if (string.IsNullOrWhiteSpace(cadenaConexion))
                throw new InvalidOperationException(
                    "La variable de entorno 'ACME_cc' no está configurada o está vacía.");
            _cadenaConexion = cadenaConexion;
        }

        // ── YA TENÍAS ESTOS DOS — sin cambios ────────────────────────────

        public async Task<UsuarioEntidad?> ObtenerPorUsuarioOCorreoAsync(string usuarioOCorreo)
        {
            const string sql = @"
                SELECT u.UsuarioId,
                       u.Nombres,
                       u.Apellidos,
                       u.NombreUsuario,
                       u.Correo,
                       u.PasswordHash,
                       u.PasswordSalt,
                       u.RolId,
                       r.Nombre  AS NombreRol,
                       u.Activo,
                       u.FechaRegistro,
                       u.UltimoAcceso
                FROM   dbo.Usuario u
                INNER  JOIN dbo.Rol r ON r.RolId = u.RolId
                WHERE  u.Activo = 1
                  AND  (u.NombreUsuario = @Valor OR u.Correo = @Valor);";

            using var cn = new SqlConnection(_cadenaConexion);
            return await cn.QueryFirstOrDefaultAsync<UsuarioEntidad>(
                sql, new { Valor = usuarioOCorreo });
        }

        public async Task RegistrarAccesoAsync(int usuarioId)
        {
            const string sql = @"
                UPDATE dbo.Usuario
                SET    UltimoAcceso = GETDATE()
                WHERE  UsuarioId = @UsuarioId;";

            using var cn = new SqlConnection(_cadenaConexion);
            await cn.ExecuteAsync(sql, new { UsuarioId = usuarioId });
        }

        // ── MÉTODOS NUEVOS — pegar a continuación ────────────────────────

        public async Task<IEnumerable<UsuarioEntidad>> ObtenerTodosAsync()
        {
            const string sql = @"
                SELECT u.UsuarioId, u.Nombres, u.Apellidos, u.NombreUsuario,
                       u.Correo, u.RolId, r.Nombre AS NombreRol,
                       u.Activo, u.FechaRegistro, u.UltimoAcceso
                FROM   dbo.Usuario u
                INNER  JOIN dbo.Rol r ON r.RolId = u.RolId
                ORDER  BY u.Nombres;";

            using var cn = new SqlConnection(_cadenaConexion);
            return await cn.QueryAsync<UsuarioEntidad>(sql);
        }

        public async Task<UsuarioEntidad?> ObtenerPorIdAsync(int usuarioId)
        {
            const string sql = @"
                SELECT u.UsuarioId, u.Nombres, u.Apellidos, u.NombreUsuario,
                       u.Correo, u.PasswordHash, u.PasswordSalt,
                       u.RolId, r.Nombre AS NombreRol,
                       u.Activo, u.FechaRegistro, u.UltimoAcceso
                FROM   dbo.Usuario u
                INNER  JOIN dbo.Rol r ON r.RolId = u.RolId
                WHERE  u.UsuarioId = @UsuarioId;";

            using var cn = new SqlConnection(_cadenaConexion);
            return await cn.QueryFirstOrDefaultAsync<UsuarioEntidad>(
                sql, new { UsuarioId = usuarioId });
        }

        public async Task<int> InsertarAsync(UsuarioEntidad u)
        {
            const string sql = @"
                INSERT INTO dbo.Usuario
                    (Nombres, Apellidos, NombreUsuario, Correo,
                     PasswordHash, PasswordSalt, RolId, Activo)
                VALUES
                    (@Nombres, @Apellidos, @NombreUsuario, @Correo,
                     @PasswordHash, @PasswordSalt, @RolId, @Activo);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using var cn = new SqlConnection(_cadenaConexion);
            return await cn.ExecuteScalarAsync<int>(sql, u);
        }

        public async Task<bool> ActualizarAsync(UsuarioEntidad u)
        {
            const string sql = @"
                UPDATE dbo.Usuario
                SET    Nombres       = @Nombres,
                       Apellidos     = @Apellidos,
                       NombreUsuario = @NombreUsuario,
                       Correo        = @Correo,
                       RolId         = @RolId,
                       Activo        = @Activo
                WHERE  UsuarioId = @UsuarioId;";

            using var cn = new SqlConnection(_cadenaConexion);
            return await cn.ExecuteAsync(sql, u) > 0;
        }

        public async Task<bool> CambiarPasswordAsync(
            int usuarioId, string nuevoHash, string nuevoSalt)
        {
            const string sql = @"
                UPDATE dbo.Usuario
                SET    PasswordHash = @Hash,
                       PasswordSalt = @Salt
                WHERE  UsuarioId   = @UsuarioId;";

            using var cn = new SqlConnection(_cadenaConexion);
            return await cn.ExecuteAsync(sql,
                new
                {
                    Hash = nuevoHash,
                    Salt = nuevoSalt,
                    UsuarioId = usuarioId
                }) > 0;
        }

        public async Task<bool> EliminarAsync(int usuarioId)
        {
            // Baja lógica — no elimina el registro físicamente
            const string sql = @"
                UPDATE dbo.Usuario
                SET    Activo = 0
                WHERE  UsuarioId = @UsuarioId;";

            using var cn = new SqlConnection(_cadenaConexion);
            return await cn.ExecuteAsync(sql,
                new { UsuarioId = usuarioId }) > 0;
        }

        public async Task<IEnumerable<RolEntidad>> ObtenerRolesAsync()
        {
            const string sql = @"
                SELECT RolId, Nombre, Descripcion
                FROM   dbo.Rol
                WHERE  Activo = 1
                ORDER  BY Nombre;";

            using var cn = new SqlConnection(_cadenaConexion);
            var resultado = await cn.QueryAsync<RolEntidad>(sql);
            return resultado?.AsList() ?? new List<RolEntidad>();
        }
    }
}