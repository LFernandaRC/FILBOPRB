
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
    class ExpositorVm : BaseViewModel
    {
        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;
        public readonly IPageServicio pageServicio;

        private readonly bool EsFavorito;
        private readonly string IdSuceso;

        private ObservableCollection<ListasGeneral> _listaUbicacion;

        private List<Expositores> _listaExpositores;
        private List<Expositores> listaExpositoresTemp;

        private ListasGeneral _selectUbicacion;

        private string _CantidadExpo;
        private string _entExpositor;
        private string _mascaraExpos;
        private string _mascaraUbi;

        private bool _equisUbicacion;
        private bool _FlechaUbicacion;

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

        public string CantidadExpo
        {
            get { return _CantidadExpo; }
            set { SetProperty(ref _CantidadExpo, value); OnPropertyChanged(nameof(CantidadExpo)); }
        }

        public string EntExpositor
        {
            get { return _entExpositor; }
            set { SetProperty(ref _entExpositor, value); OnPropertyChanged(nameof(EntExpositor)); }
        }

        public string MascaraExpos
        {
            get { return _mascaraExpos; }
            set { SetProperty(ref _mascaraExpos, value); OnPropertyChanged(nameof(MascaraExpos)); }
        }

        public string MascaraUbi
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


        public ICommand FocusUbicacion { get; }
        public ICommand BuscarExpositor { get; }
        public ICommand OrdenarExpositor { get; }
        public ICommand LimpiarUbicacion { get; }
        public ICommand PlusButtonCommand { get; }
        public Command<Expositores> SeleccionarModuloCommand { get; }

        /**
          * @brief ExpositoresVM Constructor de la clase 
          * @param idSuceso, trae el id Suceso en caso de venir de la vista de detalleSucesoVM.cs de lo contrario sera 0 (no puede ser null)
          * @param esFavorito, trae un bool indicando si viene de la vista favoritos o no, en caso de venir de la vista de menuPage.XAML.cs(Favoritos) será true (no puede ser null)
        */
        public ExpositorVm(string idSuceso, bool esFavorito, Entry filtroTres)
        {
            Title = AppResources.Expositores;
            EsFavorito = esFavorito;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            IdSuceso = Preferences.Get("IdSuceso", "");
            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();

            BuscarExpositor = new Command(FiltradoGeneral);
            PlusButtonCommand = new Command(SeleccionarFav_Mtd);
            FocusUbicacion = new Command(claseBase.FocusPicker_Mto);
            OrdenarExpositor = new Command(OrdenarExpositor_MtoAsync);
            LimpiarUbicacion = new Command(LimpiarUbicacion_MtoAsync);
            SeleccionarModuloCommand = new Command<Expositores>(SeleccionarModulo);

            filtroTres.Completed += EntryCompleted;

            Inicializar();
        }

        /*INICIO METODOS INICIALES */
        private async void Inicializar()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await ListasFiltros();
                await BuscarExpositor_MtoAsync();
                await FiltrosMascara();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ListasFiltros()
        {
            try
            {
                ListaUbicacion = new ObservableCollection<ListasGeneral>();
                string urli = logicaWS.Movile_Select_Expo_Ubicacion_Mtd(IdSuceso, EmailUsuario, LenguajeBase);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral { Descripcion = item.GetValue("nombresitio").ToString(), Id = item.GetValue("idsitio").ToString() };
                    ListaUbicacion.Add(listasGeneral);
                }
                EquisUbicacion = false;
                FlechaUbicacion = true;
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresVM", "ListasFiltros", "consultaexpositorubicacion");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
        }

        private async Task BuscarExpositor_MtoAsync()
        {
            try
            {
                ListaExpositores = new List<Expositores>();
                listaExpositoresTemp = new List<Expositores>();

                string urli;

                if (EsFavorito)
                    urli = logicaWS.Movile_Select_ExpoFavoritos_Mtd(LenguajeBase, EmailUsuario, "0", IdSuceso, "0", "0");
                else
                    urli = logicaWS.Movile_Select_Expositores_Mtd(LenguajeBase, EmailUsuario, IdSuceso, "0", "0");

                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
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
                        item.GetValue("email").ToString(), item.GetValue("telefono").ToString(), IdSuceso, "", "");

                    if (claseBase.ValidaString(Expositor.LogoDetalle).Equals(""))
                        Expositor.LogoDetalle = Expositor.Imagen;

                    ListaExpositores.Add(Expositor);
                }

                ListaExpositores = ListaExpositores.OrderBy(x => x.NombreExpositor).ToList();
                listaExpositoresTemp = ListaExpositores.OrderBy(x => x.NombreExpositor).ToList();

                CantidadExpo = ListaExpositores.Count.ToString() + " " + AppResources.resultados;
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresVM", "BuscarExpositor_MtoAsync", "consultacatalogoexpositorfav/consultacatalgoexpositor");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
        }

        private async Task FiltrosMascara()
        {
            try
            {
                string urli = logicaWS.Moviel_select_consAMascaraFiltroExp_Mtd("1", IdSuceso);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    MascaraExpos = item.GetValue("Label").ToString();
                }

                string urliDos = logicaWS.Moviel_select_consAMascaraFiltroExp_Mtd("2", IdSuceso);
                string jsonProcedimientoDos = await logicaWS.ConectionGet(urliDos);
                JArray jsArrayDos = JArray.Parse(jsonProcedimientoDos);
                foreach (JObject item in jsArrayDos)
                {
                    MascaraUbi = item.GetValue("Label").ToString();
                }

            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresVM", "FiltrosMascara", "consAMascaraFiltroExp");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
        }
        /*FIN METODOS INICIALES */

        /*INICIO BOTONERIA */
        private async void SeleccionarModulo(Expositores content)
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
        }

        void EntryCompleted(object sender, EventArgs e)
        {
            FiltradoGeneral();
        }

        private async void FiltradoGeneral()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar

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
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresVM", "FiltradoGeneral", null);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void LimpiarUbicacion_MtoAsync()
        {
            EquisUbicacion = false;
            FlechaUbicacion = true;
            _ = new ObservableCollection<ListasGeneral>();
            ObservableCollection<ListasGeneral> team = ListaUbicacion;
            ListaUbicacion = new ObservableCollection<ListasGeneral>();
            ListaUbicacion = team;

            FiltradoGeneral();
        }

        private void OrdenarExpositor_MtoAsync()
        {
            ListaExpositores.Reverse();
            ListaExpositores = ListaExpositores.ToList();
        }

        private async void SeleccionarFav_Mtd(object obj)
        {
            try
            {
                var content = obj as Expositores;

                foreach (var item in ListaExpositores)
                {
                    if (item.IdExpositor == content.IdExpositor)
                    {
                        if (item.Fav == "1") /*1 Para favorito*/
                        {
                            item.ImagenFav = "ic_favorito_obscuro";
                            item.Fav = "0";
                            string urli = logicaWS.Movile_Delet_Fav_Expo_Mtd(LenguajeBase, EmailUsuario, item.IdExpositor);
                            await logicaWS.ConectionGet(urli);
                        }
                        else
                        {
                            item.ImagenFav = "ic_favortio_relleno";
                            item.Fav = "1";
                            string urli = logicaWS.Movile_Update_Fav_Expo_Mtd(LenguajeBase, EmailUsuario, item.IdExpositor);
                            await logicaWS.ConectionGet(urli);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ExpositoresVM", "SeleccionarFav_Mtd", "eliminarusuarioexpositor/insertarusuarioexpositor");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
        }
        /*FIN BOTONERIA */
    }
}