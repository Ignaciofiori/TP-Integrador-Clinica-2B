using modelo;
using System;
using System.Collections.Generic;

namespace negocio
{
    public class EspecialidadNegocio
    {
        public List<Especialidad> Listar()
        {
            List<Especialidad> lista = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id_especialidad, nombre, descripcion FROM Especialidad");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad esp = new Especialidad();

                    // ID (no hace falta validar porque es PK NOT NULL)
                    esp.Id = (int)datos.Lector["id_especialidad"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        esp.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["descripcion"] is DBNull))
                        esp.Descripcion = (string)datos.Lector["descripcion"];

                    lista.Add(esp);
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

        public void Agregar(Especialidad esp)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Especialidad (nombre, descripcion) VALUES (@nombre, @descripcion)");
                datos.setearParametros("@nombre", esp.Nombre);
                datos.setearParametros("@descripcion", esp.Descripcion);
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

        public void Modificar(Especialidad esp)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Especialidad SET nombre = @nombre, descripcion = @descripcion WHERE id_especialidad = @id");
                datos.setearParametros("@nombre", esp.Nombre);
                datos.setearParametros("@descripcion", esp.Descripcion);
                datos.setearParametros("@id", esp.Id);
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
                datos.setearConsulta("DELETE FROM Especialidad WHERE id_especialidad = @id");
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
