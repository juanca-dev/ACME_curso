using System.Data;
using Microsoft.Data.SqlClient;
using Models.ACME;

namespace DataAccess.ACME
{
    public class RequisicionAnotacionDA
    {
        private Conexion _conexion = new Conexion();

        public void Insertar(RequisicionAnotacionEntidad requisicionAnotacionEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionAnotacion_Insertar";
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicionAnotacion", SqlDbType.Int)).Direction = ParameterDirection.Output;
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", requisicionAnotacionEntidad.IDRequisicion));
                sqlComm.Parameters.Add(new SqlParameter("@Anotacion", requisicionAnotacionEntidad.Anotacion));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", requisicionAnotacionEntidad.Activo));

                sqlComm.ExecuteNonQuery();
                requisicionAnotacionEntidad.IDRequisicionAnotacion = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDRequisicionAnotacion")].Value;

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.Insertar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Insertar(RequisicionAnotacionEntidad requisicionAnotacionEntidad, SqlConnection sqlConn, SqlTransaction sqlTrans)
        {
            // Obtener una instancia de la conexión
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionAnotacion_Insertar";
                sqlComm.Transaction = sqlTrans;
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicionAnotacion", SqlDbType.Int)).Direction = ParameterDirection.Output;
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", requisicionAnotacionEntidad.IDRequisicion));
                sqlComm.Parameters.Add(new SqlParameter("@Anotacion", requisicionAnotacionEntidad.Anotacion));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", requisicionAnotacionEntidad.Activo));

                sqlComm.ExecuteNonQuery();
                requisicionAnotacionEntidad.IDRequisicionAnotacion = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDRequisicionAnotacion")].Value;

            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.Insertar(Sob1): " + ex.Message);
            }
        }

        public void Modificar(RequisicionAnotacionEntidad requisicionAnotacionEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionAnotacion_Modificar";
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicionAnotacion", requisicionAnotacionEntidad.IDRequisicionAnotacion));
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", requisicionAnotacionEntidad.IDRequisicion));
                sqlComm.Parameters.Add(new SqlParameter("@Anotacion", requisicionAnotacionEntidad.Anotacion));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", requisicionAnotacionEntidad.Activo));

                if (sqlComm.ExecuteNonQuery() != 1)
                {
                    throw new Exception("RequisicionAnotacionDA.Modificar: Problema al actualizar.");
                }

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.Modificar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Modificar(RequisicionAnotacionEntidad requisicionAnotacionEntidad, SqlConnection sqlConn, SqlTransaction sqlTrans)
        {
            // Obtener una instancia de la conexión
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionAnotacion_Modificar";
                sqlComm.Transaction = sqlTrans;
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicionAnotacion", requisicionAnotacionEntidad.IDRequisicionAnotacion));
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", requisicionAnotacionEntidad.IDRequisicion));
                sqlComm.Parameters.Add(new SqlParameter("@Anotacion", requisicionAnotacionEntidad.Anotacion));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", requisicionAnotacionEntidad.Activo));

                sqlComm.ExecuteNonQuery();
                requisicionAnotacionEntidad.IDRequisicionAnotacion = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDRequisicionAnotacion")].Value;

            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.Modificar(Sob1): " + ex.Message);
            }
        }

        public void Anular(RequisicionAnotacionEntidad requisicionAnotacionEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionAnotacion_Eliminar";
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicionAnotacion", requisicionAnotacionEntidad.IDRequisicionAnotacion));

                sqlComm.ExecuteNonQuery();

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.Anular: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public List<RequisicionAnotacionEntidad> Listar()
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<RequisicionAnotacionEntidad>? listaRequisicionAnotacionEntidad = new();
            RequisicionAnotacionEntidad? requisicionAnotacionEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionAnotacion_Listar";

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionAnotacionEntidad = new();
                    requisicionAnotacionEntidad.IDRequisicionAnotacion = (int)sqlDataRead["IDRequisicionAnotacion"];
                    requisicionAnotacionEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionAnotacionEntidad.Anotacion = (string)sqlDataRead["Anotacion"];
                    requisicionAnotacionEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionAnotacionEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];


                    listaRequisicionAnotacionEntidad.Add(requisicionAnotacionEntidad);
                }

                sqlConn.Close();

                return listaRequisicionAnotacionEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.Listar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public List<RequisicionAnotacionEntidad> Filtrar(string filtro)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<RequisicionAnotacionEntidad>? listaRequisicionAnotacionEntidad = new();
            RequisicionAnotacionEntidad? requisicionAnotacionEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionAnotacion_Filtrar";
                sqlComm.Parameters.Add(new SqlParameter("@Filtro", filtro));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionAnotacionEntidad = new();
                    requisicionAnotacionEntidad.IDRequisicionAnotacion = (int)sqlDataRead["IDRequisicionAnotacion"];
                    requisicionAnotacionEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionAnotacionEntidad.Anotacion = (string)sqlDataRead["Anotacion"];
                    requisicionAnotacionEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionAnotacionEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];


                    listaRequisicionAnotacionEntidad.Add(requisicionAnotacionEntidad);
                }

                sqlConn.Close();

                return listaRequisicionAnotacionEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.Filtrar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public List<RequisicionAnotacionEntidad> Paginar(int pagina, int tamPagina)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<RequisicionAnotacionEntidad>? listaRequisicionAnotacionEntidad = new();
            RequisicionAnotacionEntidad? requisicionAnotacionEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionAnotacion_Paginar";
                sqlComm.Parameters.Add(new SqlParameter("@Pagina", pagina));
                sqlComm.Parameters.Add(new SqlParameter("@TamPagina", tamPagina));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionAnotacionEntidad = new();
                    requisicionAnotacionEntidad.IDRequisicionAnotacion = (int)sqlDataRead["IDRequisicionAnotacion"];
                    requisicionAnotacionEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionAnotacionEntidad.Anotacion = (string)sqlDataRead["Anotacion"];
                    requisicionAnotacionEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionAnotacionEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];


                    listaRequisicionAnotacionEntidad.Add(requisicionAnotacionEntidad);
                }

                sqlConn.Close();

                return listaRequisicionAnotacionEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.Paginar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public RequisicionAnotacionEntidad BuscarID(int IDRequisicionAnotacion)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            RequisicionAnotacionEntidad? requisicionAnotacionEntidad = null;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionAnotacion_BuscarPK";

                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicionAnotacion", IDRequisicionAnotacion));
                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionAnotacionEntidad = new();
                    requisicionAnotacionEntidad.IDRequisicionAnotacion = (int)sqlDataRead["IDRequisicionAnotacion"];
                    requisicionAnotacionEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionAnotacionEntidad.Anotacion = (string)sqlDataRead["Anotacion"];
                    requisicionAnotacionEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionAnotacionEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];

                }

                sqlConn.Close();

                if (requisicionAnotacionEntidad == null)
                {
                    throw new Exception("El ID de [dbo].RequisicionAnotacion no existe.");
                }

                return requisicionAnotacionEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.BuscarID: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public int IDInicio()
        {
            int IDRequisicionAnotacion = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDRequisicionAnotacion = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionAnotacionPrimera");

                return IDRequisicionAnotacion;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.IDInicio: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDFinal()
        {
            int IDRequisicionAnotacion = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDRequisicionAnotacion = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionAnotacionFinal");

                return IDRequisicionAnotacion;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.IDFinal: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDSiguiente(int IDRequisicionAnotacionActual)
        {
            int IDRequisicionAnotacion = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDRequisicionAnotacion = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionAnotacionSiguiente", IDRequisicionAnotacionActual);

                return IDRequisicionAnotacion;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.IDSiguiente: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDAnterior(int IDRequisicionAnotacionActual)
        {
            int IDRequisicionAnotacion = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDRequisicionAnotacion = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionAnotacionAnterior", IDRequisicionAnotacionActual);

                return IDRequisicionAnotacion;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.IDAnterior: " + ex.Message);
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
                Cantidad = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionAnotacionContar");

                return Cantidad;
            }
            catch (Exception ex)
            {
                throw new Exception("[dbo].RequisicionAnotacion.Contar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public List<RequisicionAnotacionEntidad> BuscarFKIDRequisicion(int IDRequisicion)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<RequisicionAnotacionEntidad>? listaRequisicionAnotacionEntidad = new();
            RequisicionAnotacionEntidad? requisicionAnotacionEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionAnotacion_BuscarFKIDRequisicion";

                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", IDRequisicion));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionAnotacionEntidad = new();
                    requisicionAnotacionEntidad.IDRequisicionAnotacion = (int)sqlDataRead["IDRequisicionAnotacion"];
                    requisicionAnotacionEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionAnotacionEntidad.Anotacion = (string)sqlDataRead["Anotacion"];
                    requisicionAnotacionEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionAnotacionEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];


                    listaRequisicionAnotacionEntidad.Add(requisicionAnotacionEntidad);
                }

                sqlConn.Close();

                return listaRequisicionAnotacionEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionAnotacionDA.BuscarFKIDRequisicion: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }
    }
}
