using System;

public class ProfesionalObraSocial
{
    public Profesional Profesional { get; set; }    
    public ObraSocial ObraSocial { get; set; }     

    public bool ConvenioActivo { get; set; }
    public DateTime FechaInicio { get; set; }
    public bool Activo { get; set; }
}