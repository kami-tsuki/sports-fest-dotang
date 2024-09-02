namespace sf.Server.Models.SF;

[Index(nameof(ShortName), IsUnique = true)]
public class Discipline : Entity<Guid>, IName, IComment, ICampaignJudgeReference, ILocationReference, IStudentsReference, IEntriesReference
{
    [MaxLength(64), MinLength(2), JsonProperty("name")]
    public string Name { get; set; } = Empty;

    [MaxLength(8), MinLength(2), JsonProperty("short")]
    public string ShortName { get; set; } = Empty;


    [MaxLength(1024), JsonProperty("comment")]
    public string Comment { get; set; } = Empty;


    [InverseProperty(nameof(Student.Disciplines)), JsonIgnore]
    public IEnumerable<Student>? Students { get; set; }

    [NotMapped, JsonIgnore, ForeignKey(nameof(JudgeId))]
    public CampaignJudge? Judge { get; set; }


    [NotMapped, JsonProperty("judgeId")]
    public Guid? JudgeId { get; set; }


    [JsonIgnore]
    public Guid? LocationId { get; init; }

    [ForeignKey(nameof(LocationId)), JsonProperty("locationId")]
    public Location? Location { get; set; }

    [InverseProperty(nameof(Entry.Discipline)), JsonIgnore]
    public IEnumerable<Entry>? Entries { get; init; }
}