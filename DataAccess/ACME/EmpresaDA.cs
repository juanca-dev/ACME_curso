using System.Data;
using Microsoft.Data.SqlClient;
using Models.ACME;

namespace DataAccess.ACME
{
    public class EmpresaDA
    {
        private Conexion _conexion = new Conexion();

        public void Insertar(EmpresaEntidad empresaEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Empresa_Insertar";
                sqlComm.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int)).Direction = ParameterDirection.Output;
                sqlComm.Parameters.Add(new SqlParameter("@IDTipoEmpresa", empresaEntidad.IDTipoEmpresa));
                sqlComm.Parameters.Add(new SqlParameter("@Empresa", empresaEntidad.Empresa));
                sqlComm.Parameters.Add(new SqlParameter("@Direccion", empresaEntidad.Direccion));
                sqlComm.Parameters.Add(new SqlParameter("@RUC", empresaEntidad.RUC));
                sqlComm.Parameters.Add(new SqlParameter("@FechaCreacion", empresaEntidad.FechaCreacion));
                sqlComm.Parameters.Add(new SqlParameter("@Presupuesto", empresaEntidad.Presupuesto));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", empresaEntidad.Activo));

                sqlComm.ExecuteNonQuery();
                empresaEntidad.IDEmpresa = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDEmpresa")].Value;

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.Insertar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Modificar(EmpresaEntidad empresaEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Empresa_Modificar";
                sqlComm.Parameters.Add(new SqlParameter("@IDEmpresa", empresaEntidad.IDEmpresa));
                sqlComm.Parameters.Add(new SqlParameter("@IDTipoEmpresa", empresaEntidad.IDTipoEmpresa));
                sqlComm.Parameters.Add(new SqlParameter("@Empresa", empresaEntidad.Empresa));
                sqlComm.Parameters.Add(new SqlParameter("@Direccion", empresaEntidad.Direccion));
                sqlComm.Parameters.Add(new SqlParameter("@RUC", empresaEntidad.RUC));
                sqlComm.Parameters.Add(new SqlParameter("@FechaCreacion", empresaEntidad.FechaCreacion));
                sqlComm.Parameters.Add(new SqlParameter("@Presupuesto", empresaEntidad.Presupuesto));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", empresaEntidad.Activo));

                if (sqlComm.ExecuteNonQuery() != 1)
                {
                    throw new Exception("EmpresaDA.Modificar: Problema al actualizar.");
                }

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.Modificar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Anular(EmpresaEntidad empresaEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Empresa_Anular";
                sqlComm.Parameters.Add(new SqlParameter("@IDEmpresa", empresaEntidad.IDEmpresa));

                sqlComm.ExecuteNonQuery();

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.Anular: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public List<EmpresaEntidad> Listar()
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<EmpresaEntidad>? listaEmpresaEntidad = new();
            EmpresaEntidad? empresaEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Empresa_Listar";

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    empresaEntidad = new();
                    empresaEntidad.IDEmpresa = (int)sqlDataRead["IDEmpresa"];
                    empresaEntidad.IDTipoEmpresa = (int)sqlDataRead["IDTipoEmpresa"];
                    empresaEntidad.Empresa = (string)sqlDataRead["Empresa"];
                    empresaEntidad.Direccion = (string)sqlDataRead["Direccion"];
                    empresaEntidad.RUC = (string)sqlDataRead["RUC"];
                    empresaEntidad.FechaCreacion = (DateTime)sqlDataRead["FechaCreacion"];
                    empresaEntidad.Presupuesto = (decimal)sqlDataRead["Presupuesto"];
                    empresaEntidad.Activo = (bool)sqlDataRead["Activo"];
                    //Propiedad extendida
                    empresaEntidad.TipoEmpresa = (string)sqlDataRead["TipoEmpresa"];


                    listaEmpresaEntidad.Add(empresaEntidad);
                }

                sqlConn.Close();

                return listaEmpresaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.Listar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public EmpresaEntidad BuscarID(int IDEmpresa)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            EmpresaEntidad? empresaEntidad = null;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Empresa_BuscarPK";

                sqlComm.Parameters.Add(new SqlParameter("@IDEmpresa", IDEmpresa));
                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    empresaEntidad = new();
                    empresaEntidad.IDEmpresa = (int)sqlDataRead["IDEmpresa"];
                    empresaEntidad.IDTipoEmpresa = (int)sqlDataRead["IDTipoEmpresa"];
                    empresaEntidad.Empresa = (string)sqlDataRead["Empresa"];
                    empresaEntidad.Direccion = (string)sqlDataRead["Direccion"];
                    empresaEntidad.RUC = (string)sqlDataRead["RUC"];
                    empresaEntidad.FechaCreacion = (DateTime)sqlDataRead["FechaCreacion"];
                    empresaEntidad.Presupuesto = (decimal)sqlDataRead["Presupuesto"];
                    empresaEntidad.Activo = (bool)sqlDataRead["Activo"];
                    //Propiedad extendida
                    empresaEntidad.TipoEmpresa = (string)sqlDataRead["TipoEmpresa"];

                }

                sqlConn.Close();

                if (empresaEntidad == null)
                {
                    throw new Exception("El ID de [dbo].Empresa no existe.");
                }

                return empresaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.BuscarID: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public List<EmpresaEntidad> BuscarFKIDTipoEmpresa(int IDTipoEmpresa)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<EmpresaEntidad>? listaEmpresaEntidad = new();
            EmpresaEntidad? empresaEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Empresa_BuscarFKIDTipoEmpresa";

                sqlComm.Parameters.Add(new SqlParameter("@IDTipoEmpresa", IDTipoEmpresa));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    empresaEntidad = new();
                    empresaEntidad.IDEmpresa = (int)sqlDataRead["IDEmpresa"];
                    empresaEntidad.IDTipoEmpresa = (int)sqlDataRead["IDTipoEmpresa"];
                    empresaEntidad.Empresa = (string)sqlDataRead["Empresa"];
                    empresaEntidad.Direccion = (string)sqlDataRead["Direccion"];
                    empresaEntidad.RUC = (string)sqlDataRead["RUC"];
                    empresaEntidad.FechaCreacion = (DateTime)sqlDataRead["FechaCreacion"];
                    empresaEntidad.Presupuesto = (decimal)sqlDataRead["Presupuesto"];
                    empresaEntidad.Activo = (bool)sqlDataRead["Activo"];
                    // Propiedad extendida
                    empresaEntidad.TipoEmpresa = (string)sqlDataRead["TipoEmpresa"];


                    listaEmpresaEntidad.Add(empresaEntidad);
                }

                sqlConn.Close();

                return listaEmpresaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.BuscarFKIDTipoEmpresa: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDInicio()
        {
            int IDEmpresa = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDEmpresa = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].EmpresaPrimera");

                return IDEmpresa;
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.IDInicio: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDFinal()
        {
            int IDEmpresa = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDEmpresa = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].EmpresaFinal");

                return IDEmpresa;
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.IDFinal: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDSiguiente(int IDEmpresaActual)
        {
            int IDEmpresa = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDEmpresa = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].EmpresaSiguiente", IDEmpresaActual);

                return IDEmpresa;
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.IDSiguiente: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDAnterior(int IDEmpresaActual)
        {
            int IDEmpresa = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDEmpresa = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].EmpresaAnterior", IDEmpresaActual);

                return IDEmpresa;
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.IDAnterior: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int Contar()
        {
            int Cantidad = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                Cantidad = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].EmpresaContar");

                return Cantidad;
            }
            catch (Exception ex)
            {
                throw new Exception("[dbo].Empresa.Contar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public List<EmpresaEntidad> Paginar(int pagina, int tamPagina)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<EmpresaEntidad>? listaEmpresaEntidad = new();
            EmpresaEntidad? empresaEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Empresa_Paginar";
                sqlComm.Parameters.Add(new SqlParameter("@Pagina", pagina));
                sqlComm.Parameters.Add(new SqlParameter("@TamPagina", tamPagina));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    empresaEntidad = new();
                    empresaEntidad.IDEmpresa = (int)sqlDataRead["IDEmpresa"];
                    empresaEntidad.IDTipoEmpresa = (int)sqlDataRead["IDTipoEmpresa"];
                    empresaEntidad.Empresa = (string)sqlDataRead["Empresa"];
                    empresaEntidad.Direccion = (string)sqlDataRead["Direccion"];
                    empresaEntidad.RUC = (string)sqlDataRead["RUC"];
                    empresaEntidad.FechaCreacion = (DateTime)sqlDataRead["FechaCreacion"];
                    empresaEntidad.Presupuesto = (decimal)sqlDataRead["Presupuesto"];
                    empresaEntidad.Activo = (bool)sqlDataRead["Activo"];

                    empresaEntidad.TipoEmpresa = (string)sqlDataRead["TipoEmpresa"];


                    listaEmpresaEntidad.Add(empresaEntidad);
                }

                sqlConn.Close();

                return listaEmpresaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.Paginar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }
    }
}
