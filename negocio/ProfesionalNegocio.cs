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
    }
}
