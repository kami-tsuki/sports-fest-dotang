namespace sf.Server.Models.SF;
 
public class School : Entity<Guid>, IStudentsReference, IClassesReference, ICampaignManagerReference, IName, IComment, ICampaignJudgesReference, ITutorsReference
{
    [MaxLength(64), MinLength(2), JsonProperty("name")]
    public string Name { get; set; } = Empty;
    
    [MaxLength(16), MinLength(2), JsonProperty("shortName")]
    public string ShortName { get; set; } = Empty;

    [MaxLength(1024), JsonProperty("comment")]
    public string Comment { get; set; } = Empty;
    
    
    [JsonIgnore]
    public IEnumerable<Class>? Classes { get; set; }
    
    
    [JsonIgnore]
    public IEnumerable<Student>? Students { get; set; }

    
    [JsonProperty("managerId")]
    public Guid? ManagerId { get; set; }
    
    [ForeignKey(nameof(ManagerId)),JsonIgnore]
    public CampaignManager? Manager { get; set; }
    
    [JsonIgnore]
    public IEnumerable<CampaignJudge>? Judges { get; set; }
    
    [JsonProperty("judgeIds"), NotMapped]
    public IEnumerable<Tutor>? Tutors { get; set; }
    
    [JsonProperty("studentIds"), NotMapped]
    public IEnumerable<Guid> StudentIds => Students?.Select(s => s.Id).ToList() ?? [];
    
}