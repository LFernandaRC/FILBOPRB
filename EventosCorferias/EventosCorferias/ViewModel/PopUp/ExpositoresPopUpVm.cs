using EventosCorferias.Interfaces;
using EventosCorferias.Models;
using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.Services;
using EventosCorferias.ViewModel.Suceso;
using EventosCorferias.Views.Suceso;
using Mopups.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EventosCorferias.ViewModel.PopUp
{
    public class ExpositoresPopUpVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        private readonly ClaseBase claseBase;
        public readonly IPageServicio pageServicio;

        private readonly string IdSuceso;

        private ObservableCollection<ListasGeneral> _listaUbicacion;

        private List<Expositores> _listaExpositores;
        private List<Expositores> listaExpositoresTemp;

        private Expositores _SelectedExpositor;

        private ListasGeneral _selectUbicacion;

        private string? _CantidadExpo;
        private string? _entExpositor;

        private string? _mascaraExpos;
        private string? _mascaraUbi;

        private bool _equisUbicacion;
        private bool _FlechaUbicacion;

        public ICommand FocusUbicacion { get; }

        public ICommand BuscarExpositor { get; }

        public ICommand OrdenarExpositor { get; }

        public ICommand LimpiarUbicacion { get; }

        public ICommand PlusButtonCommand { get; }

        public ICommand ItemSelectedCommandExpositor
        {
            get
            {
                return new Command(async list =>
                {
                    Expositores lista = (Expositores)list;
                    CargarExpositor(lista);
                });
            }
        }

        public List<Expositores> ListaExpositores
        {
            get { return _listaExpositores; }
            set { _listaExpositores = value; OnPropertyChanged(nameof(ListaExpositores)); }
        }

        public ObservableCollection<ListasGeneral> ListaUbicacion
        {
            get { return _listaUbicacion; }
            set { _listaUbicacion = value; OnPropertyChanged(nameof(ListaUbicacion)); }
        }

        public string? CantidadExpo
        {
            get { return _CantidadExpo; }
            set { SetProperty(ref _CantidadExpo, value); OnPropertyChanged(nameof(CantidadExpo)); }
        }

        public string? EntExpositor
        {
            get { return _entExpositor; }
            set { SetProperty(ref _entExpositor, value); OnPropertyChanged(nameof(EntExpositor)); }
        }

        public string? MascaraExpos
        {
            get { return _mascaraExpos; }
            set { SetProperty(ref _mascaraExpos, value); OnPropertyChanged(nameof(MascaraExpos)); }
        }

        public string? MascaraUbi
        {
            get { return _mascaraUbi; }
            set { SetProperty(ref _mascaraUbi, value); OnPropertyChanged(nameof(MascaraUbi)); }
        }

        public bool EquisUbicacion
        {
            get { return _equisUbicacion; }
            set
            { SetProperty(ref _equisUbicacion, value); OnPropertyChanged(nameof(EquisUbicacion)); }
        }

        public bool FlechaUbicacion
        {
            get { return _FlechaUbicacion; }
            set { SetProperty(ref _FlechaUbicacion, value); OnPropertyChanged(nameof(FlechaUbicacion)); }
        }

        public ListasGeneral SelectUbicacion
        {
            get { return _selectUbicacion; }
            set
            {
                SetProperty(ref _selectUbicacion, value);
                if (value != null)
                {
                    FlechaUbicacion = false;
                    EquisUbicacion = true;
                    FiltradoGeneral();
                }
                OnPropertyChanged(nameof(SelectUbicacion));
            }
        }

        private void SeleccionarExpositor(Expositores expositor)
        {
            if (expositor != null)
            {
                CargarExpositor(expositor);
            }
        }

        public Expositores SelectExpositor
        {
            get { return _SelectedExpositor; }
            set
            {
                _SelectedExpositor = null;
                ItemSelectedCommandExpositor.Execute(value);
                OnPropertyChanged(nameof(SelectExpositor));
            }
        }

        public Command BtnCargarSitios { get; set; }

        public Command<Expositores> SeleccionarExpositorCommand { get; }

        string idSitio_;
        string NombreServicio_;

        public ExpositoresPopUpVm(string idSuceso, Entry filtroTres, string idSitio, string NombreServicio)
        {
            IsBusy = true;
            Title = AppResources.Expositores;

            IdSuceso = Preferences.Get("IdSuceso", "");

            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            idSitio_ = idSitio;
            NombreServicio_ = NombreServicio;

            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            ImagenSplash = logicaWs.ImgMenuSuperior_Mtd();

            BtnCargarSitios = new Command(async () => await BtnCargarSitios_Mtd());
            PlusButtonCommand = new Command(SeleccionarFav_Mtd);
            BuscarExpositor = new Command(() => FiltradoGeneral());
            FocusUbicacion = new Command(claseBase.FocusPicker_Mto);
            OrdenarExpositor = new Command(() => OrdenarExpositor_MtoAsync());
            LimpiarUbicacion = new Command(() => LimpiarUbicacion_MtoAsync());
            SeleccionarExpositorCommand = new Command<Expositores>(SeleccionarExpositor);

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            filtroTres.Completed += (sender, e) => EntryCompleted(sender, e);

            Inicializar();
        }

        void EntryCompleted(object sender, EventArgs e)
        {
            try
            {
                FiltradoGeneral();
            }
            catch (Exception ex)
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresPopUpVm", "EntryCompleted", "n/a");
            }
        }

        public async Task BtnCargarSitios_Mtd()
        {
            //VALIDAR CON LINA
            //await Application.Current.MainPage.Navigation.PushPopupAsync(new SitiosPage(IdSuceso, "2", "2"));
        }

        private async void Inicializar()
        {
            try
            {
                await BuscarExpositor_MtoAsync();
                await ListasFiltros();
                await FiltrosMascara();
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresPopUpVm", "Inicializar", "n/a");
            }
        }

        private async Task FiltrosMascara()
        {
            try
            {
                string urli = logicaWs.Moviel_select_consAMascaraFiltroExp_Mtd("1", IdSuceso);
                string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    MascaraExpos = item.GetValue("Label")?.ToString();
                }

                string urliDos = logicaWs.Moviel_select_consAMascaraFiltroExp_Mtd("2", IdSuceso);
                string jsonProcedimientoDos = await logicaWs.ConectionGet(urliDos);
                JArray jsArrayDos = JArray.Parse(jsonProcedimientoDos);
                foreach (JObject item in jsArrayDos)
                {
                    MascaraUbi = item.GetValue("Label")?.ToString();
                }

            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresPopUpVm", "ListasFiltros", "consultaexpositorubicacion");
            }
        }

        private async Task ListasFiltros()
        {
            try
            {
                ListaUbicacion = new ObservableCollection<ListasGeneral>();
                string urli = logicaWs.Movile_Select_Expo_Ubicacion_Mtd(IdSuceso, EmailUsuario, LenguajeBase);
                string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral { Descripcion = item.GetValue("nombresitio")?.ToString(), Id = item.GetValue("idsitio")?.ToString() };
                    ListaUbicacion.Add(listasGeneral);
                }
                EquisUbicacion = false;
                FlechaUbicacion = true;
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresPopUpVm", "ListasFiltros", "consultaexpositorubicacion");
            }
        }

        private async void FiltradoGeneral()
        {
            IsBusy = true;

            try
            {
                ListaExpositores = listaExpositoresTemp.ToList();

                if (EquisUbicacion)
                    ListaExpositores = ListaExpositores.Where(x => x.IdSitio.ToLower().Contains(SelectUbicacion.Id.ToLower().Trim())).ToList();

                if (EntExpositor != null && EntExpositor != "" && EntExpositor != " ")
                    ListaExpositores = ListaExpositores.Where(x => x.NombreExpositor.ToLower().Contains(EntExpositor.ToLower().Trim())).ToList();

                CantidadExpo = ListaExpositores.Count.ToString() + " " + AppResources.resultados;

            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresPopUpVm", "FiltradoGeneral", "Error Filtrado Generado ExpositoresPopUp");
            }

            IsBusy = false;
        }

        private async Task BuscarExpositor_MtoAsync()
        {
            try
            {
                if (!StateConexion)
                {
                    ListaExpositores = new List<Expositores>();
                    listaExpositoresTemp = new List<Expositores>();

                    ConsultaExpositor consultaAgenda = new ConsultaExpositor
                    {
                        Idioma = "es",
                        NombreExpositor = "0",
                        correo = EmailUsuario
                    };
                    string urli = logicaWs.Moviel_select_consAExpositorSitioLugar_Mtd("1", "0", Preferences.Get("IdSuceso", "0"), idSitio_);
                    string json = JsonConvert.SerializeObject(consultaAgenda);

                    string jsonProcedimiento = await logicaWs.ConectionPost(json, urli);

                    JArray jsArray = JArray.Parse(jsonProcedimiento);

                    foreach (JObject item in jsArray)
                    {
                        Expositores Expositor = new Expositores(
                           item.GetValue("IdExpositor")?.ToString() ?? string.Empty, item.GetValue("NombreExpositor")?.ToString() ?? string.Empty,
                           item.GetValue("PerfilExpositor")?.ToString() ?? string.Empty,
                           "", "", "",
                           item.GetValue("Imagen")?.ToString() ?? string.Empty,
                           item.GetValue("Fav")?.ToString() ?? string.Empty,
                           item.GetValue("Url")?.ToString() ?? string.Empty, "",
                           item.GetValue("PabellonNivelStand")?.ToString() ?? string.Empty, item.GetValue("nivel")?.ToString() ?? string.Empty,
                           item.GetValue("stand")?.ToString() ?? string.Empty,

                          idSitio_, NombreServicio_,
                          "", "", "", item.GetValue("email")?.ToString() ?? string.Empty, "", Preferences.Get("IdSuceso", "0"), "", "");

                        if (claseBase.ValidaString(Expositor.LogoDetalle).Equals(""))
                            Expositor.LogoDetalle = Expositor.Imagen;

                        ListaExpositores.Add(Expositor);

                    }

                    ListaExpositores = ListaExpositores.OrderBy(x => x.NombreExpositor).ToList();
                    listaExpositoresTemp = ListaExpositores.OrderBy(x => x.NombreExpositor).ToList();

                    CantidadExpo = ListaExpositores.Count.ToString() + " " + AppResources.resultados;
                }
                else
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresPopUpVm", "BuscarExpositor_MtoAsync", "consultacatalogoexpositorfav/consultacatalgoexpositor");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void CargarExpositor(Expositores content)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                if (content != null)
                {
                    await RootNavigation.PushAsync(new DetalleExpositorView(content));
                }
            }
            finally
            {
                IsBusy = false;
            }

            await MopupService.Instance.PopAsync();
        }

        private void LimpiarUbicacion_MtoAsync()
        {
            try
            {
                IsBusy = true;
                EquisUbicacion = false;
                FlechaUbicacion = true;
                _ = new ObservableCollection<ListasGeneral>();
                ObservableCollection<ListasGeneral> team = ListaUbicacion;
                ListaUbicacion = new ObservableCollection<ListasGeneral>();
                ListaUbicacion = team;

                FiltradoGeneral();
            }
            catch (Exception ex)
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresPopUpVm", "LimpiarUbicacion_MtoAsync", "n/a");
            }
        }

        private void OrdenarExpositor_MtoAsync()
        {
            try
            {
                ListaExpositores.Reverse();
                ListaExpositores = ListaExpositores.ToList();
            }
            catch (Exception ex)
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresPopUpVm", "OrdenarExpositor_MtoAsync", "n/a");
            }
        }

        private async void SeleccionarFav_Mtd(object obj)
        {
            try
            {
                if (!StateConexion)
                {
                    var content = obj as Expositores;

                    foreach (var item in ListaExpositores)
                    {
                        if (item.IdExpositor == content.IdExpositor)
                        {
                            if (item.Fav == "1") /*1 Para favorito*/
                            {
                                item.ImagenFav = "ic_favorito_obscuro.png";
                                item.Fav = "0";
                                string urli = logicaWs.Movile_Delet_Fav_Expo_Mtd(LenguajeBase, EmailUsuario, item.IdExpositor);
                                await logicaWs.ConectionGet(urli);
                            }
                            else
                            {
                                item.ImagenFav = "ic_favortio_relleno.png";
                                item.Fav = "1";
                                string urli = logicaWs.Movile_Update_Fav_Expo_Mtd(LenguajeBase, EmailUsuario, item.IdExpositor);
                                await logicaWs.ConectionGet(urli);
                            }
                        }
                    }
                }
                else
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresPopUpVm", "SeleccionarFav_Mtd", "eliminarusuarioexpositor/insertarusuarioexpositor");
            }

        }
    }
}