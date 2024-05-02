using FuGetsu.Shared;
using FuGetsu.Ui.Core;

namespace FuGetsu.Ui.Features;

public sealed record CartItem(ProductDto Product, int Quantity, double Price)
{
    public double Total => Price * Quantity;
}

public sealed class Cart
{
    public List<CartItem> Items { get; set; } = [];

    public double Total => Items.Sum(x => x.Price * x.Quantity);

    public int Quantity => Items.Sum(x => x.Quantity);

    public void AddItem(ProductDto product, int quantity)
    {
        if (Items.Find(x => x.Product.Id == product.Id) is { } item)
        {
            Items[Items.IndexOf(item)] = item with { Quantity = item.Quantity + quantity };
            return;
        }
        Items.Add(new CartItem(product, quantity, product.Price));
    }

    public void RemoveItem(string productId)
    {
        if (Items.Find(x => x.Product.Id == productId) is { } item)
        {
            Items.Remove(item);
        }
    }

    public void UpdateQuantity(string productId, int quantity)
    {
        if (Items.Find(x => x.Product.Id == productId) is not { } item)
        {
            return;
        }
        if (quantity > 0)
        {
            Items[Items.IndexOf(item)] = item with { Quantity = quantity };
        }
        else
        {
            Items.Remove(item);
        }
    }

    public void Clear()
    {
        Items.Clear();
    }

    public IReadOnlyList<CartItem> GetOneTimePaymentItems() =>
        Items.Where(x => x.Product.Type == Constants.StripeTypes.OneTime).ToList();
}