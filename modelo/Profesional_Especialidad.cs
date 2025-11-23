public class ProfesionalEspecialidad
{
    public int Id { get; set; }
    public Profesional Profesional { get; set; }  
    public Especialidad Especialidad { get; set; }
    public decimal ValorConsulta { get; set; }
    public bool Activo { get; set; }
}