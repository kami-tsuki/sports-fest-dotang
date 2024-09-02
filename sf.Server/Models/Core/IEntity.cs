namespace sf.Server.Models.Core;

public interface IEntity<TKey> where TKey : IComparable<TKey>
{
    public TKey Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}