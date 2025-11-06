using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class HorarioAtencion
    {
        public int Id { get; set; } //idHorarioAtencion
        public Profesional Profesional { get; set; }
        public Consultorio Consultorio { get; set; }
        public string DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
