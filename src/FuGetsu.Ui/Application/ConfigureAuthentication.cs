using FuGetsu.Ui.Application.Options;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MyBlazorAuthB2C.Core.Auth;

namespace FuGetsu.Ui.Application;

internal static class ConfigureAuthentication
{
    internal static void AddAppAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<AzureAdB2COptions>()
            .BindConfiguration(AzureAdB2COptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddAuthorizationCore();
        services.AddCascadingAuthenticationState();

        services.AddMsalAuthentication<RemoteAuthenticationState, RemoteUserAccount>(options =>
        {
            configuration.Bind(AzureAdB2COptions.SectionName, options.ProviderOptions.Authentication);
            options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
            options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
            options.ProviderOptions.LoginMode = "redirect";
        }).AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, RemoteUserAccount, CustomAccountFactory>();
    }
}