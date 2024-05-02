using FuGetsu.Shared;
using static FuGetsu.Api.Shared.Constatns;

namespace FuGetsu.Api.Functions;

file static class Routes
{
    public const string Base = "Products";
    public const string GetAll = "GetAll";
}

internal sealed class ProductsFunction
{
    private readonly IProductsService _productsService;

    public ProductsFunction(
        IProductsService productsService)
    {
        _productsService = productsService;
    }

    [Function(Routes.GetAll)]
    [OpenApiOperation($"{Routes.Base}_{Routes.GetAll}", [Routes.Base])]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ApplicationJson, typeof(ProductResponse))]
    public async Task<IActionResult> GetAllProducts(
        [HttpTrigger(AuthorizationLevel.Anonymous, HttpMethodsConst.Get)] HttpRequest req)
    {
        var result = await _productsService.GetAllAsync(req.HttpContext.RequestAborted);
        return new OkObjectResult(new ProductResponse(result));
    }
}