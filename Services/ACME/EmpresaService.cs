using Models.ACME;
using DataAccess.ACME;
using ACME.Utilidades;

namespace Services.ACME
{
    public class EmpresaService
    {
        public bool Crear(EmpresaEntidad empresaEntidad)
        {
            EmpresaDA empresaDA = new EmpresaDA();

            try
            {
                empresaDA.Insertar(empresaEntidad);

                return true;
            }
            catch
            {
                Console.WriteLine("Error al crear empresa: ");
                return false;
            }
        }

        public bool Actualizar(EmpresaEntidad empresaEntidad)
        {
            EmpresaDA empresaDA = new EmpresaDA();

            try
            {
                empresaDA.Modificar(empresaEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(EmpresaEntidad empresaEntidad)
        {
            EmpresaDA empresaDA = new EmpresaDA();

            try
            {
                empresaDA.Anular(empresaEntidad);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<EmpresaEntidad>? Listar()
        {
            EmpresaDA? empresaDA = new();

            try
            {
                return empresaDA.Listar();
            }
            catch
            {
                return null;
            }
        }

        public EmpresaEntidad? BuscarxID(int IDEmpresa)
        {
            EmpresaDA? empresaDA = new EmpresaDA();

            try
            {
                EmpresaEntidad empresaEntidad;

                empresaEntidad = empresaDA.BuscarID(IDEmpresa);



                return empresaEntidad;

            }
            catch
            {
                return null;
            }
        }

        public List<EmpresaEntidad>? BuscarxFKIDTipoEmpresa(int IDTipoEmpresa)
        {
            EmpresaDA? empresaDA = new();

            try
            {
                return empresaDA.BuscarFKIDTipoEmpresa(IDTipoEmpresa);
            }
            catch
            {
                return null;
            }
        }

        public int IDNavegacion(TipoNavegacion tipoNavegacion, int IDEmpresaActual)
        {
            int IDEmpresa = 0;
            EmpresaDA empresaDA = new();

            switch (tipoNavegacion)
            {
                case TipoNavegacion.Inicio:
                    IDEmpresa = empresaDA.IDInicio();
                    break;
                case TipoNavegacion.Fin:
                    IDEmpresa = empresaDA.IDFinal();
                    break;
                case TipoNavegacion.Anterior:
                    IDEmpresa = empresaDA.IDAnterior(IDEmpresaActual);
                    break;
                case TipoNavegacion.Siguiente:
                    IDEmpresa = empresaDA.IDSiguiente(IDEmpresaActual);
                    break;

            }

            return IDEmpresa;
        }

        public int Contar()
        {
            EmpresaDA empresaDA = new();

            try
            {
                return empresaDA.Contar();
            }
            catch
            {
                return 0;
            }
        }

        public List<EmpresaEntidad>? ObtenerPagina(int pagina, int tamPagina)
        {
            EmpresaDA? empresaDA = new();

            try
            {
                return empresaDA.Paginar(pagina, tamPagina);
            }
            catch
            {
                return null;
            }
        }
    }
}
