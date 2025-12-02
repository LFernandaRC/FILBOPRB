using EventosCorferias.Services;
using EventosCorferias.Models;
using EventosCorferias.Views.Usuario;

using Newtonsoft.Json;
using System.Globalization;
using EventosCorferias.Resources.RecursosIdioma;
using Newtonsoft.Json.Linq;
using EventosCorferias.Views.Shared;

namespace EventosCorferias.ViewModel.Shared
{
    public class Splash : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        private readonly ClaseBase claseBase;

        private Color colorImagen;

        private bool _Gif = false;
        private bool _VerSmsError = false;
        private string? _smsError = "";
        private string? _UrlImagen = "";

        public bool Gif
        {
            get { return _Gif; }
            set { _Gif = value; OnPropertyChanged(nameof(Gif)); }
        }
      
        public bool VerSmsError
        {
            get { return _VerSmsError; }
            set { _VerSmsError = value; OnPropertyChanged(nameof(VerSmsError)); }
        }

        public string? SmsError
        {
            get { return _smsError; }
            set { _smsError = value; OnPropertyChanged(nameof(SmsError)); }
        }

        public string? UrlImagen
        {
            get { return _UrlImagen; }
            set { _UrlImagen = value; OnPropertyChanged(nameof(UrlImagen)); }
        }

        public Color ColorImagen
        {
            get => colorImagen;
            set
            {
                if (colorImagen != value)
                {
                    colorImagen = value;
                    OnPropertyChanged(nameof(ColorImagen));
                }
            }
        }

        public Splash()
        {
            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            colorImagen = new Color();
            try
            {
                Preferences.Set("IdApp", "1");

                CargarIdiomaPredetermiando();
                Validar_Conexion_Internet();
                Application.Current.UserAppTheme = AppTheme.Light;
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SplashVM", "Splash", "n/a");
                Preferences.Set("IdiomaApp", "es-ES");
            }
        }

        private void CargarIdiomaPredetermiando()
        {
            try
            {
                string IdiomaAux = "es-ES";

                if (!string.IsNullOrWhiteSpace(Preferences.Get("IdiomaApp", "")))
                {
                    if (Preferences.Get("IdiomaApp", "").ToString() == "en-US")
                    {
                        IdiomaAux = "en-US";
                        Preferences.Set("IdiomaApp", "en-US");
                        Preferences.Set("IdiomaDefecto", "en");
                    }
                    else
                    {
                        IdiomaAux = "es-ES";
                        Preferences.Set("IdiomaApp", "es-ES");
                        Preferences.Set("IdiomaDefecto", "es");
                    }
                    ;
                }
                else
                {
                    string idioma = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

                    if (idioma == "en")
                    {
                        Preferences.Set("IdiomaApp", "en-US");
                        Preferences.Set("IdiomaDefecto", "en");
                        IdiomaAux = "en-US";
                    }
                    else
                    {
                        Preferences.Set("IdiomaApp", "es-ES");
                        Preferences.Set("IdiomaDefecto", "es");
                    }
                }

                LenguajeBase = Preferences.Get("IdiomaDefecto", "");
                AppResources.Culture = new CultureInfo(IdiomaAux);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(IdiomaAux);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(IdiomaAux);
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(IdiomaAux);
                CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(IdiomaAux);
                CultureInfo.CurrentUICulture = new CultureInfo(IdiomaAux);
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SplashVM", "CargarIdiomaPredetermiando", "n/a");
                Preferences.Set("IdiomaApp", "es-ES");
                Preferences.Set("IdiomaDefecto", "es");
            }
        }

        private void Validar_Conexion_Internet()
        {
            try
            {
                bool CelularConInternet = Connectivity.Current.NetworkAccess == NetworkAccess.Internet;

                if (CelularConInternet == true)
                {
                    ValidarIdSuceso();
                }
                else
                {
                    UrlImagen = "logo_splash.png";
                    Gif = false;
                    ColorImagen = Color.FromArgb("#363a47");
                    VerSmsError = true;
                    SmsError = AppResources.ErrorSplahs;
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SplashVM", "Validar_Conexion_Internet", "n/a");
                ValidarIdSuceso();
            }
        }

        public async void ValidarIdSuceso()
        {
            try
            {

                string urlImag = logicaWs.Movile_consultarelacionferiasuceso_Mtd("1", Preferences.Get("IdApp", ""));
                string jsonProcedimiento = await logicaWs.ConectionGet(urlImag);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                if (jsArray.Count > 0)
                {
                    foreach (JObject item in jsArray)
                    {
                        Preferences.Set("IdSuceso", item.GetValue("IdSuceso").ToString());
                        Preferences.Set("FeriaVigente", item.GetValue("FeriaVigente").ToString());
                        CargarImagenAsync();
                        break;
                    }
                }
            }
            catch
            {
                UrlImagen = "logo_splash.png";
                Gif = false;
                ColorImagen = Color.FromArgb("#363a47");
                VerSmsError = true;
                SmsError = AppResources.ErrorSplahs;
            }
        }

        private async void CargarImagenAsync()
        {
            try
            {
                string urlImag = logicaWs.Movile_Splash_Mtd(Preferences.Get("IdApp", ""));
                if (urlImag != null)
                {
                    string jsonProcedimiento = await logicaWs.ConectionGet(urlImag);
                    if (jsonProcedimiento != null)
                    {
                        var Items = JsonConvert.DeserializeObject<List<TodoItem>>(jsonProcedimiento);
                        if (Items != null)
                        {
                            if (Items[0].ExtArchivo.Equals("gif"))
                            {
                                Gif = true;
                            }
                            else
                            {
                                Gif = false;
                            }

                            UrlImagen = Items[0].Imagen;
                            if (string.IsNullOrWhiteSpace(Items[0].ColorFondo))
                            {
                                ColorImagen = Colors.White;
                            }
                            else
                            {
                                ColorImagen = Color.FromArgb(Items[0].ColorFondo);
                            }

                            int segundos = int.Parse(Items[0].TiempoCarga + "000");
                            _ = PasarAsync(segundos);
                        }
                        else
                        {
                            UrlImagen = "logo_splash.png";
                            Gif = false;
                            ColorImagen = Color.FromArgb("#363a47");
                            await PasarAsync(1000);
                        }
                    }
                    else
                    {
                        UrlImagen = "logo_splash.png";
                        Gif = false;
                        ColorImagen = Color.FromArgb("#363a47");
                        await PasarAsync(1000);
                    }
                }
                else
                {
                    UrlImagen = "logo_splash.png";
                    Gif = false;
                    ColorImagen = Color.FromArgb("#363a47");
                    await PasarAsync(1000);
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SplashVM", "CargarImagenAsync", "splashpublicado");
                UrlImagen = "logo_splash.png";
                Gif = false;
                ColorImagen = Color.FromArgb("#363a47");
                await PasarAsync(1000);
            }
        }

        private async Task PasarAsync(int segundos)
        {
            try
            {
                if (segundos < 1000)
                {
                    segundos = 1000;
                }

                await Task.Delay(segundos);

                bool hasKey = Preferences.ContainsKey("MantenerConectado");
                bool hasKey2 = Preferences.ContainsKey("Email");
                if (hasKey && hasKey2)
                {
                    bool mc = Preferences.Get("MantenerConectado", false);
                    if (hasKey)
                    {
                        string email = Preferences.Get("Email", "").Trim();

                        if (!email.Equals(""))
                        {
                            Application.Current!.MainPage = new MasterHomePage();
                            GuardarTokenFirebase();
                        }
                        else
                        {
                            Application.Current!.MainPage = new LoginView();
                        }
                    }
                    else
                    {
                        Application.Current!.MainPage = new LoginView();
                    }
                }
                else
                {
                    Application.Current!.MainPage = new LoginView();
                }
            }
            catch (Exception ex)
            {
                VerSmsError = true;
                SmsError = AppResources.errorSlashdos;
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SplashVM", "PasarAsync", "n/a");
            }
        }

        private async void GuardarTokenFirebase()
        {
            if (Preferences.Get("Email", "") != null && Preferences.Get("TokenFirebase", "") != null)
            {
                var pr = Preferences.Get("Email", "");
                var ds = Preferences.Get("TokenFirebase", "");

                LogicaWs logicaWS = new LogicaWs();
                string urli = logicaWS.Movile_Update_Token_Mtd(Preferences.Get("Email", ""), Preferences.Get("TokenFirebase", "") + "prbFilbo");
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
            }
        }

        private class TodoItem
        {
            public string? IdSplash { get; set; }
            public string? Imagen { get; set; }
            public string? ExtArchivo { get; set; }
            public string? TiempoCarga { get; set; }
            public int Estado { get; set; }
            public string? FechaActiva { get; set; }
            public string? FechaInactiva { get; set; }
            public int Publicar { get; set; }
            public string? FechaPublicacion { get; set; }
            public int IdUsuarioC { get; set; }
            public string? ColorFondo { get; set; }

        }
    }
}
