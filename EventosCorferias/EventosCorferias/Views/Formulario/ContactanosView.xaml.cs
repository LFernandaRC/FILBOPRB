using EventosCorferias.ViewModel.Formulario;

namespace EventosCorferias.Views.Formulario;

public partial class ContactanosView : ContentPage
{
    public ContactanosView()
    {
        InitializeComponent();
        BindingContext = new ContactanosVm();
    }

    private async void DescripcionEditor_Focused(object sender, FocusEventArgs e)
    {
        await Task.Delay(100); // deja aparecer teclado
        await MainScroll.ScrollToAsync(
            DescripcionEditor,
            ScrollToPosition.Start,
            true);
    }
}