using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection connection { get; set; }
        private SqlCommand command { get; set; }
        private SqlDataReader lector { get; set; }
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
          //  connection = new SqlConnection("Server=localhost,1433;Database=consultorio_db;User Id=sa;Password=BaseDeDatos#2;TrustServerCertificate=True;Integrated Security=False;");
            //connection = new SqlConnection("Server=localhost;Database=PROMOS_DB;Integrated Security=True;");
            connection = new SqlConnection("Server=DANA\\SQLEXPRESS;Database=consultorio_db;Trusted_Connection=True;TrustServerCertificate=True;");
            command = new SqlCommand();
        }


        public void setearConsulta(string consulta)
        {
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            command.Connection = connection;
            try
            {
                connection.Open();
                lector = command.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void cerrarConexion()
        {
            if (lector != null)
            {
                lector.Close();
                connection.Close();
            }
        }

        public void ejecutarAccion()
        {
            command.Connection = connection;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void setearParametros(string parametro, object valor)
        {
            command.Parameters.AddWithValue(parametro, valor);
        }

        public int ejecutarAccionScalar()
        {
            // Asegurarse de que el comando usa la conexión y la consulta seteadas
            command.Connection = connection;

            try
            {
                // 1. Abre la conexión si está cerrada
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                // 2. ExecuteScalar ejecuta la consulta y devuelve la primera columna (el ID)
                object resultado = command.ExecuteScalar();

                // 3. Verifica si el resultado es válido y lo convierte a entero (int)
                if (resultado != null && resultado != DBNull.Value)
                {
                    // El resultado de ExecuteScalar es un 'object', lo convertimos a entero
                    return Convert.ToInt32(resultado);
                }

                // Si la consulta no devuelve nada, devolvemos un valor que indica error
                return -1;
            }
            catch (Exception ex)
            {
                // Propaga el error para que sea manejado por la capa de negocio
                throw new Exception("Error al ejecutar acción Scalar: " + ex.Message, ex);
            }
            finally
            {
                // 4. Cierra la conexión
                cerrarConexion();
            }
        }
    }
}