using EventosCorferias.Models;
using EventosCorferias.ViewModel.Suceso;

namespace EventosCorferias.Views.Suceso;

public partial class DetalleConferencistaView : ContentPage
{
    public DetalleConferencistaView(Conferencista conferencista)
    {
        InitializeComponent();
        BindingContext = new DetalleConferencistaVm(conferencista);
    }
}