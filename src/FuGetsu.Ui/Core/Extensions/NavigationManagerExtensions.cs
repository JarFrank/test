using Microsoft.AspNetCore.Components;

namespace FuGetsu.Ui.Core.Extensions;

public static class NavigationManagerExtensions
{
    public static string ToCompleted(this NavigationManager navigationManager)
    {
        var builder = new UriBuilder(navigationManager.BaseUri)
        {
            Path = Routes.Products.Complete,
        };
        return builder.Uri.AbsoluteUri;
    }

    public static string ToAbandoned(this NavigationManager navigationManager)
    {
        var builder = new UriBuilder(navigationManager.BaseUri)
        {
            Path = Routes.Products.Abandoned,
        };
        return builder.Uri.AbsoluteUri;
    }

    public static void ToCheckout(this NavigationManager navigationManager) =>
        navigationManager.NavigateTo(Routes.Checkout.Page);
}