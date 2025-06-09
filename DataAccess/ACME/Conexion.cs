using Microsoft.Data.SqlClient;

namespace DataAccess.ACME
{
    public class Conexion
    {
        private readonly string? _cadenaConexion;

        public Conexion()
        {
            string? cadenaConexion;

            // Obtener la cadena de conexión desde variable de entorno
            cadenaConexion = Environment.GetEnvironmentVariable("ACME_cc");

            _cadenaConexion = cadenaConexion;
        }

        public SqlConnection Conectar()
        {
            SqlConnection sqlConn;

            // Instanciar la conexión utilizando la cadena de conexión recibida
            sqlConn = new SqlConnection(_cadenaConexion);

            return sqlConn;
        }

        public int EjecutarFuncionInt(SqlConnection sqlConnection, string nombreFuncion)
        {
            int resultado = 0;


            sqlConnection.Open();

            using (SqlCommand command = new SqlCommand($"SELECT {nombreFuncion}() AS Resultado", sqlConnection))
            {
                // Ejecutar la función y obtener el resultado
                resultado = (int)command.ExecuteScalar();
            }

            return resultado;
        }

        public int EjecutarFuncionInt(SqlConnection sqlConnection, string nombreFuncion, int paramInt)
        {
            int resultado = 0;


            sqlConnection.Open();

            using (SqlCommand command = new SqlCommand($"SELECT {nombreFuncion}({paramInt.ToString()}) AS Resultado", sqlConnection))
            {
                // Ejecutar la función y obtener el resultado
                resultado = (int)command.ExecuteScalar();
            }

            return resultado;
        }
    }
}