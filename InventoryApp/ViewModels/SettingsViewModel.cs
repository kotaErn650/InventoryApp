using InventoryApp.Commands;

namespace InventoryApp.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private bool _alertasVenta = true;
    public bool AlertasVenta
    {
        get => _alertasVenta;
        set => SetProperty(ref _alertasVenta, value);
    }

    private bool _sincronizacionAutomatica = true;
    public bool SincronizacionAutomatica
    {
        get => _sincronizacionAutomatica;
        set => SetProperty(ref _sincronizacionAutomatica, value);
    }

    public RelayCommand GoInicioCommand { get; }
    public RelayCommand GoEventsCommand { get; }
    public RelayCommand GoArtistsCommand { get; }
    public RelayCommand GoSettingsCommand { get; }

    public SettingsViewModel()
    {
        GoInicioCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("//dashboard"));
        GoEventsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("events"));
        GoArtistsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("artists"));
        GoSettingsCommand = new RelayCommand(async _ => await GoSettings());
    }

    private static async Task GoSettings()
    {
        var currentRoute = Shell.Current?.CurrentState?.Location?.OriginalString;
        if (currentRoute?.Contains("settings", StringComparison.OrdinalIgnoreCase) == true)
            return;

        if (Shell.Current is not null)
            await Shell.Current.GoToAsync("settings");
    }
}
