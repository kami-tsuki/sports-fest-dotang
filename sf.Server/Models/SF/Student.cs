namespace sf.Server.Models.SF;

public class Student : User, IClassReference, IEntriesReference, ISchoolReference
{
    [InverseProperty(nameof(Discipline.Students)), JsonIgnore]
    public IEnumerable<Discipline>? Disciplines { get; set; }


    [JsonProperty("classId")]
    public Guid? ClassId { get; set; }


    [ForeignKey(nameof(ClassId)), JsonIgnore]
    public Class? Class { get; set; }
    
    [InverseProperty(nameof(Entry.Student)), JsonIgnore]
    public IEnumerable<Entry>? Entries { get; init; }
    
    [JsonProperty("schoolId")]
    public Guid? SchoolId { get; set; }

    [ForeignKey(nameof(SchoolId)), JsonIgnore]
    public School? School { get; set; }
}