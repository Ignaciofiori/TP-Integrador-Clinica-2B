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

        public List<Especialidad> ListarEspecialidadPorProfesional(int idProfesional)
            {
                List<Especialidad> lista = new List<Especialidad>();
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.setearConsulta(@"
                    SELECT e.id_especialidad, e.nombre, e.descripcion
                    FROM Profesional_Especialidad pe
                    INNER JOIN Especialidad e ON pe.id_especialidad = e.id_especialidad
                    WHERE pe.id_profesional = @id
                ");

                    datos.setearParametros("@id", idProfesional);
                    datos.ejecutarLectura();

                    while (datos.Lector.Read())
                    {
                        Especialidad aux = new Especialidad();

                        aux.Id = (int)datos.Lector["id_especialidad"];

                        if (!(datos.Lector["nombre"] is DBNull))
                            aux.Nombre = (string)datos.Lector["nombre"];

                        if (!(datos.Lector["descripcion"] is DBNull))
                            aux.Descripcion = (string)datos.Lector["descripcion"];

                        lista.Add(aux);
                    }

                    return lista;
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }

        public Especialidad BuscarPorId(int id)
        {
            Especialidad aux = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT id_especialidad, nombre, descripcion
            FROM Especialidad
            WHERE id_especialidad = @id");

                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux = new Especialidad();

                    aux.Id = (int)datos.Lector["id_especialidad"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["descripcion"];
                }

                return aux;
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

        public void AgregarEspecialidadAProfesional(int idProfesional, int idEspecialidad, decimal valorConsulta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            INSERT INTO Profesional_Especialidad (id_profesional, id_especialidad, valor_consulta)
            VALUES (@prof, @esp, @valor)
        ");

                datos.setearParametros("@prof", idProfesional);
                datos.setearParametros("@esp", idEspecialidad);
                datos.setearParametros("@valor", valorConsulta);

                datos.ejecutarAccion();
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

        public void ModificarValorConsulta(int idProfesional, int idEspecialidad, decimal nuevoValor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            UPDATE Profesional_Especialidad
            SET valor_consulta = @valor
            WHERE id_profesional = @prof AND id_especialidad = @esp
        ");

                datos.setearParametros("@valor", nuevoValor);
                datos.setearParametros("@prof", idProfesional);
                datos.setearParametros("@esp", idEspecialidad);

                datos.ejecutarAccion();
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

        public void EliminarEspecialidadDeProfesional(int idProfesional, int idEspecialidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            DELETE FROM Profesional_Especialidad
            WHERE id_profesional = @prof AND id_especialidad = @esp
        ");

                datos.setearParametros("@prof", idProfesional);
                datos.setearParametros("@esp", idEspecialidad);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
