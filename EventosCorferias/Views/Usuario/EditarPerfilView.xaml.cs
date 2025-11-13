using EventosCorferias.ViewModel.Usuario;

namespace EventosCorferias.Views.Usuario;

public partial class EditarPerfilView : ContentPage
{
    public EditarPerfilView()
    {
        InitializeComponent();
        BindingContext = new EditarPerfilVm();
    }
}