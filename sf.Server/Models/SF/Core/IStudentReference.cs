namespace sf.Server.Models.SF.Core;

public interface IStudentReference
{
    public Guid? StudentId { get; set; }
    public Student? Student { get; set; }
}