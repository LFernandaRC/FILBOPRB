using System.ComponentModel;
using System.Runtime.CompilerServices;
using EventosCorferias.Views.Usuario;
using EventosCorferias.Services;
using EventosCorferias.Resources.RecursosIdioma;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using EventosCorferias.Views.Suceso;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Boletas;
using Mopups.Services;
using EventosCorferias.Views.PopUp;



namespace EventosCorferias.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public MasterHomePage RootMainPage => Application.Current.MainPage as MasterHomePage;
        public NavigationPage RootNavigation => RootMainPage?.Detail as NavigationPage;

        private bool _VerChat;
        public string? LenguajeBase;
        private bool _isBusy = false;
        private bool _isBusyInverse = false;
        private bool _stateconexion;

        public bool VerChat
        {
            get { return _VerChat; }
            set { SetProperty(ref _VerChat, value); }
        }

        public bool IsBusyInverse
        {
            get { return _isBusyInverse; }
            set { SetProperty(ref _isBusyInverse, value); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); IsBusyInverse = !value; }
        }

        public bool StateConexion
        {
            get { return _stateconexion; }
            set { _stateconexion = value; OnPropertyChanged(nameof(StateConexion)); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action? onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
                StateConexion = false;
            else
                StateConexion = true;
        }

        public static async Task<string> GetLocalIPAddressAsync()
        {
            try
            {
                LogicaWs logicaWS = new LogicaWs();
                var ipAddress = await logicaWS.ConectionGet("https://api.ipify.org/");
                if (ipAddress == null)
                {
                    return "";
                }
                else
                {
                    return ipAddress;
                }
            }
            catch
            {
                return "";
            }
        }

        /*INICIO MENU INFERIOR*/

        private string _ImagenSplash;
        private string _EmailUsuario;
        private string _CiudadRecinto;
        private string _NombreCompletoPerfil;
        private string _title = string.Empty;

        private static bool _menu1;
        private static bool _menu2;
        private static bool _menu3;
        private static bool _menu4;

        private static string _icon1;
        private static string _icon2;
        private static string _icon3;
        private static string _icon4;

        private static string _icon1Class;
        private static string _icon2Class;
        private static string _icon3Class;
        private static string _icon4Class;

        private static string _CantidadNotificaciones;
        private static string _GridNotificaciones;
        private static bool _verNotificicaciones;

        public string CantidadNotificaciones
        {
            get { return _CantidadNotificaciones; }
            set { _CantidadNotificaciones = value; OnPropertyChanged(nameof(CantidadNotificaciones)); }
        }
        public string GridNotificaciones
        {
            get { return _GridNotificaciones; }
            set { _GridNotificaciones = value; OnPropertyChanged(nameof(GridNotificaciones)); }
        }
        public bool VerNotificicaciones
        {
            get { return _verNotificicaciones; }
            set { _verNotificicaciones = value; OnPropertyChanged(nameof(VerNotificicaciones)); }
        }

        public string ImagenSplash
        {
            get { return _ImagenSplash; }
            set { _ImagenSplash = value; OnPropertyChanged(nameof(ImagenSplash)); }
        }

        /**
        * @brief Guarda el correo del usuario 
        */
        public string EmailUsuario
        {
            get { return _EmailUsuario; }
            set { _EmailUsuario = value; OnPropertyChanged(nameof(EmailUsuario)); }
        }

        /**
        * @brief Guarda el nombre de la ciudad del recinto
        */
        public string CiudadRecinto
        {
            get { return _CiudadRecinto; }
            set { _CiudadRecinto = value; OnPropertyChanged(nameof(CiudadRecinto)); }
        }

        /**
        * @brief Guarda el nombre completo del usuario incluyendo apellidos
        */
        public string NombreCompletoPerfil
        {
            get { return _NombreCompletoPerfil; }
            set { _NombreCompletoPerfil = value; OnPropertyChanged(nameof(NombreCompletoPerfil)); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Icon1
        {
            get { return _icon1; }
            set { _icon1 = value; OnPropertyChanged(nameof(Icon1)); }
        }
        public string Icon2
        {
            get { return _icon2; }
            set { _icon2 = value; OnPropertyChanged(nameof(Icon2)); }
        }
        public string Icon3
        {
            get { return _icon3; }
            set { _icon3 = value; OnPropertyChanged(nameof(Icon3)); }
        }
        public string Icon4
        {
            get { return _icon4; }
            set { _icon4 = value; OnPropertyChanged(nameof(Icon4)); }
        }

        public string Icon1Class
        {
            get { return _icon1Class; }
            set { _icon1Class = value; OnPropertyChanged(nameof(Icon1Class)); }
        }
        public string Icon2Class
        {
            get { return _icon2Class; }
            set { _icon2Class = value; OnPropertyChanged(nameof(Icon2Class)); }
        }
        public string Icon3Class
        {
            get { return _icon3Class; }
            set { _icon3Class = value; OnPropertyChanged(nameof(Icon3Class)); }
        }
        public string Icon4Class
        {
            get { return _icon4Class; }
            set { _icon4Class = value; OnPropertyChanged(nameof(Icon4Class)); }
        }

        public bool Menu1
        {
            get { return _menu1; }
            set { _menu1 = value; OnPropertyChanged(nameof(Menu1)); }
        }

        public bool Menu2
        {
            get { return _menu2; }
            set { _menu2 = value; OnPropertyChanged(nameof(Menu2)); }
        }
        public bool Menu3
        {
            get { return _menu3; }
            set { _menu3 = value; OnPropertyChanged(nameof(Menu3)); }
        }
        public bool Menu4
        {
            get { return _menu4; }
            set { _menu4 = value; OnPropertyChanged(nameof(Menu4)); }
        }

        public async Task ValidarMenuInferior()
        {
            try
            {
                LogicaWs logicaWS = new LogicaWs();
                ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();

                Menu1 = false;
                Menu2 = false;
                Menu3 = false;
                Menu4 = false;

                string urliDos = logicaWS.Moviel_select_botonesMenuInferior_Mtd("1", Preferences.Get("IdSuceso", ""));
                string jsonProcedimientoDos = await logicaWS.ConectionGet(urliDos);
                JArray jsArrayDos = JArray.Parse(jsonProcedimientoDos);

                int AuxConut = 0;

                foreach (JObject itemDos in jsArrayDos)
                {
                    if (AuxConut == 0)
                    {
                        Menu1 = true;
                        Icon1Class = itemDos.GetValue("IdBoton").ToString();
                        switch (itemDos.GetValue("IdBoton").ToString())
                        {
                            case "1":
                                Icon1 = "ic_boleteria_obscuro.png";
                                break;
                            case "2":
                                Icon1 = "ic_notificacion_obscuro.png";
                                break;
                            case "4":
                                Icon1 = "ic_favorito_obscuro.png";
                                break;
                            case "5":
                                Icon1 = "ic_perfil_obscuro.png";
                                break;
                        }
                    }
                    ;

                    if (AuxConut == 1)
                    {
                        Menu2 = true;
                        Icon2Class = itemDos.GetValue("IdBoton").ToString();
                        switch (itemDos.GetValue("IdBoton").ToString())
                        {
                            case "1":
                                Icon2 = "ic_boleteria_obscuro.png";
                                break;
                            case "2":
                                Icon2 = "ic_notificacion_obscuro.png";
                                break;
                            case "4":
                                Icon2 = "ic_favorito_obscuro.png";
                                break;
                            case "5":
                                Icon2 = "ic_perfil_obscuro.png";
                                break;
                        }
                    }
                    ;

                    if (AuxConut == 2)
                    {
                        Menu3 = true;
                        Icon3Class = itemDos.GetValue("IdBoton").ToString();
                        switch (itemDos.GetValue("IdBoton").ToString())
                        {
                            case "1":
                                Icon3 = "ic_boleteria_obscuro.png";
                                break;
                            case "2":
                                Icon3 = "ic_notificacion_obscuro.png";
                                break;
                            case "4":
                                Icon3 = "ic_favorito_obscuro.png";
                                break;
                            case "5":
                                Icon3 = "ic_perfil_obscuro.png";
                                break;
                        }
                    }
                    ;

                    if (AuxConut == 3)
                    {
                        Menu4 = true;
                        Icon4Class = itemDos.GetValue("IdBoton").ToString();
                        switch (itemDos.GetValue("IdBoton").ToString())
                        {
                            case "1":
                                Icon4 = "ic_boleteria_obscuro.png";
                                break;
                            case "2":
                                Icon4 = "ic_notificacion_obscuro.png";
                                break;
                            case "4":
                                Icon4 = "ic_favorito_obscuro.png";
                                break;
                            case "5":
                                Icon4 = "ic_perfil_obscuro.png";
                                break;
                        }
                        break;
                    }
                    ;

                    AuxConut = AuxConut + 1;

                }

            }
            catch (Exception ex)
            {

            }

            try
            {
                CantidadNotificaciones = "0";
                int cantidadAux = 0;
                LogicaWs logicaWS = new LogicaWs();
                string urli = logicaWS.Movile_Select_Notificaciones_Mtd(Preferences.Get("Email", "").Trim(), Preferences.Get("IdiomaDefecto", "es"));
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);

                int countAux = jsArray.Count;
                if (countAux > 0)
                {
                    try
                    {
                        foreach (JObject item in jsArray)
                        {
                            if (item.GetValue("Estado").ToString() == "0")
                            {
                                cantidadAux = cantidadAux + 1;
                            }
                        }
                    }
                    catch { }

                    CantidadNotificaciones = cantidadAux.ToString();
                    if (CantidadNotificaciones != "0")
                    {
                        if (Icon1 == "ic_notificacion_obscuro.png")
                        {
                            VerNotificicaciones = true;
                            GridNotificaciones = "0";
                        }
                        else if (Icon2 == "ic_notificacion_obscuro.png")
                        {
                            VerNotificicaciones = true;
                            GridNotificaciones = "1";
                        }
                        else if (Icon3 == "ic_notificacion_obscuro.png")
                        {
                            VerNotificicaciones = true;
                            GridNotificaciones = "3";
                        }
                        else if (Icon4 == "ic_notificacion_obscuro.png")
                        {
                            VerNotificicaciones = true;
                            GridNotificaciones = "4";
                        }
                        else
                        {
                            VerNotificicaciones = false;
                        }
                    }
                }
                else
                {
                    VerNotificicaciones = false;
                }
            }
            catch (Exception ex)
            {
                VerNotificicaciones = false;
            }

        }

        public ICommand IrHome
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                    try
                    {
                        RootMainPage.Detail = new NavigationPage(new SucesoView());
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
            }
        }

        public ICommand IrMenuInferior
        {
            get { return new Command(ClickMenuInferior); }
        }

        private void ClickMenuInferior(object obj)
        {
            Image obdd = (Image)obj;

            if (obdd.ClassId == "1")
            {
                IrBoleterial();
            }

            if (obdd.ClassId == "2")
            {
                IrNotificaciones();
            }

            if (obdd.ClassId == "4")
            {
                FocusPickerFavoritos_Mtd();
            }

            if (obdd.ClassId == "5")
            {
                IrPerfil();
            }
        }

        public async void IrPerfil()
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            try
            {
                RootMainPage.Detail = new NavigationPage(new PerfilMenuView());
            }
            catch (Exception ex)
            {
                IPageServicio pageServicio = new PageServicio();
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void IrNotificaciones()
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            try
            {
                RootMainPage.Detail = new NavigationPage(new NotificacionesView("0"));
            }
            catch (Exception ex)
            {
                IPageServicio pageServicio = new PageServicio();
                await pageServicio.DisplayAlert(AppResources.nombreMarca, ex.Message, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void IrBoleterial()
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            try
            {
                RootMainPage.Detail = new NavigationPage(new BoleteriaView());
            }
            catch (Exception ex)
            {
                IPageServicio pageServicio = new PageServicio();
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void FocusPickerFavoritos_Mtd()
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            try
            {
                await MopupService.Instance.PushAsync(new FavoritosPopUP());
            }
            catch (Exception ex)
            {
                IPageServicio pageServicio = new PageServicio();
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ICommand AbrirMenu
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                    try
                    {
                        if (Application.Current.MainPage is FlyoutPage flyoutPage)
                        {
                            flyoutPage.IsPresented = true;
                        }
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
            }
        }


        /*FIN MENU INFERIOR*/


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
