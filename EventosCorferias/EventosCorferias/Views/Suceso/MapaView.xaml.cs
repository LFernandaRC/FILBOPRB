using EventosCorferias.ViewModel.Suceso;

namespace EventosCorferias.Views.Suceso;

public partial class MapaView : ContentPage
{
    public MapaView(string Id, string origen, string IdModulo, string NombreVista, string bandera)
    {
        InitializeComponent();
        BindingContext = new MapaVM(Id, origen, IdModulo, NombreVista, bandera);
    }
}