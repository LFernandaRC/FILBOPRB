using EventosCorferias.ViewModel.Boletas;

namespace EventosCorferias.Views.Boletas;

public partial class ResumenCompraView : ContentPage
{
	public ResumenCompraView(string IdTransaccion, string tipoPublico)
	{
		InitializeComponent();
		BindingContext = new ResumenCompraVm(IdTransaccion, tipoPublico);
    }
}