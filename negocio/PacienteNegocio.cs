using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            SELECT p.id_persona, p.nombre, p.apellido, p.dni, p.fecha_nacimiento,
                   p.telefono, p.email, p.direccion, p.activo,
                   pa.id_obra_social, pa.nro_afiliado
            FROM Persona p
            INNER JOIN Paciente pa ON p.id_persona = pa.id_paciente");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();

                    aux.Id = (int)datos.Lector["id_persona"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["apellido"] is DBNull))
                        aux.Apellido = (string)datos.Lector["apellido"];

                    if (!(datos.Lector["dni"] is DBNull))
                        aux.DNI = (string)datos.Lector["dni"];

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

                    // Obra Social
                    if (!(datos.Lector["id_obra_social"] is DBNull))
                        aux.ObraSocial = obraNegocio.BuscarPorId((int)datos.Lector["id_obra_social"]);

                    // Número afiliado
                    if (!(datos.Lector["nro_afiliado"] is DBNull))
                        aux.NumeroAfiliado = (string)datos.Lector["nro_afiliado"];

                    // Turnos todavia no
                    aux.Turnos = null;

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
            SELECT p.id_persona, p.nombre, p.apellido, p.dni, p.fecha_nacimiento,
                   p.telefono, p.email, p.direccion, p.activo,
                   pa.id_obra_social, pa.nro_afiliado
            FROM Persona p
            INNER JOIN Paciente pa ON p.id_persona = pa.id_paciente
            WHERE p.id_persona = @id");

                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();

                    aux.Id = (int)datos.Lector["id_persona"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["nombre"];

                    if (!(datos.Lector["apellido"] is DBNull))
                        aux.Apellido = (string)datos.Lector["apellido"];

                    if (!(datos.Lector["dni"] is DBNull))
                        aux.DNI = (string)datos.Lector["dni"];

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

                    // Obra Social
                    if (!(datos.Lector["id_obra_social"] is DBNull))
                        aux.ObraSocial = obraNegocio.BuscarPorId((int)datos.Lector["id_obra_social"]);

                    // Número afiliado
                    if (!(datos.Lector["nro_afiliado"] is DBNull))
                        aux.NumeroAfiliado = (string)datos.Lector["nro_afiliado"];

                    aux.Turnos = null; // Todavía no cargamos turnos

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
            PersonaNegocio personaNegocio = new PersonaNegocio();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                int idPersona = personaNegocio.Agregar(pac);

                datos.setearConsulta("INSERT INTO Paciente (id_paciente, id_obra_social, nro_afiliado) VALUES (@id, @obra, @nro)");

                datos.setearParametros("@id", idPersona);
                datos.setearParametros("@obra", pac.ObraSocial.Id);
                datos.setearParametros("@nro", pac.NumeroAfiliado);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Paciente pac)
        {
            PersonaNegocio personaNegocio = new PersonaNegocio();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                personaNegocio.Modificar(pac);

                datos.setearConsulta("UPDATE Paciente SET id_obra_social = @obra, nro_afiliado = @nro WHERE id_paciente = @id");

                datos.setearParametros("@obra", pac.ObraSocial.Id);
                datos.setearParametros("@nro", pac.NumeroAfiliado);
                datos.setearParametros("@id", pac.Id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            // Baja lógica se desactiva Persona 
            PersonaNegocio personaNegocio = new PersonaNegocio();
            personaNegocio.Eliminar(id);
        }
    }
}
