using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Suceso;
using EventosCorferias.Views.Usuario;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;

namespace EventosCorferias.ViewModel.Suceso
{
    public class DetalleConferencistaVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;
        private readonly Conferencista conferencista;

        public ObservableCollection<Images> _imagenesRedSuceso;

        private Images _SelectedItemRedSocial;

        private string? IdAgendaAux_;
        private string? _url;
        private string? _pais;
        private string? _titulo;
        private string? _imagenFav;
        private string? _imagenFeria;
        private string? _nombreConferencista;
        private string? _imagenConferencista;
        private string? _descripcionConferencista;

        private bool _siguenosEn;
        private bool _verSitioWeb;
        private bool _VisibleAgenda;

        public Images SelectedRedSocial
        {
            get { return _SelectedItemRedSocial; }
            set { _SelectedItemRedSocial = null; ItemSelectedRedSocialCommand.Execute(value); OnPropertyChanged(nameof(SelectedRedSocial)); }
        }

        public string? Pais
        {
            get { return _pais; }
            set { _pais = value; OnPropertyChanged(nameof(Pais)); }
        }

        public string? UrlConferencista
        {
            get { return _url; }
            set { _url = value; OnPropertyChanged(nameof(UrlConferencista)); }
        }

        public string? Titulo
        {
            get { return _titulo; }
            set { _titulo = value; OnPropertyChanged(nameof(Titulo)); }
        }

        public string? DescripcionConferencista
        {
            get { return _descripcionConferencista; }
            set { _descripcionConferencista = value; OnPropertyChanged(nameof(DescripcionConferencista)); }
        }

        public string? ImagenConferencista
        {
            get { return _imagenConferencista; }
            set { _imagenConferencista = value; OnPropertyChanged(nameof(ImagenConferencista)); }
        }

        public string? ImagenFav
        {
            get { return _imagenFav; }
            set { _imagenFav = value; OnPropertyChanged(nameof(ImagenFav)); }
        }

        public string? ImagenFeria
        {
            get { return _imagenFeria; }
            set { _imagenFeria = value; OnPropertyChanged(nameof(ImagenFeria)); }
        }

        public string? NombreConferencista
        {
            get { return _nombreConferencista; }
            set { _nombreConferencista = value; OnPropertyChanged(nameof(NombreConferencista)); }
        }

        public bool SiguenosEn
        {
            get { return _siguenosEn; }
            set { _siguenosEn = value; OnPropertyChanged(nameof(SiguenosEn)); }
        }

        public bool VisibleAgenda
        {
            get { return _VisibleAgenda; }
            set { _VisibleAgenda = value; OnPropertyChanged(nameof(VisibleAgenda)); }
        }

        public bool VerSitioWeb
        {
            get { return _verSitioWeb; }
            set { _verSitioWeb = value; OnPropertyChanged(nameof(VerSitioWeb)); }
        }

        public ObservableCollection<Images> ImagenesRedSuceso
        {
            get { return _imagenesRedSuceso; }
            set { _imagenesRedSuceso = value; OnPropertyChanged(nameof(ImagenesRedSuceso)); }
        }

        public Command PlayImg { get; }
        public Command BtnCompartir { get; }
        public Command FavActualizar { get; }
        public Command BtnCargarAgenda { get; }
        public Command UrlConferencistaLink { get; }
        public ICommand ItemSelectedRedSocialCommand
        {
            get
            {
                return new Command(async list =>
                {
                    try
                    {
                        if (!StateConexion)
                        {
                            Images lista = (Images)list;
                            IsBusy = true;
                            RootMainPage.Detail = new NavigationPage(new ContenidosWebView(AppResources.redSocial, lista.Link));
                        }
                        else
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
                    }
                    catch (Exception ex)
                    {
                        IsBusy = false;
                        Debug.WriteLine("ERROR " + ex.Message);
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.NoCargaEnlaceRedSocial, AppResources.VMaceptar);
                    }
                });
            }
        }

        public DetalleConferencistaVm(Conferencista Conferencista)
        {
            Title = AppResources.detalleConferencista;

            IsBusy = true;
            conferencista = Conferencista;

            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            ImagenSplash = logicaWs.ImgMenuSuperior_Mtd();

            PlayImg = new Command(PlayImg_Mtd);
            BtnCompartir = new Command(BtnCompartir_Mtd);
            UrlConferencistaLink = new Command(UrlConferencista_Mtd);
            BtnCargarAgenda = new Command(async () => await BtnCargarAgenda_Mtd());
            FavActualizar = new Command(async () => await FavActualizar_MtoAsync());

            Inicializar();
            ValidaAgenda();
            ContadorNotificaciones_Mtd();
        }

        private async void Inicializar()
        {
            await InfoDetalleConferencista_MtdAsync();
        }

        public async Task InfoDetalleConferencista_MtdAsync()
        {
            try
            {
                /*Información base */
                ImagenFav = conferencista.ImagenFav;
                UrlConferencista = claseBase.ValidaString(conferencista.SitioWeb);
                Titulo = claseBase.CapitalizeFirstLetter(conferencista.Perfil);
                ImagenFeria = claseBase.ValidaString(conferencista.ImagenFeria);
                Pais = claseBase.CapitalizeFirstLetter(conferencista.NombrePais);
                ImagenConferencista = claseBase.ValidaString(conferencista.Imagen);
                NombreConferencista = claseBase.ValidaString(conferencista.NombreConferencista).ToUpper();
                DescripcionConferencista = claseBase.ValidaString(conferencista.PerfilConferencista);

                if (!string.IsNullOrWhiteSpace(UrlConferencista))
                {
                    VerSitioWeb = true;
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleConferencistaVM", "InfoDetalleConferencista_MtdAsync1", "consultaredesconferencista");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }

            try
            {
                /*Redes Sociales */
                ImagenesRedSuceso = new ObservableCollection<Images>();
                string urli = logicaWs.Movile_Select_Red_Conferencista_Mtd(conferencista.idConferencista);
                string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                if (!jsonProcedimiento.Equals(""))
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    foreach (JObject item in jsArray)
                        ImagenesRedSuceso.Add(new Images() { Url = item.GetValue("Imagen")?.ToString() ?? string.Empty, Link = item.GetValue("Url")?.ToString() ?? string.Empty });
                }

                if (ImagenesRedSuceso.Count() > 0)
                    SiguenosEn = true;
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleConferencistaVM", "InfoDetalleConferencista_MtdAsync2", "consultaredesconferencista");
            }
            finally
            {
                IsBusy = false;
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

        private async void ValidaAgenda()
        {
            try
            {
                int horaActual = EntregaHoraMilitar(DateTime.Now.ToString("h:mm:ss tt"), "2");

                string urli = logicaWs.Moviel_select_consultaagendasuceso_Mtd("0", "1", EmailUsuario, Preferences.Get("IdSuceso", ""), LenguajeBase, conferencista.idConferencista, "0", "0");

                ConsultaAgenda consultaAgenda = new ConsultaAgenda
                {
                    Categoria = "0",
                    FechaInicio = "0",
                    IdAgenda = "0",
                    Lugar = "0",
                    Franja = "0"
                };


                string json = JsonConvert.SerializeObject(consultaAgenda);
                string jsonProcedimiento = await logicaWs.ConectionPost(json, urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);

                foreach (JObject item in jsArray)
                {
                    DateTime a = new DateTime();
                    if (DateTime.TryParseExact(claseBase.ValidaString(item.GetValue("FechaActiva")?.ToString() ?? string.Empty), "dd/MM/yyyy", null, DateTimeStyles.None, out a))
                        a = DateTime.ParseExact(claseBase.ValidaString(item.GetValue("FechaActiva")?.ToString() ?? string.Empty), "dd/MM/yyyy", null);

                    DateTime b = new DateTime();
                    if (DateTime.TryParseExact(claseBase.ValidaString(item.GetValue("FechaInActiva")?.ToString() ?? string.Empty), "dd/MM/yyyy", null, DateTimeStyles.None, out a))
                        b = DateTime.ParseExact(claseBase.ValidaString(item.GetValue("FechaInActiva")?.ToString() ?? string.Empty), "dd/MM/yyyy", null);

                    DateTime c = new DateTime();
                    if (DateTime.TryParseExact(claseBase.ValidaString(item.GetValue("FechaInicio")?.ToString() ?? string.Empty), "dd/MM/yyyy", null, DateTimeStyles.None, out a))
                        c = DateTime.ParseExact(claseBase.ValidaString(item.GetValue("FechaInicio")?.ToString() ?? string.Empty), "dd/MM/yyyy", null);

                    if (c >= DateTime.Now)
                    {
                        if (c.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")
                            && horaActual <= EntregaHoraMilitar(item.GetValue("HoraInicio")?.ToString() ?? string.Empty, "2"))
                        {
                            VisibleAgenda = true;
                            IdAgendaAux_ = item.GetValue("IdAgenda")?.ToString() ?? string.Empty;

                        }
                        else if (c.ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy"))
                        {
                            VisibleAgenda = true;
                            IdAgendaAux_ = item.GetValue("IdAgenda")?.ToString() ?? string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleConferencistaVM", "ValidaAgenda", "consultaagendasuceso");
            }
        }

        private async void PlayImg_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await RootNavigation.PushAsync(new ContenidosWebView(AppResources.conferencista, ImagenFeria));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleConferencistaVm", "PlayImg_Mtd", "redireccionamiento");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task BtnCargarAgenda_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await RootNavigation.PushAsync(new AgendaView(conferencista.idSuceso, conferencista.idConferencista, NombreConferencista, false, "0", false, IdAgendaAux_));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleConferencistaVm", "BtnCargarAgenda_Mtd", "redireccionamiento");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void BtnCompartir_Mtd()
        {
            try
            {
                await Share.RequestAsync(new ShareTextRequest()
                {
                    Text = NombreConferencista + " " + Titulo,
                    Uri = "https://feriadellibro.com/es/invitado/" + conferencista.IdAutor
                }); ;
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleConferencistaVM", "BtnCompartir_Mtd", "Error Redireccion Boton Compartir");
            }
        }

        public async void UrlConferencista_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                RootMainPage.Detail = new NavigationPage(new ContenidosWebView(AppResources.detalleConferencista, UrlConferencista));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleConferencistaVm", "PlayImg_Mtd", "redireccionamiento");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
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
                if (ImagenFav == "ic_favorito_obscuro.png")
                {
                    string urli = logicaWs.Movile_Update_Fav_Confenrencista_Mtd(EmailUsuario, conferencista.idConferencista);
                    await logicaWs.ConectionGet(urli);
                    ImagenFav = "ic_favortio_relleno.png";
                }
                else
                {
                    string urli = logicaWs.Movile_delet_Fav_Confenrencista_Mtd(EmailUsuario, conferencista.idConferencista);
                    await logicaWs.ConectionDelete(urli);
                    ImagenFav = "ic_favorito_obscuro.png";
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleConferencistaVM", "FavActualizar_MtoAsync", "insertarfavoritoconferencista/eliminarfavoritoconferencista");
            }
        }

    }
}
