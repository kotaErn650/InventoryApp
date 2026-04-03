using InventoryApp.Commands;

namespace InventoryApp.ViewModels;

public class DashboardViewModel : BaseViewModel
{
    public RelayCommand GoProductsCommand { get; }
    public RelayCommand GoInicioCommand { get; }

    public DashboardViewModel()
    {
        GoProductsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("products"));
        GoInicioCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("//dashboard"));
    }
}