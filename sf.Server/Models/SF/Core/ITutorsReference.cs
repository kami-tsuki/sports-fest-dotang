namespace sf.Server.Models.SF.Core;

public interface ITutorsReference
{
    public IEnumerable<Tutor>? Tutors { get; set; }
    
    [JsonProperty("tutorIds"), NotMapped]
    public IEnumerable<Guid> TutorIds => Tutors?.Select(s => s.Id).ToList() ?? new List<Guid>();
    
    [NotMapped, JsonProperty("tutorCount")]
    public long TutorCount => Tutors?.LongCount() ?? 0;
}