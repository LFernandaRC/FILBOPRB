using EventosCorferias.Models;
using EventosCorferias.ViewModel.Shared;

namespace EventosCorferias.Views.Boletas;

public partial class ContenidosWebPost : ContentPage
{
    public ContenidosWebPost(Pagos pagos)
    {
        InitializeComponent();
        BindingContext = new GeneralVm();
        CustomWeb.Url = "https://extranet.corferias.com/servicio/reg_pag/";
        CustomWeb.Pagos = pagos;
    }
}