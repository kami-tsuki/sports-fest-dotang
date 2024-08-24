using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using sports_fest_dotangular.Server.Services.Interfaces;

namespace sports_fest_dotangular.Server.Services;

public class ApiKeyService(UserManager<User> userManager) : IApiKeyService
{
    public bool ValidateApiKey(StringValues apiKey, out User? user)
    {
        user = userManager.Users.SingleOrDefault(u => u.ApiKey == apiKey);
        if (user == null || !userManager.SupportsUserLockout)
            return user != null;
        if (userManager.IsLockedOutAsync(user).Result)
            user = null;
        return user != null;
    }

    public bool ValidateApiKey(StringValues apiKey) => ValidateApiKey(apiKey, out _);
}