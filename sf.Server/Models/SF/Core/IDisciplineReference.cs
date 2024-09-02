namespace sf.Server.Models.SF.Core;

public interface IDisciplineReference
{
    public Guid? DisciplineId { get; set; }
    public Discipline? Discipline { get; set; } 
}