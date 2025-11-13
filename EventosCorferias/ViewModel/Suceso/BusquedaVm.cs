using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Suceso;
using EventosCorferias.Views.Usuario;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace EventosCorferias.ViewModel.Suceso
{
    public class BusquedaVm : BaseViewModel
    {
        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;

        private ObservableCollection<Busqueda> _listaBusqueda;
        private ObservableCollection<Busqueda> listaBusquedaTem;

        private ObservableCollection<ListasGeneral> _listaModulo;

        private ListasGeneral _selectModulo;

        private string _cantidadBusqueda;
        private string _entQueEstasBuscando;

        private bool _bloquearCampo;
        private bool _equisModulo;
        private bool _flechaModulo;

        private Picker pus;
        private Entry EntryBsq_;

        public ObservableCollection<Busqueda> ListaBusqueda
        {
            get { return _listaBusqueda; }
            set { _listaBusqueda = value; OnPropertyChanged(nameof(ListaBusqueda)); }
        }

        public ObservableCollection<ListasGeneral> ListaModulo
        {
            get { return _listaModulo; }
            set { _listaModulo = value; OnPropertyChanged(nameof(ListaModulo)); }
        }

        public ListasGeneral SelectModulo
        {
            get => _selectModulo;
            set
            {
                if (_selectModulo != value)
                {
                    _selectModulo = value;
                    OnPropertyChanged(nameof(SelectModulo));
                    EquisModulo = true;
                    FlechaModulo = false;
                    FiltradoGeneral_MtdAsync();
                }
            }
        }

        public string CantidadBusqueda
        {
            get { return _cantidadBusqueda; }
            set { _cantidadBusqueda = value; OnPropertyChanged(nameof(CantidadBusqueda)); }
        }

        public string EntQueEstasBuscando
        {
            get { return _entQueEstasBuscando; }
            set { _entQueEstasBuscando = value; OnPropertyChanged(nameof(EntQueEstasBuscando)); }
        }

        public bool BloquearCampo
        {
            get => _bloquearCampo;
            set
            {
                _bloquearCampo = value;
                OnPropertyChanged(nameof(BloquearCampo));
            }
        }

        public bool EquisModulo
        {
            get { return _equisModulo; }
            set { _equisModulo = value; OnPropertyChanged(nameof(EquisModulo)); }
        }

        public bool FlechaModulo
        {
            get { return _flechaModulo; }
            set { _flechaModulo = value; OnPropertyChanged(nameof(FlechaModulo)); }
        }

        public Command<Busqueda> SeleccionarModuloCommand { get; }

        public ICommand BuscarBtn { get; }

        public ICommand FocusPicker { get; }

        public ICommand LimpiarModulo { get; }

        public ICommand OrdenarBusqueda { get; }

        public BusquedaVm(Entry EntryBsq, Picker pu)
        {
            IsBusy = true;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();

            LimpiarModulo = new Command(LimpiarModulo_Mto);
            BuscarBtn = new Command(FiltradoGeneral_MtdAsync);
            OrdenarBusqueda = new Command(OrdenarBusqueda_Mto);
            FocusPicker = new Command(claseBase.FocusPicker_Mto);
            SeleccionarModuloCommand = new Command<Busqueda>(SeleccionarModulo);

            pus = pu;
            EntryBsq_ = EntryBsq;
            EntryBsq.Completed += EntryCompleted;

            Incializar();
        }

        private async void Incializar()
        {
            BloquearCampo = true;
            await CargarBusqueda_MtdAsync();
            await ListasFiltros_MtdAsync();
        }

        private async Task CargarBusqueda_MtdAsync()
        {
            try
            {
                ListaBusqueda = new ObservableCollection<Busqueda>();
                listaBusquedaTem = new ObservableCollection<Busqueda>();
                string urli = logicaWS.Movile_Select_BusquedaApp_Mtd("1", "0", LenguajeBase, DateTime.Now.Year.ToString(), "0", "11466", Preferences.Get("IdSuceso", ""));
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                if (!jsonProcedimiento.Equals(""))
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    foreach (JObject item in jsArray)
                    {
                        Busqueda Busqueda = new Busqueda(
                            claseBase.ValidaString(item.GetValue("Modulo").ToString()),
                            claseBase.ValidaString(item.GetValue("NombreModulo").ToString()).ToUpper(),
                            claseBase.ValidaString(item.GetValue("Id").ToString()),
                            claseBase.ValidaString(item.GetValue("Suceso").ToString()).ToUpper(),
                            claseBase.ValidaString(item.GetValue("IdSuceso").ToString()),
                            claseBase.ValidaString(item.GetValue("Titulo").ToString()).ToUpper(),
                            claseBase.CapitalizeFirstLetter(claseBase.ValidaString(item.GetValue("Descripcion").ToString())),
                            claseBase.ValidaString(item.GetValue("DiaIn").ToString()),
                            claseBase.ValidaString(item.GetValue("MesIn").ToString()),
                            claseBase.ValidaString(item.GetValue("VigIn").ToString()),
                            claseBase.ValidaString(item.GetValue("DiaFin").ToString()),
                            claseBase.ValidaString(item.GetValue("MesFin").ToString()),
                            claseBase.ValidaString(item.GetValue("VigFin").ToString()),
                            claseBase.ValidaString(item.GetValue("Imagen").ToString()));

                        char delimitador = '0';
                        string[] valores = Busqueda.Id.Split(delimitador);
                        string aux = Busqueda.Id.Substring(valores[0].Length + 1);
                        Busqueda.Id = aux;
                        listaBusquedaTem.Add(Busqueda);
                    }
                }

                var ordenada = listaBusquedaTem.OrderBy(x => x.Modulo).ToList();
                listaBusquedaTem.Clear();
                foreach (var item in ordenada)
                {
                    listaBusquedaTem.Add(item);
                }

            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BusquedaVM", "CargarBusqueda_MtdAsync", "busquedaaplicacion");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ListasFiltros_MtdAsync()
        {
            try
            {
                /* Listado modulo de busqueda usuario */
                ListaModulo = new ObservableCollection<ListasGeneral>();
                string urli = logicaWS.Movile_Select_FiltroSuceso_Mtd("3", "0", "0");
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                if (jsonProcedimiento != "")
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    foreach (JObject item in jsArray)
                    {
                        ListasGeneral listasGeneral = new ListasGeneral
                        {
                            Descripcion = item.GetValue("NombreSuceso").ToString(),
                            Id = item.GetValue("IdSuceso").ToString()
                        };
                        ListaModulo.Add(listasGeneral);
                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BusquedaVM", "ListasFiltros_MtdAsync", "Servicios multilista(filtros)");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                FlechaModulo = true;
            }
        }

        void EntryCompleted(object sender, EventArgs e)
        {
            FiltradoGeneral_MtdAsync();
        }

        private async void FiltradoGeneral_MtdAsync()
        {
            try
            {
                IsBusy = true;
                BloquearCampo = false;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                ListaBusqueda.Clear();

                if (!string.IsNullOrWhiteSpace(EntQueEstasBuscando))
                {
                    if (EntQueEstasBuscando.Length > 2)
                    {
                        var filtrado = listaBusquedaTem
                            .Where(x => x.Titulo.ToLower().Contains(EntQueEstasBuscando.ToLower().Trim())
                                     || x.Descripcion.ToLower().Contains(EntQueEstasBuscando.ToLower().Trim()));

                        if (SelectModulo != null && EquisModulo)
                        {
                            filtrado = filtrado.Where(x => x.Modulo.Trim() == SelectModulo.Id.Trim());
                        }

                        foreach (var item in filtrado)
                        {
                            ListaBusqueda.Add(item);
                        }
                    }
                    else
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.elCampoQueEstasBuscandoEsOblighatorio, AppResources.VMaceptar);
                    }
                }
                else
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.elCampoQueEstasBuscandoEsOblighatorio, AppResources.VMaceptar);
                }

                CantidadBusqueda = ListaBusqueda.Count.ToString() + " " + AppResources.resultados;
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BusquedaVM", "FiltradoGeneral_MtdAsync", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, ex.Message, AppResources.VMaceptar);
            }
            finally
            {
                pus.Unfocus();
                EntryBsq_.Unfocus();
                await Task.Delay(100);
                IsBusy = false;
                BloquearCampo = true;
            }
        }

        private void LimpiarModulo_Mto()
        {
            IsBusy = true;

            EquisModulo = false;
            FlechaModulo = true;
            _ = new ObservableCollection<ListasGeneral>();
            ObservableCollection<ListasGeneral> team = ListaModulo;
            ListaModulo = [];
            ListaModulo = team;

            FiltradoGeneral_MtdAsync();

            EquisModulo = false;
            FlechaModulo = true;
        }

        private void OrdenarBusqueda_Mto()
        {
            ListaBusqueda.Reverse();
            ListaBusqueda = [.. ListaBusqueda];
        }

        private async void SeleccionarModulo(Busqueda busqueda)
        {
            try
            {
                if (busqueda != null)
                {
                    try
                    {
                        IsBusy = true;
                        await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar

                        string urli;
                        string jsonProcedimiento;

                        switch (busqueda.Modulo)
                        {
                            case ("22"): /*Contenidos*/
                                try
                                {
                                    urli = logicaWS.Movile_select_ContenidosDetalle_Mtd(LenguajeBase, busqueda.Id, EmailUsuario);
                                    jsonProcedimiento = await logicaWS.ConectionGet(urli);
                                    if (!claseBase.ValidaString(jsonProcedimiento).Equals(""))
                                    {
                                        JArray jsArray = JArray.Parse(jsonProcedimiento);
                                        if (jsArray.Count() > 0)
                                        {
                                            foreach (JObject item in jsArray)
                                            {
                                                Contenidos contenido = new Contenidos(item.GetValue("IdContenido").ToString(), item.GetValue("Titulo").ToString().ToUpper(), claseBase.CapitalizeFirstLetter(item.GetValue("NombreCategoria").ToString()),
                                                    item.GetValue("url").ToString(), item.GetValue("ImagenPortada").ToString(), item.GetValue("ImagenesCarrusel").ToString(), claseBase.CapitalizeFirstLetter(item.GetValue("Contexto").ToString()),
                                                    item.GetValue("Contenido").ToString(), item.GetValue("PalabrasClave").ToString(), item.GetValue("Fav").ToString(), Preferences.Get("IdSuceso", "0"));
                                                await RootNavigation.PushAsync(new DetalleContenidoView(contenido, null, "Busqueda"));
                                                break;
                                            }
                                        }
                                        else
                                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                                    }
                                    else
                                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                                }
                                catch (Exception ex)
                                {
                                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BusquedaVM", "ICommand ItemSelectedCommand", "consultadetallecontenidos");
                                }
                                break;

                            case ("12"): /*Suceso*/
                                await RootNavigation.PushAsync(new SucesoView());
                                break;

                            case ("1"): /*Conferencista*/
                                try
                                {
                                    urli = logicaWS.Movile_select_conferencistaDetalle_Mtd(EmailUsuario, LenguajeBase, busqueda.IdSuceso, busqueda.Id);
                                    jsonProcedimiento = await logicaWS.ConectionGet(urli);
                                    if (!claseBase.ValidaString(jsonProcedimiento).Equals(""))
                                    {
                                        JArray jsArray = JArray.Parse(jsonProcedimiento);
                                        if (jsArray.Count() > 0)
                                        {
                                            foreach (JObject item in jsArray)
                                            {
                                                Conferencista conferencista = new Conferencista(
                                                           claseBase.ValidaString(item.GetValue("idConferencista").ToString()),
                                                           claseBase.ValidaString(item.GetValue("NombreSuceso").ToString()),
                                                           claseBase.ValidaString(item.GetValue("idSuceso").ToString()),
                                                           claseBase.ValidaString(item.GetValue("IdSucesoServicio").ToString()),
                                                           claseBase.ValidaString(item.GetValue("NombreConferencista").ToString()).ToUpper(),
                                                           claseBase.CapitalizeFirstLetter(item.GetValue("Profesion").ToString()),
                                                           claseBase.ValidaString(item.GetValue("Cita").ToString()),
                                                           claseBase.ValidaString(claseBase.CapitalizeFirstLetter(item.GetValue("Perfil").ToString())),
                                                           claseBase.ValidaString(item.GetValue("Palabraclave").ToString()),
                                                           claseBase.ValidaString(item.GetValue("PerfilConferencista").ToString()),
                                                           claseBase.ValidaString(item.GetValue("SitioWeb").ToString()),
                                                           claseBase.ValidaString(item.GetValue("Fav").ToString()),
                                                           claseBase.ValidaString(item.GetValue("Imagen").ToString()),
                                                           claseBase.ValidaString(item.GetValue("ImagenFeria").ToString()),
                                                           claseBase.ValidaString(item.GetValue("IdPais").ToString()),
                                                           claseBase.ValidaString(item.GetValue("NombrePais").ToString()),
                                                           claseBase.ValidaString(item.GetValue("IdAutor").ToString()));
                                                await RootNavigation.PushAsync(new DetalleConferencistaView(conferencista));
                                                break;
                                            }
                                        }
                                        else
                                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                                    }
                                    else
                                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                                }
                                catch (Exception ex)
                                {
                                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BusquedaVM", "ICommand ItemSelectedCommand", "consultaconferencistadetalle");
                                }
                                break;

                            case ("2"): /*Expositor*/
                                try
                                {
                                    urli = logicaWS.Movile_Detalle_Expo_Mtd(LenguajeBase, EmailUsuario, busqueda.Id, busqueda.IdSuceso);
                                    jsonProcedimiento = await logicaWS.ConectionGet(urli);
                                    if (!claseBase.ValidaString(jsonProcedimiento).Equals(""))
                                    {
                                        JArray jsArray = JArray.Parse(jsonProcedimiento);
                                        if (jsArray.Count() > 0)
                                        {
                                            foreach (JObject item in jsArray)
                                            {
                                                Expositores Expositor = new Expositores(
                                                    item.GetValue("IdExpositor").ToString(), item.GetValue("NombreExpositor").ToString(),
                                                    item.GetValue("PerfilExpositor").ToString(), item.GetValue("TiendaVirtual").ToString(),
                                                    item.GetValue("LogoDetalle").ToString(), item.GetValue("ImagenDetalle").ToString(),
                                                    item.GetValue("Imagen").ToString(), item.GetValue("Fav").ToString(),
                                                    item.GetValue("Url").ToString(), item.GetValue("Direccion").ToString(),
                                                    item.GetValue("Pabellon").ToString(), item.GetValue("nivel").ToString(),
                                                    item.GetValue("stand").ToString(), claseBase.ValidaString(item.GetValue("IdSitio").ToString()),
                                                    item.GetValue("NombreSitio").ToString(), item.GetValue("IdMapa").ToString(),
                                                    item.GetValue("IdRecintoSuceso").ToString(), item.GetValue("NombreSuceso").ToString(),
                                                    item.GetValue("email").ToString(), item.GetValue("telefono").ToString(), busqueda.IdSuceso, "", "");

                                                await RootNavigation.PushAsync(new DetalleExpositorView(Expositor));
                                                break;
                                            }
                                        }
                                        else
                                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                                    }
                                    else
                                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                                }
                                catch (Exception ex)
                                {
                                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BusquedaVM", "ICommand ItemSelectedCommand", "consultadetallexpositor");
                                }
                                break;

                            case ("3"): /*Agenda*/
                                try
                                {
                                    ConsultaAgenda consultaAgenda;
                                    urli = logicaWS.Moviel_select_consultaagendasuceso_Mtd("0", "1", EmailUsuario, busqueda.IdSuceso, LenguajeBase, "0", "0");
                                    consultaAgenda = new ConsultaAgenda
                                    {
                                        Categoria = "0",
                                        FechaInicio = "0",
                                        IdAgenda = busqueda.Id,
                                        Lugar = "0",
                                        Franja = "0"
                                    };
                                    string json = JsonConvert.SerializeObject(consultaAgenda);
                                    jsonProcedimiento = await logicaWS.ConectionPost(json, urli);
                                    if (!claseBase.ValidaString(jsonProcedimiento).Equals(""))
                                    {
                                        JArray jsArray = JArray.Parse(jsonProcedimiento);
                                        if (jsArray.Count > 0)
                                        {
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
                                                    claseBase.ValidaString(item.GetValue("IdAgenda").ToString()),
                                                    claseBase.ValidaString(item.GetValue("IdSuceso").ToString()),
                                                    claseBase.ValidaString(item.GetValue("IdAgendaServicio").ToString()),
                                                    claseBase.ValidaString(item.GetValue("Titulo").ToString().ToUpper()),
                                                    claseBase.ValidaString(item.GetValue("Categoria").ToString()),
                                                    claseBase.ValidaString(item.GetValue("Lugar").ToString()),
                                                    claseBase.ValidaString(item.GetValue("DiaIn").ToString()),
                                                    claseBase.ValidaString(item.GetValue("MesIn").ToString()),
                                                    claseBase.ValidaString(item.GetValue("DiaFin").ToString()),
                                                    claseBase.ValidaString(item.GetValue("MesFin").ToString()),
                                                    claseBase.ValidaString(item.GetValue("HoraInicio").ToString()),
                                                    claseBase.ValidaString(item.GetValue("HoraFin").ToString()),
                                                    claseBase.ValidaString(item.GetValue("Aforo").ToString()),
                                                    claseBase.ValidaString(item.GetValue("Estado").ToString()),
                                                    claseBase.ValidaString(item.GetValue("Imagen").ToString()),
                                                    claseBase.ValidaString(item.GetValue("IconoFeria").ToString()),
                                                    claseBase.ValidaString(item.GetValue("ContextoClasificacion").ToString()),
                                                    claseBase.ValidaString(item.GetValue("Descripcion").ToString()),
                                                    claseBase.ValidaString(item.GetValue("PalabrasClave").ToString()),
                                                    claseBase.ValidaString(item.GetValue("Motivocancelacion").ToString()),
                                                    claseBase.ValidaString(item.GetValue("FechaCancelacion").ToString()),
                                                    a,
                                                    b,
                                                    c,
                                                     Conferencistas, idConferencistas,
                                                    item.GetValue("fav").ToString(),
                                                     claseBase.ValidaString(item.GetValue("Franja").ToString()),
                                                      claseBase.ValidaString(item.GetValue("Organizador").ToString())
                                                    );
                                                if (!agenda.Estado.Equals(""))
                                                {
                                                    agenda.Lugar = "";
                                                }
                                                await RootNavigation.PushAsync(new DetalleAgendaView(agenda));
                                                break;
                                            }
                                        }
                                        else
                                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                                    }
                                    else
                                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                                }
                                catch (Exception ex)
                                {
                                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BusquedaVM", "ICommand ItemSelectedCommand", "consultaagendasuceso");
                                }
                                break;

                            /*   case ("5"): //Cuponera
                                   _ = Application.Current.MainPage.Navigation.PushAsync(new CuponeraPage(false, "0", busqueda.Id), false);
                                   break;*/
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BusquedaVM", "ICommand ItemSelectedCommand", "n/a");
                    }
                    finally
                    {
                        IsBusy = false;

                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}