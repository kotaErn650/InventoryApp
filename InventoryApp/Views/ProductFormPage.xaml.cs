using  InventoryApp.ViewModels;

namespace InventoryApp.Views;

public partial class ProductFormPage : ContentPage
{
	public ProductFormPage(ProductFormViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}


}