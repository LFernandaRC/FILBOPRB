using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Suceso;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace EventosCorferias.ViewModel.Suceso
{
    public class MapaVM : BaseViewModel
    {
        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;

        private ObservableCollection<SitiosM> _listaSitios;
        private ObservableCollection<ServiciosM> _listaServicios;
        private ObservableCollection<ListasGeneral> _imagenesCarrusel;

        private JArray GeneralSitios;
        private JArray GeneralServicios;

        private readonly string IdModulo;
        private readonly string idSuceso;

        private string bandera_;
        private string _HorarioM;
        private string _TituloMapa;
        private string _DescripcionM;
        private string _RecomendacionesM;
        private string _sorurceBtnMapa;
        private string _sorurceBtnServicios;
        private string _entNombreExpo;

        private bool _verContServicios;
        private bool _verContDetalleMapa;
        private bool _verDescrip;
        private bool _verBtnSitios;

        public string TituloMapa
        {
            get { return _TituloMapa; }
            set { _TituloMapa = value; OnPropertyChanged(nameof(TituloMapa)); }
        }

        public string DescripcionM
        {
            get { return _DescripcionM; }
            set { _DescripcionM = value; OnPropertyChanged(nameof(DescripcionM)); }
        }

        public string HorarioM
        {
            get { return _HorarioM; }
            set { _HorarioM = value; OnPropertyChanged(nameof(HorarioM)); }
        }

        public string RecomendacionesM
        {
            get { return _RecomendacionesM; }
            set { _RecomendacionesM = value; OnPropertyChanged(nameof(RecomendacionesM)); }
        }

        public string SorurceBtnMapa
        {
            get { return _sorurceBtnMapa; }
            set { _sorurceBtnMapa = value; OnPropertyChanged(nameof(SorurceBtnMapa)); }
        }

        public string SorurceBtnServicios
        {
            get { return _sorurceBtnServicios; }
            set { _sorurceBtnServicios = value; OnPropertyChanged(nameof(SorurceBtnServicios)); }
        }

        public string EntNombreExpo
        {
            get { return _entNombreExpo; }
            set { _entNombreExpo = value; OnPropertyChanged(nameof(EntNombreExpo)); }
        }

        public bool VerContDetalleMapa
        {
            get { return _verContDetalleMapa; }
            set { _verContDetalleMapa = value; OnPropertyChanged(nameof(VerContDetalleMapa)); }
        }

        public bool VerContServicios
        {
            get { return _verContServicios; }
            set { _verContServicios = value; OnPropertyChanged(nameof(VerContServicios)); }
        }

        public bool VerDescrip
        {
            get { return _verDescrip; }
            set { _verDescrip = value; OnPropertyChanged(nameof(VerDescrip)); }
        }

        public bool VerBtnSitios
        {
            get { return _verBtnSitios; }
            set { _verBtnSitios = value; OnPropertyChanged(nameof(VerBtnSitios)); }
        }

        public ObservableCollection<ListasGeneral> ImagenesCarrusel
        {
            get { return _imagenesCarrusel; }
            set { _imagenesCarrusel = value; OnPropertyChanged(nameof(ImagenesCarrusel)); }
        }

        public ObservableCollection<SitiosM> ListaSitios
        {
            get { return _listaSitios; }
            set { _listaSitios = value; OnPropertyChanged(nameof(ListaSitios)); }
        }

        public ObservableCollection<ServiciosM> ListaServicios
        {
            get { return _listaServicios; }
            set { _listaServicios = value; OnPropertyChanged(nameof(ListaServicios)); }
        }


        public Command PlayImg { get; }
        public ICommand AbrirMapa { get; }
        public ICommand MapaCommand { get; }
        public ICommand BtnDetalleSitio { get; }
        public ICommand ServiciosCommand { get; }
        public ICommand BtnDetalleServicio { get; }


        public MapaVM(string Id, string origen, string idmodulo, string NombreVista, string bandera)
        {
            Title = NombreVista;
            IsBusy = true;

            IdModulo = idmodulo;
            idSuceso = Id;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();

            pageServicio = new PageServicio();
            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();


            PlayImg = new Command(PlayImg_Mtd);
            MapaCommand = new Command(MostrarInfoSitios);
            BtnDetalleSitio = new Command(BtnDetalleSitio_Mtd);
            ServiciosCommand = new Command(MostrarInfoServicios);
            BtnDetalleServicio = new Command(BtnDetalleServicio_Mtd);

            Incializar(Id, origen, bandera);
            ContadorNotificaciones_Mtd();
        }

        private async void Incializar(string id, string origen, string bandera)
        {
            try
            {
                IsBusy = true;
                bandera_ = bandera;
                VerContDetalleMapa = true;
                SorurceBtnMapa = "ic_mapa_pindos.png";
                SorurceBtnServicios = "ic_mapa_expodos.png";

                await CargaGeneralInfoAsync(id, origen, bandera);
                await CargarInfoServicios(id, origen);
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("EXCEPTION", ex.Message, "MapaVM", "Incializar", "n/a");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CargaGeneralInfoAsync(string id, string origen, string bandera)
        {
            try
            {
                string urli = logicaWS.Movile_Select_General_Mapa_Mtd(bandera, EmailUsuario, LenguajeBase, id, origen, IdModulo);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);

                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    TituloMapa = claseBase.ValidaString(item.GetValue("NombreMapa").ToString());
                    DescripcionM = claseBase.ValidaString(item.GetValue("Contexto").ToString());
                    HorarioM = claseBase.ValidaString(item.GetValue("Horario").ToString());
                    RecomendacionesM = claseBase.ValidaString(item.GetValue("Recomendaciones").ToString());

                    if (!string.IsNullOrWhiteSpace(DescripcionM))
                    {
                        VerDescrip = true;
                    }

                    ImagenesCarrusel = new ObservableCollection<ListasGeneral>();
                    string[] ListaRespuesta = item.GetValue("imagenes").ToString().Split(new string[] { " | " }, StringSplitOptions.None);
                    foreach (var auxRes in ListaRespuesta)
                    {
                        if (!auxRes.Equals("0"))
                        {
                            ImagenesCarrusel.Add(new ListasGeneral { ImagenIcon = auxRes.Trim(), Observacion = auxRes.Trim() });
                        }
                    }
                    await CargarInfoSitios(item.GetValue("IdModulo").ToString(), origen);
                    break;
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "MapaVM", "CargaGeneralInfoAsync", "consultageneralmapa");
            }
        }

        /*Inicio cargar lista de pines */
        private async Task CargarInfoSitios(string idModulol, string origen)
        {
            try
            {
                string ModuloAux;
                if (bandera_ == "2")
                {
                    ModuloAux = idModulol;
                }
                else
                {
                    ModuloAux = IdModulo;
                }

                string urli = logicaWS.Moviel_select_CorferiasEventosServ_Mtd(bandera_, idSuceso, origen, ModuloAux, LenguajeBase);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);

                GeneralSitios = JArray.Parse(jsonProcedimiento);
                ListaSitios = new ObservableCollection<SitiosM>();
                foreach (JObject item in GeneralSitios)
                    ListaSitios.Add(new SitiosM
                    {
                        id = item.GetValue("id").ToString(),
                        IdSitio = item.GetValue("IdSitio").ToString(),
                        latitud = item.GetValue("latitud").ToString(),
                        longitud = item.GetValue("longitud").ToString(),
                        NombreServicio = item.GetValue("NombreSitio").ToString(),
                        Contexto = item.GetValue("Contexto").ToString(),
                        servicio = item.GetValue("servicio").ToString(),
                        iconoservicio = item.GetValue("iconoservicio").ToString(),
                        IdSitioServicio = item.GetValue("IdSitioServicio").ToString(),
                        iconossertarjeta = item.GetValue("iconossertarjeta").ToString(),
                        Descripcion = item.GetValue("Descripcion").ToString(),
                        Horario = item.GetValue("Horario").ToString(),
                        Recomendacion = item.GetValue("Recomendacion").ToString(),
                        Ira = item.GetValue("Ira").ToString()
                    });
            }
            catch (Exception ex)
            {
                IsBusy = true;
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.laInformacionDeEsteRecintoNoDisponible, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "MapaVM", "CargarInfoSitios", "consultasitiosmapab1");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CargarInfoServicios(string id, string origen)
        {
            try
            {
                string urli = logicaWS.Movile_Select_consAServiciosEvento_Mtd("1", id, origen, LenguajeBase, IdModulo);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);

                GeneralServicios = JArray.Parse(jsonProcedimiento);
                ListaServicios = new ObservableCollection<ServiciosM>();
                foreach (JObject item in GeneralServicios)
                {
                    string icoS = "";
                    var ausiconoUno = item.GetValue("iconoservicio").ToString().Split(',');
                    if (ausiconoUno.Length > 0)
                    {
                        icoS = ausiconoUno[0];
                    }
                    ListaServicios.Add(new ServiciosM
                    {
                        id = item.GetValue("id").ToString(),
                        IdSitio = item.GetValue("IdSitio").ToString(),
                        latitud = item.GetValue("latitud").ToString(),
                        longitud = item.GetValue("longitud").ToString(),
                        NombreServicio = item.GetValue("NombreServicio").ToString(),
                        Contexto = item.GetValue("Contexto").ToString(),
                        servicio = item.GetValue("servicio").ToString(),
                        iconoservicio = item.GetValue("iconoservicio").ToString(),
                        IdSitioServicio = item.GetValue("IdSitioServicio").ToString(),
                        iconossertarjeta = item.GetValue("iconossertarjeta").ToString(),
                        Descripcion = item.GetValue("Descripcion").ToString(),
                        Horario = item.GetValue("Horario").ToString(),
                        Recomendacion = item.GetValue("Recomendacion").ToString(),
                        Ira = item.GetValue("Ira").ToString(),
                        IconoPrimero = icoS
                    });
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "MapaVM", "CargarInfoServicios", "consultasitiosmapaserviciosb2");
            }
            finally
            {
                if (ListaServicios.Count > 0)
                {
                    VerBtnSitios = true;
                }
            }
        }
        /*Fin cargar lista de pines */

        private async void PlayImg_Mtd(object obj)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                ListasGeneral Aux = (ListasGeneral)obj;
                await RootNavigation.PushAsync(new ContenidosWebView(Title, Aux.Observacion));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "´MapaVM", "PlayImg_Mtd", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void BtnDetalleSitio_Mtd(object obj)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                var Aux = (SitiosM)obj;
                await RootNavigation.PushAsync(new DetalleSitioView(Aux));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "MapaVm", "BtnDetalleSitio_Mtd", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void BtnDetalleServicio_Mtd(object obj)
        {
            try
            {
                try
                {
                    IsBusy = true;
                    await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                    var Aux = (ServiciosM)obj;
                    await RootNavigation.PushAsync(new DetalleServicioView(Aux));
                }
                catch (Exception ex)
                {
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "MapaVm", "BtnDetalleServicio_Mtd", "n/a");
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                }
                finally
                {
                    IsBusy = false;
                }
            }
            catch { }
        }

        private void MostrarInfoSitios()
        {
            try
            {
                IsBusy = true;
                VerContServicios = false;
                VerContDetalleMapa = true;

                SorurceBtnMapa = "ic_mapa_pindos.png";
                SorurceBtnServicios = "ic_mapa_expodos.png";
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "MapaVM", "MostrarInfoServicios", null);
            }
            finally
            {
                IsBusy = false;
            }

        }

        private void MostrarInfoServicios()
        {
            try
            {
                IsBusy = true;

                VerContServicios = true;
                VerContDetalleMapa = false;

                SorurceBtnMapa = "ic_mapa_pinuno.png";
                SorurceBtnServicios = "ic_mapa_expouno.png";
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "MapaVM", "MostrarInfoServicios", "n/a");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
