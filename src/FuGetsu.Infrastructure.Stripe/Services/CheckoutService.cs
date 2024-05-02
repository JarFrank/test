using FuGetsu.Application.Features.Stripe;
using FuGetsu.Shared;
using Stripe.Checkout;
using System.Diagnostics;

namespace FuGetsu.Infrastructure.Stripe.Services;

internal sealed class CheckoutService : ICheckoutService
{
    private readonly SessionService _sessionService;

    public CheckoutService(SessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public async Task<string> CheckoutAsync(CheckoutRequest request, CancellationToken cancellationToken)
    {
        string mode = request.Type switch
        {
            "one_time" => "payment",
            "recurring" => "subscription",
            _ => throw new UnreachableException()
        };
        var options = new SessionCreateOptions
        {
            LineItems = request.LineItems.Select(x => new SessionLineItemOptions()
            {
                Price = x.PriceId,
                Quantity = x.Quantity,
            }).ToList(),
            Mode = mode,
            SuccessUrl = request.SuccessUrl,
            CancelUrl = request.CancelUrl,
            // TODO: add metadata like user_id, email, etc.
            Metadata = new Dictionary<string, string>
            {
                { "user_id", "my-user-id" },
                { "type", "my-special-type" },
            },
        };
        var session = await _sessionService.CreateAsync(options, cancellationToken: cancellationToken);
        return session.Url;
    }
}