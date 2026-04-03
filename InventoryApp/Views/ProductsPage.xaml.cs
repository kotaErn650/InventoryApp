using InventoryApp.ViewModels;

namespace InventoryApp.Views;

public partial class ProductsPage : ContentPage
{
    public ProductsPage(ProductsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ProductsViewModel vm)
            vm.LoadCommand.Execute(null);
    }
}