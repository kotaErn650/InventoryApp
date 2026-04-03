using System;
using System.Collections.Generic;
using System.Text;

using InventoryApp.Commands;

namespace InventoryApp.ViewModels;

public class DashboardViewModel : BaseViewModel
{
    public RelayCommand GoProductsCommand { get; }

    public DashboardViewModel()
    {
        GoProductsCommand = new RelayCommand(async _ => await GoProducts());
    }

    private async Task GoProducts()
    {
        await Shell.Current.GoToAsync("products");
    }
}