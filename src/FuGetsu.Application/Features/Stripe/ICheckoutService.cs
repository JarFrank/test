using FuGetsu.Shared;

namespace FuGetsu.Application.Features.Stripe;

public interface ICheckoutService
{
    Task<string> CheckoutAsync(CheckoutRequest request, CancellationToken cancellationToken);
}