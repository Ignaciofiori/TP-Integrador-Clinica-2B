using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class Turno
    {
        public int Id { get; set; } //idTurno
        public Paciente Paciente { get; set; }
        public HorarioAtencion Horario { get; set; }
        public Especialidad Especialidad { get; set; }
        public ObraSocial ObraSocial { get; set; }
        public DateTime FechaTurno { get; set; }
        public TimeSpan HoraTurno { get; set; }
        public string Estado { get; set; }   // pendiente / confirmado / cancelado / asistido
        public decimal MontoTotal { get; set; }
    }
}
