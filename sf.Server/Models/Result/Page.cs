namespace sports_fest_dotangular.Server.Models.Result;

public class Page<TEntity> where TEntity : class
{
    [JsonProperty("number")]
    public long Number { get; set; }
    
    [JsonProperty("size")]
    public long Count => Data.LongCount();
    
    [JsonProperty("total")]
    public long TotalEntities { get; set; }
    
    [JsonProperty("data")]
    public List<TEntity> Data { get; set; } = [];
    
    [JsonProperty("total.pages"), JsonIgnore]
    public long TotalPages => TotalEntities / Data.LongCount();
}