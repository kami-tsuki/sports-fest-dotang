using System.ComponentModel.DataAnnotations.Schema;

namespace sf.Server.Models.Core;

public class Entity<TKey> where TKey : IComparable<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonProperty("id")]
    public required TKey Id { get; set; }
    
    [Timestamp, Required, JsonProperty("created")]
    public DateTime CreatedAt { get; set; }
    
    [Timestamp, Required, JsonProperty("updated")]
    public DateTime UpdatedAt { get; set; }
}