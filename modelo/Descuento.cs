using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class Descuento
    {
        public int Id { get; set; }
        public ObraSocial ObraSocial { get; set; }
        public int EdadMin { get; set; }
        public int EdadMax { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public string Descripcion { get; set; }

        public bool Activo { get; set; }
    }
}
