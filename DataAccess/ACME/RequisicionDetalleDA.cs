using System.Data;
using Microsoft.Data.SqlClient;
using Models.ACME;

namespace DataAccess.ACME
{
    public class RequisicionDetalleDA
    {
        private Conexion _conexion = new Conexion();

        public void Insertar(RequisicionDetalleEntidad requisicionDetalleEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionDetalle_Insertar";
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicionDetalle", SqlDbType.Int)).Direction = ParameterDirection.Output;
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", requisicionDetalleEntidad.IDRequisicion));
                sqlComm.Parameters.Add(new SqlParameter("@IDArticulo", requisicionDetalleEntidad.IDArticulo));
                sqlComm.Parameters.Add(new SqlParameter("@Linea", requisicionDetalleEntidad.Linea));
                sqlComm.Parameters.Add(new SqlParameter("@Cantidad", requisicionDetalleEntidad.Cantidad));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", requisicionDetalleEntidad.Activo));

                sqlComm.ExecuteNonQuery();
                requisicionDetalleEntidad.IDRequisicionDetalle = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDRequisicionDetalle")].Value;

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.Insertar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Insertar(RequisicionDetalleEntidad requisicionDetalleEntidad, SqlConnection sqlConn, SqlTransaction sqlTrans)
        {
            // Obtener una instancia de la conexión
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionDetalle_Insertar";
                sqlComm.Transaction = sqlTrans;
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicionDetalle", SqlDbType.Int)).Direction = ParameterDirection.Output;
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", requisicionDetalleEntidad.IDRequisicion));
                sqlComm.Parameters.Add(new SqlParameter("@IDArticulo", requisicionDetalleEntidad.IDArticulo));
                sqlComm.Parameters.Add(new SqlParameter("@Linea", requisicionDetalleEntidad.Linea));
                sqlComm.Parameters.Add(new SqlParameter("@Cantidad", requisicionDetalleEntidad.Cantidad));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", requisicionDetalleEntidad.Activo));

                sqlComm.ExecuteNonQuery();
                requisicionDetalleEntidad.IDRequisicionDetalle = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDRequisicionDetalle")].Value;

            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.Insertar(Sob1): " + ex.Message);
            }
        }

        public void Modificar(RequisicionDetalleEntidad requisicionDetalleEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionDetalle_Modificar";
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicionDetalle", requisicionDetalleEntidad.IDRequisicionDetalle));
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", requisicionDetalleEntidad.IDRequisicion));
                sqlComm.Parameters.Add(new SqlParameter("@IDArticulo", requisicionDetalleEntidad.IDArticulo));
                sqlComm.Parameters.Add(new SqlParameter("@Linea", requisicionDetalleEntidad.Linea));
                sqlComm.Parameters.Add(new SqlParameter("@Cantidad", requisicionDetalleEntidad.Cantidad));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", requisicionDetalleEntidad.Activo));

                if (sqlComm.ExecuteNonQuery() != 1)
                {
                    throw new Exception("RequisicionDetalleDA.Modificar: Problema al actualizar.");
                }

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.Modificar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Modificar(RequisicionDetalleEntidad requisicionDetalleEntidad, SqlConnection sqlConn, SqlTransaction sqlTrans)
        {
            // Obtener una instancia de la conexión
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionDetalle_Modificar";
                sqlComm.Transaction = sqlTrans;
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicionDetalle", requisicionDetalleEntidad.IDRequisicionDetalle));
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", requisicionDetalleEntidad.IDRequisicion));
                sqlComm.Parameters.Add(new SqlParameter("@IDArticulo", requisicionDetalleEntidad.IDArticulo));
                sqlComm.Parameters.Add(new SqlParameter("@Linea", requisicionDetalleEntidad.Linea));
                sqlComm.Parameters.Add(new SqlParameter("@Cantidad", requisicionDetalleEntidad.Cantidad));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", requisicionDetalleEntidad.Activo));

                sqlComm.ExecuteNonQuery();
                requisicionDetalleEntidad.IDRequisicionDetalle = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDRequisicionDetalle")].Value;

            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.Modificar(Sob1): " + ex.Message);
            }
        }

        public void Anular(RequisicionDetalleEntidad requisicionDetalleEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionDetalle_Eliminar";
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicionDetalle", requisicionDetalleEntidad.IDRequisicionDetalle));

                sqlComm.ExecuteNonQuery();

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.Anular: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public List<RequisicionDetalleEntidad> Listar()
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<RequisicionDetalleEntidad>? listaRequisicionDetalleEntidad = new();
            RequisicionDetalleEntidad? requisicionDetalleEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionDetalle_Listar";

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionDetalleEntidad = new();
                    requisicionDetalleEntidad.IDRequisicionDetalle = (int)sqlDataRead["IDRequisicionDetalle"];
                    requisicionDetalleEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionDetalleEntidad.IDArticulo = (int)sqlDataRead["IDArticulo"];
                    requisicionDetalleEntidad.Linea = (short)sqlDataRead["Linea"];
                    requisicionDetalleEntidad.Cantidad = (decimal)sqlDataRead["Cantidad"];
                    requisicionDetalleEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionDetalleEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];
                    requisicionDetalleEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];


                    listaRequisicionDetalleEntidad.Add(requisicionDetalleEntidad);
                }

                sqlConn.Close();

                return listaRequisicionDetalleEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.Listar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public List<RequisicionDetalleEntidad> Paginar(int pagina, int tamPagina)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<RequisicionDetalleEntidad>? listaRequisicionDetalleEntidad = new();
            RequisicionDetalleEntidad? requisicionDetalleEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionDetalle_Paginar";
                sqlComm.Parameters.Add(new SqlParameter("@Pagina", pagina));
                sqlComm.Parameters.Add(new SqlParameter("@TamPagina", tamPagina));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionDetalleEntidad = new();
                    requisicionDetalleEntidad.IDRequisicionDetalle = (int)sqlDataRead["IDRequisicionDetalle"];
                    requisicionDetalleEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionDetalleEntidad.IDArticulo = (int)sqlDataRead["IDArticulo"];
                    requisicionDetalleEntidad.Linea = (short)sqlDataRead["Linea"];
                    requisicionDetalleEntidad.Cantidad = (decimal)sqlDataRead["Cantidad"];
                    requisicionDetalleEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionDetalleEntidad.Articulo = (string)sqlDataRead["Articulo"];
                    requisicionDetalleEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];


                    listaRequisicionDetalleEntidad.Add(requisicionDetalleEntidad);
                }

                sqlConn.Close();

                return listaRequisicionDetalleEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.Paginar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public RequisicionDetalleEntidad BuscarID(int IDRequisicionDetalle)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            RequisicionDetalleEntidad? requisicionDetalleEntidad = null;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionDetalle_BuscarPK";

                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicionDetalle", IDRequisicionDetalle));
                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionDetalleEntidad = new();
                    requisicionDetalleEntidad.IDRequisicionDetalle = (int)sqlDataRead["IDRequisicionDetalle"];
                    requisicionDetalleEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionDetalleEntidad.IDArticulo = (int)sqlDataRead["IDArticulo"];
                    requisicionDetalleEntidad.Linea = (short)sqlDataRead["Linea"];
                    requisicionDetalleEntidad.Cantidad = (decimal)sqlDataRead["Cantidad"];
                    requisicionDetalleEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionDetalleEntidad.Articulo = (string)sqlDataRead["Articulo"];
                    requisicionDetalleEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];

                }

                sqlConn.Close();

                if (requisicionDetalleEntidad == null)
                {
                    throw new Exception("El ID de [dbo].RequisicionDetalle no existe.");
                }

                return requisicionDetalleEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.BuscarID: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public int IDInicio()
        {
            int IDRequisicionDetalle = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDRequisicionDetalle = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionDetallePrimera");

                return IDRequisicionDetalle;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.IDInicio: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDFinal()
        {
            int IDRequisicionDetalle = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDRequisicionDetalle = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionDetalleFinal");

                return IDRequisicionDetalle;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.IDFinal: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDSiguiente(int IDRequisicionDetalleActual)
        {
            int IDRequisicionDetalle = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDRequisicionDetalle = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionDetalleSiguiente", IDRequisicionDetalleActual);

                return IDRequisicionDetalle;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.IDSiguiente: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDAnterior(int IDRequisicionDetalleActual)
        {
            int IDRequisicionDetalle = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDRequisicionDetalle = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionDetalleAnterior", IDRequisicionDetalleActual);

                return IDRequisicionDetalle;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.IDAnterior: " + ex.Message);
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
                Cantidad = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionDetalleContar");

                return Cantidad;
            }
            catch (Exception ex)
            {
                throw new Exception("[dbo].RequisicionDetalle.Contar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public List<RequisicionDetalleEntidad> BuscarFKIDArticulo(int IDArticulo)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<RequisicionDetalleEntidad>? listaRequisicionDetalleEntidad = new();
            RequisicionDetalleEntidad? requisicionDetalleEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionDetalle_BuscarFKIDArticulo";

                sqlComm.Parameters.Add(new SqlParameter("@IDArticulo", IDArticulo));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionDetalleEntidad = new();
                    requisicionDetalleEntidad.IDRequisicionDetalle = (int)sqlDataRead["IDRequisicionDetalle"];
                    requisicionDetalleEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionDetalleEntidad.IDArticulo = (int)sqlDataRead["IDArticulo"];
                    requisicionDetalleEntidad.Linea = (short)sqlDataRead["Linea"];
                    requisicionDetalleEntidad.Cantidad = (decimal)sqlDataRead["Cantidad"];
                    requisicionDetalleEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionDetalleEntidad.Articulo = (string)sqlDataRead["Articulo"];
                    requisicionDetalleEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];


                    listaRequisicionDetalleEntidad.Add(requisicionDetalleEntidad);
                }

                sqlConn.Close();

                return listaRequisicionDetalleEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.BuscarFKIDArticulo: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public List<RequisicionDetalleEntidad> BuscarFKIDRequisicion(int IDRequisicion)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<RequisicionDetalleEntidad>? listaRequisicionDetalleEntidad = new();
            RequisicionDetalleEntidad? requisicionDetalleEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].RequisicionDetalle_BuscarFKIDRequisicion";

                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", IDRequisicion));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionDetalleEntidad = new();
                    requisicionDetalleEntidad.IDRequisicionDetalle = (int)sqlDataRead["IDRequisicionDetalle"];
                    requisicionDetalleEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionDetalleEntidad.IDArticulo = (int)sqlDataRead["IDArticulo"];
                    requisicionDetalleEntidad.Linea = (short)sqlDataRead["Linea"];
                    requisicionDetalleEntidad.Cantidad = (decimal)sqlDataRead["Cantidad"];
                    requisicionDetalleEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionDetalleEntidad.Articulo = (string)sqlDataRead["Articulo"];
                    requisicionDetalleEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];


                    listaRequisicionDetalleEntidad.Add(requisicionDetalleEntidad);
                }

                sqlConn.Close();

                return listaRequisicionDetalleEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDetalleDA.BuscarFKIDRequisicion: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }
    }
}
