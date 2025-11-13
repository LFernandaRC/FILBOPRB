using EventosCorferias.Interfaces;
using EventosCorferias.Models;
using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.Services;
using EventosCorferias.Views.Suceso;
using EventosCorferias.Views.Usuario;

using Mopups.Services;

namespace EventosCorferias.Views.PopUp;

public partial class FavoritosPopUP
{
    public MasterHomePage RootMainPage => Application.Current.MainPage as MasterHomePage;
    public NavigationPage RootNavigation => RootMainPage?.Detail as NavigationPage;

    private readonly IPageServicio pageServicio;
    private readonly ClaseBase claseBase;

    public FavoritosPopUP()
    {
        InitializeComponent();
        claseBase = new ClaseBase();
        pageServicio = new PageServicio();
        isbusy.IsVisible = false;
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        try
        {
            isbusy.IsVisible = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            RootMainPage.Detail = new NavigationPage(new ContenidosPage(true, "favorito", "0", "Menu"));
            Cerrar();
        }
        catch (Exception ex)
        {
            Cerrar();
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "FvoritosPopUp", "TapGestureRecognizer_Tapped", "redireccionamiento");
            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
        }
    }

    private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        try
        {
            isbusy.IsVisible = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            RootMainPage.Detail = new NavigationPage(new AgendaView("0", "0", "", true, "0", false, "0"));
            Cerrar();
        }
        catch (Exception ex)
        {
            Cerrar();
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "FvoritosPopUp", "TapGestureRecognizer_Tapped_1", "redireccionamiento");
            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
        }
    }

    private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
    {
        try
        {
            isbusy.IsVisible = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            RootMainPage.Detail = new NavigationPage(new ConferencistaView("0", true));
            Cerrar();
        }
        catch (Exception ex)
        {
            Cerrar();
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "FvoritosPopUp", "TapGestureRecognizer_Tapped_2", "redireccionamiento");
            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
        }
    }

    private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
    {
        try
        {
            isbusy.IsVisible = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            RootMainPage.Detail = new NavigationPage(new ExpositorView("0", true));
            Cerrar();
        }
        catch (Exception ex)
        {
            Cerrar();
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "FvoritosPopUp", "TapGestureRecognizer_Tapped_3", "redireccionamiento");
            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
        }
    }


    private void Button_OnClicked(object sender, EventArgs e)
    {
        Cerrar();
    }

    private async void Cerrar()
    {
        try
        {
            await MopupService.Instance.PopAsync();
        }
        catch (Exception ex)
        {
            ClaseBase claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "Favoritos popup", "Cerrar", "Cierra popup");
        }
    }

}