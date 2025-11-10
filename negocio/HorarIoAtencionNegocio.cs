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
    public class HorarioAtencionNegocio
    {
        public List<HorarioAtencion> Listar()
        {
            List<HorarioAtencion> lista = new List<HorarioAtencion>();
            AccesoDatos datos = new AccesoDatos();

            ProfesionalNegocio profesionalNeg = new ProfesionalNegocio();
            ConsultorioNegocio consultorioNeg = new ConsultorioNegocio();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_horario, id_profesional, id_consultorio, dia_semana, hora_inicio, hora_fin, activo
                    FROM HorarioAtencion
                    WHERE activo = 1");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    HorarioAtencion aux = new HorarioAtencion();

                    aux.Id = (int)datos.Lector["id_horario"];
                    aux.Profesional = profesionalNeg.BuscarPorId((int)datos.Lector["id_profesional"]);
                    aux.Consultorio = consultorioNeg.BuscarPorId((int)datos.Lector["id_consultorio"]);

                    if (!(datos.Lector["dia_semana"] is DBNull))
                        aux.DiaSemana = (string)datos.Lector["dia_semana"];

                    if (!(datos.Lector["hora_inicio"] is DBNull))
                        aux.HoraInicio = (TimeSpan)datos.Lector["hora_inicio"];

                    if (!(datos.Lector["hora_fin"] is DBNull))
                        aux.HoraFin = (TimeSpan)datos.Lector["hora_fin"];

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

        public List<HorarioAtencion> ListarPorProfesional(int idProfesional)
        {
            List<HorarioAtencion> lista = new List<HorarioAtencion>();
            AccesoDatos datos = new AccesoDatos();

            ProfesionalNegocio profesionalNeg = new ProfesionalNegocio();
            ConsultorioNegocio consultorioNeg = new ConsultorioNegocio();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_horario, id_profesional, id_consultorio, dia_semana, hora_inicio, hora_fin, activo
                    FROM HorarioAtencion
                    WHERE id_profesional = @id AND activo = 1");

                datos.setearParametros("@id", idProfesional);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    HorarioAtencion aux = new HorarioAtencion();

                    aux.Id = (int)datos.Lector["id_horario"];
                    aux.Profesional = profesionalNeg.BuscarPorId((int)datos.Lector["id_profesional"]);
                    aux.Consultorio = consultorioNeg.BuscarPorId((int)datos.Lector["id_consultorio"]);
                    aux.DiaSemana = (string)datos.Lector["dia_semana"];
                    aux.HoraInicio = (TimeSpan)datos.Lector["hora_inicio"];
                    aux.HoraFin = (TimeSpan)datos.Lector["hora_fin"];
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

        public HorarioAtencion BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            ProfesionalNegocio profesionalNeg = new ProfesionalNegocio();
            ConsultorioNegocio consultorioNeg = new ConsultorioNegocio();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_horario, id_profesional, id_consultorio,
                           dia_semana, hora_inicio, hora_fin, activo
                    FROM HorarioAtencion
                    WHERE id_horario = @id");

                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                HorarioAtencion aux = null;

                if (datos.Lector.Read())
                {
                    aux = new HorarioAtencion();

                    // PK
                    if (!(datos.Lector["id_horario"] is DBNull))
                        aux.Id = (int)datos.Lector["id_horario"];

                    // Profesional (objeto)
                    if (!(datos.Lector["id_profesional"] is DBNull))
                        aux.Profesional = profesionalNeg.BuscarPorId((int)datos.Lector["id_profesional"]);

                    // Consultorio (objeto)
                    if (!(datos.Lector["id_consultorio"] is DBNull))
                        aux.Consultorio = consultorioNeg.BuscarPorId((int)datos.Lector["id_consultorio"]);

                    // Día semana
                    if (!(datos.Lector["dia_semana"] is DBNull))
                        aux.DiaSemana = (string)datos.Lector["dia_semana"];

                    // Horas (TimeSpan)
                    if (!(datos.Lector["hora_inicio"] is DBNull))
                        aux.HoraInicio = (TimeSpan)datos.Lector["hora_inicio"];

                    if (!(datos.Lector["hora_fin"] is DBNull))
                        aux.HoraFin = (TimeSpan)datos.Lector["hora_fin"];

                    // Activo (si tu modelo lo tiene)
                    // Agregá `public bool Activo { get; set; }` en HorarioAtencion
                    if (!(datos.Lector["activo"] is DBNull))
                    {
                        // Si tu propiedad existe:
                        // aux.Activo = (bool)datos.Lector["activo"];
                        // Si todavía no la agregaste, podés ignorar esta línea temporalmente.
                    }
                }

                return aux;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(HorarioAtencion h)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    INSERT INTO HorarioAtencion (id_profesional, id_consultorio, dia_semana, hora_inicio, hora_fin, activo)
                    VALUES (@prof, @cons, @dia, @inicio, @fin, @activo)");

                datos.setearParametros("@prof", h.Profesional.Id);
                datos.setearParametros("@cons", h.Consultorio.Id);
                datos.setearParametros("@dia", h.DiaSemana);
                datos.setearParametros("@inicio", h.HoraInicio);
                datos.setearParametros("@fin", h.HoraFin);
                datos.setearParametros("@activo", h.Activo);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(HorarioAtencion h)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE HorarioAtencion
                    SET id_profesional = @prof,
                        id_consultorio = @cons,
                        dia_semana = @dia,
                        hora_inicio = @inicio,
                        hora_fin = @fin,
                        activo = @activo
                    WHERE id_horario = @id");

                datos.setearParametros("@prof", h.Profesional.Id);
                datos.setearParametros("@cons", h.Consultorio.Id);
                datos.setearParametros("@dia", h.DiaSemana);
                datos.setearParametros("@inicio", h.HoraInicio);
                datos.setearParametros("@fin", h.HoraFin);
                datos.setearParametros("@activo", h.Activo);
                datos.setearParametros("@id", h.Id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Desactivar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE HorarioAtencion SET activo = 0 WHERE id_horario = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Activar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE HorarioAtencion SET activo = 1 WHERE id_horario = @id");
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
