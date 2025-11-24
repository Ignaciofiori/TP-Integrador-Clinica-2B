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
                datos.setearConsulta(@"SELECT 
                                        P.id_paciente,
                                        P.nombre,
                                        P.apellido,
                                        P.dni,
                                        P.fecha_nacimiento,
                                        P.telefono,
                                        P.email,
                                        P.direccion,
                                        P.activo,
                                        P.id_obra_social,
                                        P.nro_afiliado,

                                        O.nombre AS obra_nombre,
                                        O.activo AS obra_activo

                                    FROM Paciente P
                                    LEFT JOIN ObraSocial O ON P.id_obra_social = O.id_obra_social
                                    WHERE P.activo = 1");

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

                    // -------------- OBRA SOCIAL SEGURA --------------
                    if (!(datos.Lector["id_obra_social"] is DBNull))
                    {
                        // Si existe el ID, creo el objeto
                        aux.ObraSocial = new ObraSocial();

                        // Nombre de la obra social (puede venir null si fue borrada)
                        if (!(datos.Lector["obra_nombre"] is DBNull))
                            aux.ObraSocial.Nombre = datos.Lector["obra_nombre"].ToString();
                        else
                            aux.ObraSocial.Nombre = null;

                        // Estado de la obra social (activo/inactivo)
                        if (!(datos.Lector["obra_activo"] is DBNull))
                            aux.ObraSocial.Activo = (bool)datos.Lector["obra_activo"];
                        else
                            aux.ObraSocial.Activo = false;
                    }
                    else
                    {
                        // Si no tiene OS asignada
                        aux.ObraSocial = null;
                    }
                    // ------------------------------------------------

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
                    (@nom, @ape, @dni, @fec, @tel, @mail, @dir, @obra, 1, 1)");

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
                    datos.setearParametros("@obra", DBNull.Value);
                

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
                        nro_afiliado = 1
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

        public List<Paciente> Buscar(string criterio, string valor)
        {

            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = @"
SELECT 
    P.id_paciente,
    P.nombre,
    P.apellido,
    P.dni,
    P.fecha_nacimiento,
    P.telefono,
    P.email,
    P.direccion,
    P.activo,
    P.id_obra_social,
    P.nro_afiliado,
    O.nombre AS obra_nombre,
    O.activo AS obra_activo
FROM Paciente P
LEFT JOIN ObraSocial O ON P.id_obra_social = O.id_obra_social
WHERE P.activo = 1";

                // -----------------------------------------
                // AGREGAR FILTRO SEGÚN CRITERIO ELEGIDO
                // -----------------------------------------

                switch (criterio)
                {
                    case "Nombre":
                        query += " AND P.nombre LIKE @valor";
                        break;

                    case "Apellido":
                        query += " AND P.apellido LIKE @valor";
                        break;

                    case "Dni":
                        query += " AND P.dni LIKE @valor";
                        break;

                    case "ObraSocial":

                        // Caso especial: buscar pacientes sin obra social
                        if (valor.ToLower() == "sin obra social" || valor.ToLower() == "sin" || valor.ToLower() == "sin obra")
                        {
                            query += " AND (P.id_obra_social IS NULL OR O.activo = 0)";
                        }

                        else
                        {
                            query += " AND O.nombre LIKE @valor";
                        }
                        break;

                    // Edad menor a X → FechaNacimiento > HOY - X años
                    case "Menor a":
                        query += " AND P.fecha_nacimiento > DATEADD(YEAR, -@valorEdad, GETDATE())";
                        break;

                    // Edad mayor a X → FechaNacimiento <= HOY - X años
                    case "Mayor a":
                        query += " AND P.fecha_nacimiento <= DATEADD(YEAR, -@valorEdad, GETDATE())";
                        break;
                }

                datos.setearConsulta(query);

                // Parámetros según tipo
                if (criterio == "Menor a" || criterio == "Mayor a")
                {
                    // Convertir valor a int (edad)
                    int edad;
                    if (!int.TryParse(valor, out edad))
                        throw new Exception("La edad debe ser un número válido.");

                    datos.setearParametros("@valorEdad", edad);
                }
                else
                {
                    // Búsqueda textual
                    datos.setearParametros("@valor", "%" + valor + "%");
                }

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

                    // ----------- OBRA SOCIAL SEGURA -----------
                    if (!(datos.Lector["id_obra_social"] is DBNull))
                    {
                        aux.ObraSocial = new ObraSocial();

                        if (!(datos.Lector["obra_nombre"] is DBNull))
                            aux.ObraSocial.Nombre = datos.Lector["obra_nombre"].ToString();
                        else
                            aux.ObraSocial.Nombre = null;

                        if (!(datos.Lector["obra_activo"] is DBNull))
                            aux.ObraSocial.Activo = (bool)datos.Lector["obra_activo"];
                        else
                            aux.ObraSocial.Activo = false;
                    }
                    else
                    {
                        aux.ObraSocial = null;
                    }
                    // --------------------------------------------

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

    
}
}
