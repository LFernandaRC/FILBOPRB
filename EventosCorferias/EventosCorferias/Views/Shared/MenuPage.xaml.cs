using EventosCorferias.Interfaces;
using EventosCorferias.Models;
using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.Services;
using EventosCorferias.ViewModel.Shared;
using EventosCorferias.Views.Formulario;
using EventosCorferias.Views.PopUp;
using EventosCorferias.Views.Suceso;
using Mopups.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace EventosCorferias.Views.Usuario;

public partial class MenuPage : ContentPage
{
    private readonly LogicaWs logicaWS;
    private readonly ClaseBase claseBase;
    private readonly IPageServicio pageServicio;
    private readonly string EmailUsuario = "";

    private ObservableCollection<ListasGeneral> ListaIdiomas { get; set; }

    MasterHomePage? RootMainPage => Application.Current.MainPage as MasterHomePage;

    public MenuPage()
    {
        try
        {
            InitializeComponent();
            BindingContext = this;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            SubGrupo.IsVisible = false;
            imgf.Source = logicaWS.ImgMenuSuperior_Mtd();
            EmailUsuario = Preferences.Get("Email", "");
            NombreCompletoP.Text = Preferences.Get("NombreCompleto", "");

            _ = RecuperarToken();
            CargarListaIdiomaAsync();

        }
        catch (Exception e)
        {
            claseBase.InsertarLogs_Mtd("ERROR", e.Message, "MenuPage", "MenuPage", "Inicializar menu");
        }
    }

    private async void Options(object sender, EventArgs e)
    {
        try
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar

            var button = (StackLayout)sender;
            var classId = button.ClassId;
            RootMainPage.IsPresented = false;
            switch (classId)
            {
                case "Perfil":
                    RootMainPage.Detail = new NavigationPage(new PerfilMenuView());
                    break;
                case "Destacados":
                    NoDisponible();
                    break;
                case "Notificaciones":
                    RootMainPage.Detail = new NavigationPage(new NotificacionesView("0"));
                    break;
                case "ComoLlegar":
                    Comollegar();
                    break;
                case "Contacto":
                    RootMainPage.Detail = new NavigationPage(new ContactanosView());
                    break;
                case "Reservar":
                    NoDisponible();
                    break;
                case "Idioma":
                    await MopupService.Instance.PushAsync(new ListaMenuPopUp(ListaIdiomas));
                    break;
                case "Buscar":
                    RootMainPage.Detail = new NavigationPage(new BuscarView());
                    break;
                case "MContenido":
                    RootMainPage.Detail = new NavigationPage(new ContenidosPage(true, "favorito", "0", "Menu"));
                    break;
                case "MAgenda":
                    RootMainPage.Detail = new NavigationPage(new AgendaView("0", "0", "", true, "0", false, "0"));
                    break;
                case "MConferenciasta":
                    RootMainPage.Detail = new NavigationPage(new ConferencistaView("0", true));
                    break;
                case "MExpositor":
                    RootMainPage.Detail = new NavigationPage(new ExpositorView("0", true));
                    break;
                case "EliminarUsu":
                    bool reps = await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.SeguroEliminarUsu, AppResources.eliminar, AppResources.VMCancelar);
                    if (reps)
                    {
                        string urli = logicaWS.Movile_modAEliminaUsuario("4");
                        UsuEliman EliminaUsu = new UsuEliman
                        {
                            Email = Preferences.Get("Email", "")
                        };
                        string json = JsonConvert.SerializeObject(EliminaUsu);
                        string jsonProcedimiento = await logicaWS.ConectionPost(json, urli);
                        if (!string.IsNullOrWhiteSpace(jsonProcedimiento))
                        {
                            if (jsonProcedimiento.Contains("Usuario eliminado"))
                            {
                                Preferences.Remove("NombreCompleto");
                                Preferences.Remove("Email");
                                Preferences.Remove("IdCiudad");
                                Preferences.Remove("IdCiudadDesc");
                                Preferences.Remove("RedSocial");
                                Preferences.Clear();
                                await pageServicio.PushModalAsync(new Splash());
                            }
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, jsonProcedimiento, "Aceptar");
                        }
                        else
                        {
                            claseBase.InsertarLogs_Mtd("ERROR", "RESPUESTA VACIA", "MenuPage", "Options", "modAEliminaUsuario");
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                        }
                    }
                    break;
            }
        }
        catch (Exception es)
        {
            claseBase.InsertarLogs_Mtd("ERROR", es.Message, "MenuPage", "Options", "Opciones del menu");
            await pageServicio.DisplayAlert(AppResources.nombreMarca, es.Message, AppResources.VMaceptar);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async void CargarListaIdiomaAsync()
    {
        try
        {
            ListaIdiomas = new ObservableCollection<ListasGeneral>();
            string urli = logicaWS.Movile_select_Idioma_Mtd();
            string jsonProcedimiento = await logicaWS.ConectionGet(urli);
            JArray jsArray = JArray.Parse(jsonProcedimiento);

            foreach (JObject item in jsArray)
            {
                ListasGeneral listasGeneral = new ListasGeneral
                {
                    Descripcion = item.GetValue("NombreIdioma")?.ToString(),
                    Id = item.GetValue("CodigoIdioma")?.ToString()
                };
                ListaIdiomas.Add(listasGeneral);
            }
        }
        catch (Exception e)
        {
            claseBase.InsertarLogs_Mtd("ERROR", e.Message, "MenuPage", "CargarListaIdiomaAsync", "consultaridioma");
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if (SubGrupo.IsVisible)
        {
            flechaSub.Source = "ic_flechaabajo_obscuro.png";
            SubGrupo.IsVisible = false;
        }
        else
        {
            flechaSub.Source = "ic_flechaarriba_obscuro.png";
            SubGrupo.IsVisible = true;
        }
    }

    private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        try
        {
            try
            {
                string s2 = "corferias@ios.com";
                bool res = EmailUsuario.ToLower().Contains(s2.ToLower());

                string s3 = "appleid.com";
                bool res3 = EmailUsuario.ToLower().Contains(s3.ToLower());

                if (res || res3)
                {
                    bool resulDos = await pageServicio.DisplayAlertDos(AppResources.nombreMarca, AppResources.ActualizaDatosPerfil, AppResources.VMaceptar);

                    if (resulDos)
                    {
                        bool resul = await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.SeguroCerrarSesion, AppResources.VMaceptar, AppResources.VMCancelar);
                        if (resul)
                        {
                            Preferences.Remove("NombreCompleto");
                            Preferences.Remove("Email");
                            Preferences.Remove("IdCiudad");
                            Preferences.Remove("IdCiudadDesc");
                            Preferences.Remove("RedSocial");
                            await pageServicio.PushModalAsync(new Splash());
                        }
                    }
                }
                else
                {
                    bool resul = await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.SeguroCerrarSesion, AppResources.VMaceptar, AppResources.VMCancelar);
                    if (resul)
                    {
                        Preferences.Remove("NombreCompleto");
                        Preferences.Remove("Email");
                        Preferences.Remove("IdCiudad");
                        Preferences.Remove("IdCiudadDesc");
                        Preferences.Remove("RedSocial");
                        await pageServicio.PushModalAsync(new Splash());
                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "MenuPage", "TapGestureRecognizer_Tapped_1", "validacion iOS correo");
            }
        }
        catch (Exception ex)
        {
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "MenuPage", "TapGestureRecognizer_Tapped_1", "n/a");
        }
    }

    private async void Comollegar()
    {
        try
        {
            string urliDos = logicaWS.Movile_Select_Lista_Recintos_Mtd();
            string jsonProcedimientoDos = await logicaWS.ConectionGet(urliDos);
            JArray jsArrayDos = JArray.Parse(jsonProcedimientoDos);
            foreach (JObject itemDos in jsArrayDos)
            {
                string urli = logicaWS.Movile_Select_Recintos_C_Mtd(itemDos.GetValue("idRecinto").ToString());
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    string lat = item.GetValue("latitud").ToString();
                    string lng = item.GetValue("longitud").ToString();
                    string url = $"https://www.google.com/maps/search/?api=1&query={lat.ToString().Replace(",", ".")},{lng.ToString().Replace(",", ".")}";
                    await Launcher.OpenAsync(new Uri(url));
                }
                break;
            }
        }
        catch (Exception ex)
        {
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "MenuPage", "Comollegar", "n/a");
        }
    }

    private void NoDisponible()
    {
        pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.FuncionalidadNoDisponible, AppResources.cerrar);
    }

    public async Task RecuperarToken()
    {
        try
        {
            string Token = Preferences.Get("TokenApp", "");
            string Correo = Preferences.Get("Email", "");

            if (!Token.Equals("") && !Correo.Equals(""))
            {
                string urli = logicaWS.Movile_Update_Token_Mtd(Correo, Token);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);

                if (!jsonProcedimiento.Equals("Datos Actualizados"))
                    claseBase.InsertarLogs_Mtd("ERROR", jsonProcedimiento, "MenuPage", "RecuperarToken1", "n/a");
            }
        }
        catch (Exception e)
        {
            claseBase.InsertarLogs_Mtd("ERROR", e.Message, "MenuPage", "RecuperarToken2", "n/a");
        }
    }

}


public class UsuEliman
{
    public string? Email { get; set; }
}