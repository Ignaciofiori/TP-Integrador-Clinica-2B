using System;

public class Paciente
{
    public int IdPaciente { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Dni { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public string Direccion { get; set; }
    public ObraSocial ObraSocial { get; set; }
    public string NroAfiliado { get; set; }

    public bool Activo { get; set; }

    public string NombreCompleto => this.Nombre + " " + this.Apellido;
}