using EventosCorferias.Interfaces;
using EventosCorferias.Models;
using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.Services;
using EventosCorferias.Views.Usuario;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Globalization;

namespace EventosCorferias.Views.PopUp;

public partial class ListaMenuPopUp
{
    private readonly ClaseBase claseBase;

    public ListaMenuPopUp(ObservableCollection<ListasGeneral> ListaIdiomas)
    {
        InitializeComponent();
        claseBase = new ClaseBase();
        ListaMenu.ItemsSource = ListaIdiomas;
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        try
        {
            await MopupService.Instance.PopAsync();
        }
        catch (Exception ex)
        {
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ListaMenuPopUp", "Button_OnClicked", "Cierra popup");
        }
    }

    private async void IdiomaSeleccionado_Tapped(object sender, EventArgs e)
    {
        var frame = sender as Frame;
        if (frame?.BindingContext is ListasGeneral idiomaSeleccionado)
        {
            try
            {
                await MopupService.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ListaMenuPopUp", "IdiomaSeleccionado_Tapped", "Cierra popup");
            }
            Preferences.Set("IdiomaDefecto", idiomaSeleccionado.Id);
            ActualizarIdiomas();
            IPageServicio pageServicio = new PageServicio();
            await pageServicio.PushModalAsync(new Splash());
        }
    }

    public void ActualizarIdiomas()
    {
        try
        {
            string lenguaje = Preferences.Get("IdiomaDefecto", "es");
            CultureInfo ci;

            switch (lenguaje)
            {
                case "en":
                    ci = new CultureInfo("en-US");
                    Preferences.Set("IdiomaDefecto", "en");
                    Preferences.Set("IdiomaApp", "en-US");
                    break;
                case "es":
                    ci = new CultureInfo("es-ES");
                    Preferences.Set("IdiomaApp", "es-ES");
                    Preferences.Set("IdiomaDefecto", "es");
                    break;
                default:
                    ci = new CultureInfo("es-ES");
                    Preferences.Set("IdiomaDefecto", "es");
                    Preferences.Set("IdiomaApp", "es-ES");
                    break;
            }

            // Asignar cultura en toda la aplicación
            AppResources.Culture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;

        }
        catch (Exception ex)
        {
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ListaMenuPopUp", "ActualizarIdiomas", "n/a");
        }
    }

}