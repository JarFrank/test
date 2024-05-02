using FuGetsu.Shared;

namespace FuGetsu.Application.Features.Stripe;

public interface IProductsService
{
    Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken);
}