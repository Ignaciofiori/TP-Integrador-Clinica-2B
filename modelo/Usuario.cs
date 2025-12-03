using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public int? IdProfesionalAsociado { get; set; }
        public bool Activo { get; set; }
        public Usuario(string Username, string Password, string Nombre, string Apellido)
        {
            this.Username = Username;
            this.Password = Password;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
        }
        public Usuario() { }
    }
}
