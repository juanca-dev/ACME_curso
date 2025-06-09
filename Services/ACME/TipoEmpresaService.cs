using Models.ACME;
using DataAccess.ACME;
using ACME.Utilidades;

namespace Services.ACME
{
    public class TipoEmpresaService
    {
        public bool Crear(TipoEmpresaEntidad tipoEmpresaEntidad)
        {
            TipoEmpresaDA tipoEmpresaDA = new TipoEmpresaDA();

            try
            {
                tipoEmpresaDA.Insertar(tipoEmpresaEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(TipoEmpresaEntidad tipoEmpresaEntidad)
        {
            TipoEmpresaDA tipoEmpresaDA = new TipoEmpresaDA();

            try
            {
                tipoEmpresaDA.Modificar(tipoEmpresaEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public TipoEmpresaEntidad? BuscarxID(int IDTipoEmpresa)
        {
            TipoEmpresaDA? tipoEmpresaDA = new TipoEmpresaDA();

            try
            {
                TipoEmpresaEntidad tipoEmpresaEntidad;

                tipoEmpresaEntidad = tipoEmpresaDA.BuscarID(IDTipoEmpresa);



                return tipoEmpresaEntidad;

            }
            catch
            {
                return null;
            }
        }

        public List<TipoEmpresaEntidad>? Listar()
        {
            TipoEmpresaDA? tipoEmpresaDA = new();

            try
            {
                return tipoEmpresaDA.Listar();
            }
            catch
            {
                return null;
            }
        }

        public bool Eliminar(TipoEmpresaEntidad tipoEmpresaEntidad)
        {
            TipoEmpresaDA tipoEmpresaDA = new TipoEmpresaDA();

            try
            {
                tipoEmpresaDA.Anular(tipoEmpresaEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<TipoEmpresaEntidad>? Filtrar(string filtro)
        {
            TipoEmpresaDA? tipoEmpresaDA = new();

            try
            {
                return tipoEmpresaDA.Filtrar(filtro);
            }
            catch
            {
                return null;
            }
        }
        public List<TipoEmpresaEntidad>? ObtenerPagina(int pagina, int tamPagina)
        {
            TipoEmpresaDA? tipoEmpresaDA = new();

            try
            {
                return tipoEmpresaDA.Paginar(pagina, tamPagina);
            }
            catch
            {
                return null;
            }
        }

        public int IDNavegacion(TipoNavegacion tipoNavegacion, int IDTipoEmpresaActual)
        {
            int IDTipoEmpresa = 0;
            TipoEmpresaDA tipoEmpresaDA = new();

            switch (tipoNavegacion)
            {
                case TipoNavegacion.Inicio:
                    IDTipoEmpresa = tipoEmpresaDA.IDInicio();
                    break;
                case TipoNavegacion.Fin:
                    IDTipoEmpresa = tipoEmpresaDA.IDFinal();
                    break;
                case TipoNavegacion.Anterior:
                    IDTipoEmpresa = tipoEmpresaDA.IDAnterior(IDTipoEmpresaActual);
                    break;
                case TipoNavegacion.Siguiente:
                    IDTipoEmpresa = tipoEmpresaDA.IDSiguiente(IDTipoEmpresaActual);
                    break;

            }

            return IDTipoEmpresa;
        }

        public int Contar()
        {
            TipoEmpresaDA tipoEmpresaDA = new();

            try
            {
                return tipoEmpresaDA.Contar();
            }
            catch
            {
                return 0;
            }
        }
    }
}
