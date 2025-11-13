using EventosCorferias.ViewModel.Usuario;

namespace EventosCorferias.Views.Usuario;

public partial class NotificacionesView : ContentPage
{
	public NotificacionesView(string IdComunidad)
    {
		InitializeComponent();
        BindingContext = new NotificacionesVm(IdComunidad);
    }
}