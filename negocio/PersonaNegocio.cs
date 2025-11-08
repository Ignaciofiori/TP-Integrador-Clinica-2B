using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using modelo;
using System;
using System.Collections.Generic;

namespace negocio
{
    public class PersonaNegocio
    {
        public List<Persona> Listar()
        {
            List<Persona> lista = new List<Persona>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id_persona, nombre, apellido, dni, fecha_nacimiento, telefono, email, direccion, activo FROM Persona WHERE activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Persona p = new Persona();

                    // PK (siempre viene)
                    p.Id = (int)datos.Lector["id_persona"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        p.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["apellido"] is DBNull))
                        p.Apellido = (string)datos.Lector["apellido"];

                    if (!(datos.Lector["dni"] is DBNull))
                        p.DNI = (string)datos.Lector["dni"];

                    if (!(datos.Lector["fecha_nacimiento"] is DBNull))
                        p.FechaNacimiento = (DateTime)datos.Lector["fecha_nacimiento"];

                    if (!(datos.Lector["telefono"] is DBNull))
                        p.Telefono = (string)datos.Lector["telefono"];

                    if (!(datos.Lector["email"] is DBNull))
                        p.Email = (string)datos.Lector["email"];

                    if (!(datos.Lector["direccion"] is DBNull))
                        p.Direccion = (string)datos.Lector["direccion"];

                    if (!(datos.Lector["activo"] is DBNull))
                        p.Activo = (bool)datos.Lector["activo"];

                    lista.Add(p);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public int Agregar(Persona p)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Persona (nombre, apellido, dni, fecha_nacimiento, telefono, email, direccion)
                    OUTPUT INSERTED.id_persona
                    VALUES (@nombre, @apellido, @dni, @fecha, @telefono, @email, @direccion)");

                datos.setearParametros("@nombre", p.Nombre);
                datos.setearParametros("@apellido", p.Apellido);
                datos.setearParametros("@dni", p.DNI);
                datos.setearParametros("@fecha", p.FechaNacimiento);
                datos.setearParametros("@telefono", p.Telefono);
                datos.setearParametros("@email", p.Email);
                datos.setearParametros("@direccion", p.Direccion);

                int id = datos.ejecutarAccionScalar();
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Modificar(Persona p)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE Persona SET
                        nombre = @nombre,
                        apellido = @apellido,
                        dni = @dni,
                        fecha_nacimiento = @fecha,
                        telefono = @telefono,
                        email = @email,
                        direccion = @direccion
                    WHERE id_persona = @id");

                datos.setearParametros("@nombre", p.Nombre);
                datos.setearParametros("@apellido", p.Apellido);
                datos.setearParametros("@dni", p.DNI);
                datos.setearParametros("@fecha", p.FechaNacimiento);
                datos.setearParametros("@telefono", p.Telefono);
                datos.setearParametros("@email", p.Email);
                datos.setearParametros("@direccion", p.Direccion);
                datos.setearParametros("@id", p.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Persona SET activo = 0 WHERE id_persona = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
