﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;

namespace sf.Server.Services;

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