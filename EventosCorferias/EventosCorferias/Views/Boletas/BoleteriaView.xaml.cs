namespace EventosCorferias.Views.Boletas;

using EventosCorferias.ViewModel.Boletas;

public partial class BoleteriaView : ContentPage
{
    public BoleteriaView()
    {
        InitializeComponent();
        BindingContext = new BoleteriaVm();
    }

    //private void GenerarCodigoQR_Clicked(object sender, EventArgs e)
    //{
    //    if (BindingContext is BoleteriaVm viewModel)
    //    {
    //        viewModel.generQr_Clicked(e.ToString());
    //    }
    //}
}