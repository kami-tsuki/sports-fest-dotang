namespace sf.Server.Models.SF.Core;

public interface ILocationReference
{
    public Guid? LocationId { get; init; }
    public Location? Location { get; set; }
}