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
           // connection = new SqlConnection("Server=localhost,1433;Database=clinica_db;User Id=sa;Password=BaseDeDatos#2;TrustServerCertificate=True;Integrated Security=False;");
            //connection = new SqlConnection("Server=localhost;Database=clinica_db;Integrated Security=True;");
            connection = new SqlConnection("Server=DANA\\SQLEXPRESS;Database=clinica_db;Trusted_Connection=True;TrustServerCertificate=True;");
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
          
            command.Connection = connection;

            try
            {

                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

               
                object resultado = command.ExecuteScalar();

                
                if (resultado != null && resultado != DBNull.Value)
                {
                
                    return Convert.ToInt32(resultado);
                }

                
                return -1;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al ejecutar acción Scalar: " + ex.Message, ex);
            }
            finally
            {
             
                cerrarConexion();
            }
        }
    }
}