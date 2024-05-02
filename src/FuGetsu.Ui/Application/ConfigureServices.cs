using Blazored.LocalStorage;
using FuGetsu.Ui.Core.Export;
using FuGetsu.Ui.Core.Notifications;
using FuGetsu.Ui.Core.Storages;
using FuGetsu.Ui.Features;
using MudBlazor.Services;
using System.Text.Json;

namespace FuGetsu.Ui.Application;

internal static class ConfigureServices
{
    internal static void AddUiServices(this IServiceCollection services)
    {
        services.AddScoped<ISnackbarService, AppSnackbarService>();
        services.AddScoped<IFileService, FileService>();
        services.AddMudServices();

        services.AddBlazoredLocalStorage(opt =>
            opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
        services.AddScoped<IUserStorage, UserStorage>();
        services.AddScoped<CartState>();
        services.AddScoped<CartStorage>();
        services.AddScoped<CheckoutService>();
    }
}