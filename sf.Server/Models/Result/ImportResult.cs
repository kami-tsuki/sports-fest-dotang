namespace sf.Server.Models.Result;

public class ImportResult
{

    public int IncomingRows { get; set; } = 0;
    public int CreatedEntities { get; set; } = 0;
    public int UsedColumns { get; set; } = 0;
    public int IgnoredColumns { get; set; } = 0;
    public List<string> Errors { get; set; } = [];
    public List<string> Warnings { get; set; } = [];
    
}