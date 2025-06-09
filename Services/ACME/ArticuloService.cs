using ACME.Utilidades;
using Models.ACME;
using DataAccess.ACME;

namespace Services.ACME
{
    public class ArticuloService
    {
        public bool Crear(ArticuloEntidad articuloEntidad)
        {
            ArticuloDA articuloDA = new ArticuloDA();

            try
            {
                articuloDA.Insertar(articuloEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(ArticuloEntidad articuloEntidad)
        {
            ArticuloDA articuloDA = new ArticuloDA();

            try
            {
                articuloDA.Modificar(articuloEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public ArticuloEntidad? BuscarxID(int IDArticulo)
        {
            ArticuloDA? articuloDA = new ArticuloDA();

            try
            {
                ArticuloEntidad articuloEntidad;

                articuloEntidad = articuloDA.BuscarID(IDArticulo);



                return articuloEntidad;

            }
            catch
            {
                return null;
            }
        }

        public List<ArticuloEntidad>? Listar()
        {
            ArticuloDA? articuloDA = new();

            try
            {
                return articuloDA.Listar();
            }
            catch
            {
                return null;
            }
        }

        public List<ArticuloEntidad>? Filtrar(string filtro)
        {
            ArticuloDA? articuloDA = new();

            try
            {
                return articuloDA.Filtrar(filtro);
            }
            catch
            {
                return null;
            }
        }

        public bool Eliminar(ArticuloEntidad articuloEntidad)
        {
            ArticuloDA articuloDA = new ArticuloDA();

            try
            {
                articuloDA.Anular(articuloEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<ArticuloEntidad>? ObtenerPagina(int pagina, int tamPagina)
        {
            ArticuloDA? articuloDA = new();

            try
            {
                return articuloDA.Paginar(pagina, tamPagina);
            }
            catch
            {
                return null;
            }
        }

        public int IDNavegacion(TipoNavegacion tipoNavegacion, int IDArticuloActual)
        {
            int IDArticulo = 0;
            ArticuloDA articuloDA = new();

            switch (tipoNavegacion)
            {
                case TipoNavegacion.Inicio:
                    IDArticulo = articuloDA.IDInicio();
                    break;
                case TipoNavegacion.Fin:
                    IDArticulo = articuloDA.IDFinal();
                    break;
                case TipoNavegacion.Anterior:
                    IDArticulo = articuloDA.IDAnterior(IDArticuloActual);
                    break;
                case TipoNavegacion.Siguiente:
                    IDArticulo = articuloDA.IDSiguiente(IDArticuloActual);
                    break;

            }

            return IDArticulo;
        }

        public int Contar()
        {
            ArticuloDA articuloDA = new();

            try
            {
                return articuloDA.Contar();
            }
            catch
            {
                return 0;
            }
        }


        public List<ArticuloEntidad>? BuscarxFKIDUnidadMedida(int IDUnidadMedida)
        {
            ArticuloDA? articuloDA = new();

            try
            {
                return articuloDA.BuscarFKIDUnidadMedida(IDUnidadMedida);
            }
            catch
            {
                return null;
            }
        }

    }
}
