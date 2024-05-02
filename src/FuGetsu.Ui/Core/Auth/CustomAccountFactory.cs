using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;

namespace MyBlazorAuthB2C.Core.Auth;

public class CustomAccountFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    private readonly ILogger<CustomAccountFactory> _logger;

    public CustomAccountFactory(
        IAccessTokenProviderAccessor accessor,
        ILogger<CustomAccountFactory> logger)
        : base(accessor)
    {
        _logger = logger;
    }

    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
    {
        var initialUser = await base.CreateUserAsync(account, options);
        if (initialUser.Identity?.IsAuthenticated != true)
        {
            return initialUser;
        }
        if (initialUser.Identity is not ClaimsIdentity userIdentity)
        {
            return initialUser;
        }
        try
        {
            // TODO: Add custom claims here
            // use IServiceProvider to get services, or use http client factory to call APIs
            _logger.LogDebug("Creating custom claims for user {Name}", userIdentity.Name);
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }

        return initialUser;
    }
}