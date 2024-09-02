namespace sf.Server.Models.SF;

[Index(nameof(ShortName), IsUnique = true)]
public class Class : Entity<Guid>, ITutorReference, IStudentsReference, ILocationReference
{
    [MaxLength(64), JsonProperty("name")]
    public string Name { get; set; } = Empty;
    [MaxLength(8), JsonProperty("short")]
    public string ShortName { get; set; } = Empty;

    [MaxLength(1024), JsonProperty("comment")]
    public string Comment { get; set; } = Empty;


    [JsonProperty("tutorId")]
    public Guid? TutorId { get; set; }

    [ForeignKey(nameof(TutorId)), JsonIgnore]
    public Tutor? Tutor { get; set; }

    [JsonIgnore, InverseProperty(nameof(Student.Class))]
    public IEnumerable<Student>? Students { get; set; }

    public Guid? RoomId { get; set; }

    [ForeignKey(nameof(RoomId)), JsonIgnore]
    public Location? Room { get; set; }
    
    [JsonProperty("schoolId")]
    public Guid? SchoolId { get; set; }

    [ForeignKey(nameof(SchoolId)), JsonIgnore]
    public School? School { get; set; }

    [JsonProperty("locationId")]
    public Guid? LocationId { get; init; }

    [ForeignKey(nameof(LocationId)), JsonIgnore]
    public Location? Location { get; set; }
}