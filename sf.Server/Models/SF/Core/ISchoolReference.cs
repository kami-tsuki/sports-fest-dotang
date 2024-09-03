namespace sf.Server.Models.SF.Core;

public interface ISchoolReference
{
    public Guid? SchoolId { get; set; }
    public School? School { get; set; }
}