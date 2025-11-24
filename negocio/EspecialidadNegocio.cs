using System;
using System.Collections.Generic;
using modelo;

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
                datos.setearConsulta(@"
                    SELECT id_especialidad, nombre, descripcion, activo
                    FROM Especialidad
                    WHERE activo = 1");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad esp = new Especialidad();

                    esp.IdEspecialidad = (int)datos.Lector["id_especialidad"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        esp.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["descripcion"] is DBNull))
                        esp.Descripcion = (string)datos.Lector["descripcion"];

                    if (!(datos.Lector["activo"] is DBNull))
                        esp.Activo = (bool)datos.Lector["activo"];

                    lista.Add(esp);
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
                    SELECT id_especialidad, nombre, descripcion, activo
                    FROM Especialidad
                    WHERE id_especialidad = @id AND activo = 1");

                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux = new Especialidad();

                    aux.IdEspecialidad = (int)datos.Lector["id_especialidad"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["descripcion"];

                    if (!(datos.Lector["activo"] is DBNull))
                        aux.Activo = (bool)datos.Lector["activo"];
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
                datos.setearConsulta(@"
                    INSERT INTO Especialidad (nombre, descripcion, activo)
                    VALUES (@nom, @desc, 1)");

                datos.setearParametros("@nom", esp.Nombre);
                datos.setearParametros("@desc", esp.Descripcion);

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
                datos.setearConsulta(@"
                    UPDATE Especialidad
                    SET nombre = @nom,
                        descripcion = @desc
                    WHERE id_especialidad = @id");

                datos.setearParametros("@nom", esp.Nombre);
                datos.setearParametros("@desc", esp.Descripcion);
                datos.setearParametros("@id", esp.IdEspecialidad);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Especialidad> ListarPorProfesional(int idProfesional)
        {
            List<Especialidad> lista = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT 
                E.id_especialidad, 
                E.nombre, 
                E.descripcion,
                PE.valor_consulta
            FROM Profesional_Especialidad PE
            INNER JOIN Especialidad E ON PE.id_especialidad = E.id_especialidad
            WHERE PE.id_profesional = @id AND PE.activo = 1
            AND E.activo = 1");

                datos.setearParametros("@id", idProfesional);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad esp = new Especialidad();

                    esp.IdEspecialidad = (int)datos.Lector["id_especialidad"];
                    esp.Nombre = datos.Lector["nombre"].ToString();
                    esp.Descripcion = datos.Lector["descripcion"].ToString();
                    esp.ValorConsulta = decimal.Parse(datos.Lector["valor_consulta"].ToString());

                    lista.Add(esp);
                }

                return lista;
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
                // Baja logica
                datos.setearConsulta("UPDATE Especialidad SET activo = 0 WHERE id_especialidad = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public void Reactivar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Especialidad SET activo = 1 WHERE id_especialidad = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void AgregarRelacion(int idProfesional, int idEspecialidad, decimal valorConsulta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            INSERT INTO Profesional_Especialidad
            (id_profesional, id_especialidad, valor_consulta, activo)
            VALUES (@prof, @esp, @valor, 1)");

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


        public void ModificarValorConsulta(int idProfesional, int idEspecialidad, decimal nuevoValor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            UPDATE Profesional_Especialidad
            SET valor_consulta = @valor
            WHERE id_profesional = @prof AND id_especialidad = @esp AND activo = 1");

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


        public void BajaFisicaRelacion(int idProfesional, int idEspecialidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            DELETE FROM Profesional_Especialidad
            WHERE id_profesional = @prof AND id_especialidad = @esp");

                datos.setearParametros("@prof", idProfesional);
                datos.setearParametros("@esp", idEspecialidad);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void ReactivarRelacion(int idProfesional, int idEspecialidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            UPDATE Profesional_Especialidad
            SET activo = 1
            WHERE id_profesional = @prof AND id_especialidad = @esp");

                datos.setearParametros("@prof", idProfesional);
                datos.setearParametros("@esp", idEspecialidad);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public bool ExisteRelacionActiva(int idProfesional, int idEspecialidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT 1 
            FROM Profesional_Especialidad
            WHERE id_profesional = @prof AND id_especialidad = @esp AND activo = 1");

                datos.setearParametros("@prof", idProfesional);
                datos.setearParametros("@esp", idEspecialidad);
                datos.ejecutarLectura();

                return datos.Lector.Read(); // Si hay fila → existe
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
