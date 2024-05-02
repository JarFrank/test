using FuGetsu.Shared;
using static FuGetsu.Api.Shared.Constatns;

namespace FuGetsu.Api.Functions;

file static class Routes
{
    public const string Base = "Stripe";
    public const string Checkout = "Checkout";
}

internal sealed class StripeFunction
{
    private readonly ICheckoutService _checkoutService;

    public StripeFunction(
        ICheckoutService checkoutService)
    {
        _checkoutService = checkoutService;
    }

    [Function(Routes.Checkout)]
    [OpenApiOperation($"{Routes.Base}_{Routes.Checkout}", [Routes.Base])]
    [OpenApiRequestBody(ApplicationJson, typeof(CheckoutRequest), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ApplicationJson, typeof(CheckoutResponse))]
    public async Task<IActionResult> Checkout(
        [HttpTrigger(AuthorizationLevel.Anonymous, HttpMethodsConst.Post)] HttpRequest req)
    {
        var request = await req.ReadFromJsonAsync<CheckoutRequest>();
        if (request is null)
        {
            return new BadRequestResult();
        }
        var result = await _checkoutService.CheckoutAsync(request, req.HttpContext.RequestAborted);
        return new OkObjectResult(new CheckoutResponse(result));
    }
}