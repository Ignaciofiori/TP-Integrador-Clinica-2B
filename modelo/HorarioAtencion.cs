using System;

public class HorarioAtencion
{
    public int IdHorario { get; set; }
    public Profesional Profesional { get; set; }    
    public Especialidad Especialidad { get; set; }  
    public Consultorio Consultorio { get; set; }    
    public string DiaSemana { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFin { get; set; }
    public bool Activo { get; set; }
}