using FuGetsu.Ui.Application.Options;
using FuGetsu.Ui.Core;
using FuGetsu.Ui.Core.ApiClients;
using Microsoft.Extensions.Options;

namespace Edrington.CaskTracking.Web.Client.Application;

internal static class ConfigureHttpClients
{
    internal static void AddHttpClients(this IServiceCollection services)
    {
        services.AddOptions<ApiOptions>()
            .BindConfiguration(ApiOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddApiHttpClients();

        services.AddHttpClient(Constants.ApiHttpClientName, (cp, client) =>
        {
            var options = cp.GetRequiredService<IOptions<ApiOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });
    }

    private static void AddApiHttpClients(this IServiceCollection services)
    {
        var assembly = typeof(ConfigureHttpClients).Assembly;
        var apiHttpClientInterfaces = assembly.GetTypes()
            .Where(x => x.IsInterface && x.GetInterfaces().Contains(typeof(IApiHttpClient)));

        foreach (var interfaceType in apiHttpClientInterfaces)
        {
            var implementation = assembly.GetTypes()
                .First(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Contains(interfaceType));
            services.AddTransient(interfaceType, implementation);
        }
    }
}