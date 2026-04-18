using InventoryApp.ViewModels;

namespace InventoryApp.Views;

public partial class EventFormPage : ContentPage
{
    public EventFormPage(EventFormViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
