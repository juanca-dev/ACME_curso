using System.Data;
using Microsoft.Data.SqlClient;
using Models.ACME;

namespace DataAccess.ACME
{
    public class UnidadMedidaDA
    {
        private Conexion _conexion = new Conexion();

        public void Insertar(UnidadMedidaEntidad unidadMedidaEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].UnidadMedida_Insertar";
                sqlComm.Parameters.Add(new SqlParameter("@IDUnidadMedida", SqlDbType.Int)).Direction = ParameterDirection.Output;
                sqlComm.Parameters.Add(new SqlParameter("@UnidadMedida", unidadMedidaEntidad.UnidadMedida));
                sqlComm.Parameters.Add(new SqlParameter("@Sigla", unidadMedidaEntidad.Sigla));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", unidadMedidaEntidad.Activo));

                sqlComm.ExecuteNonQuery();
                unidadMedidaEntidad.IDUnidadMedida = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDUnidadMedida")].Value;

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("UnidadMedidaDA.Insertar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Insertar(UnidadMedidaEntidad unidadMedidaEntidad, SqlConnection sqlConn, SqlTransaction sqlTrans)
        {
            // Obtener una instancia de la conexión
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].UnidadMedida_Insertar";
                sqlComm.Transaction = sqlTrans;
                sqlComm.Parameters.Add(new SqlParameter("@IDUnidadMedida", SqlDbType.Int)).Direction = ParameterDirection.Output;
                sqlComm.Parameters.Add(new SqlParameter("@UnidadMedida", unidadMedidaEntidad.UnidadMedida));
                sqlComm.Parameters.Add(new SqlParameter("@Sigla", unidadMedidaEntidad.Sigla));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", unidadMedidaEntidad.Activo));

                sqlComm.ExecuteNonQuery();
                unidadMedidaEntidad.IDUnidadMedida = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDUnidadMedida")].Value;

            }
            catch (Exception ex)
            {
                throw new Exception("UnidadMedidaDA.Insertar(Sob1): " + ex.Message);
            }
        }

        public void Modificar(UnidadMedidaEntidad unidadMedidaEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].UnidadMedida_Modificar";
                sqlComm.Parameters.Add(new SqlParameter("@IDUnidadMedida", unidadMedidaEntidad.IDUnidadMedida));
                sqlComm.Parameters.Add(new SqlParameter("@UnidadMedida", unidadMedidaEntidad.UnidadMedida));
                sqlComm.Parameters.Add(new SqlParameter("@Sigla", unidadMedidaEntidad.Sigla));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", unidadMedidaEntidad.Activo));

                if (sqlComm.ExecuteNonQuery() != 1)
                {
                    throw new Exception("UnidadMedidaDA.Modificar: Problema al actualizar.");
                }

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("UnidadMedidaDA.Modificar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Anular(UnidadMedidaEntidad unidadMedidaEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].UnidadMedida_Eliminar";
                sqlComm.Parameters.Add(new SqlParameter("@IDUnidadMedida", unidadMedidaEntidad.IDUnidadMedida));

                sqlComm.ExecuteNonQuery();

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("UnidadMedidaDA.Anular: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public List<UnidadMedidaEntidad> Listar()
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<UnidadMedidaEntidad>? listaUnidadMedidaEntidad = new();
            UnidadMedidaEntidad? unidadMedidaEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].UnidadMedida_Listar";

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    unidadMedidaEntidad = new();
                    unidadMedidaEntidad.IDUnidadMedida = (int)sqlDataRead["IDUnidadMedida"];
                    unidadMedidaEntidad.UnidadMedida = (string)sqlDataRead["UnidadMedida"];
                    unidadMedidaEntidad.Sigla = (string)sqlDataRead["Sigla"];
                    unidadMedidaEntidad.Activo = (bool)sqlDataRead["Activo"];



                    listaUnidadMedidaEntidad.Add(unidadMedidaEntidad);
                }

                sqlConn.Close();

                return listaUnidadMedidaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("UnidadMedidaDA.Listar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public List<UnidadMedidaEntidad> Filtrar(string filtro)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<UnidadMedidaEntidad>? listaUnidadMedidaEntidad = new();
            UnidadMedidaEntidad? unidadMedidaEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].UnidadMedida_Filtrar";
                sqlComm.Parameters.Add(new SqlParameter("@Filtro", filtro));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    unidadMedidaEntidad = new();
                    unidadMedidaEntidad.IDUnidadMedida = (int)sqlDataRead["IDUnidadMedida"];
                    unidadMedidaEntidad.UnidadMedida = (string)sqlDataRead["UnidadMedida"];
                    unidadMedidaEntidad.Sigla = (string)sqlDataRead["Sigla"];
                    unidadMedidaEntidad.Activo = (bool)sqlDataRead["Activo"];



                    listaUnidadMedidaEntidad.Add(unidadMedidaEntidad);
                }

                sqlConn.Close();

                return listaUnidadMedidaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("UnidadMedidaDA.Filtrar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public List<UnidadMedidaEntidad> Paginar(int pagina, int tamPagina)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<UnidadMedidaEntidad>? listaUnidadMedidaEntidad = new();
            UnidadMedidaEntidad? unidadMedidaEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].UnidadMedida_Paginar";
                sqlComm.Parameters.Add(new SqlParameter("@Pagina", pagina));
                sqlComm.Parameters.Add(new SqlParameter("@TamPagina", tamPagina));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    unidadMedidaEntidad = new();
                    unidadMedidaEntidad.IDUnidadMedida = (int)sqlDataRead["IDUnidadMedida"];
                    unidadMedidaEntidad.UnidadMedida = (string)sqlDataRead["UnidadMedida"];
                    unidadMedidaEntidad.Sigla = (string)sqlDataRead["Sigla"];
                    unidadMedidaEntidad.Activo = (bool)sqlDataRead["Activo"];



                    listaUnidadMedidaEntidad.Add(unidadMedidaEntidad);
                }

                sqlConn.Close();

                return listaUnidadMedidaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("UnidadMedidaDA.Paginar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public UnidadMedidaEntidad BuscarID(int IDUnidadMedida)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            UnidadMedidaEntidad? unidadMedidaEntidad = null;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].UnidadMedida_BuscarPK";

                sqlComm.Parameters.Add(new SqlParameter("@IDUnidadMedida", IDUnidadMedida));
                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    unidadMedidaEntidad = new();
                    unidadMedidaEntidad.IDUnidadMedida = (int)sqlDataRead["IDUnidadMedida"];
                    unidadMedidaEntidad.UnidadMedida = (string)sqlDataRead["UnidadMedida"];
                    unidadMedidaEntidad.Sigla = (string)sqlDataRead["Sigla"];
                    unidadMedidaEntidad.Activo = (bool)sqlDataRead["Activo"];


                }

                sqlConn.Close();

                if (unidadMedidaEntidad == null)
                {
                    throw new Exception("El ID de [dbo].UnidadMedida no existe.");
                }

                return unidadMedidaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("UnidadMedidaDA.BuscarID: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public int IDInicio()
        {
            int IDUnidadMedida = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDUnidadMedida = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].UnidadMedidaPrimera");

                return IDUnidadMedida;
            }
            catch (Exception ex)
            {
                throw new Exception("UnidadMedidaDA.IDInicio: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDFinal()
        {
            int IDUnidadMedida = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDUnidadMedida = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].UnidadMedidaFinal");

                return IDUnidadMedida;
            }
            catch (Exception ex)
            {
                throw new Exception("UnidadMedidaDA.IDFinal: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDSiguiente(int IDUnidadMedidaActual)
        {
            int IDUnidadMedida = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDUnidadMedida = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].UnidadMedidaSiguiente", IDUnidadMedidaActual);

                return IDUnidadMedida;
            }
            catch (Exception ex)
            {
                throw new Exception("UnidadMedidaDA.IDSiguiente: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDAnterior(int IDUnidadMedidaActual)
        {
            int IDUnidadMedida = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDUnidadMedida = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].UnidadMedidaAnterior", IDUnidadMedidaActual);

                return IDUnidadMedida;
            }
            catch (Exception ex)
            {
                throw new Exception("UnidadMedidaDA.IDAnterior: " + ex.Message);
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
                Cantidad = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].UnidadMedidaContar");

                return Cantidad;
            }
            catch (Exception ex)
            {
                throw new Exception("[dbo].UnidadMedida.Contar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }


    }
}
