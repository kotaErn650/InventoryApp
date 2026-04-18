using InventoryApp.ViewModels;

namespace InventoryApp.Views;

public partial class ArtistFormPage : ContentPage
{
    public ArtistFormPage(ArtistFormViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
