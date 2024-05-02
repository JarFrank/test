using FuGetsu.Shared;
using static FuGetsu.Api.Shared.Constatns;

namespace FuGetsu.Api.Functions;

file static class Routes
{
    public const string Base = "Users";
    public const string Create = "Create";
    public const string Get = "Get";
}

public sealed class UserFunction
{
    private readonly ILogger<UserFunction> _logger;

    public UserFunction(ILogger<UserFunction> logger)
    {
        _logger = logger;
    }

    [Function(Routes.Create)]
    [OpenApiOperation($"{Routes.Base}_{Routes.Create}", [Routes.Base])]
    [OpenApiRequestBody(ApplicationJson, typeof(User), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ApplicationJson, typeof(User))]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, HttpMethodsConst.Post)] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult(new User() { UserName = "Welcome to Azure Functions!" });
    }

    [Function(Routes.Get)]
    [OpenApiOperation($"{Routes.Base}_{Routes.Get}", [Routes.Base])]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ApplicationJson, typeof(User))]
    public IActionResult Run2(
        [HttpTrigger(AuthorizationLevel.Anonymous, HttpMethodsConst.Get)] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult(new User() { UserName = "Welcome to Azure Functions!" });
    }
}