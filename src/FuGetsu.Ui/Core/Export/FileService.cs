using Microsoft.JSInterop;

namespace FuGetsu.Ui.Core.Export;

public interface IFileService
{
    Task DownloadFileAsync(Stream stream, string fileName);
}

internal sealed class FileService : IFileService
{
    public const string DownloadFunctionName = "downloadFileFromStream";

    private readonly IJSRuntime _jsRuntime;

    public FileService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task DownloadFileAsync(Stream stream, string fileName)
    {
        using var streamRef = new DotNetStreamReference(stream);
        await _jsRuntime.InvokeVoidAsync(DownloadFunctionName, fileName, streamRef);
    }
}