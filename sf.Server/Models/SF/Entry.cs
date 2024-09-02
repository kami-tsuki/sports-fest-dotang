namespace sf.Server.Models.SF;

public class Entry : Entity<Guid>, IStudentReference, IDisciplineReference, IScore
{ 
    [JsonProperty("studentId")]
    public Guid? StudentId { get; set; }
    [Required, JsonIgnore]
    public Student? Student { get; set; }

    
    [JsonProperty("disciplineId")]
    public Guid? DisciplineId { get; set; }
    [Required, JsonIgnore]
    public Discipline? Discipline { get; set; } 
    

    [Range(0, 100), JsonProperty("score")]
    public double Score { get; set; }

    [MaxLength(1024), JsonProperty("note")]
    public string Comment { get; set; } = Empty;
}