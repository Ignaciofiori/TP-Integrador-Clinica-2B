using modelo;
using System;
using System.Collections.Generic;

namespace negocio
{
    public class TurnoNegocio
    {
        private readonly PacienteNegocio pacienteNeg = new PacienteNegocio();
        private readonly HorarioAtencionNegocio horarioNeg = new HorarioAtencionNegocio();
        private readonly ObraSocialNegocio obraNeg = new ObraSocialNegocio();

        public List<Turno> Listar()
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT id_turno, id_paciente, id_horario, id_obra_social,
                   fecha_turno, hora_turno, estado, monto_total
            FROM Turno
        ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(MapearTurno(datos));
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

            try
            {
                datos.setearConsulta(@"
                    SELECT id_turno, id_paciente, id_horario, id_obra_social,
                           fecha_turno, hora_turno, estado, monto_total
                    FROM Turno
                    WHERE id_paciente = @id
                ");

                datos.setearParametros("@id", idPaciente);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(MapearTurno(datos));
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

            try
            {
                datos.setearConsulta(@"
                    SELECT t.id_turno, t.id_paciente, t.id_horario, t.id_obra_social,
                           t.fecha_turno, t.hora_turno, t.estado, t.monto_total
                    FROM Turno t
                    INNER JOIN HorarioAtencion h ON t.id_horario = h.id_horario
                    WHERE h.id_profesional = @id
                ");

                datos.setearParametros("@id", idProfesional);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(MapearTurno(datos));
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

            try
            {
                datos.setearConsulta(@"
                    SELECT id_turno, id_paciente, id_horario, id_obra_social,
                           fecha_turno, hora_turno, estado, monto_total
                    FROM Turno
                    WHERE id_turno = @id
                ");

                datos.setearParametros("@id", idTurno);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    aux = MapearTurno(datos);

                return aux;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private Turno MapearTurno(AccesoDatos datos)
        {
            Turno aux = new Turno();

            aux.IdTurno = (int)datos.Lector["id_turno"];

            aux.Paciente = pacienteNeg.BuscarPorId((int)datos.Lector["id_paciente"]);
            aux.Horario = horarioNeg.BuscarPorId((int)datos.Lector["id_horario"]);


            if (!(datos.Lector["id_obra_social"] is DBNull))
                aux.ObraSocial = obraNeg.BuscarPorId((int)datos.Lector["id_obra_social"]);

            aux.FechaTurno = (DateTime)datos.Lector["fecha_turno"];
            aux.HoraTurno = (TimeSpan)datos.Lector["hora_turno"];
            aux.Estado = (string)datos.Lector["estado"];

            if (!(datos.Lector["monto_total"] is DBNull))
                aux.MontoTotal = (decimal)datos.Lector["monto_total"];

            return aux;
        }

        public void RegistrarTurno(Turno t)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    INSERT INTO Turno (id_paciente, id_horario, id_obra_social, 
                                       fecha_turno, hora_turno, estado, monto_total)
                    VALUES (@pac, @hor, @obra, @fecha, @hora, @estado, NULL)
                ");

                datos.setearParametros("@pac", t.Paciente.IdPaciente);
                datos.setearParametros("@hor", t.Horario.IdHorario);

                datos.setearParametros("@obra",
                    t.ObraSocial != null ? (object)t.ObraSocial.IdObraSocial : DBNull.Value);

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

        public void CancelarTurno(int idTurno) => CambiarEstado(idTurno, "cancelado");

        public void ConfirmarAsistencia(int idTurno) => CambiarEstado(idTurno, "asistido");

        public decimal DeterminarMonto(Turno turno)
        {
            // Valor base
            int idProf = turno.Horario.Profesional.IdProfesional;
            int idEsp = turno.Horario.Especialidad.IdEspecialidad;

            EspecialidadNegocio negocioEsp =  new    EspecialidadNegocio();
            decimal valorBase = negocioEsp.BuscarValorConsulta(idProf,idEsp);

            // Tiene convenio con la obra social
            ObraSocialNegocio  negocioObra = new ObraSocialNegocio();


            int idPaciente = turno.Paciente.IdPaciente;
            int idObraSocialTurno = turno.Paciente.ObraSocial?.IdObraSocial ?? 0;

            bool tieneConvenio = negocioObra.ExisteRelacionActiva(idProf, idObraSocialTurno);

            // Si NO hay obra social Paga completo
            if (idObraSocialTurno == 0)
                return valorBase;
            // Si NO tiene convenio Paga completo
            if (!tieneConvenio)
                return valorBase;


            // Edad del paciente
            int edad = DateTime.Today.Year - turno.Paciente.FechaNacimiento.Value.Year;
            if (turno.Paciente.FechaNacimiento > DateTime.Today.AddYears(-edad))
                edad--;

            // 5. Descuento por edad (si existe)
            DescuentoNegocio descuentoNegocio = new DescuentoNegocio();
            List<decimal> descuentos = descuentoNegocio.ObtenerDescuentosPorEdad(edad, idObraSocialTurno);


            // 6. Aplicar cobertura OS
            decimal cobertura = turno.Paciente.ObraSocial.PorcentajeCobertura;
            decimal montoPaciente = valorBase * (1 - cobertura / 100);

            //) Aplicar descuentos (todos)
            foreach (decimal d in descuentos)
                montoPaciente *= (1 - d / 100);

            return montoPaciente;
          
        }
    }
}
