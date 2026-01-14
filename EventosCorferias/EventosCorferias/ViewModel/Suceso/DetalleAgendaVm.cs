using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Suceso;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Input;


namespace EventosCorferias.ViewModel.Suceso
{
    public class DetalleAgendaVm : BaseViewModel
    {
        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;
        public readonly IPageServicio pageServicio;

        private List<string> _Conferencistas;
        private List<string> _IDCONFERENCISTA;

        private Agenda _infoAgenda;

        private string? _Aforo;
        private string? _Lugar;
        private string? _DiaIn;
        private string? _MesIn;
        private string? _Imagen;
        private string? _Titulo;
        private string? _DiaFin;
        private string? _MesFin;
        private string? _HoraFin;
        private string? _Categoria;
        private string? _imagenFav;
        private string? _HoraInicio;
        private string? _IconoFeria;
        private string? _Descripcion;
        private string? _SelectedItem;
        private string? _franja;

        private bool _verFecha;
        private bool _verHora;
        private bool _verLugar;
        private bool _verAforo;
        private bool _verConferencista;
        private bool _verFranja;

        private string _encuentralo;

        public string? Encuentralo
        {
            get { return _encuentralo; }
            set { _encuentralo = value; OnPropertyChanged(Encuentralo); }
        }

        private bool _verEncuentralo;

        public bool VerEncuentralo
        {
            get { return _verEncuentralo; }
            set { _verEncuentralo = value; OnPropertyChanged(nameof(VerEncuentralo)); }
        }

        public bool VerFranja
        {
            get { return _verFranja; }
            set { _verFranja = value; OnPropertyChanged(nameof(VerFecha)); }
        }

        public bool VerFecha
        {
            get { return _verFecha; }
            set { _verFecha = value; OnPropertyChanged(nameof(VerFecha)); }
        }

        public bool VerHora
        {
            get { return _verHora; }
            set { _verHora = value; OnPropertyChanged(nameof(VerHora)); }
        }

        public bool VerLugar
        {
            get { return _verLugar; }
            set { _verLugar = value; OnPropertyChanged(nameof(VerLugar)); }
        }

        public bool VerAforo
        {
            get { return _verAforo; }
            set { _verAforo = value; OnPropertyChanged(nameof(VerAforo)); }
        }

        public string? Titulo
        {
            get { return _Titulo; }
            set { _Titulo = value; OnPropertyChanged(Titulo); }
        }

        public string? Categoria
        {
            get { return _Categoria; }
            set { _Categoria = value; OnPropertyChanged(Categoria); }
        }

        public string? DiaIn
        {
            get { return _DiaIn; }
            set { _DiaIn = value; OnPropertyChanged(DiaIn); }
        }
        public string? Franja
        {
            get { return _franja; }
            set { _franja = value; OnPropertyChanged(Franja); }
        }

        public string? MesIn
        {
            get { return _MesIn; }
            set { _MesIn = value; OnPropertyChanged(MesIn); }
        }

        public string? DiaFin
        {
            get { return _DiaFin; }
            set { _DiaFin = value; OnPropertyChanged(DiaFin); }
        }

        public string? MesFin
        {
            get { return _MesFin; }
            set { _MesFin = value; OnPropertyChanged(MesFin); }
        }

        public string? HoraInicio
        {
            get { return _HoraInicio; }
            set { _HoraInicio = value; OnPropertyChanged(HoraInicio); }
        }

        public string? HoraFin
        {
            get { return _HoraFin; }
            set { _HoraFin = value; OnPropertyChanged(HoraFin); }
        }

        public string? Aforo
        {
            get { return _Aforo; }
            set { _Aforo = value; OnPropertyChanged(Aforo); }
        }

        public string? Lugar
        {
            get { return _Lugar; }
            set { _Lugar = value; OnPropertyChanged(Lugar); }
        }

        public string? Imagen
        {
            get { return _Imagen; }
            set { _Imagen = value; OnPropertyChanged(Imagen); }
        }

        public string? IconoFeria
        {
            get { return _IconoFeria; }
            set { _IconoFeria = value; OnPropertyChanged(IconoFeria); }
        }

        public string? Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; OnPropertyChanged(Descripcion); }
        }

        public List<string> Conferencistas
        {
            get { return _Conferencistas; }
            set { _Conferencistas = value; OnPropertyChanged(); }
        }

        public List<string> IDCONFERENCISTA
        {
            get { return _IDCONFERENCISTA; }
            set { _IDCONFERENCISTA = value; OnPropertyChanged(); }
        }

        public string? ImagenFav
        {
            get { return _imagenFav; }
            set { _imagenFav = value; OnPropertyChanged(nameof(ImagenFav)); }
        }

        public bool VerConferencista
        {
            get { return _verConferencista; }
            set { _verConferencista = value; OnPropertyChanged(nameof(VerConferencista)); }
        }

        public Agenda InfoAgenda
        {
            get { return _infoAgenda; }
            set { _infoAgenda = value; OnPropertyChanged(); }
        }

        public string? SelectedConferencista
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = null;
                ItemSelectedCommand.Execute(value);
                OnPropertyChanged(nameof(SelectedConferencista));
            }
        }

        public ICommand ItemSelectedCommand
        {
            get
            {
                return new Command(async list =>
                {
                    try
                    {
                        if (!StateConexion)
                        {
                            IsBusy = true;
                            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                            int a = InfoAgenda.Conferencistas.IndexOf(list as string);
                            string b = InfoAgenda.IDCONFERENCISTA[a];

                            string urli = logicaWS.Movile_select_conferencistaDetalle_Mtd(EmailUsuario, LenguajeBase, InfoAgenda.IdSuceso, b);
                            string? jsonProcedimiento = await logicaWS.ConectionGet(urli);
                            if (!claseBase.ValidaString(jsonProcedimiento).Equals(""))
                            {
                                JArray jsArray = JArray.Parse(jsonProcedimiento);
                                Conferencista conferencista;
                                foreach (JObject item in jsArray)
                                {
                                    conferencista = new Conferencista(
                                        item.GetValue("idConferencista")?.ToString() ?? string.Empty, item.GetValue("NombreSuceso")?.ToString() ?? string.Empty,
                                        item.GetValue("idSuceso")?.ToString() ?? string.Empty, item.GetValue("IdSucesoServicio")?.ToString() ?? string.Empty,
                                        item.GetValue("NombreConferencista")?.ToString() ?? string.Empty, item.GetValue("Profesion")?.ToString() ?? string.Empty,
                                        item.GetValue("Cita")?.ToString() ?? string.Empty, item.GetValue("Perfil")?.ToString() ?? string.Empty,
                                        item.GetValue("Palabraclave")?.ToString() ?? string.Empty, item.GetValue("PerfilConferencista")?.ToString() ?? string.Empty,
                                        item.GetValue("SitioWeb")?.ToString() ?? string.Empty, item.GetValue("Fav")?.ToString() ?? string.Empty,
                                        item.GetValue("Imagen")?.ToString() ?? string.Empty, item.GetValue("ImagenFeria")?.ToString() ?? string.Empty,
                                        item.GetValue("IdPais")?.ToString() ?? string.Empty, item.GetValue("NombrePais")?.ToString() ?? string.Empty,
                                        claseBase.ValidaString(item.GetValue("IdAutor")?.ToString() ?? string.Empty));

                                    await RootNavigation.PushAsync(new DetalleConferencistaView(conferencista), false);
                                    break;
                                }

                                if (jsArray.Count == 0)
                                    await pageServicio.DisplayAlert(AppResources.nombreMarca, "AppResources.esteConferencistaNoCuentaConInformacion", AppResources.VMaceptar);
                            }
                            else
                                await pageServicio.DisplayAlert(AppResources.nombreMarca, "AppResources.esteConferencistaNoCuentaConInformacion", AppResources.VMaceptar);
                        }
                        else
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
                    }
                    catch (Exception ex)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleAgendaVM", "ICommand ItemSelectedCommand", "consultaconferencistadetalle");
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
            }
        }

        public Command PlayImg { get; }
        public Command BtnCompartir { get; }
        public ICommand FavButtonCommand { get; }

        public DetalleAgendaVm(Agenda agenda)
        {
            Title = AppResources.detalleAgenda;

            IsBusy = true;
            InfoAgenda = agenda;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();

            PlayImg = new Command(PlayImg_Mtd);
            FavButtonCommand = new Command(SeleccionarFav_MtdAsync);
            BtnCompartir = new Command(BtnCompartir_Mtd);

            CargarDetalle();
            ContadorNotificaciones_Mtd();
        }

        private async void CargarDetalle()
        {
            try
            {
                Titulo = InfoAgenda.Titulo;
                Categoria = InfoAgenda.Categoria;
                Lugar = InfoAgenda.Lugar;
                DiaIn = InfoAgenda.DiaIn;
                MesIn = InfoAgenda.MesIn;
                DiaFin = InfoAgenda.DiaFin;
                MesFin = InfoAgenda.MesFin;
                HoraInicio = InfoAgenda.HoraInicio;
                HoraFin = InfoAgenda.HoraFin;
                Aforo = InfoAgenda.Aforo;
                Imagen = InfoAgenda.Imagen;
                IconoFeria = InfoAgenda.IconoFeria;
                Descripcion = InfoAgenda.Descripcion;
                Conferencistas = InfoAgenda.Conferencistas;
                IDCONFERENCISTA = InfoAgenda.IDCONFERENCISTA;
                Franja = InfoAgenda.Franja;
                Encuentralo = InfoAgenda.Organizador;


                if (!string.IsNullOrWhiteSpace(Aforo))
                    VerAforo = true;

                if (!string.IsNullOrWhiteSpace(Lugar))
                    VerLugar = true;

                if (!string.IsNullOrWhiteSpace(HoraInicio) || !string.IsNullOrWhiteSpace(HoraFin))
                    VerHora = true;

                if (!string.IsNullOrWhiteSpace(DiaIn))
                    VerFecha = true;

                if (!string.IsNullOrWhiteSpace(Franja))
                    VerFranja = true;

                if (!string.IsNullOrWhiteSpace(Encuentralo))
                    VerEncuentralo = true;


                if (Conferencistas != null)
                {
                    if (Conferencistas.Count > 0)
                    {
                        foreach (var aux in Conferencistas)
                        {
                            if (aux != null)
                            {
                                if (!aux.Equals("") && !aux.Equals("0"))
                                {
                                    VerConferencista = true;
                                }
                            }
                        }
                    }
                }

                if (claseBase.ValidaString(InfoAgenda.Fav).Equals("1"))
                    ImagenFav = "ic_favortio_relleno.png";
                else
                    ImagenFav = "ic_favorito_obscuro.png";
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleAgendaVm", "CargarDetalle", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally { IsBusy = false; }


        }

        private async void PlayImg_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await RootNavigation.PushAsync(new ContenidosWebView(AppResources.agenda, IconoFeria));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleAgendaVm", "PlayImg_Mtd", "n/a");
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
                if (string.IsNullOrWhiteSpace(InfoAgenda.IdAgendaServicio))
                {
                    await Share.RequestAsync(new ShareTextRequest()
                    {
                        Text = Titulo + " " + Categoria + " " + HoraInicio + " " + HoraFin + " " + Lugar,
                        Uri = "https://feriadellibro.com/"
                    });
                }
                else
                {
                    await Share.RequestAsync(new ShareTextRequest()
                    {
                        Text = Titulo + " " + Categoria + " " + HoraInicio + " " + HoraFin + " " + Lugar,
                        Uri = "https://feriadellibro.com/es/descripcion-actividad/" + InfoAgenda.IdAgendaServicio
                    });
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleAgendaVm", "BtnCompartir_Mtd", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
        }

        private async void SeleccionarFav_MtdAsync()
        {
            try
            {
                if (InfoAgenda.Fav == "1") /*1 Para favorito*/
                {
                    ImagenFav = "ic_favorito_obscuro.png";
                    InfoAgenda.Fav = "0";
                    UptFranja ip = new UptFranja()
                    {
                        Franja = "0"
                    };

                    string jsonip = JsonConvert.SerializeObject(ip);
                    string urli = logicaWS.Movile_Delet_Fav_Agenda_Mtd(EmailUsuario, InfoAgenda.IdAgenda, "0");
                    await logicaWS.ConectionPost(jsonip, urli);
                }
                else
                {
                    ImagenFav = "ic_favortio_relleno.png";
                    InfoAgenda.Fav = "1";
                    UptFranja ip = new UptFranja()
                    {
                        Franja = "0"
                    };

                    string jsonip = JsonConvert.SerializeObject(ip);
                    string urli = logicaWS.Movile_Update_Fav_Agenda_Mtd(EmailUsuario, InfoAgenda.IdAgenda, "0");
                    await logicaWS.ConectionPost(jsonip, urli);
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleAgendaVM", "SeleccionarFav_MtdAsync", "eliminafavoritoagenda/insertafavoritoagenda");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
        }
    }
}
