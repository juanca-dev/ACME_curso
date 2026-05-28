using System;
using Models.ACME;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ACME
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioEntidad>>   ObtenerTodosAsync();
        Task<UsuarioEntidad?>               ObtenerPorIdAsync(int id);
        Task<(bool ok, string mensaje)>     CrearAsync(UsuarioViewModel vm);
        Task<(bool ok, string mensaje)>     ActualizarAsync(UsuarioViewModel vm);
        Task<(bool ok, string mensaje)>     EliminarAsync(int id);
        Task<IEnumerable<RolEntidad>>       ObtenerRolesAsync();
    }
}
