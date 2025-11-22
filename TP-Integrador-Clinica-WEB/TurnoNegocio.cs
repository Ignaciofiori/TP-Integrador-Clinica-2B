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
    public class TurnoNegocio
    {
        public List<Turno> Listar()
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();

            PacienteNegocio pacienteNeg = new PacienteNegocio();
            HorarioAtencionNegocio horarioNeg = new HorarioAtencionNegocio();
            EspecialidadNegocio especialidadNeg = new EspecialidadNegocio();
            ObraSocialNegocio obraNeg = new ObraSocialNegocio();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_turno, id_paciente, id_horario, id_especialidad, id_obra_social,
                           fecha_turno, hora_turno, estado, monto_total
                    FROM Turno
                ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno aux = new Turno();

                    aux.Id = (int)datos.Lector["id_turno"];
                    aux.Paciente = pacienteNeg.BuscarPorId((int)datos.Lector["id_paciente"]);
                    aux.Horario = horarioNeg.BuscarPorId((int)datos.Lector["id_horario"]);
                    aux.Especialidad = especialidadNeg.BuscarPorId((int)datos.Lector["id_especialidad"]);

                    if (!(datos.Lector["id_obra_social"] is DBNull))
                        aux.ObraSocial = obraNeg.BuscarPorId((int)datos.Lector["id_obra_social"]);

                    aux.FechaTurno = (DateTime)datos.Lector["fecha_turno"];
                    aux.HoraTurno = (TimeSpan)datos.Lector["hora_turno"];
                    aux.Estado = (string)datos.Lector["estado"];

                    if (!(datos.Lector["monto_total"] is DBNull))
                        aux.MontoTotal = (decimal)datos.Lector["monto_total"];

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Turno> ListarPorPaciente(int idPaciente)
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();

            PacienteNegocio pacienteNeg = new PacienteNegocio();
            HorarioAtencionNegocio horarioNeg = new HorarioAtencionNegocio();
            EspecialidadNegocio especialidadNeg = new EspecialidadNegocio();
            ObraSocialNegocio obraNeg = new ObraSocialNegocio();

            try
            {
                datos.setearConsulta(@"
                    SELECT id_turno, id_paciente, id_horario, id_especialidad, id_obra_social,
                           fecha_turno, hora_turno, estado, monto_total
                    FROM Turno
                    WHERE id_paciente = @id
                ");

                datos.setearParametros("@id", idPaciente);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno aux = new Turno();

                    aux.Id = (int)datos.Lector["id_turno"];
                    aux.Paciente = pacienteNeg.BuscarPorId(idPaciente);
                    aux.Horario = horarioNeg.BuscarPorId((int)datos.Lector["id_horario"]);
                    aux.Especialidad = especialidadNeg.BuscarPorId((int)datos.Lector["id_especialidad"]);

                    if (!(datos.Lector["id_obra_social"] is DBNull))
                        aux.ObraSocial = obraNeg.BuscarPorId((int)datos.Lector["id_obra_social"]);

                    aux.FechaTurno = (DateTime)datos.Lector["fecha_turno"];
                    aux.HoraTurno = (TimeSpan)datos.Lector["hora_turno"];
                    aux.Estado = (string)datos.Lector["estado"];

                    if (!(datos.Lector["monto_total"] is DBNull))
                        aux.MontoTotal = (decimal)datos.Lector["monto_total"];

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Turno> ListarPorProfesional(int idProfesional)
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();

            PacienteNegocio pacienteNeg = new PacienteNegocio();
            HorarioAtencionNegocio horarioNeg = new HorarioAtencionNegocio();
            EspecialidadNegocio especialidadNeg = new EspecialidadNegocio();
            ObraSocialNegocio obraNeg = new ObraSocialNegocio();

            try
            {
                datos.setearConsulta(@"
                    SELECT t.id_turno, t.id_paciente, t.id_horario, t.id_especialidad, t.id_obra_social,
                           t.fecha_turno, t.hora_turno, t.estado, t.monto_total
                    FROM Turno t
                    INNER JOIN HorarioAtencion h ON t.id_horario = h.id_horario
                    WHERE h.id_profesional = @id
                ");

                datos.setearParametros("@id", idProfesional);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno aux = new Turno();

                    aux.Id = (int)datos.Lector["id_turno"];
                    aux.Paciente = pacienteNeg.BuscarPorId((int)datos.Lector["id_paciente"]);
                    aux.Horario = horarioNeg.BuscarPorId((int)datos.Lector["id_horario"]);
                    aux.Especialidad = especialidadNeg.BuscarPorId((int)datos.Lector["id_especialidad"]);

                    if (!(datos.Lector["id_obra_social"] is DBNull))
                        aux.ObraSocial = obraNeg.BuscarPorId((int)datos.Lector["id_obra_social"]);

                    aux.FechaTurno = (DateTime)datos.Lector["fecha_turno"];
                    aux.HoraTurno = (TimeSpan)datos.Lector["hora_turno"];
                    aux.Estado = (string)datos.Lector["estado"];

                    if (!(datos.Lector["monto_total"] is DBNull))
                        aux.MontoTotal = (decimal)datos.Lector["monto_total"];

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Turno BuscarPorId(int idTurno)
        {
            Turno aux = null;
            AccesoDatos datos = new AccesoDatos();

            // Negocios relacionados para reconstruir objetos
            PacienteNegocio pacienteNeg = new PacienteNegocio();
            HorarioAtencionNegocio horarioNeg = new HorarioAtencionNegocio();
            EspecialidadNegocio especialidadNeg = new EspecialidadNegocio();
            ObraSocialNegocio obraNeg = new ObraSocialNegocio();

            try
            {
                datos.setearConsulta(@"
            SELECT id_turno, id_paciente, id_horario, id_especialidad, id_obra_social,
                   fecha_turno, hora_turno, estado, monto_total
            FROM Turno
            WHERE id_turno = @id");

                datos.setearParametros("@id", idTurno);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux = new Turno();

                    aux.Id = (int)datos.Lector["id_turno"];
                    aux.Paciente = pacienteNeg.BuscarPorId((int)datos.Lector["id_paciente"]);
                    aux.Horario = horarioNeg.BuscarPorId((int)datos.Lector["id_horario"]);
                    aux.Especialidad = especialidadNeg.BuscarPorId((int)datos.Lector["id_especialidad"]);

                    if (!(datos.Lector["id_obra_social"] is DBNull))
                        aux.ObraSocial = obraNeg.BuscarPorId((int)datos.Lector["id_obra_social"]);

                    aux.FechaTurno = (DateTime)datos.Lector["fecha_turno"];
                    aux.HoraTurno = (TimeSpan)datos.Lector["hora_turno"];
                    aux.Estado = (string)datos.Lector["estado"];

                    if (!(datos.Lector["monto_total"] is DBNull))
                        aux.MontoTotal = (decimal)datos.Lector["monto_total"];
                }

                return aux;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void RegistrarTurno(Turno t)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Turno (id_paciente, id_horario, id_especialidad, id_obra_social, fecha_turno, hora_turno, estado, monto_total)
                    VALUES (@pac, @hor, @esp, @obra, @fecha, @hora, @estado, NULL) -- FUNCION DE CALCULO A IMPLEMENTAR
                ");

                datos.setearParametros("@pac", t.Paciente.Id);
                datos.setearParametros("@hor", t.Horario.Id);
                datos.setearParametros("@esp", t.Especialidad.Id);
                datos.setearParametros("@obra", t.ObraSocial != null ? (object)t.ObraSocial.Id : DBNull.Value);
                datos.setearParametros("@fecha", t.FechaTurno);
                datos.setearParametros("@hora", t.HoraTurno);
                datos.setearParametros("@estado", t.Estado);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void CambiarEstado(int idTurno, string nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Turno SET estado = @estado WHERE id_turno = @id");
                datos.setearParametros("@estado", nuevoEstado);
                datos.setearParametros("@id", idTurno);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void CancelarTurno(int idTurno)
        {
            CambiarEstado(idTurno, "cancelado");
        }

        public void ConfirmarAsistencia(int idTurno)
        {
            CambiarEstado(idTurno, "asistido");
        }
    }
}
