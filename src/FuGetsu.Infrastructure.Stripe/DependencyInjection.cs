using FuGetsu.Application.Features.Stripe;
using FuGetsu.Infrastructure.Stripe.Options;
using FuGetsu.Infrastructure.Stripe.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace FuGetsu.Infrastructure.Stripe;

public static class StripeInfrastructure
{
    private const string HttpClientName = "Stripe";

    public static IServiceCollection AddStripeInfrastructure(this IServiceCollection services)
    {
        services.AddOptions<StrapiOptions>()
            .BindConfiguration(StrapiOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddTransient<ICheckoutService, CheckoutService>();
        services.AddTransient<IProductsService, ProductsService>();

        services.AddHttpClient(HttpClientName);
        services.AddStripe();
        return services;
    }

    private static void AddStripe(this IServiceCollection services)
    {
        StripeConfiguration.MaxNetworkRetries = 3;
        services.AddTransient<IStripeClient, StripeClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<StrapiOptions>>();
            var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = new SystemNetHttpClient(
                httpClient: clientFactory.CreateClient(HttpClientName),
                maxNetworkRetries: StripeConfiguration.MaxNetworkRetries,
                enableTelemetry: StripeConfiguration.EnableTelemetry);

            return new StripeClient(options.Value.SecretKey, httpClient: httpClient);
        });

        services.AddScoped<CustomerService>();
        services.AddScoped<ChargeService>();
        services.AddScoped<ProductService>();
        services.AddScoped<SessionService>();
        services.AddScoped<TokenService>();
    }
}