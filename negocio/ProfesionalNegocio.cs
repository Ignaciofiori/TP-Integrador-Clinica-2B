using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using modelo;


namespace negocio
{
    public class ProfesionalNegocio
    {
        public List<Profesional> Listar()
        {
            List<Profesional> lista = new List<Profesional>();
            AccesoDatos datos = new AccesoDatos();

            // NEGOCIOS RELACIONADOS
            EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
            ObraSocialNegocio obraSocialNegocio = new ObraSocialNegocio();

            try
            {
                datos.setearConsulta(@"
                    SELECT p.id_persona, p.nombre, p.apellido, p.dni, p.fecha_nacimiento,
                           p.telefono, p.email, p.direccion, p.activo,
                           pr.matricula
                    FROM Persona p
                    INNER JOIN Profesional pr ON p.id_persona = pr.id_profesional
                ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Profesional aux = new Profesional();

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

                    if (!(datos.Lector["matricula"] is DBNull))
                        aux.Matricula = (string)datos.Lector["matricula"];

                  
                    aux.Especialidades = especialidadNegocio.ListarEspecialidadPorProfesional(aux.Id);

                
                    aux.ObrasSociales = obraSocialNegocio.ListarPorProfesional(aux.Id);

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
            SELECT p.id_persona, p.nombre, p.apellido, p.dni, p.fecha_nacimiento,
                   p.telefono, p.email, p.direccion, p.activo,
                   pr.matricula
            FROM Persona p
            INNER JOIN Profesional pr ON p.id_persona = pr.id_profesional
            WHERE p.id_persona = @id");

                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Profesional aux = new Profesional();

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

                    if (!(datos.Lector["matricula"] is DBNull))
                        aux.Matricula = (string)datos.Lector["matricula"];

                    aux.Especialidades = null; // Se cargará luego desde Profesional_EspecialidadNegocio

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
            PersonaNegocio personaNegocio = new PersonaNegocio();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // 1) Insert Persona y obtener ID
                int idPersona = personaNegocio.Agregar(prof);

                // 2) Insert Profesional con ese ID
                datos.setearConsulta("INSERT INTO Profesional (id_profesional, matricula) VALUES (@id, @matricula)");

                datos.setearParametros("@id", idPersona);
                datos.setearParametros("@matricula", prof.Matricula);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Modificar(Profesional prof)
        {
            PersonaNegocio personaNegocio = new PersonaNegocio();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // 1) Modificar Persona
                personaNegocio.Modificar(prof);

                // 2) Modificar Profesional
                datos.setearConsulta("UPDATE Profesional SET matricula = @matricula WHERE id_profesional = @id");

                datos.setearParametros("@matricula", prof.Matricula);
                datos.setearParametros("@id", prof.Id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Eliminar(int id)
        {
            // Igual que Paciente: baja lógica en Persona
            PersonaNegocio personaNegocio = new PersonaNegocio();
            personaNegocio.Eliminar(id);
        }
    }
}
