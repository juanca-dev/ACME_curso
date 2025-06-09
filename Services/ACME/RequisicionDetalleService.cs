using ACME.Utilidades;
using Models.ACME;
using DataAccess.ACME;

namespace Services.ACME
{
    public class RequisicionDetalleService
    {
        public bool Crear(RequisicionDetalleEntidad requisicionDetalleEntidad)
        {
            RequisicionDetalleDA requisicionDetalleDA = new RequisicionDetalleDA();

            try
            {
                requisicionDetalleDA.Insertar(requisicionDetalleEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(RequisicionDetalleEntidad requisicionDetalleEntidad)
        {
            RequisicionDetalleDA requisicionDetalleDA = new RequisicionDetalleDA();

            try
            {
                requisicionDetalleDA.Modificar(requisicionDetalleEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public RequisicionDetalleEntidad? BuscarxID(int IDRequisicionDetalle)
        {
            RequisicionDetalleDA? requisicionDetalleDA = new RequisicionDetalleDA();

            try
            {
                RequisicionDetalleEntidad requisicionDetalleEntidad;

                requisicionDetalleEntidad = requisicionDetalleDA.BuscarID(IDRequisicionDetalle);



                return requisicionDetalleEntidad;

            }
            catch
            {
                return null;
            }
        }

        public List<RequisicionDetalleEntidad>? Listar()
        {
            RequisicionDetalleDA? requisicionDetalleDA = new();

            try
            {
                return requisicionDetalleDA.Listar();
            }
            catch
            {
                return null;
            }
        }



        public bool Eliminar(RequisicionDetalleEntidad requisicionDetalleEntidad)
        {
            RequisicionDetalleDA requisicionDetalleDA = new RequisicionDetalleDA();

            try
            {
                requisicionDetalleDA.Anular(requisicionDetalleEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<RequisicionDetalleEntidad>? ObtenerPagina(int pagina, int tamPagina)
        {
            RequisicionDetalleDA? requisicionDetalleDA = new();

            try
            {
                return requisicionDetalleDA.Paginar(pagina, tamPagina);
            }
            catch
            {
                return null;
            }
        }

        public int IDNavegacion(TipoNavegacion tipoNavegacion, int IDRequisicionDetalleActual)
        {
            int IDRequisicionDetalle = 0;
            RequisicionDetalleDA requisicionDetalleDA = new();

            switch (tipoNavegacion)
            {
                case TipoNavegacion.Inicio:
                    IDRequisicionDetalle = requisicionDetalleDA.IDInicio();
                    break;
                case TipoNavegacion.Fin:
                    IDRequisicionDetalle = requisicionDetalleDA.IDFinal();
                    break;
                case TipoNavegacion.Anterior:
                    IDRequisicionDetalle = requisicionDetalleDA.IDAnterior(IDRequisicionDetalleActual);
                    break;
                case TipoNavegacion.Siguiente:
                    IDRequisicionDetalle = requisicionDetalleDA.IDSiguiente(IDRequisicionDetalleActual);
                    break;

            }

            return IDRequisicionDetalle;
        }

        public int Contar()
        {
            RequisicionDetalleDA requisicionDetalleDA = new();

            try
            {
                return requisicionDetalleDA.Contar();
            }
            catch
            {
                return 0;
            }
        }


        public List<RequisicionDetalleEntidad>? BuscarxFKIDArticulo(int IDArticulo)
        {
            RequisicionDetalleDA? requisicionDetalleDA = new();

            try
            {
                return requisicionDetalleDA.BuscarFKIDArticulo(IDArticulo);
            }
            catch
            {
                return null;
            }
        }
        public List<RequisicionDetalleEntidad>? BuscarxFKIDRequisicion(int IDRequisicion)
        {
            RequisicionDetalleDA? requisicionDetalleDA = new();

            try
            {
                return requisicionDetalleDA.BuscarFKIDRequisicion(IDRequisicion);
            }
            catch
            {
                return null;
            }
        }

    }
}
