using Microsoft.AspNetCore.Components;

namespace FuGetsu.Ui.Features;

public sealed class CartState
{
    private readonly CartStorage _cartStorage;

    private Cart? _state;

    private readonly HashSet<CartStateChangedSubscription> _changeSubscriptions = [];

    public CartState(CartStorage cartStorage)
    {
        _cartStorage = cartStorage;
    }

    public async ValueTask AddAsync(CartItem cartItem, CancellationToken cancellationToken)
    {
        var cart = await GetAsync(cancellationToken);
        cart.AddItem(cartItem.Product, cartItem.Quantity);
        await _cartStorage.UpsertAsync(cart, cancellationToken);
        await NotifyAllSubscribers();
    }

    public async ValueTask RemoveAsync(string productId, CancellationToken cancellationToken)
    {
        var cart = await GetAsync(cancellationToken);
        cart.RemoveItem(productId);
        await _cartStorage.UpsertAsync(cart, cancellationToken);
        await NotifyAllSubscribers();
    }

    public async ValueTask UpdateQuantityAsync(string productId, int quantity, CancellationToken cancellationToken)
    {
        var cart = await GetAsync(cancellationToken);
        cart.UpdateQuantity(productId, quantity);
        await _cartStorage.UpsertAsync(cart, cancellationToken);
        await NotifyAllSubscribers();
    }

    public async ValueTask<Cart> GetAsync(CancellationToken cancellationToken)
    {
        if (_state is not null)
        {
            return _state;
        }
        _state = await _cartStorage.GetAsync(cancellationToken) ?? new Cart();
        return _state;
    }

    public IDisposable NotifyOnChange(EventCallback callback)
    {
        var subscription = new CartStateChangedSubscription(this, callback);
        _changeSubscriptions.Add(subscription);
        return subscription;
    }

    private async Task NotifyAllSubscribers() =>
        await Task.WhenAll(_changeSubscriptions.Select(x => x.NotifyAsync()));

    private sealed class CartStateChangedSubscription(CartState Owner, EventCallback Callback) : IDisposable
    {
        public Task NotifyAsync() => Callback.InvokeAsync();

        public void Dispose() => Owner._changeSubscriptions.Remove(this);
    }
}