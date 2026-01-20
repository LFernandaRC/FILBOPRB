using EventosCorferias.Interfaces;
using EventosCorferias.Models;
using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using System.Diagnostics;
using EventosCorferias.Views.Usuario;
using Mopups.Services;

namespace EventosCorferias.ViewModel.PopUp
{
    public class AgendaSitiosPopUpVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        private readonly ClaseBase claseBase;
        public readonly IPageServicio pageServicio;

        private readonly string IdSuceso;

        private List<Agenda> listaAgenda;
        private List<Agenda> listaAgendaTem;

        private ObservableCollection<ListasGeneral> _listaLugar;
        private ObservableCollection<ListasGeneral> _listaCategoria;
        private ObservableCollection<ListasGeneral> _listaFranja;

        private ListasGeneral _selectLugar;
        private ListasGeneral _selectFranja;
        private ListasGeneral _selectCategoria;

        public ConsultaAgenda consultaAgenda;

        private Color _colorEntryConferencista;

        private DateTime _entFechaBuscar = DateTime.Now;

        private Agenda _SelectedAgenda;

        private string? _Filtro1;
        private string? _Filtro2;
        private string? _Filtro3;
        private string? _Filtro4;
        private string? _Filtro5;
        private string? _entConferencista;
        private string? _cantidadAgenda;

        private bool auxFecha;
        private bool _equisLugar;
        private bool _flechaLugar;
        private bool _equisCategoria;
        private bool _flechaCategoria;
        private bool _equisCalendario;
        private bool _flechaCalendario;
        private bool _equisFranja;
        private bool _flechaFranja;

        private bool _isReadOnlyEntryConfe;

        public ICommand LimpiarFranja { get; }

        public ICommand LimpiarLugar { get; }

        public ICommand OrdenarAgenda { get; }

        public ICommand BuscarEntAgenda { get; }

        public ICommand FocusCalendario { get; }

        public ICommand LimpiarCategoria { get; }

        public ICommand FavButtonCommand { get; }

        public ICommand LimpiarCalendario { get; }

        public ICommand FocusPicker { get; }

        public ICommand ItemSelectedCommandAgenda
        {
            get
            {
                return new Command(async list =>
                {
                    Agenda lista = (Agenda)list;
                    CargarAGenda(lista);
                });
            }
        }

        public DateTime EntFechaBuscar
        {
            get { return _entFechaBuscar; }
            set
            {
                SetProperty(ref _entFechaBuscar, value);
                if (value != null)
                {
                    if (auxFecha.Equals(true))
                    {
                        FlechaCalendario = false;
                        EquisCalendario = true;
                        FiltradoGeneral_MtoAsync();
                    }
                }
                OnPropertyChanged(nameof(EntFechaBuscar));
            }
        }

        public ListasGeneral SelectCategoria
        {
            get { return _selectCategoria; }
            set
            {
                SetProperty(ref _selectCategoria, value);
                if (value != null)
                {
                    FlechaCategoria = false;
                    EquisCategoria = true;
                    FiltradoGeneral_MtoAsync();
                }
                OnPropertyChanged(nameof(SelectCategoria));
            }
        }

        public ListasGeneral SelectLugar
        {
            get { return _selectLugar; }
            set
            {
                SetProperty(ref _selectLugar, value);
                if (value != null)
                {
                    FlechaLugar = false;
                    EquisLugar = true;
                    FiltradoGeneral_MtoAsync();
                }
                OnPropertyChanged(nameof(SelectLugar));
            }
        }

        public ListasGeneral SelectFranja
        {
            get { return _selectFranja; }
            set
            {
                SetProperty(ref _selectFranja, value);
                if (value != null)
                {
                    FlechaFranja = false;
                    EquisFranja = true;
                    FiltradoGeneral_MtoAsync();
                }
                OnPropertyChanged(nameof(SelectFranja));
            }
        }

        public ObservableCollection<ListasGeneral> ListaCategoria
        {
            get { return _listaCategoria; }
            set { _listaCategoria = value; OnPropertyChanged(nameof(ListaCategoria)); }
        }

        public ObservableCollection<ListasGeneral> ListaFranja
        {
            get { return _listaFranja; }
            set { _listaFranja = value; OnPropertyChanged(nameof(ListaFranja)); }
        }

        public ObservableCollection<ListasGeneral> ListaLugar
        {
            get { return _listaLugar; }
            set { _listaLugar = value; OnPropertyChanged(nameof(ListaLugar)); }
        }

        public string CantidadAgenda
        {
            get { return _cantidadAgenda; }
            set { _cantidadAgenda = value; OnPropertyChanged(nameof(CantidadAgenda)); }
        }

        public Color ColorEntryConferencista
        {
            get { return _colorEntryConferencista; }
            set { _colorEntryConferencista = value; OnPropertyChanged(nameof(ColorEntryConferencista)); }
        }

        public bool IsReadOnlyEntryConfe
        {
            get { return _isReadOnlyEntryConfe; }
            set { _isReadOnlyEntryConfe = value; OnPropertyChanged(nameof(IsReadOnlyEntryConfe)); }
        }

        public bool EquisCalendario
        {
            get { return _equisCalendario; }
            set { _equisCalendario = value; OnPropertyChanged(nameof(EquisCalendario)); }
        }

        public bool FlechaCalendario
        {
            get { return _flechaCalendario; }
            set { _flechaCalendario = value; OnPropertyChanged(nameof(FlechaCalendario)); }
        }

        public bool EquisCategoria
        {
            get { return _equisCategoria; }
            set { _equisCategoria = value; OnPropertyChanged(nameof(EquisCategoria)); }
        }

        public bool FlechaCategoria
        {
            get { return _flechaCategoria; }
            set { _flechaCategoria = value; OnPropertyChanged(nameof(FlechaCategoria)); }
        }

        public bool EquisLugar
        {
            get { return _equisLugar; }
            set { _equisLugar = value; OnPropertyChanged(nameof(EquisLugar)); }
        }

        public bool FlechaLugar
        {
            get { return _flechaLugar; }
            set { _flechaLugar = value; OnPropertyChanged(nameof(FlechaLugar)); }
        }

        public bool FlechaFranja
        {
            get { return _flechaFranja; }
            set { _flechaFranja = value; OnPropertyChanged(nameof(FlechaFranja)); }
        }

        public bool EquisFranja
        {
            get { return _equisFranja; }
            set { _equisFranja = value; OnPropertyChanged(nameof(EquisFranja)); }
        }

        public List<Agenda> ListaAgenda
        {
            get { return listaAgenda; }
            set { listaAgenda = value; OnPropertyChanged(); }
        }

        public string? Filtro1
        {
            get { return _Filtro1; }
            set { _Filtro1 = value; OnPropertyChanged(nameof(Filtro1)); }
        }

        public string? Filtro2
        {
            get { return _Filtro2; }
            set { _Filtro2 = value; OnPropertyChanged(nameof(Filtro2)); }
        }

        public string? Filtro5
        {
            get { return _Filtro5; }
            set { _Filtro5 = value; OnPropertyChanged(nameof(Filtro5)); }
        }

        public string? Filtro3
        {
            get { return _Filtro3; }
            set { _Filtro3 = value; OnPropertyChanged(nameof(Filtro3)); }
        }

        public string? Filtro4
        {
            get { return _Filtro4; }
            set { _Filtro4 = value; OnPropertyChanged(nameof(Filtro4)); }
        }

        public string? EntConferencista
        {
            get { return _entConferencista; }
            set { _entConferencista = value; OnPropertyChanged(nameof(EntConferencista)); }
        }

        private void SeleccionarAgenda(Agenda agenda)
        {
            if (agenda != null)
            {
                CargarAGenda(agenda);
            }
        }

        public Agenda SelectAgenda
        {
            get { return _SelectedAgenda; }
            set
            {
                _SelectedAgenda = null;
                ItemSelectedCommandAgenda.Execute(value);
                OnPropertyChanged(nameof(SelectAgenda));
            }
        }

        public Command BtnCargarSitios { get; set; }

        public Command<Agenda> SeleccionarAgendaCommand { get; }

        string idSitio_;
        string AuxIdAgendFiltro_;

        public AgendaSitiosPopUpVm(string idSuceso, string idSitio, Entry filtroTres, string AuxIdAgendFiltro)
        {
            Title = AppResources.agenda;

            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            IsBusy = true;
            idSitio_ = idSitio;
            AuxIdAgendFiltro_ = AuxIdAgendFiltro;

            IdSuceso = Preferences.Get("IdSuceso", "");
            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");

            FocusPicker = new Command(claseBase.FocusPicker_Mto);
            FocusCalendario = new Command(claseBase.FocusPickerDate_Mtd);
            LimpiarLugar = new Command(() => LimpiarLugar_Mto());
            LimpiarFranja = new Command(() => LimpiarFranja_Mto());
            OrdenarAgenda = new Command(() => OrdenarAgenda_Mto());
            FavButtonCommand = new Command(ActualizarFav_MtoAsync);
            LimpiarCategoria = new Command(() => LimpiarCategoria_Mto());
            LimpiarCalendario = new Command(() => LimpiarCalendario_Mto());
            BuscarEntAgenda = new Command(() => FiltradoGeneral_MtoAsync());
            BtnCargarSitios = new Command(async () => await BtnCargarSitios_Mtd());
            SeleccionarAgendaCommand = new Command<Agenda>(SeleccionarAgenda);

            filtroTres.Completed += (sender, e) => EntryCompleted(sender, e);

            Inicializar();
        }
        void EntryCompleted(object sender, EventArgs e)
        {
            try
            {
                FiltradoGeneral_MtoAsync();
            }
            catch (Exception ex)
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaSitiosPopUpVm", "EntryCompleted", "n/a");
            }
        }

        public async Task BtnCargarSitios_Mtd()
        {
            //VALIDAR CON LINA
            //await Application.Current.MainPage.Navigation.PushPopupAsync(new SitiosPage(IdSuceso, "2", "3"));
        }

        private async void Inicializar()
        {
            try
            {
                ColorEntryConferencista = Color.FromArgb("#333333");
                await ListasFiltros_MtoAsync();
                await CargarAgenda_MtoAsync(IdSuceso);
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaSitiosPopUpVm", "Inicializar", "n/a");
            }
        }

        int horaActual;

        private async Task CargarAgenda_MtoAsync(string idSuceso)
        {
            try
            {
                horaActual = EntregaHoraMilitar(DateTime.Now.ToString("h:mm:ss tt"), "2");

                ListaAgenda = new List<Agenda>();
                listaAgendaTem = new List<Agenda>();
                consultaAgenda = new ConsultaAgenda
                {
                    Categoria = "0",
                    FechaInicio = "0",
                    IdAgenda = "0",
                    Lugar = "0",
                    Franja = "0"
                };

                string urli = logicaWs.Moviel_select_consultaagendasuceso_Mtd("0", "3", EmailUsuario, Preferences.Get("IdSuceso", "0"), LenguajeBase, "0", idSitio_, "0");

                string json = JsonConvert.SerializeObject(consultaAgenda);
                string jsonProcedimiento = await logicaWs.ConectionPost(json, urli);

                if (!claseBase.ValidaString(jsonProcedimiento).Equals(""))
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    foreach (JObject item in jsArray)
                    {
                        List<string> Conferencistas = claseBase.ValidaString(item.GetValue("Conferencistas").ToString()).Split(',').ToList();
                        List<string> idConferencistas = item.GetValue("IDCONFERENCISTA").ToString().Split(',').ToList();
                        DateTime a = new DateTime();
                        if (DateTime.TryParseExact(claseBase.ValidaString(item.GetValue("FechaActiva")?.ToString() ?? string.Empty), "dd/MM/yyyy", null, DateTimeStyles.None, out a))
                            a = DateTime.ParseExact(claseBase.ValidaString(item.GetValue("FechaActiva")?.ToString() ?? string.Empty), "dd/MM/yyyy", null);

                        DateTime b = new DateTime();
                        if (DateTime.TryParseExact(claseBase.ValidaString(item.GetValue("FechaInActiva")?.ToString() ?? string.Empty), "dd/MM/yyyy", null, DateTimeStyles.None, out a))
                            b = DateTime.ParseExact(claseBase.ValidaString(item.GetValue("FechaInActiva")?.ToString() ?? string.Empty), "dd/MM/yyyy", null);

                        DateTime c = new DateTime();
                        if (DateTime.TryParseExact(claseBase.ValidaString(item.GetValue("FechaInicio")?.ToString() ?? string.Empty), "dd/MM/yyyy", null, DateTimeStyles.None, out a))
                            c = DateTime.ParseExact(claseBase.ValidaString(item.GetValue("FechaInicio")?.ToString() ?? string.Empty), "dd/MM/yyyy", null);

                        Agenda agenda = new Agenda(
                            claseBase.ValidaString(item.GetValue("IdAgenda")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("IdSuceso")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("IdAgendaServicio")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("Titulo")?.ToString() ?? string.Empty.ToUpper()),
                            claseBase.ValidaString(item.GetValue("Categoria")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("Lugar")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("DiaIn")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("MesIn")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("DiaFin")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("MesFin")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("HoraInicio")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("HoraFin")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("Aforo")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("Estado")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("Imagen")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("IconoFeria")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("ContextoClasificacion")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("Descripcion")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("PalabrasClave")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("Motivocancelacion")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("FechaCancelacion")?.ToString() ?? string.Empty),
                            a,
                            b,
                            c,
                             Conferencistas, idConferencistas,
                            item.GetValue("fav")?.ToString() ?? string.Empty,
                            claseBase.ValidaString(item.GetValue("Franja")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("Organizador")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("programacionoficial")?.ToString() ?? string.Empty));

                        if (!agenda.Estado.Equals(""))
                            agenda.Lugar = "";
                        listaAgendaTem.Add(agenda);

                        if (c >= DateTime.Now)
                        {
                            if (c.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")
                                && horaActual <= EntregaHoraMilitar(item.GetValue("HoraInicio")?.ToString() ?? string.Empty, "2"))
                            {
                                ListaAgenda.Add(agenda);
                            }
                            else if (c.ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy"))
                            {
                                ListaAgenda.Add(agenda);

                            }
                        }
                    }
                }

                ListaAgenda = ListaAgenda.OrderBy(x => x.FechaInicio).ToList();
                listaAgendaTem = listaAgendaTem.OrderBy(x => x.FechaInicio).ToList();
                CantidadAgenda = ListaAgenda.Count.ToString() + " " + AppResources.resultados;

                auxFecha = true;

            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaSitiosPopUpVm", "CargarAgenda_MtoAsync", "consultaagendasuceso");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void CargarAGenda(Agenda agenda)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar

                if (agenda.Estado.Equals(""))
                    await RootNavigation.PushAsync(new DetalleAgendaView(agenda), false);
                else if (agenda.Estado.Equals("CANCELADO"))
                    pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.esteEventoFueCancelado, AppResources.VMaceptar);
                else
                    pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);

            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaSitiosPopUpVm", "CargarAGenda", "n/a");
            }
            finally
            {
                IsBusy = false;
                await MopupService.Instance.PopAsync();
            }
        }


        private async Task ListasFiltros_MtoAsync()
        {
            try
            {
                if (!StateConexion)
                {

                    /* Listado categoria */
                    ListaCategoria = new ObservableCollection<ListasGeneral>();
                    string urli = logicaWs.Movile_Select_Opcion_Categoria_Mtd(IdSuceso, EmailUsuario, LenguajeBase);
                    string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                    JArray jsArray;
                    if (!jsonProcedimiento.Equals(""))
                    {
                        jsArray = JArray.Parse(jsonProcedimiento);
                        foreach (JObject item in jsArray)
                        {
                            ListasGeneral listasGeneral = new ListasGeneral { Descripcion = item.GetValue("Descripcion")?.ToString() ?? string.Empty, Id = item.GetValue("Id")?.ToString() ?? string.Empty };
                            ListaCategoria.Add(listasGeneral);
                        }
                    }

                    if (ListaCategoria.Count() == 0)
                    {
                        ListasGeneral listasGeneral = new ListasGeneral { Descripcion = AppResources.laConsultaGeneradaNoCuentaConRegistro, Id = "0" };
                        ListaCategoria.Add(listasGeneral);
                    }

                    /* Listado lugar */
                    ListaLugar = new ObservableCollection<ListasGeneral>();
                    urli = logicaWs.Movile_Select_Opcion_Lugar_Mtd(IdSuceso, EmailUsuario, LenguajeBase);
                    jsonProcedimiento = await logicaWs.ConectionGet(urli);
                    if (!jsonProcedimiento.Equals(""))
                    {
                        jsArray = JArray.Parse(jsonProcedimiento);
                        foreach (JObject item in jsArray)
                        {
                            ListasGeneral listasGeneral = new ListasGeneral { Descripcion = item.GetValue("Descripcion")?.ToString() ?? string.Empty, Id = item.GetValue("Id")?.ToString() ?? string.Empty };
                            ListaLugar.Add(listasGeneral);
                        }
                    }

                    if (ListaLugar.Count() == 0)
                    {
                        ListasGeneral listasGeneral = new ListasGeneral { Descripcion = AppResources.laConsultaGeneradaNoCuentaConRegistro, Id = "0" };
                        ListaLugar.Add(listasGeneral);
                    }

                    /* Listado Franja */
                    ListaFranja = new ObservableCollection<ListasGeneral>();
                    FiltroFranja filtroFranja = new FiltroFranja()
                    {
                        correo = EmailUsuario,
                        Idioma = LenguajeBase
                    };

                    string AuxBandera;
                    AuxBandera = "3";

                    string urliFranja = logicaWs.Movile_consAFiltroFranja_Mtd(AuxBandera, IdSuceso, AuxIdAgendFiltro_);
                    string jsonFranja = JsonConvert.SerializeObject(filtroFranja);
                    string jsonProcedimientoFranja = await logicaWs.ConectionPost(jsonFranja, urliFranja);
                    JArray jsArrayFranja;
                    if (!jsonProcedimiento.Equals(""))
                    {
                        jsArrayFranja = JArray.Parse(jsonProcedimientoFranja);
                        foreach (JObject item in jsArrayFranja)
                        {
                            ListasGeneral listasGeneral = new ListasGeneral { Descripcion = item.GetValue("Descripcion")?.ToString() ?? string.Empty, Id = item.GetValue("ID")?.ToString() ?? string.Empty };
                            ListaFranja.Add(listasGeneral);
                        }
                    }

                    /* Listado mascara filtros */
                    string urlfilter = logicaWs.Movile_Select_Mascara_Agenda_Mtd(IdSuceso, LenguajeBase);
                    jsonProcedimiento = await logicaWs.ConectionGet(urlfilter);
                    if (!jsonProcedimiento.Equals(""))
                    {
                        try
                        {
                            jsArray = JArray.Parse(jsonProcedimiento);
                            foreach (JObject item in jsArray)
                            {
                                if (!claseBase.ValidaString(item.GetValue("clasificacion").ToString()).Equals(""))
                                    Filtro1 = item.GetValue("clasificacion")?.ToString();

                                if (!claseBase.ValidaString(item.GetValue("lugar").ToString()).Equals(""))
                                    Filtro2 = item.GetValue("lugar")?.ToString();

                                if (!claseBase.ValidaString(item.GetValue("conferencista").ToString()).Equals(""))
                                    Filtro3 = item.GetValue("conferencista")?.ToString();

                                if (!claseBase.ValidaString(item.GetValue("fecha").ToString()).Equals(""))
                                    Filtro4 = item.GetValue("fecha")?.ToString();

                                if (!claseBase.ValidaString(item.GetValue("Franja").ToString()).Equals(""))
                                    Filtro5 = item.GetValue("Franja")?.ToString();

                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            claseBase.InsertarLogs_Mtd("EXCEPTION", ex.Message, "AgendaSitiosPopUpVm", "ListasFiltros_MtoAsync", "consultamascarasagendasuceso");
                        }
                    }

                    if (claseBase.ValidaString(Filtro1).Equals(""))
                        Filtro1 = AppResources.categoria;

                    if (claseBase.ValidaString(Filtro2).Equals(""))
                        Filtro2 = AppResources.lugar;

                    if (claseBase.ValidaString(Filtro3).Equals(""))
                        Filtro3 = AppResources.conferencistaInvitado;

                    if (claseBase.ValidaString(Filtro4).Equals(""))
                        Filtro4 = "dd/mm/yyyy";

                    if (claseBase.ValidaString(Filtro5).Equals(""))
                        Filtro5 = "Franja";

                    FlechaLugar = true;
                    FlechaCategoria = true;
                    FlechaCalendario = true;
                    FlechaFranja = true;
                }
                else
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaSitiosPopUpVm", "ListasFiltros_MtoAsync", "Servicios multilistas(filtros)");
            }
        }

        private async void FiltradoGeneral_MtoAsync()
        {
            IsBusy = true;

            try
            {
                ListaAgenda = listaAgendaTem.ToList();

                if (EquisCategoria)
                    ListaAgenda = ListaAgenda.Where(x => x.Categoria.ToLower().Contains(SelectCategoria.Descripcion.ToLower())).ToList();

                if (EquisLugar)
                    ListaAgenda = ListaAgenda.Where(x => x.Lugar.ToLower().Contains(SelectLugar.Descripcion.ToLower())).ToList();

                if (EquisCalendario)
                    ListaAgenda = ListaAgenda.Where(x => x.FechaInicio.Equals(EntFechaBuscar)).ToList();

                if (EquisFranja)
                    ListaAgenda = ListaAgenda.Where(x => x.Franja.ToLower().Contains(SelectFranja.Descripcion.ToLower())).ToList();

                if (!claseBase.ValidaString(EntConferencista).Equals(""))
                    ListaAgenda = ListaAgenda.Where(x => x.NameList.ToLower().Contains(EntConferencista.ToLower().Trim())
                    || x.Titulo.ToLower().Contains(EntConferencista.ToLower().Trim()) ||
                    x.Categoria.ToLower().Contains(EntConferencista.ToLower().Trim())).ToList();

                CantidadAgenda = ListaAgenda.Count.ToString() + " " + AppResources.resultados;
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaSitiosPopUpVm", "FiltradoGeneral_MtoAsync", "n/a");
            }

            auxFecha = true;
            IsBusy = false;
        }

        private void LimpiarCalendario_Mto()
        {
            try
            {
                IsBusy = true;
                auxFecha = false;
                FlechaCalendario = true;
                EquisCalendario = false;
                EntFechaBuscar = DateTime.Now;
                FiltradoGeneral_MtoAsync();
            }
            catch (Exception ex)
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaSitiosPopUpVm", "LimpiarCalendario_Mto", "n/a");
            }
        }

        private void LimpiarLugar_Mto()
        {
            try
            {
                IsBusy = true;
                FlechaLugar = true;
                EquisLugar = false;
                ObservableCollection<ListasGeneral> team = ListaLugar;
                ListaLugar = new ObservableCollection<ListasGeneral>();
                ListaLugar = team;
                FiltradoGeneral_MtoAsync();
            }
            catch (Exception ex)
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaSitiosPopUpVm", "LimpiarLugar_Mto", "n/a");
            }
        }

        private void LimpiarFranja_Mto()
        {
            try
            {
                IsBusy = true;
                FlechaFranja = true;
                EquisFranja = false;
                ObservableCollection<ListasGeneral> team = ListaFranja;
                ListaFranja = new ObservableCollection<ListasGeneral>();
                ListaFranja = team;
                FiltradoGeneral_MtoAsync();
            }
            catch (Exception ex)
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaSitiosPopUpVm", "LimpiarFranja_Mto", "n/a");
            }
        }

        private void LimpiarCategoria_Mto()
        {
            try
            {
                IsBusy = true;
                FlechaCategoria = true;
                EquisCategoria = false;
                ObservableCollection<ListasGeneral> team = ListaCategoria;
                ListaCategoria = new ObservableCollection<ListasGeneral>();
                ListaCategoria = team;
                FiltradoGeneral_MtoAsync();
            }
            catch (Exception ex)
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaSitiosPopUpVm", "LimpiarCategoria_Mto", "n/a");
            }
        }

        private void OrdenarAgenda_Mto()
        {
            try
            {
                ListaAgenda.Reverse();
                ListaAgenda = ListaAgenda.ToList();
            }
            catch (Exception ex)
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaSitiosPopUpVm", "OrdenarAgenda_Mto", "n/a");
            }
        }

        private async void ActualizarFav_MtoAsync(object obj)
        {
            if (!StateConexion)
            {
                try
                {
                    var content = obj as Agenda;

                    foreach (var item in ListaAgenda)
                    {
                        if (item.IdAgenda.Equals(content.IdAgenda))
                        {
                            if (item.Fav.Equals("1")) /*1 Para favorito*/
                            {
                                item.ImagenFav = "ic_favorito_obscuro.png";
                                item.Fav = "0";

                                UptFranja ip = new UptFranja()
                                {
                                    Franja = "0"
                                };

                                string jsonip = JsonConvert.SerializeObject(ip);
                                string urli = logicaWs.Movile_Delet_Fav_Agenda_Mtd(EmailUsuario, item.IdAgenda, "0");
                                var respu = await logicaWs.ConectionPost(jsonip, urli);
                            }
                            else
                            {
                                item.ImagenFav = "ic_favortio_relleno.png";
                                item.Fav = "1";
                                UptFranja ip = new UptFranja()
                                {
                                    Franja = "0"
                                };

                                string jsonip = JsonConvert.SerializeObject(ip);
                                string urli = logicaWs.Movile_Update_Fav_Agenda_Mtd(EmailUsuario, item.IdAgenda, "0");
                                var respu = await logicaWs.ConectionPost(jsonip, urli);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ERROR " + ex.Message);
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);

                    string msmError = ex.Message;
                    if (msmError.Length > 150)
                        msmError = msmError.Substring(0, 148);

                    string urliEx = logicaWs.Movile_Insert_Logs_Mtd();
                    Logs logs = new Logs("AgendaSitiosPopUpVm", "ERROR " + msmError, "ActualizarFav_MtoAsync", "eliminafavoritoagenda/insertafavoritoagenda");
                    string json = JsonConvert.SerializeObject(logs);
                    await logicaWs.ConectionPost(json, urliEx);
                }
            }
            else
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
        }

        private int EntregaHoraMilitar(string Hora, string Tipo)
        {
            try
            {
                string AmPm;
                int HoraFinal;

                if (Hora.ToLower().Replace(" ", "").Contains("a"))
                {
                    AmPm = "am";
                }
                else
                {
                    AmPm = "pm";
                }

                if (Tipo == "1")

                {
                    var splitvar = Hora.Split('.');
                    HoraFinal = int.Parse(splitvar[0]);
                }
                else
                {
                    var splitvar = Hora.Split(':');
                    HoraFinal = int.Parse(splitvar[0]);
                }

                if (AmPm.Equals("pm"))
                {
                    HoraFinal = HoraFinal + 12;
                }

                return HoraFinal;
            }
            catch
            {
                return 1;
            }
        }
    }

    public class FiltroFranja
    {
        public string? correo { get; set; }
        public string? Idioma { get; set; }

    }
}