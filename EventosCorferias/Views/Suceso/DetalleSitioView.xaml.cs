using EventosCorferias.Models;
using EventosCorferias.ViewModel.Suceso;

namespace EventosCorferias.Views.Suceso;

public partial class DetalleSitioView : ContentPage
{
    SitiosM sitios_;

    public DetalleSitioView(SitiosM sitios)
    {
        InitializeComponent();
        sitios_ = sitios;
        BindingContext = new DetalleSitioVm(sitios_);
    }

}