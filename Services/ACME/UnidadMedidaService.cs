using ACME.Utilidades;
using Models.ACME;
using DataAccess.ACME;

namespace Services.ACME
{
    public class UnidadMedidaService
    {
        public bool Crear(UnidadMedidaEntidad unidadMedidaEntidad)
        {
            UnidadMedidaDA unidadMedidaDA = new UnidadMedidaDA();

            try
            {
                unidadMedidaDA.Insertar(unidadMedidaEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(UnidadMedidaEntidad unidadMedidaEntidad)
        {
            UnidadMedidaDA unidadMedidaDA = new UnidadMedidaDA();

            try
            {
                unidadMedidaDA.Modificar(unidadMedidaEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public UnidadMedidaEntidad? BuscarxID(int IDUnidadMedida)
        {
            UnidadMedidaDA? unidadMedidaDA = new UnidadMedidaDA();

            try
            {
                UnidadMedidaEntidad unidadMedidaEntidad;

                unidadMedidaEntidad = unidadMedidaDA.BuscarID(IDUnidadMedida);

                

                return unidadMedidaEntidad;

            }
            catch
            {
                return null;
            }
        }

        public List<UnidadMedidaEntidad>? Listar()
        {
            UnidadMedidaDA? unidadMedidaDA = new();

            try
            {
                return unidadMedidaDA.Listar();
            }
            catch
            {
                return null;
            }
        }

        public List<UnidadMedidaEntidad>? Filtrar(string filtro)
        {
            UnidadMedidaDA? unidadMedidaDA = new();

            try
            {
                return unidadMedidaDA.Filtrar(filtro);
            }
            catch
            {
                return null;
            }
        }

        public bool Eliminar(UnidadMedidaEntidad unidadMedidaEntidad)
        {
            UnidadMedidaDA unidadMedidaDA = new UnidadMedidaDA();

            try
            {
                unidadMedidaDA.Anular(unidadMedidaEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<UnidadMedidaEntidad>? ObtenerPagina(int pagina, int tamPagina)
        {
            UnidadMedidaDA? unidadMedidaDA = new();

            try
            {
                return unidadMedidaDA.Paginar(pagina, tamPagina);
            }
            catch
            {
                return null;
            }
        }

        public int IDNavegacion(TipoNavegacion tipoNavegacion, int IDUnidadMedidaActual)
        {
            int IDUnidadMedida = 0;
            UnidadMedidaDA unidadMedidaDA = new();

            switch (tipoNavegacion)
            {
                case TipoNavegacion.Inicio:
                    IDUnidadMedida = unidadMedidaDA.IDInicio();
                    break;
                case TipoNavegacion.Fin:
                    IDUnidadMedida = unidadMedidaDA.IDFinal();
                    break;
                case TipoNavegacion.Anterior:
                    IDUnidadMedida = unidadMedidaDA.IDAnterior(IDUnidadMedidaActual);
                    break;
                case TipoNavegacion.Siguiente:
                    IDUnidadMedida = unidadMedidaDA.IDSiguiente(IDUnidadMedidaActual);
                    break;
                
            }

            return IDUnidadMedida;
        }

        public int Contar()
        {
            UnidadMedidaDA unidadMedidaDA = new();

            try
            {
                return unidadMedidaDA.Contar();
            }
            catch
            {
                return 0;
            }
        }

        

    }
}
