using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Usuario;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using EventosCorferias.GoogleAuth;
using Mopups.Services;
using EventosCorferias.Views.PopUp;
using EventosCorferias.ViewModel.Usuario;


#if IOS
using EventosCorferias.Platforms.iOS;
using AuthenticationServices;
#endif

namespace EventosCorferias.ViewModel.Shared
{
    public class LoginVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;

        private UsuarioLogin? usuarioLogin;

        private string? _imagenLogin;
        private string? _colorTexto;
        private string? _imagenFondo;
        private string? _colorFondo;

        private string? _entClave;
        private string? _entCorreo;
        private string? jsonProcedimiento;

        private string? _mesIncio;
        private string? _diaInicio;
        private string? _mesFin;
        private string? _diaFin;
        private string? _AnoFeria;


        private bool _BtnApple;
        private bool _BtnGoogle;
        private bool _ingresoRedes;
        private bool _regresarIcon;
        private bool _matenerConectado;
        private bool _ingresoCorreoClave;


        public string? ImagenLogin
        {
            get { return _imagenLogin; }
            set { _imagenLogin = value; OnPropertyChanged(nameof(ImagenLogin)); }
        }

        public string? ColorTexto
        {
            get { return _colorTexto; }
            set { _colorTexto = value; OnPropertyChanged(nameof(ColorTexto)); }
        }

        public string? ImagenFondo
        {
            get { return _imagenFondo; }
            set { _imagenFondo = value; OnPropertyChanged(nameof(ImagenFondo)); }
        }

        public string? ColorFondo
        {
            get { return _colorFondo; }
            set { _colorFondo = value; OnPropertyChanged(nameof(ColorFondo)); }
        }

        public string? EntClave
        {
            get { return _entClave; }
            set { _entClave = value; OnPropertyChanged(nameof(EntClave)); }
        }

        public string? EntCorreo
        {
            get { return _entCorreo; }
            set { _entCorreo = value; OnPropertyChanged(nameof(EntCorreo)); }
        }

        public string? MesIncio
        {
            get { return _mesIncio; }
            set { _mesIncio = value; OnPropertyChanged(nameof(MesIncio)); }
        }

        public string? DiaInicio
        {
            get { return _diaInicio; }
            set { _diaInicio = value; OnPropertyChanged(nameof(DiaInicio)); }
        }

        public string? MesFin
        {
            get { return _mesFin; }
            set { _mesFin = value; OnPropertyChanged(nameof(MesFin)); }
        }

        public string? DiaFin
        {
            get { return _diaFin; }
            set { _diaFin = value; OnPropertyChanged(nameof(DiaFin)); }
        }

        public string? AnoFeria
        {
            get { return _AnoFeria; }
            set { _AnoFeria = value; OnPropertyChanged(nameof(AnoFeria)); }
        }

        public bool RegresarIcon
        {
            get { return _regresarIcon; }
            set { _regresarIcon = value; OnPropertyChanged(nameof(RegresarIcon)); }
        }

        public bool IngresoRedes
        {
            get { return _ingresoRedes; }
            set { _ingresoRedes = value; OnPropertyChanged(nameof(IngresoRedes)); }
        }

        public bool BtnApple
        {
            get { return _BtnApple; }
            set { _BtnApple = value; OnPropertyChanged(nameof(BtnApple)); }
        }

        public bool BtnGoogle
        {
            get { return _BtnGoogle; }
            set { _BtnGoogle = value; OnPropertyChanged(nameof(BtnGoogle)); }
        }

        public bool MatenerConectado
        {
            get { return _matenerConectado; }
            set { _matenerConectado = value; OnPropertyChanged(nameof(_matenerConectado)); }
        }

        public bool IngresoCorreoClave
        {
            get { return _ingresoCorreoClave; }
            set { _ingresoCorreoClave = value; OnPropertyChanged(nameof(IngresoCorreoClave)); }
        }

        public ICommand Regresar { get; }
        public ICommand LoginCorreo { get; }
        public ICommand Crear_Cuenta { get; }
        public ICommand Olvide_Clave { get; }
        public ICommand Ingresar_Apple { get; }
        public ICommand Ingresar_Correo { get; }
        public ICommand Ingresar_Google { get; }

        public LoginVm()
        {
            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            LenguajeBase = Preferences.Get("IdiomaDefecto", "");

            Regresar = new Command(Regresar_Mto);
            Crear_Cuenta = new Command(Crear_Cuenta_Mtd);
            Ingresar_Correo = new Command(Ingresar_Mto);
            Ingresar_Google = new Command(IngresoGoogle);
            Ingresar_Apple = new Command(Ingresar_Apple_Mto);
            LoginCorreo = new Command(async () => await LoginCorreo_Mto());
            Olvide_Clave = new Command(async () => await Olvide_Clave_Link_MtoAsync());

            try
            {
                CargarFondoLogin();
                CargarFechaFeria();
                Inicializar();
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginVm", "LoginVm", "carga inicial");
            }
        }

        public void Inicializar()
        {
            try
            {
                BtnApple = false;
                BtnGoogle = false;
                IngresoRedes = true;
                MatenerConectado = true;

                Preferences.Remove("Email");
                Preferences.Remove("Celular");
                Preferences.Remove("IdCiudad");
                Preferences.Remove("RedSocial");
                Preferences.Remove("IdCiudadDesc");
                Preferences.Remove("NombreCompleto");
                Preferences.Remove("MantenerConectado");
#if IOS
                BtnApple = true;
                BtnGoogle = true;
#endif

#if ANDROID
                BtnApple = false;
                BtnGoogle = true;
#endif
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginVm", "Inicializar", "Quitar preferencias");
            }
        }

        private async void CargarFondoLogin()
        {
            try
            {
                string? urli = logicaWs.Movile_Login_Mtd("1", "1", Preferences.Get("IdSuceso", ""));
                var jsonProcedimiento = await logicaWs.ConectionGet(urli);

                if (jsonProcedimiento != null)
                {
                    var Items = JsonConvert.DeserializeObject<List<LoginItem>>(jsonProcedimiento);

                    if (Items != null && Items.Count > 0)
                    {
                        ImagenLogin = Items[0].ImagenLogin;
                        ColorTexto = Items[0].ColorTexto;
                        ImagenFondo = Items[0].ImagenFondo;
                        ColorFondo = Items[0].ColorFondo;
                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginVm", "CargarFondoLogin", "consultaAdministraLogin");
            }
        }

        private async void CargarFechaFeria()
        {
            try
            {
                string[]? FechaIncio;
                string[] FechaInicioDetalle;

                string[]? FechaFin;
                string[] FechaFinDetalle;

                string? urli = logicaWs.Movile_Select_EventoPriniapl_Mtd("1", "ninguno@gmail.com", LenguajeBase, Preferences.Get("IdApp", ""));
                string? jsonProcedimiento = await logicaWs.ConectionGet(urli);

                // Verificar si jsonProcedimiento es null o vacío antes de parsearlo
                if (!string.IsNullOrWhiteSpace(jsonProcedimiento))
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);

                    foreach (JObject item in jsArray)
                    {
                        FechaIncio = item.GetValue("FechaInicio")?.ToString()?.Split(' ');
                        FechaInicioDetalle = FechaIncio?.Length > 0 ? FechaIncio[0].Split('-') : [];

                        FechaFin = item.GetValue("Fechafin")?.ToString()?.Split(' ');
                        FechaFinDetalle = FechaFin?.Length > 0 ? FechaFin[0].Split('-') : [];

                        if (FechaInicioDetalle.Length >= 3 && FechaFinDetalle.Length >= 3)
                        {
                            MesIncio = MesAno(FechaInicioDetalle[1], LenguajeBase);
                            DiaInicio = FechaInicioDetalle[2];

                            MesFin = MesAno(FechaFinDetalle[1], LenguajeBase);
                            DiaFin = FechaFinDetalle[2];

                            AnoFeria = FechaInicioDetalle[0];
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginVm", "CargarFechaFeria", "consAEvento");
            }
        }

        private string? MesAno(string? mes, string idioma)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mes)) return null;

                // Diccionario de nombres de meses en español e inglés
                var meses = new Dictionary<string, string[]>
                {
                    { "es", new[] { "ENE", "FEB", "MAR", "ABR", "MAY", "JUN", "JUL", "AGO", "SEP", "OCT", "NOV", "DIC" } },
                    { "en", new[] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" } }
                };

                // Normalizar el número del mes (remueve ceros a la izquierda)
                if (!int.TryParse(mes.TrimStart('0'), out int mesNum) || mesNum < 1 || mesNum > 12)
                    return null;

                // Si el idioma no está en el diccionario, usar español por defecto
                if (!meses.TryGetValue(idioma.ToLower(), out var nombresMes))
                    nombresMes = meses["es"];

                return nombresMes[mesNum - 1]; // Se ajusta el índice (de 1-12 a 0-11)
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginVm", "MesAno", "n/a");
                return null;
            }
        }

        private void Regresar_Mto()
        {
            IngresoRedes = true;
            RegresarIcon = false;
            IngresoCorreoClave = false;
        }

        private void Ingresar_Mto()
        {
            RegresarIcon = true;
            IngresoRedes = false;
            IngresoCorreoClave = true;
        }

        private async Task LoginCorreo_Mto()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                if (await ValidarLogin_Mto())
                {
                    try
                    {
                        usuarioLogin = new UsuarioLogin(EntClave);
                        string? urli = logicaWs.Movile_Consulta_Login_Mtd(EntCorreo);
                        string? json = JsonConvert.SerializeObject(usuarioLogin);
                        jsonProcedimiento = await logicaWs.ConectionPost(json, urli);

                        if (jsonProcedimiento == "Credenciales Incorrectos")
                        {
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMCredencialesIncorrectas, AppResources.VMaceptar);
                        }
                        else
                        {
                            if (jsonProcedimiento == "Usuario No Existe")
                            {
                                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEsteUsuaraioNoExisteMas, AppResources.VMaceptar);
                            }
                            else
                            {
                                try
                                {
                                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                                    foreach (JObject item in jsArray)
                                    {
                                        string? Email = item.GetValue("Email")?.ToString() ?? string.Empty;
                                        string? contrasena = item.GetValue("contrasena")?.ToString() ?? string.Empty;
                                        string? Identificacion = item.GetValue("identificacion")?.ToString() ?? string.Empty;
                                        string? IdIdentificacion = item.GetValue("ididentificacion")?.ToString() ?? string.Empty;
                                        string? NombreCompleto = item.GetValue("NombreCompleto")?.ToString() ?? string.Empty;
                                        string? IdCiudad = item.GetValue("idciudad")?.ToString() ?? string.Empty;
                                        string? IdCiudadDesc = item.GetValue("idciudaddesc")?.ToString() ?? string.Empty;
                                        string? Celular = item.GetValue("celular")?.ToString() ?? string.Empty;

                                        if (!string.IsNullOrEmpty(Celular))
                                        {
                                            Celular = "0";
                                        }

                                        Preferences.Set("Celular", Celular);
                                        Preferences.Set("Email", Email);
                                        Preferences.Set("IdIdentificacion", IdIdentificacion);
                                        Preferences.Set("numeroIdentificacion", Identificacion);
                                        Preferences.Set("NombreCompleto", NombreCompleto);
                                        Preferences.Set("IdCiudad", IdCiudad);
                                        Preferences.Set("IdCiudadDesc", IdCiudadDesc);
                                        Preferences.Set("IdiomaDefecto", LenguajeBase);

                                        if (MatenerConectado)
                                        {
                                            Preferences.Set("MantenerConectado", MatenerConectado);
                                        }
                                    }
                                    Application.Current!.MainPage = new MasterHomePage();
                                    await RelacionUsuarioApp();
                                }
                                catch (Exception ex)
                                {
                                    IsBusy = false;
                                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginVm", "LoginCorreo_Mto", "validausuarioaplicacion1");
                                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginVm", "LoginCorreo_Mto", "validausuarioaplicacion2");
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<bool> ValidarLogin_Mto()
        {
            try
            {
                char[] charsToTrim = { ' ' };
                string? expreregularcorreos = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

                if (EntCorreo != "" && EntClave != "" && EntCorreo != null && EntClave != null)
                {
                    /* validar formato correo */
                    bool resultadoCorreo = Regex.IsMatch(EntCorreo.Trim(charsToTrim), expreregularcorreos, RegexOptions.IgnoreCase);
                    if (!resultadoCorreo)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoTieneFormatoCorreo, AppResources.VMaceptar);
                        return false;
                    }

                    /* validar formato contraseña */
                    if (EntClave.Length < 6)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMLaContrasenaDebeTenerMinimoMas, AppResources.VMaceptar);
                        return false;
                    }
                    EntCorreo = EntCorreo.ToLower();
                    return true;
                }
                else
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVacios, AppResources.VMaceptar);
                    return false;
                }
            }
            catch (Exception exs)
            {
                IsBusy = false;
                claseBase.InsertarLogs_Mtd("ERROR", exs.Message, "LoginVm", "ValidarLogin_Mto", "Validacion de datos");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                return false;
            }
        }

        private async Task Olvide_Clave_Link_MtoAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await Application.Current.MainPage.Navigation.PushModalAsync(new RecuperarClavePage());
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginVm", "Olvide_Clave_Link_MtoAsync", "Redireccionamiento");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.ErrorRedireccionarLogin, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void Crear_Cuenta_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await Application.Current.MainPage.Navigation.PushModalAsync(new RegistroView());
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginVm", "Crear_Cuenta_Mtd", "Redireccionamiento");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.ErrorRedireccionarLogin, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        // INICIO DE SESION DE GOOGLE

        private void IngresoGoogle()
        {
#if IOS
            Ingresar_Google_MtdiOS();
#endif
#if ANDROID
            Ingresar_Google_MtdAndroid();
#endif
        }

        private async void Ingresar_Google_MtdAndroid()
        {
         
        }

        private async void Ingresar_Google_MtdiOS()
        {
        }

        private void GuardarPreferenciasUsuarioNuevo(GoogleUserDTO loggedUser)
        {
            Preferences.Set("Email", loggedUser.Email);
            Preferences.Set("Imagen", "https://thumbs.dreamstime.com/b/omita-el-icono-del-perfil-avatar-placeholder-gris-de-la-foto-99724602.jpg");
            Preferences.Set("NombreCompleto", loggedUser.FullName);
            Preferences.Set("RedSocial", 2);
            Preferences.Set("IdiomaApp", LenguajeBase);
        }

        private async Task ProcesarUsuarioExistente(string jsonResponse, string email)
        {
            try
            {
                JArray jsArray = JArray.Parse(jsonResponse);
                foreach (JObject item in jsArray)
                {
                    string celular = item.GetValue("celular")?.ToString()?.Trim();
                    if (string.IsNullOrWhiteSpace(celular))
                    {
                        celular = "0";
                    }

                    Preferences.Set("Celular", celular);
                    Preferences.Set("Email", email);
                    Preferences.Set("IdIdentificacion", item.GetValue("identificacion")?.ToString());
                    Preferences.Set("numeroIdentificacion", item.GetValue("ididentificacion")?.ToString());
                    Preferences.Set("NombreCompleto", item.GetValue("NombreCompleto")?.ToString());
                    Preferences.Set("IdCiudad", item.GetValue("idciudad")?.ToString());
                    Preferences.Set("IdCiudadDesc", item.GetValue("idciudaddesc")?.ToString());
                    Preferences.Set("IdiomaApp", LenguajeBase);
                }

                Preferences.Set("PrimerUso", true);
                Preferences.Set("MantenerConectado", MatenerConectado);

                await RelacionUsuarioApp();
            }
            catch (Exception ex)
            {
                IsBusy = false;
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginVm", "Ingresar_Google_Mtd", "AvalidarUsuarioExistente");
            }
        }


        // INICIO DE SESION DE APPLE

        private async void Ingresar_Apple_Mto()
        {
#if IOS
            try
            {
                var appleIdProvider = new ASAuthorizationAppleIdProvider();
                var request = appleIdProvider.CreateRequest();
                request.RequestedScopes = new[] { ASAuthorizationScope.Email, ASAuthorizationScope.FullName };

                var controller = new ASAuthorizationController(new[] { request });

                controller.Delegate = new AppleAuthControllerDelegate(async (msg, result) =>
                {
                    if (result != null)
                    {
                        Preferences.Set("TokeniOS", result.UserId);

                        if (!string.IsNullOrWhiteSpace(result.Email))
                            Preferences.Set("EmailiOS", result.Email);

                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            if (!string.IsNullOrWhiteSpace(Preferences.Get("EmailiOS", "")))
                            {
                                InsercionDatosApple();
                                InicioSessionApple();
                            }
                            else
                            {
                                try
                                {
                                    if (!string.IsNullOrWhiteSpace(Preferences.Get("TokeniOS", "")))
                                    {
                                        LogicaWs logicaWS = new LogicaWs();
                                        string urli = logicaWS.Movile_Select_servicios_Recinto_Mtd("0", Preferences.Get("TokeniOS", ""), "APPLE");
                                        var o = await logicaWS.ConectionGet(urli);

                                        if (!string.IsNullOrWhiteSpace(o))
                                        {
                                            JArray jsArray = JArray.Parse(o);
                                            if (jsArray.Count > 0)
                                            {
                                                foreach (JObject item in jsArray)
                                                {
                                                    Preferences.Set("EmailiOS", item.GetValue("nombreRecinto").ToString());
                                                    InicioSessionApple();
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                claseBase.InsertarLogs_Mtd("ERROR", "TOKEN iOS3", "LoginVm", "Ingresar_Apple_Mto", "n/a");
                                                await MopupService.Instance.PushAsync(new ApplePopUp());
                                            }
                                        }
                                        else
                                        {
                                            claseBase.InsertarLogs_Mtd("ERROR", "TOKEN iOS2", "LoginVm", "Ingresar_Apple_Mto", "n/a");
                                            await MopupService.Instance.PushAsync(new ApplePopUp());
                                        }
                                    }
                                    else
                                    {
                                        claseBase.InsertarLogs_Mtd("ERROR", "TOKEN iOS1", "LoginVm", "Ingresar_Apple_Mto", "n/a");
                                        await MopupService.Instance.PushAsync(new ApplePopUp());
                                    }
                                }
                                catch (Exception es)
                                {
                                    claseBase.InsertarLogs_Mtd("ERROR", es.Message, "LoginVm", "Ingresar_Apple_Mto", "n/a");
                                    await MopupService.Instance.PushAsync(new ApplePopUp());
                                }
                            }
                        });
                    }
                    else
                    {
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            claseBase.InsertarLogs_Mtd("ERROR", msg, "LoginVm", "Ingresar_Apple_Mto", "n/a");
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, msg, AppResources.VMaceptar);
                        });
                    }
                });

                controller.PresentationContextProvider = new AppleAuthPresentationProvider();
                controller.PerformRequests();
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginVm", "Ingresar_Apple_Mto1", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, ex.Message, AppResources.VMaceptar);
            }
#endif
        }

        private async void InicioSessionApple()
        {
            try
            {
                LogicaWs logicaWS = new LogicaWs();
                string urli = logicaWS.Movile_AvalidarUsuarioExistente_Mtd(Preferences.Get("EmailiOS", ""), "3");
                var o = await logicaWS.ConectionPost(" ", urli);
                if (string.IsNullOrWhiteSpace(o))
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.ErrorAppleUno, AppResources.VMaceptar);
                    claseBase.InsertarLogs_Mtd("ERROR", "Usuario No Existe", "LoginVm", "OnAppleSignInRequest inicio1", "AvalidarUsuarioExistente Usuario No Existe");

                }
                else if (o.Equals("Usuario No Existe"))
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.ErrorAppleUno, AppResources.VMaceptar);
                    claseBase.InsertarLogs_Mtd("ERROR", "Usuario No Existe", "LoginVm", "OnAppleSignInRequest inicio2", "AvalidarUsuarioExistente Usuario No Existe");
                }
                else
                {
                    JArray jsArray = JArray.Parse(o);
                    foreach (JObject item in jsArray)
                    {
                        string Identificacion = item.GetValue("ididentificacion").ToString();
                        string IdIdentificacion = item.GetValue("identificacion").ToString();
                        string NombreCompleto = item.GetValue("NombreCompleto").ToString();
                        string IdCiudad = item.GetValue("idciudad").ToString();
                        string IdCiudadDesc = item.GetValue("idciudaddesc").ToString();
                        string Celular = item.GetValue("celular").ToString();

                        if (Celular == null)
                        {
                            if (Celular == "" || Celular == " ")
                            {
                                Celular = "0";
                            }
                        }

                        Preferences.Set("Celular", Celular);
                        Preferences.Set("Email", Preferences.Get("EmailiOS", ""));
                        Preferences.Set("IdIdentificacion", IdIdentificacion);
                        Preferences.Set("numeroIdentificacion", Identificacion);
                        Preferences.Set("NombreCompleto", NombreCompleto);
                        Preferences.Set("IdCiudad", IdCiudad);
                        Preferences.Set("IdCiudadDesc", IdCiudadDesc);
                        Preferences.Set("IdiomaApp", LenguajeBase);

                    }

                    if (Preferences.ContainsKey("PrimerUso"))
                    {
                        Preferences.Set("PrimerUso", true);
                    }
                    Preferences.Set("MantenerConectado", true);
                    Application.Current!.MainPage = new MasterHomePage();
                    await RelacionUsuarioApp();
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginVm", "Ingresar_Apple_Mto1", "LOGIN CORRECTO APPLE" + Preferences.Get("EmailiOS", ""));
                await pageServicio.DisplayAlert(AppResources.nombreMarca, ex.Message, AppResources.VMaceptar);
            }
        }

        private async void InsercionDatosApple()
        {
            LogicaWs logicaWS = new LogicaWs();
            string urliDos = logicaWs.Movile_Insert_habeasData_Mtd("5", Preferences.Get("EmailiOS", ""));

            HabeasData habeasData = new HabeasData
            {
                IpCel = Preferences.Get("TokeniOS", ""), //TOKEN DEL USUARIO
                Acepto = "1", // 1 APPLE
                IdModulo = "0", // sin uso
                IdTerminoPolitico = "0", // sin uso
                Navegador = "0" // sin uso
            };

            string json = JsonConvert.SerializeObject(habeasData);
            string? res = await logicaWs.ConectionPost(json, urliDos);
        }

        private async Task RelacionUsuarioApp()
        {
            try
            {
                string urli = logicaWs.Movile_actualizarelacionusuario_Mtd("2");
                UsuActuApp ip = new UsuActuApp()
                {
                    Email = Preferences.Get("Email", ""),
                    IdApp = Preferences.Get("IdApp", "")
                };

                string jsonip = JsonConvert.SerializeObject(ip);
                string jsonProcedimiento = await logicaWs.ConectionPost(urli, jsonip);
            }
            catch (Exception e)
            {
                claseBase.InsertarLogs_Mtd("ERROR", e.Message, "RegistroVM", "RecuperarToken", "actualizatoken");
            }
        }

        private class LoginItem
        {
            public int IdLogin { get; set; }
            public string? ImagenLogin { get; set; }
            public string? ColorTexto { get; set; }
            public string? ImagenFondo { get; set; }
            public string? ColorFondo { get; set; }
        }

    }

}
