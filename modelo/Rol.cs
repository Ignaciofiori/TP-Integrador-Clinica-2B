using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; }

        public Rol(string nombreRol)
        {
            NombreRol = nombreRol;
        }
    }
}
