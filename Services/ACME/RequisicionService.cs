using ACME.Utilidades;
using Models.ACME;
using DataAccess.ACME;

namespace Services.ACME
{
    public class RequisicionService
    {
        public bool Crear(RequisicionEntidad requisicionEntidad)
        {
            RequisicionDA requisicionDA = new RequisicionDA();

            try
            {
                requisicionDA.Insertar(requisicionEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(RequisicionEntidad requisicionEntidad)
        {
            RequisicionDA requisicionDA = new RequisicionDA();

            try
            {
                requisicionDA.Modificar(requisicionEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public RequisicionEntidad? BuscarxID(int IDRequisicion)
        {
            RequisicionDA? requisicionDA = new RequisicionDA();

            try
            {
                RequisicionEntidad requisicionEntidad;

                requisicionEntidad = requisicionDA.BuscarID(IDRequisicion);

                #region Obtener los detalles referenciados
                // Obtener detalle - RequisicionDetalle
                RequisicionDetalleDA _RequisicionDetalle = new RequisicionDetalleDA();
                requisicionEntidad.ListaRequisicionDetalle = _RequisicionDetalle.BuscarFKIDRequisicion(IDRequisicion);
                // Obtener detalle - RequisicionAnotacion
                RequisicionAnotacionDA _RequisicionAnotacion = new RequisicionAnotacionDA();
                requisicionEntidad.ListaRequisicionAnotacion = _RequisicionAnotacion.BuscarFKIDRequisicion(IDRequisicion);
                #endregion

                return requisicionEntidad;

            }
            catch
            {
                return null;
            }
        }

        public List<RequisicionEntidad>? Listar()
        {
            RequisicionDA? requisicionDA = new();

            try
            {
                return requisicionDA.Listar();
            }
            catch
            {
                return null;
            }
        }

        public List<RequisicionEntidad>? Filtrar(string filtro)
        {
            RequisicionDA? requisicionDA = new();

            try
            {
                return requisicionDA.Filtrar(filtro);
            }
            catch
            {
                return null;
            }
        }

        public bool Eliminar(RequisicionEntidad requisicionEntidad)
        {
            RequisicionDA requisicionDA = new RequisicionDA();

            try
            {
                requisicionDA.Anular(requisicionEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<RequisicionEntidad>? ObtenerPagina(int pagina, int tamPagina)
        {
            RequisicionDA? requisicionDA = new();

            try
            {
                return requisicionDA.Paginar(pagina, tamPagina);
            }
            catch
            {
                return null;
            }
        }

        public int IDNavegacion(TipoNavegacion tipoNavegacion, int IDRequisicionActual)
        {
            int IDRequisicion = 0;
            RequisicionDA requisicionDA = new();

            switch (tipoNavegacion)
            {
                case TipoNavegacion.Inicio:
                    IDRequisicion = requisicionDA.IDInicio();
                    break;
                case TipoNavegacion.Fin:
                    IDRequisicion = requisicionDA.IDFinal();
                    break;
                case TipoNavegacion.Anterior:
                    IDRequisicion = requisicionDA.IDAnterior(IDRequisicionActual);
                    break;
                case TipoNavegacion.Siguiente:
                    IDRequisicion = requisicionDA.IDSiguiente(IDRequisicionActual);
                    break;

            }

            return IDRequisicion;
        }

        public int Contar()
        {
            RequisicionDA requisicionDA = new();

            try
            {
                return requisicionDA.Contar();
            }
            catch
            {
                return 0;
            }
        }


        public List<RequisicionEntidad>? BuscarxFKIDEmpresa(int IDEmpresa)
        {
            RequisicionDA? requisicionDA = new();

            try
            {
                return requisicionDA.BuscarFKIDEmpresa(IDEmpresa);
            }
            catch
            {
                return null;
            }
        }

    }
}
