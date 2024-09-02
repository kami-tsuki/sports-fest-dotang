namespace sf.Server.Models.SF.Core;

public interface IEntriesReference
{
    public IEnumerable<Entry>? Entries { get; init; }

    [NotMapped, JsonProperty("entryIds")]
    public IEnumerable<Guid>? EntryIds => Entries?.Select(e => e.Id);

    [NotMapped, JsonProperty("entriesCount")]
    public long? EntriesCount => Entries?.LongCount();
}