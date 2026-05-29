using DataAccess.ACME;
using Models.ACME;

namespace Services.ACME
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioDA _usuarioDA;
        private readonly IAccountService _accountService;

        public UsuarioService(UsuarioDA usuarioDA, IAccountService accountService)
        {
            _usuarioDA = usuarioDA;
            _accountService = accountService;
        }

        // ── Firma EXACTA igual que la interfaz ───────────────────────────

        public Task<IEnumerable<UsuarioEntidad>> ObtenerTodosAsync()
            => _usuarioDA.ObtenerTodosAsync();

        public Task<UsuarioEntidad?> ObtenerPorIdAsync(int id)
            => _usuarioDA.ObtenerPorIdAsync(id);

        public async Task<IEnumerable<RolEntidad>> ObtenerRolesAsync()
        {
            var roles = await _usuarioDA.ObtenerRolesAsync();
            return roles ?? new List<RolEntidad>();
        }

        public async Task<(bool ok, string mensaje)> CrearAsync(UsuarioViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Password))
                return (false, "La contraseña es obligatoria al crear un usuario.");

            var (hash, salt) = _accountService.GenerarHash(vm.Password);

            var entidad = new UsuarioEntidad
            {
                Nombres = vm.Nombres.Trim(),
                Apellidos = vm.Apellidos.Trim(),
                NombreUsuario = vm.NombreUsuario.Trim().ToLower(),
                Correo = vm.Correo.Trim().ToLower(),
                PasswordHash = hash,
                PasswordSalt = salt,
                RolId = vm.RolId,
                Activo = vm.Activo
            };

            int nuevoId = await _usuarioDA.InsertarAsync(entidad);
            return nuevoId > 0
                ? (true, "Usuario creado correctamente.")
                : (false, "No se pudo crear el usuario.");
        }

        public async Task<(bool ok, string mensaje)> ActualizarAsync(UsuarioViewModel vm)
        {
            var entidad = new UsuarioEntidad
            {
                UsuarioId = vm.UsuarioId,
                Nombres = vm.Nombres.Trim(),
                Apellidos = vm.Apellidos.Trim(),
                NombreUsuario = vm.NombreUsuario.Trim().ToLower(),
                Correo = vm.Correo.Trim().ToLower(),
                RolId = vm.RolId,
                Activo = vm.Activo
            };

            bool ok = await _usuarioDA.ActualizarAsync(entidad);

            if (ok && !string.IsNullOrWhiteSpace(vm.Password))
            {
                var (hash, salt) = _accountService.GenerarHash(vm.Password);
                await _usuarioDA.CambiarPasswordAsync(vm.UsuarioId, hash, salt);
            }

            return ok
                ? (true, "Usuario actualizado correctamente.")
                : (false, "No se pudo actualizar el usuario.");
        }

        public async Task<(bool ok, string mensaje)> EliminarAsync(int id)
        {
            bool ok = await _usuarioDA.EliminarAsync(id);
            return ok
                ? (true, "Usuario desactivado correctamente.")
                : (false, "No se pudo eliminar el usuario.");
        }
    }
}