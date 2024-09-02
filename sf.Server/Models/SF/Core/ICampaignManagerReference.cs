namespace sf.Server.Models.SF.Core;

public interface ICampaignManagerReference
{
    public Guid? ManagerId { get; set; }
    
    public CampaignManager? Manager { get; set; }
}