using FuGetsu.Shared;
using FuGetsu.Ui.Core;
using FuGetsu.Ui.Core.ApiClients;
using FuGetsu.Ui.Core.Extensions;
using Microsoft.AspNetCore.Components;

namespace FuGetsu.Ui.Features;

public sealed class CheckoutService
{
    private readonly CartState _cartState;
    private readonly IStripeClient _stripeClient;
    private readonly NavigationManager _navigationManager;

    public CheckoutService(
        CartState cartState,
        IStripeClient stripeClient,
        NavigationManager navigationManager)
    {
        _cartState = cartState;
        _stripeClient = stripeClient;
        _navigationManager = navigationManager;
    }

    public async Task Checkout(CancellationToken cancellationToken)
    {
        var cart = await _cartState.GetAsync(cancellationToken);
        var lineItems = cart
            .GetOneTimePaymentItems()
            .Select(x => new LineItem(x.Product.DefaultPriceId, x.Product.Type, x.Quantity))
            .ToList();

        var request = new CheckoutRequest(
            Constants.StripeTypes.OneTime,
            _navigationManager.ToCompleted(),
            _navigationManager.ToAbandoned(),
            lineItems
        );
        var result = await _stripeClient.CheckoutAsync(request, cancellationToken);
        _navigationManager.NavigateTo(result.Url);
    }
}