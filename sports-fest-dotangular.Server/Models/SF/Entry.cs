namespace sports_fest_dotangular.Server.Models.SF;

public class Entry : Entity<Guid>
{ 
    [JsonProperty("studentId")]
    public Guid? StudentId { get; set; }
    [Required, JsonIgnore]
    public User? Student { get; init; }

    
    [JsonProperty("disciplineId")]
    public Guid? DisciplineId { get; set; }
    [Required, JsonIgnore]
    public Discipline? Discipline { get; init; } 

    [Range(0, 100), JsonProperty("score")]
    public double Score { get; set; }

    [MaxLength(1024), JsonProperty("note")]
    public string Comment { get; set; } = Empty;
}