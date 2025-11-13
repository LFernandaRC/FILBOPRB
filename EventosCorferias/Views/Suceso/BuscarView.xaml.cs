using EventosCorferias.ViewModel.Suceso;

namespace EventosCorferias.Views.Suceso;

public partial class BuscarView : ContentPage
{
    public BuscarView()
    {
        InitializeComponent();
        BindingContext = new BusquedaVm(EntryBsq, PickerRutaBusqueda);
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        EntryBsq.Unfocus();
        PickerRutaBusqueda.Unfocus();
    }

    private void EntryBsq_Completed(object sender, EventArgs e)
    {
        EntryBsq.Unfocus();
        PickerRutaBusqueda.Unfocus();
    }

}