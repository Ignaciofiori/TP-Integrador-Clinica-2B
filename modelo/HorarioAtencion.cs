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

    public string HoraInicioStr => HoraInicio.ToString(@"hh\:mm");
    public string HoraFinStr => HoraFin.ToString(@"hh\:mm");

    public string HorarioDisplay { get; set; }

    public string NombreCompletoProfesional
    {
        get
        {
            if (Profesional == null)
                return "";

            return Profesional.NombreCompleto;
        }
    }
    public string NombreConsultorio
    {
        get
        {
            if (Consultorio == null)
                return "";
            return Consultorio.NombreCompleto;
        }
    }
}