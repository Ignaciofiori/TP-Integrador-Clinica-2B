public class Profesional
{
    public int IdProfesional { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Dni { get; set; }
    public string Matricula { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public bool Activo { get; set; }

    public string NombreCompleto => this.Nombre + " " + this.Apellido;  
}