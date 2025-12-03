using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using modelo;

namespace negocio
{
    internal class UsuarioNegocio
    {
        public Usuario Autenticar(string username, string password)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = null;

            try
            {
                // 1. Consulta SQL con JOIN:
                // Se busca el usuario por credenciales Y se hace JOIN con Rol y Usuario_Profesional
                // para obtener toda la información de seguridad en una sola consulta.
                
                string consulta = "SELECT " +
                                  "U.IdUsuario, U.Username, U.Nombre, U.Apellido, U.IdRol, U.Activo, " +
                                  "R.nombre_rol, " +
                                  "UP.id_profesional " +
                                  "FROM Usuario U " +
                                  "INNER JOIN Rol R ON U.id_rol = R.id_rol " +
                                  "LEFT JOIN Usuario_Profesional UP ON U.IdUsuario = UP.id_usuario " + // LEFT JOIN: Si no es profesional (Admin), UP.id_profesional será NULL.
                                  "WHERE U.Username = @username AND U.password = @password AND U.Activo = 1";

                datos.setearConsulta(consulta);
                datos.setearParametros("@username", username);
                datos.setearParametros("@password", password); 

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    // 2. Mapeo: Si encuentra una fila, crea y popula el objeto Usuario.
                    usuario = new Usuario();

                    // Mapeo de campos básicos de Usuario
                    usuario.IdUsuario = datos.Lector.GetInt32(0);
                    usuario.Username = datos.Lector.GetString(1);
                    usuario.Nombre = datos.Lector.GetString(2);
                    usuario.Apellido = datos.Lector.GetString(3);
                    usuario.IdRol = datos.Lector.GetInt32(4);
                    usuario.Activo = datos.Lector.GetBoolean(5);

                    // Mapeo del Rol (que viene del JOIN)
                    usuario.NombreRol = datos.Lector.GetString(6);

                    // Mapeo del IdProfesional (Viene del LEFT JOIN, puede ser NULL)
                    if (!datos.Lector.IsDBNull(7)) // Verifica si el campo id_profesional es nulo
                    {
                        // Si no es nulo, significa que el usuario es un Profesional asociado
                        usuario.IdProfesionalAsociado = datos.Lector.GetInt32(7);
                    }
                    else
                    {
                        // Si es nulo, es un Administrador, y IdProfesionalAsociado queda en null.
                        usuario.IdProfesionalAsociado = null;
                    }
                }
                // Si datos.Lector.Read() devuelve false, 'usuario' sigue siendo null (login fallido).
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al intentar validar el usuario.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return usuario; // Devuelve el objeto Usuario completo o null.
        }

        public bool CrearUsuario(Usuario nuevoUsuario, int? idProfesionalAsociado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                return datos.EjecutarTransaccionCrearUsuario(nuevoUsuario, idProfesionalAsociado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar crear el usuario.", ex);
            }
        }

        public bool CambiarPassword(int idUsuario, string nuevaPassword)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "UPDATE Usuario SET password = @nuevaPassword WHERE IdUsuario = @idUsuario";
                datos.setearConsulta(consulta);
                datos.setearParametros("@nuevaPassword", nuevaPassword);
                datos.setearParametros("@idUsuario", idUsuario);
                int filaAfectada = datos.ejecutarAccionConRetorno();

                return filaAfectada > 0; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar cambiar la contraseña.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
