using DataAccess.ACME;
using Models.ACME;
using System.Security.Cryptography;
using System.Text;

namespace Services.ACME
{
    public class AccountService : IAccountService
    {
        private readonly UsuarioDA _usuarioDA;

        public AccountService(UsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }

        // ── Validación principal ──────────────────────────────────────────
        public async Task<UsuarioEntidad?> ValidarCredencialesAsync(
            string usuarioOCorreo, string password)
        {
            if (string.IsNullOrWhiteSpace(usuarioOCorreo) ||
                string.IsNullOrWhiteSpace(password))
                return null;

            var usuario = await _usuarioDA.ObtenerPorUsuarioOCorreoAsync(
                usuarioOCorreo.Trim().ToLower());

            if (usuario is null) return null;

            bool credencialValida = VerificarHash(
                password, usuario.PasswordHash, usuario.PasswordSalt);

            if (!credencialValida) return null;

            await _usuarioDA.RegistrarAccesoAsync(usuario.UsuarioId);
            return usuario;
        }

        // ── Generación de Hash SHA-256 con Salt ───────────────────────────
        public (string hash, string salt) GenerarHash(string password)
        {
            // Salt = GUID + timestamp, codificado en Base64
            string saltRaw = $"{Guid.NewGuid()}-{DateTime.UtcNow.Ticks}";
            string salt = Convert.ToBase64String(Encoding.UTF8.GetBytes(saltRaw));
            string hash = ComputarHash(password, salt);
            return (hash, salt);
        }

        // ── Verificación ─────────────────────────────────────────────────
        public bool VerificarHash(string password, string hashGuardado, string saltGuardado)
        {
            string hashCalculado = ComputarHash(password, saltGuardado);
            // Comparación de tiempo constante para evitar timing attacks
            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(hashCalculado),
                Encoding.UTF8.GetBytes(hashGuardado));
        }

        // ── Privado ───────────────────────────────────────────────────────
        private static string ComputarHash(string password, string salt)
        {
            byte[] bytes = SHA256.HashData(
                Encoding.UTF8.GetBytes(password + salt));
            return Convert.ToBase64String(bytes);
        }
    }
}