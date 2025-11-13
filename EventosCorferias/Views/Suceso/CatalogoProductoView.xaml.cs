using EventosCorferias.ViewModel.Suceso;

namespace EventosCorferias.Views.Suceso;

public partial class CatalogoProductoView : ContentPage
{
	public CatalogoProductoView(string idExpositor)
	{
		InitializeComponent();
        BindingContext = new CatalogoProductoVm(idExpositor, FiltroEntry);
    }
}