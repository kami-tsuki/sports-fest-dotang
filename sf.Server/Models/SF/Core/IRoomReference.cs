namespace sf.Server.Models.SF.Core;

public interface IRoomReference
{
    
    public Guid? RoomId { get; set; }
    public Location? Room { get; set; }
}