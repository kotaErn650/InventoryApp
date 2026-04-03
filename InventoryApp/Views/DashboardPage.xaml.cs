namespace InventoryApp.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    private async void GoProducts(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("products");
    }

    private async void GoInicio(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Menu");
    }


}