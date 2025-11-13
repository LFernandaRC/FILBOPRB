using EventosCorferias.Models;
using EventosCorferias.ViewModel.Suceso;

namespace EventosCorferias.Views.Usuario;

public partial class DetalleAgendaView : ContentPage
{
	public DetalleAgendaView(Agenda content)
	{
		InitializeComponent();
        //CerrarModal();
        BindingContext = new DetalleAgendaVm(content);
    }

    //private async void CerrarModal()
    //{
    //    try
    //    {
    //        await Navigation.PopPopupAsync();
    //    }
    //    catch { }
    //}
}