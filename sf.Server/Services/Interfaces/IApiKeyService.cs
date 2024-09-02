using Microsoft.Extensions.Primitives;

namespace sf.Server.Services.Interfaces;

public interface IApiKeyService
{
    bool ValidateApiKey(StringValues apiKey);
    bool ValidateApiKey(StringValues apiKey, out User? user);
}