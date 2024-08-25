using sf.Server.Builder;
using sf.Server.Models.Result;

namespace sf.Server.Services
{
    public class ResultService
    {
        public ResultModel<T> BuildResult<T>(bool success, T data, string title = "", List<string>? messages = null)
        {
            var resultBuilder = new ResultModelBuilder<T>().WithSuccess(success).WithData(data);
            if (!IsNullOrEmpty(title)) resultBuilder.WithTitle(title);
            if (messages is not { Count: > 0 }) return resultBuilder.Build();
            foreach (var message in messages) resultBuilder.AddMessage(message, ErrorResult.Validation);
            return resultBuilder.Build();
        }

        public ResultModel<object> BuildErrorResult(string title, string message) => new ResultModelBuilder<object>()
                                                                                    .WithSuccess(false)
                                                                                    .WithTitle(title)
                                                                                    .AddMessage(message, ErrorResult.Validation)
                                                                                    .Build();
    }
}