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
            EspecialidadNegocio especialidadNeg = new EspecialidadNegocio();
            ConsultorioNegocio consultorioNeg = new ConsultorioNegocio();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_horario, id_profesional, id_especialidad,
       id_consultorio, dia_semana, hora_inicio, hora_fin, activo
FROM HorarioAtencion
WHERE activo = 1
ORDER BY 
    CASE dia_semana
        WHEN 'Lunes' THEN 1
        WHEN 'Martes' THEN 2
        WHEN 'Miércoles' THEN 3
        WHEN 'Jueves' THEN 4
        WHEN 'Viernes' THEN 5
        WHEN 'Sábado' THEN 6
        WHEN 'Domingo' THEN 7
        ELSE 8
    END,
    hora_inicio;
");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    HorarioAtencion aux = new HorarioAtencion();

                    // ID
                    aux.IdHorario = (int)datos.Lector["id_horario"];

                    // Profesional
                    aux.Profesional = profesionalNeg.BuscarPorId((int)datos.Lector["id_profesional"]);

                    // Especialidad (OBJETO)
                    aux.Especialidad = especialidadNeg.BuscarPorId((int)datos.Lector["id_especialidad"]);

                    // Consultorio
                    aux.Consultorio = consultorioNeg.BuscarPorId((int)datos.Lector["id_consultorio"]);

                    // Día semana
                    if (!(datos.Lector["dia_semana"] is DBNull))
                        aux.DiaSemana = (string)datos.Lector["dia_semana"];

                    // Horas
                    if (!(datos.Lector["hora_inicio"] is DBNull))
                        aux.HoraInicio = (TimeSpan)datos.Lector["hora_inicio"];

                    if (!(datos.Lector["hora_fin"] is DBNull))
                        aux.HoraFin = (TimeSpan)datos.Lector["hora_fin"];

                    // Activo
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
            EspecialidadNegocio especialidadNeg = new EspecialidadNegocio();
            ConsultorioNegocio consultorioNeg = new ConsultorioNegocio();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_horario, id_profesional, id_especialidad,
                           id_consultorio, dia_semana, hora_inicio, hora_fin, activo
                    FROM HorarioAtencion
                    WHERE id_profesional = @id AND activo = 1");

                datos.setearParametros("@id", idProfesional);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    HorarioAtencion aux = new HorarioAtencion();

                    aux.IdHorario = (int)datos.Lector["id_horario"];
                    aux.Profesional = profesionalNeg.BuscarPorId((int)datos.Lector["id_profesional"]);
                    aux.Especialidad = especialidadNeg.BuscarPorId((int)datos.Lector["id_especialidad"]);
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



        public HorarioAtencion BuscarPorId(int idHorario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT h.id_horario,
                   h.id_profesional,
                   h.id_especialidad,
                   h.id_consultorio,
                   h.hora_inicio,
                   h.hora_fin,
                   h.dia_semana,
                   c.nombre AS consultorio_nombre,
                   c.direccion,
                   c.piso,
                   c.numero_sala
            FROM HorarioAtencion h
            INNER JOIN Consultorio c ON c.id_consultorio = h.id_consultorio
            WHERE h.id_horario = @id
        ");

                datos.setearParametros("@id", idHorario);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    HorarioAtencion h = new HorarioAtencion();

                    h.IdHorario = (int)datos.Lector["id_horario"];

                    // PROFESIONAL
                    h.Profesional = new ProfesionalNegocio().BuscarPorId((int)datos.Lector["id_profesional"]);

                    // ESPECIALIDAD
                    h.Especialidad = new EspecialidadNegocio().BuscarPorId((int)datos.Lector["id_especialidad"]);

                    // CONSULTORIO
                    h.Consultorio = new Consultorio
                    {
                        IdConsultorio = (int)datos.Lector["id_consultorio"],
                        Nombre = datos.Lector["consultorio_nombre"].ToString(),
                        Direccion = datos.Lector["direccion"].ToString(),
                        Piso = datos.Lector["piso"].ToString(),
                        NumeroSala = datos.Lector["numero_sala"].ToString()
                    };

                    // HORARIOS Y DÍA
                    h.HoraInicio = (TimeSpan)datos.Lector["hora_inicio"];
                    h.HoraFin = (TimeSpan)datos.Lector["hora_fin"];
                    h.DiaSemana = datos.Lector["dia_semana"].ToString();

                    return h;
                }

                return null;
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
                    INSERT INTO HorarioAtencion
                    (id_profesional, id_especialidad, id_consultorio,
                     dia_semana, hora_inicio, hora_fin, activo)
                    VALUES (@prof, @esp, @cons, @dia, @inicio, @fin, @activo)");

                datos.setearParametros("@prof", h.Profesional.IdProfesional);
                datos.setearParametros("@esp", h.Especialidad.IdEspecialidad);
                datos.setearParametros("@cons", h.Consultorio.IdConsultorio);
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
                        id_especialidad = @esp,
                        id_consultorio = @cons,
                        dia_semana = @dia,
                        hora_inicio = @inicio,
                        hora_fin = @fin,
                        activo = @activo
                    WHERE id_horario = @id");

                datos.setearParametros("@prof", h.Profesional.IdProfesional);
                datos.setearParametros("@esp", h.Especialidad.IdEspecialidad);
                datos.setearParametros("@cons", h.Consultorio.IdConsultorio);
                datos.setearParametros("@dia", h.DiaSemana);
                datos.setearParametros("@inicio", h.HoraInicio);
                datos.setearParametros("@fin", h.HoraFin);
                datos.setearParametros("@activo", h.Activo);
                datos.setearParametros("@id", h.IdHorario);

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

        public bool ExisteConflictoHorario(HorarioAtencion nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // -----------------------------
                // 1) CONFLICTO POR PROFESIONAL
                // -----------------------------
                datos.setearConsulta(@"
            SELECT 1
            FROM HorarioAtencion ha
            WHERE ha.id_profesional = @prof
              AND ha.dia_semana = @dia
              AND ha.activo = 1
              AND (@inicio < ha.hora_fin AND @fin > ha.hora_inicio)
        ");

                datos.setearParametros("@prof", nuevo.Profesional.IdProfesional);
                datos.setearParametros("@dia", nuevo.DiaSemana);
                datos.setearParametros("@inicio", nuevo.HoraInicio);
                datos.setearParametros("@fin", nuevo.HoraFin);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return true; // solapamiento profesional

                datos.cerrarConexion();
                datos = new AccesoDatos();


                // -----------------------------
                // 2) CONFLICTO POR CONSULTORIO
                // -----------------------------
                datos.setearConsulta(@"
            SELECT 1
            FROM HorarioAtencion ha
            WHERE ha.id_consultorio = @cons
              AND ha.dia_semana = @dia
              AND ha.activo = 1
              AND (@inicio < ha.hora_fin AND @fin > ha.hora_inicio)
        ");

                datos.setearParametros("@cons", nuevo.Consultorio.IdConsultorio);
                datos.setearParametros("@dia", nuevo.DiaSemana);
                datos.setearParametros("@inicio", nuevo.HoraInicio);
                datos.setearParametros("@fin", nuevo.HoraFin);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return true; // solapamiento consultorio

                return false;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<HorarioAtencion> ListarDisponibles(int idProfesional, DateTime fecha)
        {
            List<HorarioAtencion> lista = new List<HorarioAtencion>();
            AccesoDatos datos = new AccesoDatos();

            // Convertir el día de la semana de la fecha a un formato que coincida con tu base de datos.
            // Asumo que tu base de datos usa el nombre del día en español (ej: "Lunes", "Martes").
            // Si tu DB usa números (0=Dom, 1=Lun, etc.), deberás adaptar este mapeo.
            // ---------------------------------------------------------------------------------------
            string diaSemanaDB = ObtenerDiaSemanaEnEspañol(fecha.DayOfWeek);
            // ---------------------------------------------------------------------------------------

            ProfesionalNegocio profesionalNeg = new ProfesionalNegocio();
            EspecialidadNegocio especialidadNeg = new EspecialidadNegocio();
            ConsultorioNegocio consultorioNeg = new ConsultorioNegocio();

            try
            {
                // Consulta: Busca todos los HorariosAtencion activos para el Profesional y el Día de la Semana dado.
                // Utiliza LEFT JOIN con la tabla Turno para encontrar qué horarios NO tienen un turno confirmado/pendiente.
                datos.setearConsulta(@"
            SELECT H.id_horario, H.id_profesional, H.id_especialidad,
                   H.id_consultorio, H.dia_semana, H.hora_inicio, H.hora_fin, H.activo
            FROM HorarioAtencion H
            LEFT JOIN Turno T ON H.id_horario = T.id_horario 
                                 AND T.fecha_turno = @fecha 
                                 AND T.estado = 'pendiente' 
            WHERE H.activo = 1 
              AND H.id_profesional = @idProf
              AND H.dia_semana = @dia
              AND T.id_turno IS NULL -- CRUCIAL: Excluye los horarios que ya tienen un turno en esa fecha
        ");

                datos.setearParametros("@idProf", idProfesional);
                datos.setearParametros("@fecha", fecha.Date); // Solo necesitamos la fecha, sin la hora
                datos.setearParametros("@dia", diaSemanaDB);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    // Reutilizamos la lógica de mapeo que ya tienes
                    HorarioAtencion aux = new HorarioAtencion();

                    aux.IdHorario = (int)datos.Lector["id_horario"];
                    aux.Profesional = profesionalNeg.BuscarPorId((int)datos.Lector["id_profesional"]);
                    aux.Especialidad = especialidadNeg.BuscarPorId((int)datos.Lector["id_especialidad"]);
                    aux.Consultorio = consultorioNeg.BuscarPorId((int)datos.Lector["id_consultorio"]);

                    aux.DiaSemana = (string)datos.Lector["dia_semana"];
                    aux.HoraInicio = (TimeSpan)datos.Lector["hora_inicio"];
                    aux.HoraFin = (TimeSpan)datos.Lector["hora_fin"];
                    aux.Activo = (bool)datos.Lector["activo"];

                    // Agregamos un campo de Display que usaremos en el DropDownList
                    // Esto asume que el turno dura un bloque (ej: 30 minutos)
                    aux.HorarioDisplay = $"{aux.HoraInicio:hh\\:mm} a {aux.HoraFin:hh\\:mm}";

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private string ObtenerDiaSemanaEnEspañol(DayOfWeek dia)
        {
            switch (dia)
            {
                case DayOfWeek.Monday: return "Lunes";
                case DayOfWeek.Tuesday: return "Martes";
                case DayOfWeek.Wednesday: return "Miércoles";
                case DayOfWeek.Thursday: return "Jueves";
                case DayOfWeek.Friday: return "Viernes";
                case DayOfWeek.Saturday: return "Sábado";
                case DayOfWeek.Sunday: return "Domingo";
                default: return "";
            }
        }
    
        
    }

}
