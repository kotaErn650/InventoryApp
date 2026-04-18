using InventoryApp.Commands;

namespace InventoryApp.ViewModels;

public class ConfiguracionViewModel : BaseViewModel
{
    private bool _notificacionesActivas = true;
    public bool NotificacionesActivas
    {
        get => _notificacionesActivas;
        set => SetProperty(ref _notificacionesActivas, value);
    }

    private bool _modoOscuro;
    public bool ModoOscuro
    {
        get => _modoOscuro;
        set => SetProperty(ref _modoOscuro, value);
    }

    public RelayCommand GoInicioCommand { get; }
    public RelayCommand GoProductsCommand { get; }
    public RelayCommand GoProveedoresCommand { get; }
    public RelayCommand GoConfiguracionCommand { get; }

    public ConfiguracionViewModel()
    {
        GoInicioCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("//dashboard"));
        GoProductsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("products"));
        GoProveedoresCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("proveedores"));
        GoConfiguracionCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("configuracion"));
    }
}
