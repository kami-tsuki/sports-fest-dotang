namespace sf.Server.Models.SF.Core;

public interface ICampaignJudgeReference
{
    public Guid? JudgeId { get; set; }
    public CampaignJudge? Judge { get; set; }
}