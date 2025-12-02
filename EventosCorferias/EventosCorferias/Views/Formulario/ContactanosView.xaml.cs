using EventosCorferias.ViewModel.Formulario;

namespace EventosCorferias.Views.Formulario;

public partial class ContactanosView : ContentPage
{
    public ContactanosView()
    {
        InitializeComponent();
        BindingContext = new ContactanosVm();
    }
}