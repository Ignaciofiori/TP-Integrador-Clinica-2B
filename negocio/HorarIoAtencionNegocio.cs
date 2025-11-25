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
                    WHERE activo = 1");

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



        public HorarioAtencion BuscarPorId(int id)
        {
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
                    WHERE id_horario = @id");

                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                HorarioAtencion aux = null;

                if (datos.Lector.Read())
                {
                    aux = new HorarioAtencion();

                    aux.IdHorario = (int)datos.Lector["id_horario"];
                    aux.Profesional = profesionalNeg.BuscarPorId((int)datos.Lector["id_profesional"]);
                    aux.Especialidad = especialidadNeg.BuscarPorId((int)datos.Lector["id_especialidad"]);
                    aux.Consultorio = consultorioNeg.BuscarPorId((int)datos.Lector["id_consultorio"]);

                    aux.DiaSemana = (string)datos.Lector["dia_semana"];
                    aux.HoraInicio = (TimeSpan)datos.Lector["hora_inicio"];
                    aux.HoraFin = (TimeSpan)datos.Lector["hora_fin"];
                    aux.Activo = (bool)datos.Lector["activo"];
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
                
                // 1) Conflictos por PROFESIONAL

                datos.setearConsulta(@"
            SELECT 1
            FROM HorarioAtencion
            WHERE id_profesional = @prof
              AND dia_semana = @dia
              AND activo = 1
              AND (@inicio < hora_fin AND @fin > hora_inicio)
        ");

                datos.setearParametros("@prof", nuevo.Profesional.IdProfesional);
                datos.setearParametros("@dia", nuevo.DiaSemana);
                datos.setearParametros("@inicio", nuevo.HoraInicio);
                datos.setearParametros("@fin", nuevo.HoraFin);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return true; // conflicto  profesional ocupado

                datos.cerrarConexion();
                datos = new AccesoDatos(); // reset para segunda query


                // 2) Conflictos por CONSULTORIO
                datos.setearConsulta(@"
            SELECT 1
            FROM HorarioAtencion
            WHERE id_consultorio = @cons
              AND dia_semana = @dia
              AND activo = 1
              AND (@inicio < hora_fin AND @fin > hora_inicio)
        ");

                datos.setearParametros("@cons", nuevo.Consultorio.IdConsultorio);
                datos.setearParametros("@dia", nuevo.DiaSemana);
                datos.setearParametros("@inicio", nuevo.HoraInicio);
                datos.setearParametros("@fin", nuevo.HoraFin);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return true; // conflicto consultorio ocupado

                return false; // No hubo conflictos
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }

}
