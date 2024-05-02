using FuGetsu.Api.App;
using FuGetsu.Infrastructure.Stripe;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    //.ConfigureAppConfiguration(builder =>
    //    builder.AddUserSecrets<Program>(optional: true, reloadOnChange: false))
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddAppSwagger();
        services.AddStripeInfrastructure();
    })
    .Build();

await host.RunAsync();