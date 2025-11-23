using System;
using System.Collections.Generic;
using modelo;

namespace negocio
{
    public class PacienteNegocio
    {
        public List<Paciente> Listar()
        {
            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();
            ObraSocialNegocio obraNegocio = new ObraSocialNegocio();

            try
            {
                datos.setearConsulta(@"
            SELECT id_paciente, nombre, apellido, dni, fecha_nacimiento,
                   telefono, email, direccion, activo,
                   id_obra_social, nro_afiliado
            FROM Paciente
            WHERE activo = 1");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();

                    aux.IdPaciente = (int)datos.Lector["id_paciente"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["apellido"] is DBNull))
                        aux.Apellido = (string)datos.Lector["apellido"];

                    if (!(datos.Lector["dni"] is DBNull))
                        aux.Dni = (string)datos.Lector["dni"];

                    if (!(datos.Lector["fecha_nacimiento"] is DBNull))
                        aux.FechaNacimiento = (DateTime)datos.Lector["fecha_nacimiento"];

                    if (!(datos.Lector["telefono"] is DBNull))
                        aux.Telefono = (string)datos.Lector["telefono"];

                    if (!(datos.Lector["email"] is DBNull))
                        aux.Email = (string)datos.Lector["email"];

                    if (!(datos.Lector["direccion"] is DBNull))
                        aux.Direccion = (string)datos.Lector["direccion"];

                    if (!(datos.Lector["activo"] is DBNull))
                        aux.Activo = (bool)datos.Lector["activo"];

                    // SI TIENE obra social > la busco por negocio
                    if (!(datos.Lector["id_obra_social"] is DBNull))
                    {
                        int idObra = (int)datos.Lector["id_obra_social"];
                        aux.ObraSocial = obraNegocio.BuscarPorId(idObra);
                    }

                    if (!(datos.Lector["nro_afiliado"] is DBNull))
                        aux.NroAfiliado = (string)datos.Lector["nro_afiliado"];

                    lista.Add(aux);
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


        public Paciente BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            ObraSocialNegocio obraNegocio = new ObraSocialNegocio();

            try
            {
                datos.setearConsulta(@"
            SELECT id_paciente, nombre, apellido, dni, fecha_nacimiento,
                   telefono, email, direccion, activo,
                   id_obra_social, nro_afiliado
            FROM Paciente
            WHERE id_paciente = @id AND activo = 1");

                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();

                    aux.IdPaciente = (int)datos.Lector["id_paciente"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["apellido"] is DBNull))
                        aux.Apellido = (string)datos.Lector["apellido"];

                    if (!(datos.Lector["dni"] is DBNull))
                        aux.Dni = (string)datos.Lector["dni"];

                    if (!(datos.Lector["fecha_nacimiento"] is DBNull))
                        aux.FechaNacimiento = (DateTime)datos.Lector["fecha_nacimiento"];

                    if (!(datos.Lector["telefono"] is DBNull))
                        aux.Telefono = (string)datos.Lector["telefono"];

                    if (!(datos.Lector["email"] is DBNull))
                        aux.Email = (string)datos.Lector["email"];

                    if (!(datos.Lector["direccion"] is DBNull))
                        aux.Direccion = (string)datos.Lector["direccion"];

                    if (!(datos.Lector["activo"] is DBNull))
                        aux.Activo = (bool)datos.Lector["activo"];

                    // Obra social desde negocio
                    if (!(datos.Lector["id_obra_social"] is DBNull))
                    {
                        int idObra = (int)datos.Lector["id_obra_social"];
                        aux.ObraSocial = obraNegocio.BuscarPorId(idObra);
                    }

                    if (!(datos.Lector["nro_afiliado"] is DBNull))
                        aux.NroAfiliado = (string)datos.Lector["nro_afiliado"];

                    return aux;
                }

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(Paciente pac)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Paciente
                    (nombre, apellido, dni, fecha_nacimiento, telefono, email, direccion,
                     id_obra_social, nro_afiliado, activo)
                    VALUES
                    (@nom, @ape, @dni, @fec, @tel, @mail, @dir, @obra, @nro, 1)");

                datos.setearParametros("@nom", pac.Nombre);
                datos.setearParametros("@ape", pac.Apellido);
                datos.setearParametros("@dni", pac.Dni);
                datos.setearParametros("@fec", pac.FechaNacimiento);
                datos.setearParametros("@tel", pac.Telefono);
                datos.setearParametros("@mail", pac.Email);
                datos.setearParametros("@dir", pac.Direccion);

                if (pac.ObraSocial != null)
                    datos.setearParametros("@obra", pac.ObraSocial.IdObraSocial);
                else
                    datos.setearParametros("@obra", null);

                datos.setearParametros("@nro", pac.NroAfiliado);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Modificar(Paciente pac)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE Paciente
                    SET nombre = @nom,
                        apellido = @ape,
                        dni = @dni,
                        fecha_nacimiento = @fec,
                        telefono = @tel,
                        email = @mail,
                        direccion = @dir,
                        id_obra_social = @obra,
                        nro_afiliado = @nro
                    WHERE id_paciente = @id");

                datos.setearParametros("@nom", pac.Nombre);
                datos.setearParametros("@ape", pac.Apellido);
                datos.setearParametros("@dni", pac.Dni);
                datos.setearParametros("@fec", pac.FechaNacimiento);
                datos.setearParametros("@tel", pac.Telefono);
                datos.setearParametros("@mail", pac.Email);
                datos.setearParametros("@dir", pac.Direccion);

                if (pac.ObraSocial != null)
                    datos.setearParametros("@obra", pac.ObraSocial.IdObraSocial);
                else
                    datos.setearParametros("@obra", null);

                datos.setearParametros("@nro", pac.NroAfiliado);
                datos.setearParametros("@id", pac.IdPaciente);

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
                datos.setearConsulta("UPDATE Paciente SET activo = 0 WHERE id_paciente = @id");
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
                datos.setearConsulta("UPDATE Paciente SET activo = 1 WHERE id_paciente = @id");
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
