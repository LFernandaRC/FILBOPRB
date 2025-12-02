using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Usuario;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace EventosCorferias.ViewModel.Usuario
{
    public class AgendaVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;

        private readonly string IdSuceso;
        private readonly string IdConferencista;
        private readonly string AuxIdAgendFiltro_;
        private readonly string IdContenido_;

        private List<Agenda> listaAgenda;
        private List<Agenda> listaAgendaTem;

        private ObservableCollection<ListasGeneral> _listaLugar;
        private ObservableCollection<ListasGeneral> _listaCategoria;
        private ObservableCollection<ListasGeneral> _listaFranja;

        private ListasGeneral _selectLugar;
        private ListasGeneral _selectFranja;
        private ListasGeneral _selectCategoria;

        public ConsultaAgenda consultaAgenda;
        private DateTime _entFechaBuscar = DateTime.Now;

        private string _Filtro1;
        private string _Filtro2;
        private string _Filtro3;
        private string _Filtro4;
        private string _Filtro5;
        private string _cantidadAgenda;
        private string _entConferencista;

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
        private bool _sinRegistros;
        public bool esfavorti;
        public bool OrigenPrincipal_;

        private int horaActual;

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

        public string EntConferencista
        {
            get { return _entConferencista; }
            set { _entConferencista = value; OnPropertyChanged(nameof(EntConferencista)); }
        }

        public string CantidadAgenda
        {
            get { return _cantidadAgenda; }
            set { _cantidadAgenda = value; OnPropertyChanged(nameof(CantidadAgenda)); }
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

        public bool SinRegistros
        {
            get { return _sinRegistros; }
            set { _sinRegistros = value; OnPropertyChanged(nameof(SinRegistros)); }
        }

        public List<Agenda> ListaAgenda
        {
            get { return listaAgenda; }
            set { listaAgenda = value; OnPropertyChanged(); }
        }

        public string Filtro1
        {
            get { return _Filtro1; }
            set { _Filtro1 = value; OnPropertyChanged(nameof(Filtro1)); }
        }

        public string Filtro2
        {
            get { return _Filtro2; }
            set { _Filtro2 = value; OnPropertyChanged(nameof(Filtro2)); }
        }

        public string Filtro3
        {
            get { return _Filtro3; }
            set { _Filtro3 = value; OnPropertyChanged(nameof(Filtro3)); }
        }

        public string Filtro4
        {
            get { return _Filtro4; }
            set { _Filtro4 = value; OnPropertyChanged(nameof(Filtro4)); }
        }

        public string Filtro5
        {
            get { return _Filtro5; }
            set { _Filtro5 = value; OnPropertyChanged(nameof(Filtro5)); }
        }


        public ICommand LimpiarFranja { get; }

        public ICommand LimpiarLugar { get; }

        public ICommand OrdenarAgenda { get; }

        public ICommand BuscarEntAgenda { get; }

        public ICommand FocusCalendario { get; }

        public ICommand LimpiarCategoria { get; }

        public ICommand FavButtonCommand { get; }

        public ICommand LimpiarCalendario { get; }

        public ICommand FocusPicker { get; }

        public Command<Agenda> SeleccionarAgendaCommand { get; }

        public AgendaVm(string idSuceso, string idConferencista, string nombreConferncista, bool favg, string IdContenido,
            Entry filtroTres, bool OrigenPrincipal, string AuxIdAgendFiltro)
        {
            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            esfavorti = favg;
            IdContenido_ = IdContenido;
            IdConferencista = idConferencista;
            OrigenPrincipal_ = OrigenPrincipal;
            AuxIdAgendFiltro_ = AuxIdAgendFiltro;
            EntConferencista = nombreConferncista;

            IdSuceso = Preferences.Get("IdSuceso", "");
            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            ImagenSplash = logicaWs.ImgMenuSuperior_Mtd();

            FocusPicker = new Command(claseBase.FocusPicker_Mto);
            FocusCalendario = new Command(claseBase.FocusPickerDate_Mtd);
            LimpiarLugar = new Command(LimpiarLugar_Mto);
            LimpiarFranja = new Command(LimpiarFranja_Mto);
            OrdenarAgenda = new Command(OrdenarAgenda_Mto);
            FavButtonCommand = new Command(ActualizarFav_MtoAsync);
            LimpiarCategoria = new Command(LimpiarCategoria_Mto);
            LimpiarCalendario = new Command(LimpiarCalendario_Mto);
            BuscarEntAgenda = new Command(FiltradoGeneral_MtoAsync);
            SeleccionarAgendaCommand = new Command<Agenda>(SeleccionarAgenda);

            filtroTres.Completed += EntryCompleted;

            Inicializar();
        }

        /* INICIO CARGA INICIAL*/
        private async void Inicializar()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await ListasFiltros_MtoAsync();
                await CargarAgenda_MtoAsync(IdSuceso, IdConferencista);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ListasFiltros_MtoAsync()
        {
            try
            {
                /* Listado categoria */
                ListaCategoria = new ObservableCollection<ListasGeneral>();
                string urli = logicaWs.Movile_Select_Opcion_Categoria_Mtd(IdSuceso, EmailUsuario, LenguajeBase);
                string? jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray;
                if (!jsonProcedimiento.Equals(""))
                {
                    jsArray = JArray.Parse(jsonProcedimiento);
                    foreach (JObject item in jsArray)
                    {
                        ListasGeneral listasGeneral = new ListasGeneral { Descripcion = item.GetValue("Descripcion")?.ToString(), Id = item.GetValue("Id")?.ToString() };
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
                        ListasGeneral listasGeneral = new ListasGeneral { Descripcion = item.GetValue("Descripcion")?.ToString(), Id = item.GetValue("Id")?.ToString() };
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
                if (esfavorti)
                {
                    AuxBandera = "2";
                }
                else if (!OrigenPrincipal_)
                {
                    AuxBandera = "3";
                }
                else
                {
                    AuxBandera = "1";
                }

                string urliFranja = logicaWs.Movile_consAFiltroFranja_Mtd(AuxBandera, IdSuceso, AuxIdAgendFiltro_);
                string jsonFranja = JsonConvert.SerializeObject(filtroFranja);
                string? jsonProcedimientoFranja = await logicaWs.ConectionPost(jsonFranja, urliFranja);
                JArray jsArrayFranja;
                if (!jsonProcedimiento.Equals(""))
                {
                    jsArrayFranja = JArray.Parse(jsonProcedimientoFranja);
                    foreach (JObject item in jsArrayFranja)
                    {
                        ListasGeneral listasGeneral = new ListasGeneral { Descripcion = item.GetValue("Descripcion")?.ToString(), Id = item.GetValue("ID")?.ToString() };
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
                                Filtro1 = item.GetValue("clasificacion")?.ToString() ?? string.Empty;

                            if (!claseBase.ValidaString(item.GetValue("lugar").ToString()).Equals(""))
                                Filtro2 = item.GetValue("lugar")?.ToString() ?? string.Empty;

                            if (!claseBase.ValidaString(item.GetValue("conferencista").ToString()).Equals(""))
                                Filtro3 = item.GetValue("conferencista")?.ToString() ?? string.Empty;

                            if (!claseBase.ValidaString(item.GetValue("fecha").ToString()).Equals(""))
                                Filtro4 = item.GetValue("fecha")?.ToString() ?? string.Empty;

                            if (!claseBase.ValidaString(item.GetValue("Franja").ToString()).Equals(""))
                                Filtro5 = item.GetValue("Franja")?.ToString() ?? string.Empty;

                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaVM", "ListasFiltros_MtoAsync", "consultamascarasagendasuceso");
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
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaVM", "ListasFiltros_MtoAsync2", "Servicios multilistas(filtros)");
            }
        }

        private async Task CargarAgenda_MtoAsync(string idSuceso, string idConferencista)
        {
            try
            {
                horaActual = EntregaHoraMilitar(DateTime.Now.ToString("h:mm:ss tt"), "2");

                ListaAgenda = new List<Agenda>();
                listaAgendaTem = new List<Agenda>();
                string urli;
                consultaAgenda = new ConsultaAgenda
                {
                    Categoria = "0",
                    FechaInicio = "0",
                    IdAgenda = "0",
                    Lugar = "0",
                    Franja = "0"
                };

                if (esfavorti)
                {
                    urli = logicaWs.Moviel_select_consultaagendasuceso_Mtd("0", "5", EmailUsuario, idSuceso, LenguajeBase, idConferencista, IdContenido_);
                }
                else
                {
                    urli = logicaWs.Moviel_select_consultaagendasuceso_Mtd("0", "1", EmailUsuario, idSuceso, LenguajeBase, idConferencista, IdContenido_);
                }

                string json = JsonConvert.SerializeObject(consultaAgenda);
                string? jsonProcedimiento = await logicaWs.ConectionPost(json, urli);

                if (!claseBase.ValidaString(jsonProcedimiento).Equals(""))
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    foreach (JObject item in jsArray)
                    {
                        List<string> Conferencistas = claseBase.ValidaString(item.GetValue("Conferencistas").ToString()).Split(',').ToList();
                        List<string> idConferencistas = item.GetValue("IDCONFERENCISTA").ToString().Split(',').ToList();
                        DateTime a = new DateTime();
                        if (DateTime.TryParseExact(claseBase.ValidaString(item.GetValue("FechaActiva").ToString()), "dd/MM/yyyy", null, DateTimeStyles.None, out a))
                            a = DateTime.ParseExact(claseBase.ValidaString(item.GetValue("FechaActiva").ToString()), "dd/MM/yyyy", null);

                        DateTime b = new DateTime();
                        if (DateTime.TryParseExact(claseBase.ValidaString(item.GetValue("FechaInActiva").ToString()), "dd/MM/yyyy", null, DateTimeStyles.None, out a))
                            b = DateTime.ParseExact(claseBase.ValidaString(item.GetValue("FechaInActiva").ToString()), "dd/MM/yyyy", null);

                        DateTime c = new DateTime();
                        if (DateTime.TryParseExact(claseBase.ValidaString(item.GetValue("FechaInicio").ToString()), "dd/MM/yyyy", null, DateTimeStyles.None, out a))
                            c = DateTime.ParseExact(claseBase.ValidaString(item.GetValue("FechaInicio").ToString()), "dd/MM/yyyy", null);

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
                            claseBase.ValidaString(item.GetValue("Organizador")?.ToString() ?? string.Empty));

                        if (!agenda.Estado.Equals(""))
                            agenda.Lugar = "";
                        listaAgendaTem.Add(agenda);

                        if (c >= DateTime.Today)
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

                        foreach (var x in ListaAgenda)
                        {
                            if (x.IdAgenda.Equals(agenda.IdAgenda))
                            {
                                if (x.Fav.Equals("1"))
                                {
                                    x.ImagenFav = "ic_favortio_relleno.png";
                                }
                                else
                                {
                                    x.ImagenFav = "ic_favorito_obscuro.png";
                                }
                            }
                        }
                    }
                }
                ListaAgenda = ListaAgenda.OrderBy(x => x.FechaInicio).ToList();
                listaAgendaTem = listaAgendaTem.OrderBy(x => x.FechaInicio).ToList();
                CantidadAgenda = ListaAgenda.Count.ToString() + " " + AppResources.resultados;

                if (ListaAgenda.Count == 0)
                    SinRegistros = true;

                auxFecha = true;
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaVM", "CargarAgenda_MtoAsync", "consultaagendasuceso");
            }
            IsBusy = false;
        }

        void EntryCompleted(object sender, EventArgs e)
        {
            FiltradoGeneral_MtoAsync();
        }
        /* FIN CARGA INICIAL*/

        private async void FiltradoGeneral_MtoAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
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
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaVM", "FiltradoGeneral_MtoAsync", "Error filtrado general");
            }
            finally
            {
                auxFecha = true;
                IsBusy = false;
            }
        }

        private void LimpiarCalendario_Mto()
        {
            auxFecha = false;
            FlechaCalendario = true;
            EquisCalendario = false;
            EntFechaBuscar = DateTime.Now;
            FiltradoGeneral_MtoAsync();
        }

        private void LimpiarLugar_Mto()
        {
            FlechaLugar = true;
            EquisLugar = false;
            ObservableCollection<ListasGeneral> team = ListaLugar;
            ListaLugar = new ObservableCollection<ListasGeneral>();
            ListaLugar = team;
            FiltradoGeneral_MtoAsync();
        }

        private void LimpiarFranja_Mto()
        {
            FlechaFranja = true;
            EquisFranja = false;
            ObservableCollection<ListasGeneral> team = ListaFranja;
            ListaFranja = new ObservableCollection<ListasGeneral>();
            ListaFranja = team;
            FiltradoGeneral_MtoAsync();
        }

        private void LimpiarCategoria_Mto()
        {
            FlechaCategoria = true;
            EquisCategoria = false;
            ObservableCollection<ListasGeneral> team = ListaCategoria;
            ListaCategoria = new ObservableCollection<ListasGeneral>();
            ListaCategoria = team;
            FiltradoGeneral_MtoAsync();
        }

        private async void OrdenarAgenda_Mto()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                ListaAgenda.Reverse();
                ListaAgenda = ListaAgenda.ToList();
            }
            finally
            {
                IsBusy = false;
            }

        }

        private async void ActualizarFav_MtoAsync(object obj)
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
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaVM", "ActualizarFav_MtoAsync", "eliminafavoritoagenda");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
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
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaVM", "EntregaHoraMilitar", "n/a");
                return 1;
            }
        }

        private void SeleccionarAgenda(Agenda agenda)
        {
            if (agenda != null)
            {
                CargarAGenda(agenda);
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
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AgendaVM", "CargarAGenda", "redireccionamieto");
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
    public class FiltroFranja
    {
        public string? correo { get; set; }
        public string? Idioma { get; set; }

    }
}
