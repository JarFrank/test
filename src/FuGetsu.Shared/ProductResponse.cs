namespace FuGetsu.Shared;

public sealed record ProductResponse(IReadOnlyList<ProductDto> Products);

public sealed record ProductDto(
    string Id,
    string Name,
    string Description,
    IReadOnlyList<string> Images,
    string DefaultPriceId,
    string Currency,
    double Price,
    string Type
);