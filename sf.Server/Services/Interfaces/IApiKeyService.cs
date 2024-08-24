using Microsoft.Extensions.Primitives;

namespace sports_fest_dotangular.Server.Services.Interfaces;

public interface IApiKeyService
{
    bool ValidateApiKey(StringValues apiKey);
    bool ValidateApiKey(StringValues apiKey, out User? user);
}