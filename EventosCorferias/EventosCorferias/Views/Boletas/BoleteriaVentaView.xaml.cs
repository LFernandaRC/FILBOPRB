using EventosCorferias.ViewModel.Boletas;

namespace EventosCorferias.Views.Boletas;

public partial class BoleteriaVentaView : ContentPage
{
    public BoleteriaVentaView(string idSuceso)
    {
        InitializeComponent();
        BindingContext = new BoleteriaVentaVm(idSuceso);
    }
}