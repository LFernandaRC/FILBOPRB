using EventosCorferias.ViewModel.Suceso;

namespace EventosCorferias.Views.Suceso;

public partial class SucesoView : ContentPage
{
    public SucesoView()
    {
        InitializeComponent();
        BindingContext = new SucesoVm();
        Title = "";
    }
}