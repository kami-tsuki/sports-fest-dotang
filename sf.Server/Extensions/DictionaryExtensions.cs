namespace sf.Server.Extensions;

public static class DictionaryExtensions
{
    public static Dictionary<string, string>? AddFilter(this Dictionary<string, string>? dictionary, string key, string value) 

    {
        dictionary?.Add(key, value);
        return dictionary;
    }
    public static Dictionary<string, string>? AddFilter(this Dictionary<string, string>? dictionary, string key, Guid value) 

    {
        dictionary?.Add(key, value.ToString());
        return dictionary;
    }
}
