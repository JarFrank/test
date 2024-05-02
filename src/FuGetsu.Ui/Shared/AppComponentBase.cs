using FuGetsu.Ui.Core.Notifications;
using Microsoft.AspNetCore.Components;

namespace FuGetsu.Ui.Shared;

public abstract class AppComponentBase : ComponentBase, IDisposable
{
    [Inject] public required ISnackbarService SnackbarService { get; set; }

    private CancellationTokenSource? _cancellationTokenSource;

    protected CancellationToken ComponentDetached => (_cancellationTokenSource ??= new CancellationTokenSource()).Token;

    protected virtual void OnComponentDispose()
    { }

    public void Dispose()
    {
        OnComponentDispose();
        if (_cancellationTokenSource is null)
        {
            return;
        }

        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
        _cancellationTokenSource = null;
        GC.SuppressFinalize(this);
    }
}