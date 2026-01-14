using Mopups.Services;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;

using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.PopUp;
using EventosCorferias.Views.Suceso;
using EventosCorferias.Views.Usuario;
using EventosCorferias.Views.Boletas;
using EventosCorferias.Resources.RecursosIdioma;

namespace EventosCorferias.ViewModel.Suceso
{
    public class SucesoVm : BaseViewModel
    {
        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;

        private string idSuceso;

        private string idRecinto;

        private Color _colorParametrizable;

        private string _url;
        private string _diaFin;
        private string _titulo;
        private string _diaInicio;
        private string _imagenSuceso;
        private string _descripcionFeria;
        private string _informacionFeria;

        private Color _ColorFondoPrincipal;
        private string _ImagenPantallaFinal;

        private string _tpata1;
        private string _tpata2;
        private string _tpata3;
        private string _tpata4;
        private string _tpata5;
        private string _tpata6;
        private string _tpata7;
        private string _tpata8;
        private string _tpata9;
        private string _tpata10;
        private string _tpata11;
        private string _tpata12;
        private string _tpata13;
        private string _tpata14;
        private string _tpata15;

        private int _heighCarrusel;

        private bool _vPata1;
        private bool _vPata2;
        private bool _vPata3;
        private bool _vPata4;
        private bool _vPata5;
        private bool _vPata6;
        private bool _vPata7;
        private bool _vPata8;
        private bool _vPata9;
        private bool _vPata10;
        private bool _vPata11;
        private bool _vPata12;
        private bool _vPata13;
        private bool _vPata14;
        private bool _vPata15;
        private bool _moduloRedes;
        private bool _checkNewsletter;
        private bool _moduloCoferiasEn;

        private ObservableCollection<Images> _imagenRedRecinto;
        private ObservableCollection<Images> _imagenesRedSuceso;

        private ObservableCollection<ListasGeneral> _modulosCarrusel;
        private ObservableCollection<ListasGeneral> _imagenesCarrusel;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata1;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata2;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata3;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata4;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata5;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata6;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata7;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata8;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata9;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata10;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata11;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata12;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata13;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata14;
        private ObservableCollection<ListasGeneral> _imagenesCarruselPata15;

        private Images _SelectedItemModuloRedSocial;
        private Images _SelectedItemModuloRedSocialRecinto;

        public Images SelectedRedSocial
        {
            get { return _SelectedItemModuloRedSocial; }
            set
            {
                _SelectedItemModuloRedSocial = null;
                ItemSelectedRedSocialCommand.Execute(value);
                OnPropertyChanged(nameof(SelectedRedSocial));
            }
        }

        public Images SelectedRedSocialRecinto
        {
            get { return _SelectedItemModuloRedSocialRecinto; }
            set { _SelectedItemModuloRedSocialRecinto = null; ItemSelectedRedSocialCommand.Execute(value); OnPropertyChanged(nameof(SelectedRedSocialRecinto)); }
        }

        public Color ColorFondoPrincipal
        {
            get { return _ColorFondoPrincipal; }
            set { _ColorFondoPrincipal = value; OnPropertyChanged(nameof(ColorFondoPrincipal)); }
        }

        public Color ColorParametrizable
        {
            get { return _colorParametrizable; }
            set { _colorParametrizable = value; OnPropertyChanged(nameof(ColorParametrizable)); }
        }

        public string ImagenPantallaFinal
        {
            get { return _ImagenPantallaFinal; }
            set { _ImagenPantallaFinal = value; OnPropertyChanged(nameof(ImagenPantallaFinal)); }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; OnPropertyChanged(nameof(Titulo)); }
        }

        public string DiaInicio
        {
            get { return _diaInicio; }
            set { _diaInicio = value; OnPropertyChanged(nameof(DiaInicio)); }
        }

        public string DiaFin
        {
            get { return _diaFin; }
            set { _diaFin = value; OnPropertyChanged(nameof(DiaFin)); }
        }

        public string UrlSuceso
        {
            get { return _url; }
            set { _url = value; OnPropertyChanged(nameof(UrlSuceso)); }
        }

        public string InformacionFeria
        {
            get { return _informacionFeria; }
            set { _informacionFeria = value; OnPropertyChanged(nameof(InformacionFeria)); }
        }

        public string DescripcionFeria
        {
            get { return _descripcionFeria; }
            set { _descripcionFeria = value; OnPropertyChanged(nameof(DescripcionFeria)); }
        }

        public string ImagenSuceso
        {
            get { return _imagenSuceso; }
            set { _imagenSuceso = value; OnPropertyChanged(nameof(ImagenSuceso)); }
        }

        public string Tpata1
        {
            get { return _tpata1; }
            set { _tpata1 = value; OnPropertyChanged(nameof(Tpata1)); }
        }

        public string Tpata2
        {
            get { return _tpata2; }
            set { _tpata2 = value; OnPropertyChanged(nameof(Tpata2)); }
        }

        public string Tpata3
        {
            get { return _tpata3; }
            set { _tpata3 = value; OnPropertyChanged(nameof(Tpata3)); }
        }

        public string Tpata4
        {
            get { return _tpata4; }
            set { _tpata4 = value; OnPropertyChanged(nameof(Tpata4)); }
        }

        public string Tpata5
        {
            get { return _tpata5; }
            set { _tpata5 = value; OnPropertyChanged(nameof(Tpata5)); }
        }

        public string Tpata6
        {
            get { return _tpata6; }
            set { _tpata6 = value; OnPropertyChanged(nameof(Tpata6)); }
        }

        public string Tpata7
        {
            get { return _tpata7; }
            set { _tpata7 = value; OnPropertyChanged(nameof(Tpata7)); }
        }

        public string Tpata8
        {
            get { return _tpata8; }
            set { _tpata8 = value; OnPropertyChanged(nameof(Tpata8)); }
        }

        public string Tpata9
        {
            get { return _tpata9; }
            set { _tpata9 = value; OnPropertyChanged(nameof(Tpata9)); }
        }

        public string Tpata10
        {
            get { return _tpata10; }
            set { _tpata10 = value; OnPropertyChanged(nameof(Tpata10)); }
        }

        public string Tpata11
        {
            get { return _tpata11; }
            set { _tpata11 = value; OnPropertyChanged(nameof(Tpata11)); }
        }

        public string Tpata12
        {
            get { return _tpata12; }
            set { _tpata12 = value; OnPropertyChanged(nameof(Tpata12)); }
        }

        public string Tpata13
        {
            get { return _tpata13; }
            set { _tpata13 = value; OnPropertyChanged(nameof(Tpata13)); }
        }

        public string Tpata14
        {
            get { return _tpata14; }
            set { _tpata14 = value; OnPropertyChanged(nameof(Tpata14)); }
        }

        public string Tpata15
        {
            get { return _tpata15; }
            set { _tpata15 = value; OnPropertyChanged(nameof(Tpata15)); }
        }

        public bool VPata1
        {
            get { return _vPata1; }
            set { _vPata1 = value; OnPropertyChanged(nameof(VPata1)); }
        }

        public bool VPata2
        {
            get { return _vPata2; }
            set { _vPata2 = value; OnPropertyChanged(nameof(VPata2)); }
        }

        public bool VPata3
        {
            get { return _vPata3; }
            set { _vPata3 = value; OnPropertyChanged(nameof(VPata3)); }
        }

        public bool VPata4
        {
            get { return _vPata4; }
            set { _vPata4 = value; OnPropertyChanged(nameof(VPata4)); }
        }

        public bool VPata5
        {
            get { return _vPata5; }
            set { _vPata5 = value; OnPropertyChanged(nameof(VPata5)); }
        }

        public bool VPata6
        {
            get { return _vPata6; }
            set { _vPata6 = value; OnPropertyChanged(nameof(VPata6)); }
        }

        public bool VPata7
        {
            get { return _vPata7; }
            set { _vPata7 = value; OnPropertyChanged(nameof(VPata7)); }
        }

        public bool VPata8
        {
            get { return _vPata8; }
            set { _vPata8 = value; OnPropertyChanged(nameof(VPata8)); }
        }

        public bool VPata9
        {
            get { return _vPata9; }
            set { _vPata9 = value; OnPropertyChanged(nameof(VPata9)); }
        }

        public bool VPata10
        {
            get { return _vPata10; }
            set { _vPata10 = value; OnPropertyChanged(nameof(VPata10)); }
        }

        public bool VPata11
        {
            get { return _vPata11; }
            set { _vPata11 = value; OnPropertyChanged(nameof(VPata11)); }
        }

        public bool VPata12
        {
            get { return _vPata12; }
            set { _vPata12 = value; OnPropertyChanged(nameof(VPata12)); }
        }

        public bool VPata13
        {
            get { return _vPata13; }
            set { _vPata13 = value; OnPropertyChanged(nameof(VPata13)); }
        }

        public bool VPata14
        {
            get { return _vPata14; }
            set { _vPata14 = value; OnPropertyChanged(nameof(VPata14)); }
        }

        public bool VPata15
        {
            get { return _vPata15; }
            set { _vPata15 = value; OnPropertyChanged(nameof(VPata15)); }
        }

        public int HeighCarrusel
        {
            get { return _heighCarrusel; }
            set { _heighCarrusel = value; OnPropertyChanged(nameof(HeighCarrusel)); }
        }

        public bool CheckNewsletter
        {
            get { return _checkNewsletter; }
            set { _checkNewsletter = value; OnPropertyChanged(nameof(CheckNewsletter)); _ = ActualizarEstadoNews_MtdAsync(CheckNewsletter); }
        }

        public bool ModuloRedes
        {
            get { return _moduloRedes; }
            set { _moduloRedes = value; OnPropertyChanged(nameof(ModuloRedes)); }
        }

        public bool ModuloCoferiasEn
        {
            get { return _moduloCoferiasEn; }
            set { _moduloCoferiasEn = value; OnPropertyChanged(nameof(ModuloCoferiasEn)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarrusel
        {
            get { return _imagenesCarrusel; }
            set { _imagenesCarrusel = value; OnPropertyChanged(nameof(ImagenesCarrusel)); }
        }

        public ObservableCollection<Images> ImagenesRedSuceso
        {
            get { return _imagenesRedSuceso; }
            set { _imagenesRedSuceso = value; OnPropertyChanged(nameof(ImagenesRedSuceso)); }
        }

        public ObservableCollection<ListasGeneral> ModulosCarrusel
        {
            get { return _modulosCarrusel; }
            set { _modulosCarrusel = value; OnPropertyChanged(nameof(ModulosCarrusel)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata1
        {
            get { return _imagenesCarruselPata1; }
            set { _imagenesCarruselPata1 = value; OnPropertyChanged(nameof(ImagenesCarruselPata1)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata2
        {
            get { return _imagenesCarruselPata2; }
            set { _imagenesCarruselPata2 = value; OnPropertyChanged(nameof(ImagenesCarruselPata2)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata3
        {
            get { return _imagenesCarruselPata3; }
            set { _imagenesCarruselPata3 = value; OnPropertyChanged(nameof(ImagenesCarruselPata3)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata4
        {
            get { return _imagenesCarruselPata4; }
            set { _imagenesCarruselPata4 = value; OnPropertyChanged(nameof(ImagenesCarruselPata4)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata5
        {
            get { return _imagenesCarruselPata5; }
            set { _imagenesCarruselPata5 = value; OnPropertyChanged(nameof(ImagenesCarruselPata5)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata6
        {
            get { return _imagenesCarruselPata6; }
            set { _imagenesCarruselPata6 = value; OnPropertyChanged(nameof(ImagenesCarruselPata6)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata7
        {
            get { return _imagenesCarruselPata7; }
            set { _imagenesCarruselPata7 = value; OnPropertyChanged(nameof(ImagenesCarruselPata7)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata8
        {
            get { return _imagenesCarruselPata8; }
            set { _imagenesCarruselPata8 = value; OnPropertyChanged(nameof(ImagenesCarruselPata8)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata9
        {
            get { return _imagenesCarruselPata9; }
            set { _imagenesCarruselPata9 = value; OnPropertyChanged(nameof(ImagenesCarruselPata9)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata10
        {
            get { return _imagenesCarruselPata10; }
            set { _imagenesCarruselPata10 = value; OnPropertyChanged(nameof(ImagenesCarruselPata10)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata11
        {
            get { return _imagenesCarruselPata11; }
            set { _imagenesCarruselPata11 = value; OnPropertyChanged(nameof(ImagenesCarruselPata11)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata12
        {
            get { return _imagenesCarruselPata12; }
            set { _imagenesCarruselPata12 = value; OnPropertyChanged(nameof(ImagenesCarruselPata12)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata13
        {
            get { return _imagenesCarruselPata13; }
            set { _imagenesCarruselPata13 = value; OnPropertyChanged(nameof(ImagenesCarruselPata13)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata14
        {
            get { return _imagenesCarruselPata14; }
            set { _imagenesCarruselPata14 = value; OnPropertyChanged(nameof(ImagenesCarruselPata14)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarruselPata15
        {
            get { return _imagenesCarruselPata15; }
            set { _imagenesCarruselPata15 = value; OnPropertyChanged(nameof(ImagenesCarruselPata15)); }
        }

        public ObservableCollection<Images> ImagenRedRecinto
        {
            get { return _imagenRedRecinto; }
            set { _imagenRedRecinto = value; OnPropertyChanged(nameof(ImagenRedRecinto)); }
        }

        public Command PlayImg { get; }

        public Command UrlSucesoLink { get; }

        public Command SelectedPataLogoUrl { get; }

        public Command<ListasGeneral> SeleccionarModuloCommand { get; }

        public ICommand ItemSelectedRedSocialCommand
        {
            get
            {
                return new Command(async list =>
                {
                    try
                    {
                        IsBusy = true;
                        await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                        Images lista = (Images)list;

                        if (lista.Link.ToLower().Contains("https://www.facebook.com"))
                        {
                            await Launcher.Default.OpenAsync(lista.Link);
                        }
                        else
                        {
                            await RootNavigation.PushAsync(new ContenidosWebView("Red Social", lista.Link));
                        }
                    }
                    catch (Exception ex)
                    {
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "ItemSelectedRedSocialCommand", "n/a");
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
            }
        }


        public SucesoVm()
        {
            IsBusy = true;
            ModuloRedes = true;
            ModuloCoferiasEn = true;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            idSuceso = Preferences.Get("IdSuceso", "");
            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();

            PlayImg = new Command(PlayImg_Mtd);
            UrlSucesoLink = new Command(AbrirLinkWeb_MtdAsync);
            SelectedPataLogoUrl = new Command(SeleccionarPataLogo_MtdAsync);
            SeleccionarModuloCommand = new Command<ListasGeneral>(SeleccionarModulo);

            _ = Inicializar_MtoAsync();
            ContadorNotificaciones_Mtd(); 
        }


        /*INICIO CARGA INCIAL*/
        private async Task Inicializar_MtoAsync()
        {
            try
            {
                await InfoGeneralSuceso_MtoAsync();
                await CargarRecursos();
                await InfoCarruselModulosSuceso_MtoAsync();
                await InfoRedSocialSuceso_MtoAsync();
                await InfoPatasLogoSuceso_MtdAsync();
                await ValidarMenuInferior();
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "Inicializar_MtoAsync", "n/a");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task InfoGeneralSuceso_MtoAsync()
        {
            try
            {
                /*Informacion general */
                string urli = logicaWS.Movile_Select_DetalleSuseco_Mtd(idSuceso, LenguajeBase, EmailUsuario);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                JArray jsArray;
                if (jsonProcedimiento != "")
                {
                    jsArray = JArray.Parse(jsonProcedimiento);
                    if (jsArray.Count > 0)
                    {
                        foreach (JObject item in jsArray)
                        {
                            DiaFin = claseBase.ValidaString(item.GetValue("DiaFin").ToString());
                            UrlSuceso = claseBase.ValidaString(item.GetValue("Url").ToString());
                            DiaInicio = claseBase.ValidaString(item.GetValue("DiaIn").ToString());
                            idRecinto = claseBase.ValidaString(item.GetValue("IdRecinto").ToString());
                            ImagenSuceso = claseBase.ValidaString(item.GetValue("imagen").ToString());
                            Titulo = claseBase.ValidaString(item.GetValue("NombreSuceso").ToString()).ToUpper();
                            InformacionFeria = claseBase.CapitalizeFirstLetter(item.GetValue("Descripcion").ToString());
                            DescripcionFeria = claseBase.CapitalizeFirstLetter(item.GetValue("DetalleSuceso").ToString());

                            ImagenesCarrusel = new ObservableCollection<ListasGeneral>();
                            if (claseBase.ValidaString(item.GetValue("Imagenes").ToString()) != "")
                            {
                                string[] arrayAux = item.GetValue("Imagenes").ToString().Split([" | "], System.StringSplitOptions.None);
                                if (arrayAux.Length > 0)
                                {
                                    foreach (var Aux in arrayAux)
                                    {
                                        ListasGeneral listasGeneral = new ListasGeneral { Observacion = Aux };
                                        ImagenesCarrusel.Add(listasGeneral);
                                    }
                                }
                                else
                                {
                                    ListasGeneral listasGeneral = new ListasGeneral { Observacion = ImagenSuceso };
                                    ImagenesCarrusel.Add(listasGeneral);
                                }
                            }
                            else
                            {
                                ListasGeneral listasGeneral = new ListasGeneral { Observacion = ImagenSuceso };
                                ImagenesCarrusel.Add(listasGeneral);
                            }

                            if (item.GetValue("New").ToString() == "1")
                                CheckNewsletter = true;

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "InfoGeneralSuceso_MtoAsync", "consultadetallesuceso");
            }
        }

        private async Task CargarRecursos()
        {
            try
            {
                string? urli = logicaWS.Movile_Login_Mtd("1", "1", Preferences.Get("IdSuceso", ""));
                var jsonProcedimiento = await logicaWS.ConectionGet(urli);

                if (jsonProcedimiento != null)
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    foreach (JObject item in jsArray)
                    {
                        ColorFondoPrincipal = Color.FromArgb(item.GetValue("ColorFondo").ToString());
                        ImagenPantallaFinal = item.GetValue("ImagenInferior").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "CargarRecursos", "consultaAdministraLogin");
            }
        }

        public async Task InfoCarruselModulosSuceso_MtoAsync()
        {
            try
            {
                /*Carrusel de iconos*/
                string urli = logicaWS.Movile_Select_DetalleSucesoModulosl_Mtd(LenguajeBase, EmailUsuario, idSuceso);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                JArray jsArray;
                if (jsonProcedimiento != "")
                {
                    jsArray = JArray.Parse(jsonProcedimiento);
                    ModulosCarrusel = new ObservableCollection<ListasGeneral>();
                    foreach (JObject item in jsArray)
                    {
                        ModulosCarrusel.Add(new ListasGeneral()
                        {
                            Id = item.GetValue("IdModulo").ToString(),
                            Descripcion = item.GetValue("NombreModulo").ToString(),
                            Observacion = item.GetValue("imagen").ToString(),
                            Path = item.GetValue("Path").ToString(),
                            TipoIcono = item.GetValue("TipoIcono").ToString(),
                            ColorTexto = Color.FromArgb(item.GetValue("ColorTexto").ToString())
                        });
                        ColorParametrizable = Color.FromArgb(item.GetValue("ColorTexto").ToString());
                    }
                    await Task.Delay(100);
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "InfoCarruselModulosSuceso_MtoAsync", "consultasucesocarrusel");
            }
        }

        public async Task InfoRedSocialSuceso_MtoAsync()
        {
            try
            {
                /*Redes Sociales */
                string urli = logicaWS.Movile_Select_Red_Suceso_Mtd(idSuceso);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                ImagenesRedSuceso = new ObservableCollection<Images>();
                if (jsonProcedimiento != "")
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    foreach (JObject item in jsArray)
                    {
                        ImagenesRedSuceso.Add(new Images() { Url = item.GetValue("Imagen").ToString(), Link = item.GetValue("url").ToString() });
                    }
                }

                if (ImagenesRedSuceso.Count() > 0)
                    ModuloRedes = true;
                else
                    ModuloRedes = false;
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "InfoRedSocialSuceso_MtoAsync1", "consultaredes");
            }

            try
            {
                /*Redes del recinto */
                string urli = logicaWS.Movile_Select_DetalleSucesoRedRecinto_Mtd(idRecinto);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                ImagenRedRecinto = new ObservableCollection<Images>();
                if (jsonProcedimiento != "")
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    foreach (JObject item in jsArray)
                    {
                        ImagenRedRecinto.Add(new Images() { Url = item.GetValue("Imagen").ToString(), Link = item.GetValue("Url").ToString() });
                    }

                    if (ImagenRedRecinto.Count() > 0)
                        ModuloCoferiasEn = true;
                    else
                        ModuloCoferiasEn = false;
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "InfoRedSocialSuceso_MtoAsync2", "consultaredes");
            }
        }

        public async Task InfoPatasLogoSuceso_MtdAsync()
        {
            try
            {
                /*patas de logo */
                ImagenesCarruselPata1 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata2 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata3 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata4 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata5 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata6 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata7 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata8 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata9 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata10 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata11 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata12 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata13 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata14 = new ObservableCollection<ListasGeneral>();
                ImagenesCarruselPata15 = new ObservableCollection<ListasGeneral>();

                string auxNombreCategoria = null;

                string urli = logicaWS.Movile_Select_DetalleSucesoPataLogos_Mtd(LenguajeBase, EmailUsuario, idSuceso);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                JArray jsArray;
                if (jsonProcedimiento != "")
                {
                    jsArray = JArray.Parse(jsonProcedimiento);

                    foreach (JObject item in jsArray)
                    {
                        auxNombreCategoria = item.GetValue("Categoria").ToString();
                        Tpata1 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(item.GetValue("Categoria").ToString()));
                        VPata1 = true;
                        foreach (JObject item2 in jsArray)
                        {
                            if (auxNombreCategoria == item2.GetValue("Categoria").ToString())
                                ImagenesCarruselPata1.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                            else
                                break;
                        }
                        break;
                    }
                    int auxLista = 1;
                    foreach (JObject item in jsArray)
                    {
                        if (auxNombreCategoria != item.GetValue("Categoria").ToString())
                        {
                            auxLista += 1;
                            auxNombreCategoria = item.GetValue("Categoria").ToString();
                            foreach (JObject item2 in jsArray)
                            {
                                if (auxNombreCategoria == item2.GetValue("Categoria").ToString())
                                {
                                    if (auxLista == 2)
                                    {
                                        VPata2 = true;
                                        Tpata2 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata2.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }
                                    if (auxLista == 3)
                                    {
                                        VPata3 = true;
                                        Tpata3 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata3.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }
                                    if (auxLista == 4)
                                    {
                                        VPata4 = true;
                                        Tpata4 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata4.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }
                                    if (auxLista == 5)
                                    {
                                        VPata5 = true;
                                        Tpata5 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata5.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }
                                    if (auxLista == 6)
                                    {
                                        VPata6 = true;
                                        Tpata6 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata6.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }
                                    if (auxLista == 7)
                                    {
                                        VPata7 = true;
                                        Tpata7 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata7.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }
                                    if (auxLista == 8)
                                    {
                                        VPata8 = true;
                                        Tpata8 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata8.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }
                                    if (auxLista == 9)
                                    {
                                        VPata9 = true;
                                        Tpata9 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata9.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }
                                    if (auxLista == 10)
                                    {
                                        VPata10 = true;
                                        Tpata10 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata10.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }
                                    if (auxLista == 11)
                                    {
                                        VPata11 = true;
                                        Tpata11 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata11.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }
                                    if (auxLista == 12)
                                    {
                                        VPata12 = true;
                                        Tpata12 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata12.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }
                                    if (auxLista == 13)
                                    {
                                        VPata13 = true;
                                        Tpata13 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata13.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }
                                    if (auxLista == 14)
                                    {
                                        VPata14 = true;
                                        Tpata14 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata14.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }
                                    if (auxLista == 15)
                                    {
                                        VPata15 = true;
                                        Tpata15 = claseBase.CapitalizeFirstLetter(claseBase.ValidaString(auxNombreCategoria));
                                        ImagenesCarruselPata15.Add(new ListasGeneral { Observacion = item2.GetValue("Imagen").ToString(), Descripcion = item2.GetValue("Url").ToString() });
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "InfoPatasLogoSuceso_MtdAsync", "consultapatalogosuceso");
            }
        }
        /*FIN CARGA INCIAL*/


        /*INICIO BOTONERIA*/
        private async Task ActualizarEstadoNews_MtdAsync(bool CheckNewsletter)
        {
            try
            {
                string urli;
                if (CheckNewsletter == true)
                {
                    urli = logicaWS.Movile_Update_NewsUsuario_Mtd(EmailUsuario, idSuceso);
                    await logicaWS.ConectionGet(urli);
                }
                else
                {
                    urli = logicaWS.Movile_Delet_NewsUsuario_Mtd(EmailUsuario, idSuceso);
                    await logicaWS.ConectionDelete(urli);
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "ActualizarEstadoNews_MtdAsync", "insertarNewsUsuario/eliminarNewsUsuario");
            }
        }

        private async void SeleccionarPataLogo_MtdAsync(object obj)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await RootNavigation.PushAsync(new ContenidosWebView("Ferias y Eventos", obj.ToString()));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "SeleccionarPataLogo_MtdAsync", "redireccionamiento");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void AbrirLinkWeb_MtdAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await RootNavigation.PushAsync(new ContenidosWebView("Ferias y Eventos", UrlSuceso));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "AbrirLinkWeb_MtdAsync", "redireccionamiento");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void PlayImg_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await RootNavigation.PushAsync(new ContenidosWebView("Ferias y Eventos", ImagenSuceso));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "PlayImg_Mtd", "redireccionamiento");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void SeleccionarModulo(ListasGeneral lista)
        {
            if (lista != null)
            {
                CargarOpcionesCarrusel(lista);
            }
        }

        private async void CargarOpcionesCarrusel(ListasGeneral lista)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar

                if (lista.Id == "1") // conferencistas
                {
                    try
                    {
                        string urli = logicaWS.Movile_select_Confenrencistas_Fav_Mtd("0", "0", "1", "0", EmailUsuario, idSuceso, LenguajeBase);
                        string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                        JArray jsArray = JArray.Parse(jsonProcedimiento);

                        if (jsArray.Count() > 0)
                            await RootNavigation.PushAsync(new ConferencistaView(idSuceso, false));
                        else
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);

                    }
                    catch (Exception ex)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, ex.Message, AppResources.VMaceptar);
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "ICommand ItemSelectedCommandModulos", "consultaconferencista");
                    }

                }

                else if (lista.Id == "2") // Expositores
                {
                    try
                    {
                        string urli = logicaWS.Movile_Select_Expositores_Mtd(LenguajeBase, EmailUsuario, idSuceso, "0", "0");
                        string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                        JArray jsArray = JArray.Parse(jsonProcedimiento);

                        if (jsArray.Count() > 0)
                            await RootNavigation.PushAsync(new ExpositorView(idSuceso, false));
                        else
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                    }
                    catch (Exception ex)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, ex.Message, AppResources.VMaceptar);
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "ICommand ItemSelectedCommandModulos", "consultacatalgoexpositor");
                    }

                }

                else if (lista.Id == "22") // Contenidos
                {
                    try
                    {
                        string urli = logicaWS.Movile_Select_ContenidosSuceso_Mtd("1", LenguajeBase, EmailUsuario, "0", idSuceso);
                        string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                        JArray jsArray = JArray.Parse(jsonProcedimiento);

                        if (jsArray.Count() > 0)
                            await RootNavigation.PushAsync(new ContenidosPage(false, "suceso", idSuceso, "Jornadas"));
                        else
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);

                    }
                    catch (Exception ex)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, ex.Message, AppResources.VMaceptar);
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SucesoVm", "ICommand ItemSelectedCommandModulos", "consultacontenidosuceso");
                    }
                }

                else if (lista.Id == "3")
                {// Agenda
                    RootMainPage.Detail = new NavigationPage(new AgendaView(idSuceso, "0", "", false, "0", true, "0"));
                }
                else if (lista.Id == "15")
                {// servicios popup
                    await MopupService.Instance.PushAsync(new ServiciosPopUp(idSuceso, "2", "2"));
                }
                else if (lista.Id == "6")
                {// boleteria
                    RootMainPage.Detail = new NavigationPage(new BoleteriaView());
                }
                else
                {
                    // LOS QUE ABREN UNA PAGINA TIPO 1
                    if (lista.TipoIcono == "1")
                    {
                        await RootNavigation.PushAsync(new ContenidosWebView(lista.Descripcion, lista.Path));
                    }

                    // LOS QUE ABREN SITIOS TIPO 2
                    if (lista.TipoIcono == "2")
                    {
                        await RootNavigation.PushAsync(new MapaView(idSuceso, "2", lista.Id, lista.Descripcion, "1"));
                    }

                    // LOS QUE ABREN EL DETALLE CONTENIDOS SON TIPO 3
                    if (lista.TipoIcono == "3")
                    {
                        string urli = logicaWS.Movile_Select_ContenidosSuceso_Mtd("3", LenguajeBase, EmailUsuario, "0", idSuceso);
                        string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                        JArray jsArray = JArray.Parse(jsonProcedimiento);

                        foreach (JObject item in jsArray)
                        {
                            if (item.GetValue("IdContenido").ToString() == lista.Id)
                            {
                                Contenidos contenidos = new Contenidos(item.GetValue("IdContenido").ToString(), item.GetValue("Titulo").ToString(), claseBase.CapitalizeFirstLetter(item.GetValue("NombreCategoria").ToString()),
                                item.GetValue("url").ToString(), item.GetValue("ImagenPortada").ToString(), item.GetValue("ImagenesCarrusel").ToString(), claseBase.CapitalizeFirstLetter(item.GetValue("Contexto").ToString()),
                                item.GetValue("Contenido").ToString(), item.GetValue("PalabrasClave").ToString(), item.GetValue("Fav").ToString(), idSuceso);

                                await RootNavigation.PushAsync(new DetalleContenidoView(contenidos, lista.Path, "Sucesos"));
                                IsBusy = false;
                                break;
                            }
                        }
                    }
                }

            }
            catch (Exception exs)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, exs.Message, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", exs.Message, "SucesoVm", "ICommand ItemSelectedCommandModulos", null);
            }
            finally
            {
                IsBusy = false;
            }
        }
        /*FIN BOTONERIA*/
    }
}
