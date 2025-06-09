using System.Data;
using Microsoft.Data.SqlClient;
using Models.ACME;

namespace DataAccess.ACME
{
    public class ArticuloDA
    {
        private Conexion _conexion = new Conexion();

        public void Insertar(ArticuloEntidad articuloEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Articulo_Insertar";
                sqlComm.Parameters.Add(new SqlParameter("@IDArticulo", SqlDbType.Int)).Direction = ParameterDirection.Output;
                sqlComm.Parameters.Add(new SqlParameter("@IDUnidadMedida", articuloEntidad.IDUnidadMedida));
                sqlComm.Parameters.Add(new SqlParameter("@Articulo", articuloEntidad.Articulo));
                sqlComm.Parameters.Add(new SqlParameter("@Precio", (articuloEntidad.Precio is null) ? DBNull.Value : articuloEntidad.Precio));
                sqlComm.Parameters.Add(new SqlParameter("@StockActual", (articuloEntidad.StockActual is null) ? DBNull.Value : articuloEntidad.StockActual));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", articuloEntidad.Activo));

                sqlComm.ExecuteNonQuery();
                articuloEntidad.IDArticulo = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDArticulo")].Value;

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("ArticuloDA.Insertar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Insertar(ArticuloEntidad articuloEntidad, SqlConnection sqlConn, SqlTransaction sqlTrans)
        {
            // Obtener una instancia de la conexión
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Articulo_Insertar";
                sqlComm.Transaction = sqlTrans;
                sqlComm.Parameters.Add(new SqlParameter("@IDArticulo", SqlDbType.Int)).Direction = ParameterDirection.Output;
                sqlComm.Parameters.Add(new SqlParameter("@IDUnidadMedida", articuloEntidad.IDUnidadMedida));
                sqlComm.Parameters.Add(new SqlParameter("@Articulo", articuloEntidad.Articulo));
                sqlComm.Parameters.Add(new SqlParameter("@Precio", (articuloEntidad.Precio is null) ? DBNull.Value : articuloEntidad.Precio));
                sqlComm.Parameters.Add(new SqlParameter("@StockActual", (articuloEntidad.StockActual is null) ? DBNull.Value : articuloEntidad.StockActual));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", articuloEntidad.Activo));

                sqlComm.ExecuteNonQuery();
                articuloEntidad.IDArticulo = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDArticulo")].Value;

            }
            catch (Exception ex)
            {
                throw new Exception("ArticuloDA.Insertar(Sob1): " + ex.Message);
            }
        }

        public void Modificar(ArticuloEntidad articuloEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Articulo_Modificar";
                sqlComm.Parameters.Add(new SqlParameter("@IDArticulo", articuloEntidad.IDArticulo));
                sqlComm.Parameters.Add(new SqlParameter("@IDUnidadMedida", articuloEntidad.IDUnidadMedida));
                sqlComm.Parameters.Add(new SqlParameter("@Articulo", articuloEntidad.Articulo));
                sqlComm.Parameters.Add(new SqlParameter("@Precio", (articuloEntidad.Precio is null) ? DBNull.Value : articuloEntidad.Precio));
                sqlComm.Parameters.Add(new SqlParameter("@StockActual", (articuloEntidad.StockActual is null) ? DBNull.Value : articuloEntidad.StockActual));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", articuloEntidad.Activo));

                if (sqlComm.ExecuteNonQuery() != 1)
                {
                    throw new Exception("ArticuloDA.Modificar: Problema al actualizar.");
                }

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("ArticuloDA.Modificar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Anular(ArticuloEntidad articuloEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Articulo_Eliminar";
                sqlComm.Parameters.Add(new SqlParameter("@IDArticulo", articuloEntidad.IDArticulo));

                sqlComm.ExecuteNonQuery();

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("ArticuloDA.Anular: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public List<ArticuloEntidad> Listar()
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<ArticuloEntidad>? listaArticuloEntidad = new();
            ArticuloEntidad? articuloEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Articulo_Listar";

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    articuloEntidad = new();
                    articuloEntidad.IDArticulo = (int)sqlDataRead["IDArticulo"];
                    articuloEntidad.IDUnidadMedida = (int)sqlDataRead["IDUnidadMedida"];
                    articuloEntidad.Articulo = (string)sqlDataRead["Articulo"];
                    if (sqlDataRead["Precio"] != DBNull.Value) articuloEntidad.Precio = (decimal)sqlDataRead["Precio"];
                    if (sqlDataRead["StockActual"] != DBNull.Value) articuloEntidad.StockActual = (decimal)sqlDataRead["StockActual"];
                    articuloEntidad.Activo = (bool)sqlDataRead["Activo"];

                    articuloEntidad.UnidadMedida = (string)sqlDataRead["UnidadMedida"];


                    listaArticuloEntidad.Add(articuloEntidad);
                }

                sqlConn.Close();

                return listaArticuloEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("ArticuloDA.Listar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public List<ArticuloEntidad> Filtrar(string filtro)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<ArticuloEntidad>? listaArticuloEntidad = new();
            ArticuloEntidad? articuloEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Articulo_Filtrar";
                sqlComm.Parameters.Add(new SqlParameter("@Filtro", filtro));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    articuloEntidad = new();
                    articuloEntidad.IDArticulo = (int)sqlDataRead["IDArticulo"];
                    articuloEntidad.IDUnidadMedida = (int)sqlDataRead["IDUnidadMedida"];
                    articuloEntidad.Articulo = (string)sqlDataRead["Articulo"];
                    if (sqlDataRead["Precio"] != DBNull.Value) articuloEntidad.Precio = (decimal)sqlDataRead["Precio"];
                    if (sqlDataRead["StockActual"] != DBNull.Value) articuloEntidad.StockActual = (decimal)sqlDataRead["StockActual"];
                    articuloEntidad.Activo = (bool)sqlDataRead["Activo"];

                    articuloEntidad.UnidadMedida = (string)sqlDataRead["UnidadMedida"];


                    listaArticuloEntidad.Add(articuloEntidad);
                }

                sqlConn.Close();

                return listaArticuloEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("ArticuloDA.Filtrar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public List<ArticuloEntidad> Paginar(int pagina, int tamPagina)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<ArticuloEntidad>? listaArticuloEntidad = new();
            ArticuloEntidad? articuloEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Articulo_Paginar";
                sqlComm.Parameters.Add(new SqlParameter("@Pagina", pagina));
                sqlComm.Parameters.Add(new SqlParameter("@TamPagina", tamPagina));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    articuloEntidad = new();
                    articuloEntidad.IDArticulo = (int)sqlDataRead["IDArticulo"];
                    articuloEntidad.IDUnidadMedida = (int)sqlDataRead["IDUnidadMedida"];
                    articuloEntidad.Articulo = (string)sqlDataRead["Articulo"];
                    if (sqlDataRead["Precio"] != DBNull.Value) articuloEntidad.Precio = (decimal)sqlDataRead["Precio"];
                    if (sqlDataRead["StockActual"] != DBNull.Value) articuloEntidad.StockActual = (decimal)sqlDataRead["StockActual"];
                    articuloEntidad.Activo = (bool)sqlDataRead["Activo"];

                    articuloEntidad.UnidadMedida = (string)sqlDataRead["UnidadMedida"];


                    listaArticuloEntidad.Add(articuloEntidad);
                }

                sqlConn.Close();

                return listaArticuloEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("ArticuloDA.Paginar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public ArticuloEntidad BuscarID(int IDArticulo)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            ArticuloEntidad? articuloEntidad = null;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Articulo_BuscarPK";

                sqlComm.Parameters.Add(new SqlParameter("@IDArticulo", IDArticulo));
                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    articuloEntidad = new();
                    articuloEntidad.IDArticulo = (int)sqlDataRead["IDArticulo"];
                    articuloEntidad.IDUnidadMedida = (int)sqlDataRead["IDUnidadMedida"];
                    articuloEntidad.Articulo = (string)sqlDataRead["Articulo"];
                    if (sqlDataRead["Precio"] != DBNull.Value) articuloEntidad.Precio = (decimal)sqlDataRead["Precio"];
                    if (sqlDataRead["StockActual"] != DBNull.Value) articuloEntidad.StockActual = (decimal)sqlDataRead["StockActual"];
                    articuloEntidad.Activo = (bool)sqlDataRead["Activo"];

                    articuloEntidad.UnidadMedida = (string)sqlDataRead["UnidadMedida"];

                }

                sqlConn.Close();

                if (articuloEntidad == null)
                {
                    throw new Exception("El ID de [dbo].Articulo no existe.");
                }

                return articuloEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("ArticuloDA.BuscarID: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public int IDInicio()
        {
            int IDArticulo = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDArticulo = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].ArticuloPrimera");

                return IDArticulo;
            }
            catch (Exception ex)
            {
                throw new Exception("ArticuloDA.IDInicio: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDFinal()
        {
            int IDArticulo = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDArticulo = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].ArticuloFinal");

                return IDArticulo;
            }
            catch (Exception ex)
            {
                throw new Exception("ArticuloDA.IDFinal: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDSiguiente(int IDArticuloActual)
        {
            int IDArticulo = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDArticulo = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].ArticuloSiguiente", IDArticuloActual);

                return IDArticulo;
            }
            catch (Exception ex)
            {
                throw new Exception("ArticuloDA.IDSiguiente: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDAnterior(int IDArticuloActual)
        {
            int IDArticulo = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDArticulo = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].ArticuloAnterior", IDArticuloActual);

                return IDArticulo;
            }
            catch (Exception ex)
            {
                throw new Exception("ArticuloDA.IDAnterior: " + ex.Message);
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
                Cantidad = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].ArticuloContar");

                return Cantidad;
            }
            catch (Exception ex)
            {
                throw new Exception("[dbo].Articulo.Contar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }


        public List<ArticuloEntidad> BuscarFKIDUnidadMedida(int IDUnidadMedida)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<ArticuloEntidad>? listaArticuloEntidad = new();
            ArticuloEntidad? articuloEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Articulo_BuscarFKIDUnidadMedida";

                sqlComm.Parameters.Add(new SqlParameter("@IDUnidadMedida", IDUnidadMedida));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    articuloEntidad = new();
                    articuloEntidad.IDArticulo = (int)sqlDataRead["IDArticulo"];
                    articuloEntidad.IDUnidadMedida = (int)sqlDataRead["IDUnidadMedida"];
                    articuloEntidad.Articulo = (string)sqlDataRead["Articulo"];
                    if (sqlDataRead["Precio"] != DBNull.Value) articuloEntidad.Precio = (decimal)sqlDataRead["Precio"];
                    if (sqlDataRead["StockActual"] != DBNull.Value) articuloEntidad.StockActual = (decimal)sqlDataRead["StockActual"];
                    articuloEntidad.Activo = (bool)sqlDataRead["Activo"];

                    articuloEntidad.UnidadMedida = (string)sqlDataRead["UnidadMedida"];


                    listaArticuloEntidad.Add(articuloEntidad);
                }

                sqlConn.Close();

                return listaArticuloEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("ArticuloDA.BuscarFKIDUnidadMedida: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }
    }
}
