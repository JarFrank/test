using FuGetsu.Application.Features.Stripe;
using FuGetsu.Shared;
using Stripe;

namespace FuGetsu.Infrastructure.Stripe.Services;

internal sealed class ProductsService : IProductsService
{
    private readonly ProductService _productService;

    public ProductsService(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var options = new ProductListOptions
        {
            Limit = 10,
            Expand = ["data.default_price"]
        };
        var products = await _productService.ListAsync(options, cancellationToken: cancellationToken);
        return products.Select(x => new ProductDto(
                x.Id,
                x.Name,
                x.Description,
                x.Images,
                x.DefaultPriceId,
                x.DefaultPrice.Currency,
                x.DefaultPrice.UnitAmount.HasValue ? (double)x.DefaultPrice.UnitAmount / 100.0 : 0,
                x.DefaultPrice.Type
            )).ToList();
    }
}