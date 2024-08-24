namespace sports_fest_dotangular.Server.Models.Result;

public class ResultModel<T>
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("error")]
    public bool Error => !Success; 

    [JsonProperty("message")]
    public string? Message { get; set; } = "An error occurred.";

    [JsonProperty("data")]
    public T? Data { get; set; } = default!;

    [JsonProperty("messages")]
    public List<Message>? Messages { get; set; } = [];
}

public class Message
{
    [JsonProperty("error.message")]
    public string? MessageText { get; set; } = "An error occurred.";

    [JsonProperty("error.type")]
    public ErrorResult? Type { get; set; } = ErrorResult.InternalException;

    [JsonProperty("error.severity")]
    public Severity? Severity { get; set; } = Result.Severity.Error;
}

public enum ErrorResult
{
    InternalException = 1,
    InvalidModel = 2,
    InvalidCredentials = 3,
    InvalidToken = 4,
    InvalidRole = 5,
    InvalidUser = 6,
    InvalidPassword = 7,
    InvalidEmail = 8,
    InvalidUsername = 9,
    InvalidOldPassword = 10,
    InvalidNewPassword = 11,
    InvalidModelState,
    PasswordChangeFailed,
    AccountLocked,
    UserNotFound,
    UserCreationFailed,
    RoleCreationFailed,
    Validation,
    InvalidId
}

public enum Severity
{
    Verbose = 0,
    Debug = 1,
    Information = 2,
    Warning = 3,
    Error = 4,
    Fatal = 5
}