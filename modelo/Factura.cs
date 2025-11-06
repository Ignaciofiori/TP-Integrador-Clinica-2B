using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class Factura
    {
        public int Id { get; set; }
        public Turno Turno { get; set; }
        public decimal MontoBase { get; set; }
        public decimal CoberturaAplicada { get; set; }
        public decimal DescuentoAplicado { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaEmision { get; set; }
    }
}
