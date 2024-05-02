using Blazored.LocalStorage;

namespace FuGetsu.Ui.Features;

public sealed class CartStorage
{
    private const string CartKey = "fg-cart";

    private readonly ILocalStorageService _localStorageService;

    public CartStorage(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async ValueTask UpsertAsync(Cart cart, CancellationToken cancellationToken) =>
        await _localStorageService.SetItemAsync(CartKey, cart, cancellationToken);

    public async ValueTask RemoveAsync(CancellationToken cancellationToken) =>
        await _localStorageService.RemoveItemAsync(CartKey, cancellationToken);

    public async ValueTask<Cart?> GetAsync(CancellationToken cancellationToken) =>
        await _localStorageService.GetItemAsync<Cart>(CartKey, cancellationToken);
}