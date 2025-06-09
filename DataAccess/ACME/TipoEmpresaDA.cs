using System.Data;
using Microsoft.Data.SqlClient;
using Models.ACME;

namespace DataAccess.ACME
{
    public class TipoEmpresaDA
    {
        private Conexion _conexion = new Conexion();

        public void Insertar(TipoEmpresaEntidad tipoEmpresaEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].TipoEmpresa_Insertar";
                sqlComm.Parameters.Add(new SqlParameter("@IDTipoEmpresa", SqlDbType.Int)).Direction = ParameterDirection.Output;
                sqlComm.Parameters.Add(new SqlParameter("@TipoEmpresa", tipoEmpresaEntidad.TipoEmpresa));
                sqlComm.Parameters.Add(new SqlParameter("@Descripcion", tipoEmpresaEntidad.Descripcion));
                sqlComm.Parameters.Add(new SqlParameter("@Sigla", (tipoEmpresaEntidad.Sigla is null) ? DBNull.Value : tipoEmpresaEntidad.Sigla));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", tipoEmpresaEntidad.Activo));

                sqlComm.ExecuteNonQuery();
                tipoEmpresaEntidad.IDTipoEmpresa = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDTipoEmpresa")].Value;

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("TipoEmpresaDA.Insertar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Modificar(TipoEmpresaEntidad tipoEmpresaEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].TipoEmpresa_Modificar";
                sqlComm.Parameters.Add(new SqlParameter("@IDTipoEmpresa", tipoEmpresaEntidad.IDTipoEmpresa));
                sqlComm.Parameters.Add(new SqlParameter("@TipoEmpresa", tipoEmpresaEntidad.TipoEmpresa));
                sqlComm.Parameters.Add(new SqlParameter("@Descripcion", tipoEmpresaEntidad.Descripcion));
                sqlComm.Parameters.Add(new SqlParameter("@Sigla", (tipoEmpresaEntidad.Sigla is null) ? DBNull.Value : tipoEmpresaEntidad.Sigla));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", tipoEmpresaEntidad.Activo));

                if (sqlComm.ExecuteNonQuery() != 1)
                {
                    throw new Exception("TipoEmpresaDA.Modificar: Problema al actualizar.");
                }

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("TipoEmpresaDA.Modificar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Anular(TipoEmpresaEntidad tipoEmpresaEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].TipoEmpresa_Eliminar";
                sqlComm.Parameters.Add(new SqlParameter("@IDTipoEmpresa", tipoEmpresaEntidad.IDTipoEmpresa));

                sqlComm.ExecuteNonQuery();

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("TipoEmpresaDA.Anular: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public List<TipoEmpresaEntidad> Listar()
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<TipoEmpresaEntidad>? listaTipoEmpresaEntidad = new();
            TipoEmpresaEntidad? tipoEmpresaEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].TipoEmpresa_Listar";

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    tipoEmpresaEntidad = new();
                    tipoEmpresaEntidad.IDTipoEmpresa = (int)sqlDataRead["IDTipoEmpresa"];
                    tipoEmpresaEntidad.TipoEmpresa = (string)sqlDataRead["TipoEmpresa"];
                    tipoEmpresaEntidad.Descripcion = (string)sqlDataRead["Descripcion"];
                    if (sqlDataRead["Sigla"] != DBNull.Value) tipoEmpresaEntidad.Sigla = (string)sqlDataRead["Sigla"];
                    tipoEmpresaEntidad.Activo = (bool)sqlDataRead["Activo"];



                    listaTipoEmpresaEntidad.Add(tipoEmpresaEntidad);
                }

                sqlConn.Close();

                return listaTipoEmpresaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("TipoEmpresaDA.Listar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public TipoEmpresaEntidad BuscarID(int IDTipoEmpresa)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            TipoEmpresaEntidad? tipoEmpresaEntidad = null;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].TipoEmpresa_BuscarPK";

                sqlComm.Parameters.Add(new SqlParameter("@IDTipoEmpresa", IDTipoEmpresa));
                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    tipoEmpresaEntidad = new();
                    tipoEmpresaEntidad.IDTipoEmpresa = (int)sqlDataRead["IDTipoEmpresa"];
                    tipoEmpresaEntidad.TipoEmpresa = (string)sqlDataRead["TipoEmpresa"];
                    tipoEmpresaEntidad.Descripcion = (string)sqlDataRead["Descripcion"];
                    if (sqlDataRead["Sigla"] != DBNull.Value) tipoEmpresaEntidad.Sigla = (string)sqlDataRead["Sigla"];
                    tipoEmpresaEntidad.Activo = (bool)sqlDataRead["Activo"];


                }

                sqlConn.Close();

                if (tipoEmpresaEntidad == null)
                {
                    throw new Exception("El ID de [dbo].TipoEmpresa no existe.");
                }

                return tipoEmpresaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("TipoEmpresaDA.BuscarID: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }
        public List<TipoEmpresaEntidad> Filtrar(string filtro)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<TipoEmpresaEntidad>? listaTipoEmpresaEntidad = new();
            TipoEmpresaEntidad? tipoEmpresaEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].TipoEmpresa_Filtrar";
                sqlComm.Parameters.Add(new SqlParameter("@Filtro", filtro));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    tipoEmpresaEntidad = new();
                    tipoEmpresaEntidad.IDTipoEmpresa = (int)sqlDataRead["IDTipoEmpresa"];
                    tipoEmpresaEntidad.TipoEmpresa = (string)sqlDataRead["TipoEmpresa"];
                    tipoEmpresaEntidad.Descripcion = (string)sqlDataRead["Descripcion"];
                    if (sqlDataRead["Sigla"] != DBNull.Value) tipoEmpresaEntidad.Sigla = (string)sqlDataRead["Sigla"];
                    tipoEmpresaEntidad.Activo = (bool)sqlDataRead["Activo"];



                    listaTipoEmpresaEntidad.Add(tipoEmpresaEntidad);
                }

                sqlConn.Close();

                return listaTipoEmpresaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("TipoEmpresaDA.Filtrar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }
        public List<TipoEmpresaEntidad> Paginar(int pagina, int tamPagina)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<TipoEmpresaEntidad>? listaTipoEmpresaEntidad = new();
            TipoEmpresaEntidad? tipoEmpresaEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].TipoEmpresa_Paginar";
                sqlComm.Parameters.Add(new SqlParameter("@Pagina", pagina));
                sqlComm.Parameters.Add(new SqlParameter("@TamPagina", tamPagina));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    tipoEmpresaEntidad = new();
                    tipoEmpresaEntidad.IDTipoEmpresa = (int)sqlDataRead["IDTipoEmpresa"];
                    tipoEmpresaEntidad.TipoEmpresa = (string)sqlDataRead["TipoEmpresa"];
                    tipoEmpresaEntidad.Descripcion = (string)sqlDataRead["Descripcion"];
                    if (sqlDataRead["Sigla"] != DBNull.Value) tipoEmpresaEntidad.Sigla = (string)sqlDataRead["Sigla"];
                    tipoEmpresaEntidad.Activo = (bool)sqlDataRead["Activo"];



                    listaTipoEmpresaEntidad.Add(tipoEmpresaEntidad);
                }

                sqlConn.Close();

                return listaTipoEmpresaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("TipoEmpresaDA.Paginar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        
        public int IDInicio()
        {
            int IDTipoEmpresa = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDTipoEmpresa = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].TipoEmpresaPrimera");

                return IDTipoEmpresa;
            }
            catch (Exception ex)
            {
                throw new Exception("TipoEmpresaDA.IDInicio: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDFinal()
        {
            int IDTipoEmpresa = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDTipoEmpresa = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].TipoEmpresaFinal");

                return IDTipoEmpresa;
            }
            catch (Exception ex)
            {
                throw new Exception("TipoEmpresaDA.IDFinal: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDSiguiente(int IDTipoEmpresaActual)
        {
            int IDTipoEmpresa = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDTipoEmpresa = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].TipoEmpresaSiguiente", IDTipoEmpresaActual);

                return IDTipoEmpresa;
            }
            catch (Exception ex)
            {
                throw new Exception("TipoEmpresaDA.IDSiguiente: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDAnterior(int IDTipoEmpresaActual)
        {
            int IDTipoEmpresa = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDTipoEmpresa = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].TipoEmpresaAnterior", IDTipoEmpresaActual);

                return IDTipoEmpresa;
            }
            catch (Exception ex)
            {
                throw new Exception("TipoEmpresaDA.IDAnterior: " + ex.Message);
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
                Cantidad = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].TipoEmpresaContar");

                return Cantidad;
            }
            catch (Exception ex)
            {
                throw new Exception("[dbo].TipoEmpresa.Contar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }
    }
}
