using System.Text.Json;

namespace FuGetsu.Ui.Core.ApiClients;

public abstract class HttpClientBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    protected HttpClientBase(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    protected virtual ValueTask<HttpClient> CreateHttpClientAsync(CancellationToken token = default)
    {
        return ValueTask.FromResult(_httpClientFactory.CreateClient(Constants.ApiHttpClientName));
    }

    protected static void UpdateJsonSerializerSettings(JsonSerializerOptions settings)
    {
        settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    }
}