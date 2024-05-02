using Blazored.LocalStorage;

namespace FuGetsu.Ui.Core.Storages;

public interface IUserStorage
{
    Task<bool> GetIsDarkModeAsync(CancellationToken cancellationToken);

    Task SetIsDarkModeAsync(bool isDarkMode, CancellationToken cancellationToken);
}

internal sealed class UserStorage : IUserStorage
{
    private const string DarkModeKey = "ct-settings-dark-mode";

    private readonly ILocalStorageService _localStorageService;

    public UserStorage(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<bool> GetIsDarkModeAsync(CancellationToken cancellationToken)
    {
        return await _localStorageService.GetItemAsync<bool>(DarkModeKey, cancellationToken);
    }

    public async Task SetIsDarkModeAsync(bool isDarkMode, CancellationToken cancellationToken)
    {
        await _localStorageService.SetItemAsync(DarkModeKey, isDarkMode, cancellationToken);
    }
}