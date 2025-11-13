namespace EventosCorferias.Views.Usuario;

public partial class Splash : ContentPage
{
	public Splash()
	{
		InitializeComponent();
		BindingContext = new ViewModel.Shared.Splash();
	}
}