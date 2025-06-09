using System.Data;
using Microsoft.Data.SqlClient;
using Models.ACME;

namespace DataAccess.ACME
{
    public class RequisicionDA
    {
        private Conexion _conexion = new Conexion();

        public void Insertar(RequisicionEntidad requisicionEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();
            SqlTransaction sqlTrans = null;

            try
            {
                sqlConn.Open();
                sqlTrans = sqlConn.BeginTransaction();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Requisicion_Insertar";
                sqlComm.Transaction = sqlTrans;
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", SqlDbType.Int)).Direction = ParameterDirection.Output;
                sqlComm.Parameters.Add(new SqlParameter("@IDEmpresa", requisicionEntidad.IDEmpresa));
                sqlComm.Parameters.Add(new SqlParameter("@NroRequisicion", requisicionEntidad.NroRequisicion));
                sqlComm.Parameters.Add(new SqlParameter("@FechaEmision", requisicionEntidad.FechaEmision));
                sqlComm.Parameters.Add(new SqlParameter("@Aprobada", requisicionEntidad.Aprobada));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", requisicionEntidad.Activo));

                sqlComm.ExecuteNonQuery();
                requisicionEntidad.IDRequisicion = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDRequisicion")].Value;

                // Insertar el detalle
                RequisicionDetalleDA requisicionDetalleDA = new RequisicionDetalleDA();
                foreach (var requisicionDetalle in requisicionEntidad.ListaRequisicionDetalle)
                {
                    requisicionDetalle.IDRequisicion = requisicionEntidad.IDRequisicion;
                    requisicionDetalleDA.Insertar(requisicionDetalle, sqlConn, sqlTrans);
                }
                RequisicionAnotacionDA requisicionAnotacionDA = new RequisicionAnotacionDA();
                foreach (var requisicionAnotacion in requisicionEntidad.ListaRequisicionAnotacion)
                {
                    requisicionAnotacion.IDRequisicion = requisicionEntidad.IDRequisicion;
                    requisicionAnotacionDA.Insertar(requisicionAnotacion, sqlConn, sqlTrans);
                }

                sqlTrans.Commit();

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                sqlTrans.Rollback();
                throw new Exception("RequisicionDA.Insertar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Modificar(RequisicionEntidad requisicionEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();
            SqlTransaction sqlTrans = null;

            try
            {
                sqlConn.Open();
                sqlTrans = sqlConn.BeginTransaction();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Requisicion_Modificar";
                sqlComm.Transaction = sqlTrans;
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", requisicionEntidad.IDRequisicion));
                sqlComm.Parameters.Add(new SqlParameter("@IDEmpresa", requisicionEntidad.IDEmpresa));
                sqlComm.Parameters.Add(new SqlParameter("@NroRequisicion", requisicionEntidad.NroRequisicion));
                sqlComm.Parameters.Add(new SqlParameter("@FechaEmision", requisicionEntidad.FechaEmision));
                sqlComm.Parameters.Add(new SqlParameter("@Aprobada", requisicionEntidad.Aprobada));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", requisicionEntidad.Activo));

                // Insertar el detalle
                RequisicionDetalleDA requisicionDetalleDA = new RequisicionDetalleDA();
                foreach (var requisicionDetalle in requisicionEntidad.ListaRequisicionDetalle)
                {
                    if (requisicionDetalle.IDRequisicionDetalle == 0)
                    {
                        // Insertar nuevos ítems
                        requisicionDetalle.IDRequisicion = requisicionEntidad.IDRequisicion;
                        requisicionDetalleDA.Insertar(requisicionDetalle, sqlConn, sqlTrans);
                    }
                    else
                    {
                        // Modificar ítems existentes
                        requisicionDetalleDA.Modificar(requisicionDetalle, sqlConn, sqlTrans);
                    }
                }
                RequisicionAnotacionDA requisicionAnotacionDA = new RequisicionAnotacionDA();
                foreach (var requisicionAnotacion in requisicionEntidad.ListaRequisicionAnotacion)
                {
                    if (requisicionAnotacion.IDRequisicionAnotacion == 0)
                    {
                        // Insertar nuevos ítems
                        requisicionAnotacion.IDRequisicion = requisicionEntidad.IDRequisicion;
                        requisicionAnotacionDA.Insertar(requisicionAnotacion, sqlConn, sqlTrans);
                    }
                    else
                    {
                        // Modificar ítems existentes
                        requisicionAnotacionDA.Modificar(requisicionAnotacion, sqlConn, sqlTrans);
                    }
                }

                sqlTrans.Commit();

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                sqlTrans.Rollback();
                throw new Exception("RequisicionDA.Modificar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public void Anular(RequisicionEntidad requisicionEntidad)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Requisicion_Eliminar";
                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", requisicionEntidad.IDRequisicion));

                sqlComm.ExecuteNonQuery();

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDA.Anular: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public List<RequisicionEntidad> Listar()
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<RequisicionEntidad>? listaRequisicionEntidad = new();
            RequisicionEntidad? requisicionEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Requisicion_Listar";

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionEntidad = new();
                    requisicionEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionEntidad.IDEmpresa = (int)sqlDataRead["IDEmpresa"];
                    requisicionEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];
                    requisicionEntidad.FechaEmision = (DateTime)sqlDataRead["FechaEmision"];
                    requisicionEntidad.Aprobada = (bool)sqlDataRead["Aprobada"];
                    requisicionEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionEntidad.RUC = (string)sqlDataRead["RUC"];
                    requisicionEntidad.Empresa = (string)sqlDataRead["Empresa"];


                    listaRequisicionEntidad.Add(requisicionEntidad);
                }

                sqlConn.Close();

                return listaRequisicionEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDA.Listar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public List<RequisicionEntidad> Filtrar(string filtro)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<RequisicionEntidad>? listaRequisicionEntidad = new();
            RequisicionEntidad? requisicionEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Requisicion_Filtrar";
                sqlComm.Parameters.Add(new SqlParameter("@Filtro", filtro));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionEntidad = new();
                    requisicionEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionEntidad.IDEmpresa = (int)sqlDataRead["IDEmpresa"];
                    requisicionEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];
                    requisicionEntidad.FechaEmision = (DateTime)sqlDataRead["FechaEmision"];
                    requisicionEntidad.Aprobada = (bool)sqlDataRead["Aprobada"];
                    requisicionEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionEntidad.RUC = (string)sqlDataRead["RUC"];
                    requisicionEntidad.Empresa = (string)sqlDataRead["Empresa"];


                    listaRequisicionEntidad.Add(requisicionEntidad);
                }

                sqlConn.Close();

                return listaRequisicionEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDA.Filtrar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public List<RequisicionEntidad> Paginar(int pagina, int tamPagina)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<RequisicionEntidad>? listaRequisicionEntidad = new();
            RequisicionEntidad? requisicionEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Requisicion_Paginar";
                sqlComm.Parameters.Add(new SqlParameter("@Pagina", pagina));
                sqlComm.Parameters.Add(new SqlParameter("@TamPagina", tamPagina));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionEntidad = new();
                    requisicionEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionEntidad.IDEmpresa = (int)sqlDataRead["IDEmpresa"];
                    requisicionEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];
                    requisicionEntidad.FechaEmision = (DateTime)sqlDataRead["FechaEmision"];
                    requisicionEntidad.Aprobada = (bool)sqlDataRead["Aprobada"];
                    requisicionEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionEntidad.RUC = (string)sqlDataRead["RUC"];
                    requisicionEntidad.Empresa = (string)sqlDataRead["Empresa"];


                    listaRequisicionEntidad.Add(requisicionEntidad);
                }

                sqlConn.Close();

                return listaRequisicionEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDA.Paginar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public RequisicionEntidad BuscarID(int IDRequisicion)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            RequisicionEntidad? requisicionEntidad = null;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Requisicion_BuscarPK";

                sqlComm.Parameters.Add(new SqlParameter("@IDRequisicion", IDRequisicion));
                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionEntidad = new();
                    requisicionEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionEntidad.IDEmpresa = (int)sqlDataRead["IDEmpresa"];
                    requisicionEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];
                    requisicionEntidad.FechaEmision = (DateTime)sqlDataRead["FechaEmision"];
                    requisicionEntidad.Aprobada = (bool)sqlDataRead["Aprobada"];
                    requisicionEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionEntidad.RUC = (string)sqlDataRead["RUC"];
                    requisicionEntidad.Empresa = (string)sqlDataRead["Empresa"];

                }

                sqlConn.Close();

                if (requisicionEntidad == null)
                {
                    throw new Exception("El ID de [dbo].Requisicion no existe.");
                }

                return requisicionEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDA.BuscarID: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }

        public int IDInicio()
        {
            int IDRequisicion = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDRequisicion = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionPrimera");

                return IDRequisicion;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDA.IDInicio: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDFinal()
        {
            int IDRequisicion = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDRequisicion = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionFinal");

                return IDRequisicion;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDA.IDFinal: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDSiguiente(int IDRequisicionActual)
        {
            int IDRequisicion = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDRequisicion = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionSiguiente", IDRequisicionActual);

                return IDRequisicion;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDA.IDSiguiente: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }

        public int IDAnterior(int IDRequisicionActual)
        {
            int IDRequisicion = 0;

            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();

            try
            {
                IDRequisicion = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionAnterior", IDRequisicionActual);

                return IDRequisicion;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDA.IDAnterior: " + ex.Message);
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
                Cantidad = _conexion.EjecutarFuncionInt(sqlConn, "[dbo].RequisicionContar");

                return Cantidad;
            }
            catch (Exception ex)
            {
                throw new Exception("[dbo].Requisicion.Contar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }


        public List<RequisicionEntidad> BuscarFKIDEmpresa(int IDEmpresa)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();

            List<RequisicionEntidad>? listaRequisicionEntidad = new();
            RequisicionEntidad? requisicionEntidad;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "[dbo].Requisicion_BuscarFKIDEmpresa";

                sqlComm.Parameters.Add(new SqlParameter("@IDEmpresa", IDEmpresa));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    requisicionEntidad = new();
                    requisicionEntidad.IDRequisicion = (int)sqlDataRead["IDRequisicion"];
                    requisicionEntidad.IDEmpresa = (int)sqlDataRead["IDEmpresa"];
                    requisicionEntidad.NroRequisicion = (string)sqlDataRead["NroRequisicion"];
                    requisicionEntidad.FechaEmision = (DateTime)sqlDataRead["FechaEmision"];
                    requisicionEntidad.Aprobada = (bool)sqlDataRead["Aprobada"];
                    requisicionEntidad.Activo = (bool)sqlDataRead["Activo"];

                    requisicionEntidad.RUC = (string)sqlDataRead["RUC"];
                    requisicionEntidad.Empresa = (string)sqlDataRead["Empresa"];


                    listaRequisicionEntidad.Add(requisicionEntidad);
                }

                sqlConn.Close();

                return listaRequisicionEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("RequisicionDA.BuscarFKIDEmpresa: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }
    }
}
