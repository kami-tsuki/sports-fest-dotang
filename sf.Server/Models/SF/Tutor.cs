namespace sf.Server.Models.SF;

public class Tutor : User, IClassReference, ISchoolReference
{
    [JsonProperty("classId")]
    public Guid? ClassId { get; set; }
    
    
    [ForeignKey(nameof(ClassId)), JsonIgnore]
    public Class? Class { get; set; }
    
    [JsonProperty("schoolId")]
    public Guid? SchoolId { get; set; }

    [ForeignKey(nameof(SchoolId)), JsonIgnore]
    public School? School { get; set; }
    
}