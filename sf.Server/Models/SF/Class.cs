using System.ComponentModel.DataAnnotations.Schema;
using sf.Server.Models.Auth;
using sf.Server.Models.Core;

namespace sf.Server.Models.SF;

[Index(nameof(ShortName), IsUnique = true)]
public class Class : Entity<Guid>
{
    [MaxLength(64), JsonProperty("name")]
    public string Name { get; set; } = Empty;
    [MaxLength(8), JsonProperty("short")]
    public string ShortName { get; set; } = Empty;

    [MaxLength(1024), JsonProperty("comment")]
    public string Comment { get; set; } = Empty;

    [InverseProperty(nameof(User.Class)), System.Text.Json.Serialization.JsonIgnore]
    public IEnumerable<User>? Users { get; init; }

    [NotMapped, System.Text.Json.Serialization.JsonIgnore]
    public User? Tutor => Users?.FirstOrDefault(u => u.Role == RoleType.Tutor);

    [JsonProperty("tutorId")]
    public Guid? RoomId { get; set; }

    [ForeignKey(nameof(RoomId)), System.Text.Json.Serialization.JsonIgnore]
    public Location? Room { get; init; }

    [NotMapped, System.Text.Json.Serialization.JsonIgnore]
    public IEnumerable<User>? Students => Users?.Where(u => u.Role == RoleType.Student);

    [NotMapped, JsonProperty("studentIds")]
    public IEnumerable<Guid>? StudentIds => Students?.Select(s => s.Id) ?? [];

    [NotMapped, JsonProperty("studentCount")]
    public long StudentCount => Students?.LongCount() ?? 0;
}