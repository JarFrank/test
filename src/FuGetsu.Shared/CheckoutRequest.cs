namespace FuGetsu.Shared;

public sealed record CheckoutRequest(
    string Type,
    string SuccessUrl,
    string CancelUrl,
    IReadOnlyList<LineItem> LineItems
);

public sealed record LineItem(string PriceId, string Type, int Quantity = 1);

public sealed record CheckoutResponse(string Url);