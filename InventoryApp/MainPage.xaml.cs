namespace InventoryApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;
            CounterBtn.Text = count == 1
                ? "✨ Branding listo para eventos"
                : $"✨ Revisión visual #{count}";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnHelpSoporClicked(object? sender, EventArgs e)
        {
            if (Shell.Current is not null)
                await Shell.Current.GoToAsync("configuracion");
        }
    }
}
