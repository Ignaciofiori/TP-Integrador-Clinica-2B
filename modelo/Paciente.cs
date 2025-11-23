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

    // Nueva propiedad para mostrar la obra social correctamente
    public string ObraSocialNombre
    {
        get
        {
            if (ObraSocial == null)
                return "Sin Obra Social";

            if (!ObraSocial.Activo)
                return ObraSocial.Nombre + " Inactiva Momentaneamente" ;

            return ObraSocial.Nombre;
        }
    }

    public int Edad
    {
        get
        {
            if (!FechaNacimiento.HasValue)
                return 0; // o -1 si querés diferenciar "sin fecha"

            DateTime hoy = DateTime.Today;
            int edad = hoy.Year - FechaNacimiento.Value.Year;

            if (FechaNacimiento.Value.Date > hoy.AddYears(-edad))
                edad--;

            return edad;
        }
    }
}

