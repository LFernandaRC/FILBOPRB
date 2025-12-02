using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.ViewModel.Usuario;

namespace EventosCorferias.Views.Usuario;

public partial class PerfilMenuView : ContentPage
{
    public PerfilMenuView()
    {
        InitializeComponent();

        BindingContext = new PerfilMenuVm();
    }


    protected override bool OnBackButtonPressed()
    {
#if ANDROID
        bool result = true;
        var vw = BindingContext as PerfilMenuVm;

        this.Dispatcher.Dispatch(async () =>
        {
            var a = await vw.pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.seguroQuiereCerrarLaAplicacion, AppResources.VMaceptar, AppResources.VMCancelar);

            if (a)
            {

                Application.Current.Quit();

            }
            else
            {
                result = true;
            }
        });

        return result;
#endif
        return false;
    }

}
