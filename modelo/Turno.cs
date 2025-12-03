using System;

public class Turno
{
    public int IdTurno { get; set; }
    public Paciente Paciente { get; set; }
    public HorarioAtencion Horario { get; set; }
    public ObraSocial ObraSocial { get; set; }

    public DateTime FechaTurno { get; set; }
    public TimeSpan HoraTurno { get; set; }
    public string Estado { get; set; }
    public decimal? MontoTotal { get; set; }
    public bool Activo { get; set; }
    public decimal MontoBase { get; set; }
    public decimal DescuentoAplicado { get; set; }
    public string NotaProfesional { get; set; }
    public string NombreObraSocialTurno
    {
        get
        {
            if (ObraSocial == null || string.IsNullOrWhiteSpace(ObraSocial.Nombre))
                return "Particular";

            return ObraSocial.Nombre;
        }
    }

    public string ConsultorioNombre
    {
        get
        {
            if (Horario?.Consultorio == null)
                return "Sin consultorio";

            
            return Horario.Consultorio.NombreCompleto;
        }
    }
}