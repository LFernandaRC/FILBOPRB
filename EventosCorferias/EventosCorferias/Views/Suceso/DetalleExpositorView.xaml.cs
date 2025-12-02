using EventosCorferias.Models;
using EventosCorferias.ViewModel.Suceso;

namespace EventosCorferias.Views.Suceso;

public partial class DetalleExpositorView : ContentPage
{
    public DetalleExpositorView(Expositores expositores)
    {
        InitializeComponent();
        BindingContext = new DetalleExpositorVm(expositores);
    }
}