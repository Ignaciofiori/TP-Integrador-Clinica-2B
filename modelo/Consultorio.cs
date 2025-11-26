public class Consultorio
{
    public int IdConsultorio { get; set; }
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Piso { get; set; }
    public string NumeroSala { get; set; }
    public bool Activo { get; set; }

    public string NombreCompleto
    {
        get
        {
            return $"{Nombre} - Sala {NumeroSala}";
        }
    }
}
