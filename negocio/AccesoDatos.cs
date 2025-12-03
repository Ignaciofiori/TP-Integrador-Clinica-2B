using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using modelo;

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
            //connection = new SqlConnection("Server=localhost,1433;Database=clinica_db;User Id=sa;Password=BaseDeDatos#2;TrustServerCertificate=True;Integrated Security=False;");
            connection = new SqlConnection("Server=localhost;Database=clinica_db;Integrated Security=True;");
            // connection = new SqlConnection("Server=DANA\\SQLEXPRESS;Database=clinica_db;Trusted_Connection=True;TrustServerCertificate=True;");
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

        public int ejecutarAccionConRetorno()
        {
            command.Connection = connection;

            try
            {
                connection.Open();
                return command.ExecuteNonQuery();
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

        public bool EjecutarTransaccionCrearUsuario(Usuario nuevoUsuario, int? idProfesionalAsociado)
        {
            command.Connection = connection;
            SqlTransaction transaccion = null;

            try
            {
                connection.Open();
                transaccion = connection.BeginTransaction();
                command.Transaction = transaccion;
                command.Parameters.Clear(); 

                string insertUsuarioSql = "INSERT INTO Usuario (Username, Password, Nombre, Apellido, IdRol, Activo) " +
                                          "VALUES (@Username, @Password, @Nombre, @Apellido, @IdRol, 1); " +
                                          "SELECT CAST(SCOPE_IDENTITY() AS INT)";

                setearConsulta(insertUsuarioSql);

                // Parámetros
                setearParametros("@Username", nuevoUsuario.Username);
                setearParametros("@Password", nuevoUsuario.Password);
                setearParametros("@Nombre", nuevoUsuario.Nombre);
                setearParametros("@Apellido", nuevoUsuario.Apellido);
                setearParametros("@IdRol", nuevoUsuario.IdRol);

                // **USO DIRECTO DE ExecuteScalar()** - Evita que se cierre la conexión
                object resultadoScalar = command.ExecuteScalar();
                int nuevoIdUsuario = resultadoScalar != null && resultadoScalar != DBNull.Value ? Convert.ToInt32(resultadoScalar) : -1;

                if (nuevoIdUsuario == -1)
                {
                    transaccion.Rollback();
                    return false;
                }

                // --- Segundo UPDATE (Asignación de Profesional, basado en tu lógica) ---
                if (idProfesionalAsociado.HasValue)
                {
                    // Limpiar parámetros para la nueva consulta
                    command.Parameters.Clear();

                    // Consulta
                    string updateProfesionalSql = "UPDATE Usuario SET IdProfesionalAsociado = @IdProfesionalAsociado WHERE IdUsuario = @IdUsuario";
                    setearConsulta(updateProfesionalSql);

                    // Asignar nuevos parámetros
                    setearParametros("@IdProfesionalAsociado", idProfesionalAsociado.Value);
                    setearParametros("@IdUsuario", nuevoIdUsuario);

                    // **USO DIRECTO DE ExecuteNonQuery()** - Evita que se cierre la conexión
                    command.ExecuteNonQuery();
                }

                // Si todo va bien
                transaccion.Commit();
                return true;
            }
            catch (Exception ex)
            {
                // Rollback y lanzar excepción
                transaccion?.Rollback();
                throw new Exception("Error durante la transacción de Creación de Usuario.", ex);
            }
            finally
            {
                cerrarConexion();
                command.Parameters.Clear(); 
            }
        }
    }
}