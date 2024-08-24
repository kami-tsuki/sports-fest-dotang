namespace sports_fest_dotangular.Server.Builder.Core;

public class Builder<T> where T : class
{
    private T? _instance;

    public T? Build() => _instance;

    public Builder<T> WithInstance(T? instance)
    {
        _instance = instance;
        return this;
    }
}