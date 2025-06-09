using ACME.Utilidades;
using Models.ACME;
using DataAccess.ACME;

namespace Services.ACME
{
    public class RequisicionAnotacionService
    {
        public bool Crear(RequisicionAnotacionEntidad requisicionAnotacionEntidad)
        {
            RequisicionAnotacionDA requisicionAnotacionDA = new RequisicionAnotacionDA();

            try
            {
                requisicionAnotacionDA.Insertar(requisicionAnotacionEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(RequisicionAnotacionEntidad requisicionAnotacionEntidad)
        {
            RequisicionAnotacionDA requisicionAnotacionDA = new RequisicionAnotacionDA();

            try
            {
                requisicionAnotacionDA.Modificar(requisicionAnotacionEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public RequisicionAnotacionEntidad? BuscarxID(int IDRequisicionAnotacion)
        {
            RequisicionAnotacionDA? requisicionAnotacionDA = new RequisicionAnotacionDA();

            try
            {
                RequisicionAnotacionEntidad requisicionAnotacionEntidad;

                requisicionAnotacionEntidad = requisicionAnotacionDA.BuscarID(IDRequisicionAnotacion);



                return requisicionAnotacionEntidad;

            }
            catch
            {
                return null;
            }
        }

        public List<RequisicionAnotacionEntidad>? Listar()
        {
            RequisicionAnotacionDA? requisicionAnotacionDA = new();

            try
            {
                return requisicionAnotacionDA.Listar();
            }
            catch
            {
                return null;
            }
        }

        public List<RequisicionAnotacionEntidad>? Filtrar(string filtro)
        {
            RequisicionAnotacionDA? requisicionAnotacionDA = new();

            try
            {
                return requisicionAnotacionDA.Filtrar(filtro);
            }
            catch
            {
                return null;
            }
        }

        public bool Eliminar(RequisicionAnotacionEntidad requisicionAnotacionEntidad)
        {
            RequisicionAnotacionDA requisicionAnotacionDA = new RequisicionAnotacionDA();

            try
            {
                requisicionAnotacionDA.Anular(requisicionAnotacionEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<RequisicionAnotacionEntidad>? ObtenerPagina(int pagina, int tamPagina)
        {
            RequisicionAnotacionDA? requisicionAnotacionDA = new();

            try
            {
                return requisicionAnotacionDA.Paginar(pagina, tamPagina);
            }
            catch
            {
                return null;
            }
        }

        public int IDNavegacion(TipoNavegacion tipoNavegacion, int IDRequisicionAnotacionActual)
        {
            int IDRequisicionAnotacion = 0;
            RequisicionAnotacionDA requisicionAnotacionDA = new();

            switch (tipoNavegacion)
            {
                case TipoNavegacion.Inicio:
                    IDRequisicionAnotacion = requisicionAnotacionDA.IDInicio();
                    break;
                case TipoNavegacion.Fin:
                    IDRequisicionAnotacion = requisicionAnotacionDA.IDFinal();
                    break;
                case TipoNavegacion.Anterior:
                    IDRequisicionAnotacion = requisicionAnotacionDA.IDAnterior(IDRequisicionAnotacionActual);
                    break;
                case TipoNavegacion.Siguiente:
                    IDRequisicionAnotacion = requisicionAnotacionDA.IDSiguiente(IDRequisicionAnotacionActual);
                    break;

            }

            return IDRequisicionAnotacion;
        }

        public int Contar()
        {
            RequisicionAnotacionDA requisicionAnotacionDA = new();

            try
            {
                return requisicionAnotacionDA.Contar();
            }
            catch
            {
                return 0;
            }
        }


        public List<RequisicionAnotacionEntidad>? BuscarxFKIDRequisicion(int IDRequisicion)
        {
            RequisicionAnotacionDA? requisicionAnotacionDA = new();

            try
            {
                return requisicionAnotacionDA.BuscarFKIDRequisicion(IDRequisicion);
            }
            catch
            {
                return null;
            }
        }

    }
}
