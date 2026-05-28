using Models.ACME;

namespace Services.ACME
{
    public interface IAccountService
    {
        Task<UsuarioEntidad?> ValidarCredencialesAsync(string usuarioOCorreo, string password);
        (string hash, string salt) GenerarHash(string password);
        bool VerificarHash(string password, string hashGuardado, string saltGuardado);
    }
}