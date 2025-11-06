using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class Paciente : Persona
    {
        public ObraSocial ObraSocial { get; set; }
        public string NumeroAfiliado { get; set; }
        public List<Turno> Turnos { get; set; }

    }
}
