namespace sf.Server.Models.SF.Core;

public interface ICampaignJudgesReference
{
    public IEnumerable<CampaignJudge>? Judges { get; set; }
    
    [JsonProperty("judgeIds"), NotMapped]
    public IEnumerable<Guid> JudgeIds => Judges?.Select(s => s.Id).ToList() ?? new List<Guid>();
    
    [NotMapped, JsonProperty("judgeCount")]
    public long JudgeCount => Judges?.LongCount() ?? 0;
}