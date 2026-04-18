using InventoryApp.ViewModels;

namespace InventoryApp.Views;

public partial class ArtistsPage : ContentPage
{
    public ArtistsPage(ArtistsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ArtistsViewModel vm)
            vm.LoadCommand.Execute(null);
    }
}
