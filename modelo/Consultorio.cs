using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class Consultorio
    {
        public int Id { get; set; } //idConsultorio
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Piso { get; set; }
        public string NumeroSala { get; set; }
    }
}
