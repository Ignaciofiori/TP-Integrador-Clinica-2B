using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class Profesional : Persona
    {
        public string Matricula { get; set; }

        // Relación: un profesional puede tener varias especialidades
        public List<Especialidad> Especialidades { get; set; }

        // Relación: un profesional puede trabajar con varias obras sociales
        public List<ObraSocial> ObrasSociales { get; set; }
    }
}
