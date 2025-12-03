using System;
using System.Collections.Generic;
using modelo;

namespace negocio
{
    public class ProfesionalNegocio
    {
        public List<Profesional> Listar()
        {
            List<Profesional> lista = new List<Profesional>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_profesional, nombre, apellido, dni,
                           matricula, telefono, email, activo
                    FROM Profesional
                    WHERE activo = 1");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Profesional aux = new Profesional();

                    aux.IdProfesional = (int)datos.Lector["id_profesional"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["apellido"] is DBNull))
                        aux.Apellido = (string)datos.Lector["apellido"];

                    if (!(datos.Lector["dni"] is DBNull))
                        aux.Dni = (string)datos.Lector["dni"];

                    if (!(datos.Lector["matricula"] is DBNull))
                        aux.Matricula = (string)datos.Lector["matricula"];

                    if (!(datos.Lector["telefono"] is DBNull))
                        aux.Telefono = (string)datos.Lector["telefono"];

                    if (!(datos.Lector["email"] is DBNull))
                        aux.Email = (string)datos.Lector["email"];

                    if (!(datos.Lector["activo"] is DBNull))
                        aux.Activo = (bool)datos.Lector["activo"];
                    
                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public Profesional BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_profesional, nombre, apellido, dni,
                           matricula, telefono, email, activo
                    FROM Profesional
                    WHERE id_profesional = @id AND activo = 1");

                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Profesional aux = new Profesional();

                    aux.IdProfesional = (int)datos.Lector["id_profesional"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["apellido"] is DBNull))
                        aux.Apellido = (string)datos.Lector["apellido"];

                    if (!(datos.Lector["dni"] is DBNull))
                        aux.Dni = (string)datos.Lector["dni"];

                    if (!(datos.Lector["matricula"] is DBNull))
                        aux.Matricula = (string)datos.Lector["matricula"];

                    if (!(datos.Lector["telefono"] is DBNull))
                        aux.Telefono = (string)datos.Lector["telefono"];

                    if (!(datos.Lector["email"] is DBNull))
                        aux.Email = (string)datos.Lector["email"];

                    if (!(datos.Lector["activo"] is DBNull))
                        aux.Activo = (bool)datos.Lector["activo"];

                    return aux;
                }

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Agregar(Profesional prof)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Profesional
                    (nombre, apellido, dni, matricula, telefono, email, activo)
                    VALUES
                    (@nom, @ape, @dni, @mat, @tel, @mail, 1)");

                datos.setearParametros("@nom", prof.Nombre);
                datos.setearParametros("@ape", prof.Apellido);
                datos.setearParametros("@dni", prof.Dni);
                datos.setearParametros("@mat", prof.Matricula);
                datos.setearParametros("@tel", prof.Telefono);
                datos.setearParametros("@mail", prof.Email);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Modificar(Profesional prof)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE Profesional
                    SET nombre = @nom,
                        apellido = @ape,
                        dni = @dni,
                        matricula = @mat,
                        telefono = @tel,
                        email = @mail
                    WHERE id_profesional = @id");

                datos.setearParametros("@nom", prof.Nombre);
                datos.setearParametros("@ape", prof.Apellido);
                datos.setearParametros("@dni", prof.Dni);
                datos.setearParametros("@mat", prof.Matricula);
                datos.setearParametros("@tel", prof.Telefono);
                datos.setearParametros("@mail", prof.Email);
                datos.setearParametros("@id", prof.IdProfesional);

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
                datos.setearConsulta("UPDATE Profesional SET activo = 0 WHERE id_profesional = @id");
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
                datos.setearConsulta("UPDATE Profesional SET activo = 1 WHERE id_profesional = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Profesional> Buscar(string criterio, string filtro)
        {
            List<Profesional> lista = new List<Profesional>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta =
                    "SELECT id_profesional, nombre, apellido, dni, telefono, email, matricula " +
                    "FROM Profesional " +
                    "WHERE activo = 1 AND ";

                switch (criterio)
                {
                    case "Nombre":
                        consulta += "nombre LIKE @filtro";
                        break;

                    case "Apellido":
                        consulta += "apellido LIKE @filtro";
                        break;

                    case "Dni":
                        consulta += "dni LIKE @filtro";
                        break;

                    case "Telefono":
                        consulta += "telefono LIKE @filtro";
                        break;

                    case "Email":
                        consulta += "email LIKE @filtro";
                        break;

                    case "Matricula":
                        consulta += "matricula LIKE @filtro";
                        break;

                    case "Especialidad":
                        consulta =
                            "SELECT P.id_profesional, P.nombre, P.apellido, P.dni, P.telefono, P.email, P.matricula " +
                            "FROM Profesional P " +
                            "INNER JOIN Profesional_Especialidad PE ON P.id_profesional = PE.id_profesional " +
                            "INNER JOIN Especialidad E ON E.id_especialidad = PE.id_especialidad " +
                            "WHERE P.activo = 1 AND E.nombre LIKE @filtro";
                        break;

                    case "ObraSocial":
                        consulta =
                            "SELECT P.id_profesional, P.nombre, P.apellido, P.dni, P.telefono, P.email, P.matricula " +
                            "FROM Profesional P " +
                            "INNER JOIN Profesional_ObraSocial PO ON P.id_profesional = PO.id_profesional " +
                            "INNER JOIN ObraSocial O ON O.id_obra_social = PO.id_obra_social " +
                            "WHERE P.activo = 1 AND O.nombre LIKE @filtro";
                        break;
                }

                datos.setearConsulta(consulta);
                datos.setearParametros("@filtro", "%" + filtro + "%");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Profesional aux = new Profesional();

                    if (!(datos.Lector["id_profesional"] is DBNull))
                        aux.IdProfesional = (int)datos.Lector["id_profesional"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["apellido"] is DBNull))
                        aux.Apellido = (string)datos.Lector["apellido"];

                    if (!(datos.Lector["dni"] is DBNull))
                        aux.Dni = (string)datos.Lector["dni"];

                    if (!(datos.Lector["telefono"] is DBNull))
                        aux.Telefono = (string)datos.Lector["telefono"];

                    if (!(datos.Lector["email"] is DBNull))
                        aux.Email = (string)datos.Lector["email"];

                    if (!(datos.Lector["matricula"] is DBNull))
                        aux.Matricula = (string)datos.Lector["matricula"];

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public bool ProfesionalDictaEspecialidad(int idProfesional, int idEspecialidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT 1
            FROM Profesional_Especialidad
            WHERE id_profesional = @prof
              AND id_especialidad = @esp
              AND activo = 1
        ");

                datos.setearParametros("@prof", idProfesional);
                datos.setearParametros("@esp", idEspecialidad);

                datos.ejecutarLectura();

                return datos.Lector.Read();
               
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Profesional> ListarPorEspecialidad(int idEsp)
        {
            List<Profesional> lista = new List<Profesional>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT P.id_profesional, P.nombre, P.apellido, P.dni, P.telefono, P.email, P.matricula 
            FROM Profesional P 
            INNER JOIN Profesional_Especialidad PE ON P.id_profesional = PE.id_profesional 
            WHERE P.activo = 1 AND PE.id_especialidad = @idEsp
        ");

                datos.setearParametros("@idEsp", idEsp);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Profesional aux = new Profesional();
                    // --- INICIO Mapeo manual (puedes reutilizar tu lógica de mapeo si es más concisa) ---
                    if (!(datos.Lector["id_profesional"] is DBNull))
                        aux.IdProfesional = (int)datos.Lector["id_profesional"];
                    if (!(datos.Lector["nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["nombre"];
                    if (!(datos.Lector["apellido"] is DBNull))
                        aux.Apellido = (string)datos.Lector["apellido"];
                    if (!(datos.Lector["dni"] is DBNull))
                        aux.Dni = (string)datos.Lector["dni"];
                    if (!(datos.Lector["telefono"] is DBNull))
                        aux.Telefono = (string)datos.Lector["telefono"];
                    if (!(datos.Lector["email"] is DBNull))
                        aux.Email = (string)datos.Lector["email"];
                    if (!(datos.Lector["matricula"] is DBNull))
                        aux.Matricula = (string)datos.Lector["matricula"];

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
