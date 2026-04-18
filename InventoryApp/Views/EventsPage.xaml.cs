using InventoryApp.ViewModels;

namespace InventoryApp.Views;

public partial class EventsPage : ContentPage
{
    public EventsPage(EventsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is EventsViewModel vm)
            vm.LoadCommand.Execute(null);
    }
}
