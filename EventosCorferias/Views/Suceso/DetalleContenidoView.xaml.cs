using EventosCorferias.Models;
using EventosCorferias.ViewModel.Suceso;

namespace EventosCorferias.Views.Suceso;

public partial class DetalleContenidoView : ContentPage
{
    public DetalleContenidoView(Contenidos contenidos, string IdSitios, string vista)
    {
        InitializeComponent();
        BindingContext = new DetalleContenidoVm(contenidos, IdSitios, vista);
    }
}