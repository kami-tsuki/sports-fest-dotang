namespace sf.Server.Models.SF.Core;

public interface IClassReference
{
    public Guid? ClassId { get; set; }
    public Class? Class { get; set; }
}