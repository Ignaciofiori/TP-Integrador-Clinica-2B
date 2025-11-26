using System;

public class FacturaListado
{
    public int IdFactura { get; set; }

    public decimal MontoBase { get; set; }
    public decimal CoberturaAplicada { get; set; }
    public decimal DescuentoAplicado { get; set; }
    public decimal MontoTotal { get; set; }

    public DateTime FechaEmision { get; set; }

    public int IdTurno { get; set; }
    public DateTime FechaTurno { get; set; }
    public TimeSpan HoraTurno { get; set; }
    public string EstadoTurno { get; set; }

    public string PacienteNombreCompleto { get; set; }
    public string ProfesionalNombreCompleto { get; set; }
    public string EspecialidadNombre { get; set; }
    public string ObraSocialNombre { get; set; }
    public string ConsultorioDisplay { get; set; }
}