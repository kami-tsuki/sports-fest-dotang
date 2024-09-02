namespace sf.Server.Models.SF.Core;

public interface ITutorReference
{
    
    public Guid? TutorId { get; set; }
    public Tutor? Tutor { get; set; }
}