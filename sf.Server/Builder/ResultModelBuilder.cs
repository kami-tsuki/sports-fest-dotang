using sf.Server.Builder.Core;
using sf.Server.Models.Result;

namespace sf.Server.Builder;

public class ResultModelBuilder<T> : Builder<ResultModel<T>>
{
    private bool _success;
    private string? _title;
    private T? _data;
    private List<Message>? _messages;
    
    public ResultModelBuilder<T> WithSuccess(bool success)
    {
        _success = success;
        return this;
    }

    public ResultModelBuilder<T> AddMessage(string message, ErrorResult type, Severity severity = Severity.Error)
    {
        _messages ??= [];
        _messages.Add(new()
        {
            MessageText = message,
            Type = type,
            Severity = severity
        });

        return this;
    }
    public ResultModelBuilder<T> AddMessage(Message message)
    {
        _messages ??= [];
        _messages.Add(message);
        return this;
    }
    
    public ResultModelBuilder<T> WithMessages(List<Message> messages)
    {
        _messages = messages;
        return this;
    }
    
    
    
    public ResultModelBuilder<T> WithTitle(string title)
    {
        _title = title;
        return this;
    }
    
    public ResultModelBuilder<T> WithData(T data)
    {
        _data = data;
        return this;
    }
    
    public new ResultModel<T> Build() => new()
    {
        Success = _success,
        Message = _title,
        Messages = _messages,
        Data = _data
    };
    
    public ResultModelBuilder<T> FromInstance(ResultModel<T> instance)
    {
        _success = instance.Success;
        _title = instance.Message;
        _messages = instance.Messages;
        _data = instance.Data;
        return this;
    }
    
    public ResultModelBuilder<T> FromInstance<T2>(ResultModel<T2> instance) 
    {
        _success = instance.Success;
        _title = instance.Message;
        _messages = instance.Messages;
        return this;
    }
}