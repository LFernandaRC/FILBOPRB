using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.ViewModel.Suceso;

namespace EventosCorferias.Views.Suceso;

public partial class CuponesView : ContentPage
{
	public CuponesView(bool Favorito, string idExpositor, string idCupon)
	{
        InitializeComponent();
        BindingContext = new CuponesVm(Favorito, idExpositor, idCupon);
    }

    protected override bool OnBackButtonPressed()
    {
        bool result = true;
        var vw = BindingContext as CuponesVm;
        if (vw?.pageServicio == null)
            return true;

        Dispatcher.Dispatch(async () =>
        {
            var a = await vw.pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.seguroQuiereCerrarLaAplicacion, AppResources.VMaceptar, AppResources.VMCancelar);

            if (a)
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            else
                result = true;
        });
        return result;
    }
}