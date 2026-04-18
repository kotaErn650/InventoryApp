using InventoryApp.Views;

namespace InventoryApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("events", typeof(EventsPage));
        Routing.RegisterRoute("eventform", typeof(EventFormPage));
        Routing.RegisterRoute("artists", typeof(ArtistsPage));
        Routing.RegisterRoute("artistform", typeof(ArtistFormPage));
        Routing.RegisterRoute("settings", typeof(SettingsPage));
    }
}
