using EventosCorferias.Interfaces;
using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.Services;
using Mopups.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Input;

namespace EventosCorferias.Views.PopUp;

public partial class CambioContrasenaPopUp
{
    private readonly LogicaWs logicaWs;
    private readonly IPageServicio pageServicio;

    private readonly string EmailUsuario;

    private string _entClaveNueva = "";
    private string _enClaveNueva2 = "";
    private string _entClaveAntigua = "";

    private bool _stateconexion;

    public ICommand ClaveNueva { get; }
    public ICommand ClaveAntigua { get; }

    public string EntClaveAntigua
    {
        get { return _entClaveAntigua; }
        set { _entClaveAntigua = value; OnPropertyChanged(nameof(EntClaveAntigua)); }
    }

    public string EntClaveNueva
    {
        get { return _entClaveNueva; }
        set { _entClaveNueva = value; OnPropertyChanged(nameof(EntClaveNueva)); }
    }

    public string EnClaveNueva2
    {
        get { return _enClaveNueva2; }
        set { _enClaveNueva2 = value; OnPropertyChanged(nameof(EnClaveNueva2)); }
    }

    public bool StateConexion
    {
        get { return _stateconexion; }
        set { _stateconexion = value; OnPropertyChanged(nameof(StateConexion)); }
    }

    public CambioContrasenaPopUp()
    {
        InitializeComponent();
        BindingContext = this;

        logicaWs = new LogicaWs();
        pageServicio = new PageServicio();

        EmailUsuario = Preferences.Get("Email", "");

        SMante1.IsVisible = false;
        seccion1.IsVisible = true;
        seccion2.IsVisible = false;
        SMante2.IsVisible = false;
        IsPasword2.IsPassword = true;
        IsPasword3.IsPassword = true;
        IsPasword1.IsPassword = true;
        campoVacio1.IsVisible = false;
        campoVacio2.IsVisible = false;
        ClaveIncorrecta.IsVisible = false;
        ClaveNoConincide.IsVisible = false;

        ImageButton1.Source = "ic_ocultar.png";
        ImageButton2.Source = "ic_ocultar.png";
        ImageButton3.Source = "ic_ocultar.png";
    }

    private async void ClaveAntigua_Mto(object sender, EventArgs e)
    {
        if (!StateConexion)
        {
            try
            {
                if (EntClaveAntigua != "" && EntClaveAntigua != null && EntClaveAntigua != " ")
                {
                    string jsonProcedimiento;
                    Clave clave = new Clave { Contrasena = EntClaveAntigua };
                    string urli = logicaWs.Movile_Consulta_Login_Mtd(EmailUsuario);
                    string json = JsonConvert.SerializeObject(clave);
                    jsonProcedimiento = await logicaWs.ConectionPost(json, urli);

                    if (jsonProcedimiento == "Credenciales Incorrectos")
                    {
                        ClaveIncorrecta.IsVisible = true;
                        SMante1.IsVisible = false;
                        campoVacio1.IsVisible = false;
                    }
                    else
                    {
                        if (jsonProcedimiento == "Usuario no existe")
                        {
                            ClaveIncorrecta.IsVisible = true;
                            SMante1.IsVisible = false;
                            campoVacio1.IsVisible = false;
                        }
                        else
                        {
                            try
                            {
                                JArray jsArray = JArray.Parse(jsonProcedimiento);
                                foreach (JObject item in jsArray)
                                {
                                    string Email = item.GetValue("Email")?.ToString() ?? string.Empty;
                                    string contrasena = item.GetValue("contrasena")?.ToString() ?? string.Empty;
                                }

                                seccion2.IsVisible = true;
                                seccion1.IsVisible = false;
                                ClaveIncorrecta.IsVisible = false;
                                SMante1.IsVisible = false;
                                campoVacio1.IsVisible = false;

                            }
                            catch
                            {
                                ClaveIncorrecta.IsVisible = false;
                                SMante1.IsVisible = true;
                                campoVacio1.IsVisible = false;
                            }
                        }
                    }
                }
                else
                {
                    SMante1.IsVisible = false;
                    ClaveIncorrecta.IsVisible = false;
                    campoVacio1.IsVisible = true;
                }

            }
            catch
            {
                ClaveIncorrecta.IsVisible = false;
                SMante1.IsVisible = true;
                campoVacio1.IsVisible = false;
            }

        }
        else
        {
            await MopupService.Instance.PopAsync();
            await pageServicio.DisplayAlert("Corferias", AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
        }

    }

    private async void ClaveNueva_Mto(object sender, EventArgs e)
    {
        if (!StateConexion)
        {
            try
            {
                if (EntClaveNueva != "" && EntClaveNueva != null && EntClaveNueva != " " &&
                    EnClaveNueva2 != "" && EnClaveNueva2 != null && EnClaveNueva2 != " ")
                {
                    if (EntClaveNueva == EnClaveNueva2)
                    {
                        string jsonProcedimiento;
                        char[] charsToTrim = { ' ' };


                        if (EntClaveNueva.Length < 6)
                        {
                            await pageServicio.DisplayAlert("CorferiasApp", AppResources.VMLaContrasenaDebeTenerMinimoMas, AppResources.VMaceptar);
                        }
                        else
                        {
                            string urli = logicaWs.Movile_Cambio_Clave_Mtd(EmailUsuario);

                            Clave clave = new Clave();
                            clave.Contrasena = EntClaveNueva;

                            string json = JsonConvert.SerializeObject(clave);
                            jsonProcedimiento = await logicaWs.ConectionPost(json, urli);

                            if (jsonProcedimiento == "Valores actualizados")
                            {
                                ClaveNoConincide.IsVisible = false;
                                SMante2.IsVisible = false;
                                campoVacio2.IsVisible = false;

                                await MopupService.Instance.PopAsync();
                                await pageServicio.DisplayAlert("CorferiasApp", AppResources.VMLaContrasenaHaSidoActualizada, AppResources.VMaceptar);
                            }
                            else
                            {
                                ClaveNoConincide.IsVisible = false;
                                SMante2.IsVisible = true;
                                campoVacio2.IsVisible = false;
                            }
                        }
                    }
                    else
                    {
                        ClaveNoConincide.IsVisible = true;
                        SMante2.IsVisible = false;
                        campoVacio2.IsVisible = false;
                    }
                }
                else
                {
                    ClaveNoConincide.IsVisible = false;
                    SMante2.IsVisible = false;
                    campoVacio2.IsVisible = true;
                }

            }
            catch
            {
                ClaveNoConincide.IsVisible = false;
                SMante2.IsVisible = true;
            }
        }
        else
        {
            await MopupService.Instance.PopAsync();
            await pageServicio.DisplayAlert("CorferiasApp", AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
        }
    }

    private void ImageButton_Clicked1(object sender, EventArgs e)
    {
        if (IsPasword1.IsPassword == true)
        {
            IsPasword1.IsPassword = false;
            ImageButton1.Source = "ic_mostrar_obscuro.png";

        }
        else
        {
            IsPasword1.IsPassword = true;
            ImageButton1.Source = "ic_ocultar_obscuro.png";

        }
    }

    private void ImageButton_Clicked2(object sender, EventArgs e)
    {
        if (IsPasword2.IsPassword == true)
        {
            IsPasword2.IsPassword = false;
            ImageButton2.Source = "ic_mostrar_obscuro.png";

        }
        else
        {
            IsPasword2.IsPassword = true;
            ImageButton2.Source = "ic_ocultar_obscuro.png";

        }
    }

    private void ImageButton_Clicked3(object sender, EventArgs e)
    {
        if (IsPasword3.IsPassword == true)
        {
            IsPasword3.IsPassword = false;
            ImageButton3.Source = "ic_mostrar_obscuro.png";

        }
        else
        {
            IsPasword3.IsPassword = true;
            ImageButton3.Source = "ic_ocultar_obscuro.png";

        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await pageServicio.DisplayAlert("CorferiasApp", AppResources.VMLaContrasenaDebeTenerMinimoMas, AppResources.VMaceptar);
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        try
        {
            await MopupService.Instance.PopAsync();
        }
        catch
        {

        }
    }

    public class Clave
    {
        public string? Contrasena
        {
            get; set;
        }
    }
}