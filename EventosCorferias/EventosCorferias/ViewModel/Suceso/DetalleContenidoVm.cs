using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Suceso;
using EventosCorferias.Views.Usuario;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Windows.Input;
using System.Collections.ObjectModel;


namespace EventosCorferias.ViewModel.Suceso
{
    public class DetalleContenidoVm : BaseViewModel
    {
        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;

        public ObservableCollection<ListasGeneral> _imagenesCarrusel;

        private readonly Contenidos contenidoDetalle;

        private string ImgPrincipal_;
        private string IdAgendaAux_;
        private string idSitiosContendos;
        private string _titulo;
        private string _categoria;
        private string _imagenFav;
        private string _contenido;
        private string _descripcionFeria;
        private string _fechaContexto;
        private string UrlPlayContenidos;
        private bool _visiblePlay;

        private bool _visibleAgenda;
        private bool _visibleSitios;

        public bool VisibleSitios
        {
            get { return _visibleSitios; }
            set { _visibleSitios = value; OnPropertyChanged(nameof(VisibleSitios)); }
        }
        public bool VisibleAgenda
        {
            get { return _visibleAgenda; }
            set { _visibleAgenda = value; OnPropertyChanged(nameof(VisibleAgenda)); }
        }

        public string ImgPrincipal
        {
            get { return ImgPrincipal_; }
            set { ImgPrincipal_ = value; OnPropertyChanged(nameof(ImgPrincipal)); }
        }

        public string FechaContexto
        {
            get { return _fechaContexto; }
            set { _fechaContexto = value; OnPropertyChanged(nameof(FechaContexto)); }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; OnPropertyChanged(nameof(Titulo)); }
        }

        public string Categoria
        {
            get { return _categoria; }
            set { _categoria = value; OnPropertyChanged(nameof(Categoria)); }
        }

        public string DescripcionFeria
        {
            get { return _descripcionFeria; }
            set { _descripcionFeria = value; OnPropertyChanged(nameof(DescripcionFeria)); }
        }

        public string ImagenFav
        {
            get { return _imagenFav; }
            set { _imagenFav = value; OnPropertyChanged(nameof(ImagenFav)); }
        }

        public string Contenido
        {
            get { return _contenido; }
            set { _contenido = value; OnPropertyChanged(nameof(Contenido)); }
        }

        public bool VisiblePlay
        {
            get { return _visiblePlay; }
            set { _visiblePlay = value; OnPropertyChanged(nameof(VisiblePlay)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarrusel
        {
            get { return _imagenesCarrusel; }
            set { _imagenesCarrusel = value; OnPropertyChanged(nameof(ImagenesCarrusel)); }
        }

        private ObservableCollection<ListasGeneral> _puntosLista;
        public ObservableCollection<ListasGeneral> PuntosLista
        {
            get { return _puntosLista; }
            set { _puntosLista = value; OnPropertyChanged(nameof(PuntosLista)); }
        }


        public ICommand PlaySitios { get; }
        public ICommand PlayAgenda { get; }
        public ICommand PlayImagen { get; }
        public Command BtnCompartir { get; }
        public ICommand FavActualizar { get; }
        public ICommand PlayContenidos { get; }

        public DetalleContenidoVm(Contenidos contenido, string IdSitios, string Vista)
        {
            Title = AppResources.detalleContenido;

            contenidoDetalle = contenido;
            idSitiosContendos = IdSitios;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();

            PlayImagen = new Command(PlayImagen_Mtd);
            BtnCompartir = new Command(BtnCompartir_Mtd);
            PlaySitios = new Command(async () => await PlaySitios_Mtd());
            PlayAgenda = new Command(async () => await PlayAgenda_Mtd());
            FavActualizar = new Command(async () => await FavActualizar_MtoAsync());
            PlayContenidos = new Command(async () => await PlayContenidos_MtoAsync());

            Incializar();

            ValidaAgenda();

            if (Vista == "Jornadas")
            {
                VisibleSitios = false;
            }
            else
            {
                ValidaSitios();
            }
        }

        public async void Incializar()
        {
            await InfoDetalleContenido();
        }

        public async Task InfoDetalleContenido()
        {
            try
            {
                // Carrusel de imagenes
                ImagenesCarrusel = new ObservableCollection<ListasGeneral>();
                if (claseBase.ValidaString(contenidoDetalle.ImagenesCarrusel) != "")
                {
                    string[] arrayAux = contenidoDetalle.ImagenesCarrusel.Split([" | "], System.StringSplitOptions.None);
                    foreach (var Aux in arrayAux)
                    {
                        ListasGeneral listasGeneral = new ListasGeneral { Observacion = Aux.Trim() };
                        ImagenesCarrusel.Add(listasGeneral);
                    }
                }
                else
                {
                    ListasGeneral listasGeneral = new ListasGeneral { Observacion = contenidoDetalle.ImagenPortada.Trim() };
                    ImagenesCarrusel.Add(listasGeneral);
                }

                ImgPrincipal = contenidoDetalle.ImagenPortada.Trim();
                Titulo = contenidoDetalle.Titulo;
                Categoria = claseBase.CapitalizeFirstLetter(contenidoDetalle.NombreCategoria.ToString());
                Contenido = contenidoDetalle.Contenido.ToString();
                UrlPlayContenidos = contenidoDetalle.url.ToString();
                FechaContexto = contenidoDetalle.Contexto;

                ImagenFav = contenidoDetalle.ImagenFav;

                if (claseBase.ValidaString(UrlPlayContenidos) != "")
                    VisiblePlay = true;

                CarruselImagenesLista();
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleContenido", "InfoDetalleContenido", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
        }

        private async void ValidaSitios()
        {
            try
            {
                string urli = logicaWS.Movile_Select_General_Mapa_Mtd("2", EmailUsuario, LenguajeBase, contenidoDetalle.idSuseso, "2", idSitiosContendos);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);

                JArray jsArray = JArray.Parse(jsonProcedimiento);

                if (jsArray.Count > 0)
                {
                    VisibleSitios = true;
                }

            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleContenido", "ValidaSitios", "n/a");
            }
        }

        private async void ValidaAgenda()
        {
            try
            {
                int horaActual = EntregaHoraMilitar(DateTime.Now.ToString("h:mm:ss tt"), "2");

                string urli = logicaWS.Moviel_select_consultaagendasuceso_Mtd("0", "1", EmailUsuario, Preferences.Get("IdSuceso", ""), LenguajeBase, "0", contenidoDetalle.IdContenido);

                ConsultaAgenda consultaAgenda = new ConsultaAgenda
                {
                    Categoria = "0",
                    FechaInicio = "0",
                    IdAgenda = "0",
                    Lugar = "0",
                    Franja = "0"
                };


                string json = JsonConvert.SerializeObject(consultaAgenda);
                string jsonProcedimiento = await logicaWS.ConectionPost(json, urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);

                foreach (JObject item in jsArray)
                {
                    DateTime a = new DateTime();
                    if (DateTime.TryParseExact(claseBase.ValidaString(item.GetValue("FechaActiva").ToString()), "dd/MM/yyyy", null, DateTimeStyles.None, out a))
                        a = DateTime.ParseExact(claseBase.ValidaString(item.GetValue("FechaActiva").ToString()), "dd/MM/yyyy", null);

                    DateTime b = new DateTime();
                    if (DateTime.TryParseExact(claseBase.ValidaString(item.GetValue("FechaInActiva").ToString()), "dd/MM/yyyy", null, DateTimeStyles.None, out a))
                        b = DateTime.ParseExact(claseBase.ValidaString(item.GetValue("FechaInActiva").ToString()), "dd/MM/yyyy", null);

                    DateTime c = new DateTime();
                    if (DateTime.TryParseExact(claseBase.ValidaString(item.GetValue("FechaInicio").ToString()), "dd/MM/yyyy", null, DateTimeStyles.None, out a))
                        c = DateTime.ParseExact(claseBase.ValidaString(item.GetValue("FechaInicio").ToString()), "dd/MM/yyyy", null);

                    if (c >= DateTime.Now)
                    {
                        if (c.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")
                            && horaActual <= EntregaHoraMilitar(item.GetValue("HoraInicio").ToString(), "2"))
                        {
                            VisibleAgenda = true;
                            IdAgendaAux_ = item.GetValue("IdAgenda").ToString();
                        }
                        else if (c.ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy"))
                        {
                            VisibleAgenda = true;
                            IdAgendaAux_ = item.GetValue("IdAgenda").ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleContenido", "ValidaAgenda", "n/a");
            }
        }

        private async Task PlaySitios_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100);
                await RootNavigation.PushAsync(new MapaView(contenidoDetalle.idSuseso, "2", idSitiosContendos, "Sitios", "2"));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleContenido", "PlaySitios_Mtd", "redireccionamiento");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task PlayAgenda_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100);
                await RootNavigation.PushAsync(new AgendaView(contenidoDetalle.idSuseso, "0", "", false, contenidoDetalle.IdContenido, false, IdAgendaAux_));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleContenido", "PlayAgenda_Mtd", "redireccionamiento");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void PlayImagen_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100);
                await RootNavigation.PushAsync(new ContenidosWebView(AppResources.Contenidos, ImgPrincipal));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleContenido", "PlayImagen_Mtd", "redireccionamiento");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void CarruselImagenesLista()
        {
            try
            {
                PuntosLista = new ObservableCollection<ListasGeneral>();
                int auxUno = 0;

                foreach (var aux in ImagenesCarrusel)
                {
                    PuntosLista.Add(new ListasGeneral { Observacion = "ic_puntoClaro.png", Id = auxUno.ToString() });
                    auxUno += 1;
                }
            }
            catch { }
        }

        public void PintarCarruselImagenesLista(int posision)
        {
            try
            {
                if (PuntosLista.Count > 0)
                {
                    foreach (var axun in PuntosLista)
                    {
                        if (axun.Id == posision.ToString())
                            axun.Observacion = "ic_puntoOscuro.png";
                        else
                            axun.Observacion = "ic_puntoClaro.png";
                    }
                }
            }
            catch { }
        }

        public async Task PlayContenidos_MtoAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100);
                await RootNavigation.PushAsync(new ContenidosWebView(AppResources.reproducirContendo, UrlPlayContenidos));
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleContenido", "PlayContenidos_MtoAsync", null);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task FavActualizar_MtoAsync()
        {
            try
            {
                string urli;
                string jsonProcedimiento;
                if (ImagenFav == "ic_favorito_obscuro")
                {
                    ImagenFav = "ic_favortio_relleno";
                    urli = logicaWS.Movile_Update_Fav_Contenidos_Mtd(EmailUsuario, contenidoDetalle.IdContenido);
                    jsonProcedimiento = await logicaWS.ConectionGet(urli);
                }
                else
                {
                    ImagenFav = "ic_favorito_obscuro";
                    urli = logicaWS.Movile_delet_Fav_Contenidos_Mtd(EmailUsuario, contenidoDetalle.IdContenido);
                    jsonProcedimiento = await logicaWS.ConectionDelete(urli);
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleContenido", "PlayContenidos_MtoAsync", "agregarfavoritocontenido/eliminarfavoritocontenido");
            }
        }

        private async void BtnCompartir_Mtd()
        {
            try
            {
                if (VisiblePlay)
                {
                    await Share.RequestAsync(new ShareTextRequest()
                    {
                        Text = Titulo,
                        Uri = UrlPlayContenidos
                    });
                }
                else
                {
                    await Share.RequestAsync(new ShareTextRequest()
                    {
                        Text = Titulo,
                        Uri = "https://feriadellibro.com"
                    });
                }

            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleContenido", "BtnCompartir_Mtd", "n/a");
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
            catch
            {
                return 1;
            }
        }

    }
}
