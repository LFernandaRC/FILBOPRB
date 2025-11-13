using EventosCorferias.Interfaces;
using EventosCorferias.Models;
using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.Services;
using EventosCorferias.Views.Suceso;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EventosCorferias.ViewModel.Suceso
{
    class ContenidosVm : BaseViewModel
    {
        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;

        private List<Contenidos> _listaContenidos;
        private List<Contenidos> ListaContenidosTemp;

        private ObservableCollection<ListasGeneral> _listaContenidosFiltro;

        private ListasGeneral _SelectFiltroComunidad;

        private readonly string TipoContenido;
        private readonly string IdSucesoComunidad;
        private readonly string Vista_;

        private string _entContenidos;
        private string _cantidadContenidos;

        private bool _equisFiltro;
        private bool _flechaFiltro;
        private bool _categoriaVisible;

        public ListasGeneral SelectFiltroComunidad
        {
            get { return _SelectFiltroComunidad; }
            set
            {
                _SelectFiltroComunidad = value;
                if (value != null)
                {
                    FlechaFiltro = false;
                    EquisFiltro = true;
                    FiltrosGenerales_MtdAsycn();
                }
                OnPropertyChanged(nameof(SelectFiltroComunidad));
            }
        }

        public List<Contenidos> ListaContenidos
        {
            get { return _listaContenidos; }
            set { _listaContenidos = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ListasGeneral> ListaContenidosFiltro
        {
            get { return _listaContenidosFiltro; }
            set { _listaContenidosFiltro = value; OnPropertyChanged(); }
        }

        public string CantidadContenidos
        {
            get { return _cantidadContenidos; }
            set { _cantidadContenidos = value; OnPropertyChanged(nameof(CantidadContenidos)); }
        }

        public string EntContenidos
        {
            get { return _entContenidos; }
            set { _entContenidos = value; OnPropertyChanged(nameof(EntContenidos)); }
        }

        public bool CategoriaVisible
        {
            get { return _categoriaVisible; }
            set { _categoriaVisible = value; OnPropertyChanged(nameof(CategoriaVisible)); }
        }

        public bool EquisFiltro
        {
            get { return _equisFiltro; }
            set { _equisFiltro = value; OnPropertyChanged(nameof(EquisFiltro)); }
        }

        public bool FlechaFiltro
        {
            get { return _flechaFiltro; }
            set { _flechaFiltro = value; OnPropertyChanged(nameof(FlechaFiltro)); }
        }

        public ICommand LimpiarFiltro { get; }

        public ICommand SeleccionarFav { get; }

        public ICommand BuscarContenidos { get; }

        public ICommand OrdenarContenidos { get; }

        public Command<Contenidos> SeleccionarModuloCommand { get; }

        public ContenidosVm(bool categoriaVisible, string tipoContenido, string idSucesoComuinidad, Entry filtroTres, string vista)
        {
            IsBusy = true;
            Vista_ = vista;
            TipoContenido = tipoContenido;
            CategoriaVisible = categoriaVisible;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
            IdSucesoComunidad = Preferences.Get("IdSuceso", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");

            ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();

            LimpiarFiltro = new Command(LimpiarFiltro_Mto);
            SeleccionarFav = new Command(SeleccionarFav_mtd);
            OrdenarContenidos = new Command(OrdenarContenidos_Mto);
            BuscarContenidos = new Command(FiltrosGenerales_MtdAsycn);
            SeleccionarModuloCommand = new Command<Contenidos>(SeleccionarModulo);

            filtroTres.Completed += EntryCompleted;

            Inicializar();
        }

        private async void Inicializar()
        {
            try
            {
                await BuscarContenidos_MtoAsync();
                if (TipoContenido.Equals("favorito"))
                    await FiltroContenidos_MtdAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task BuscarContenidos_MtoAsync()
        {
            try
            {
                ListaContenidos = new List<Contenidos>();
                ListaContenidosTemp = new List<Contenidos>();
                string urli = "";
                if (TipoContenido == "suceso")
                    urli = logicaWS.Movile_Select_ContenidosSuceso_Mtd("1", LenguajeBase, EmailUsuario, "0", IdSucesoComunidad);
                if (TipoContenido == "comunidad")
                    urli = logicaWS.Movile_Select_ContenidosComunidad_Mtd(LenguajeBase, EmailUsuario, "0", IdSucesoComunidad);
                if (TipoContenido == "favorito")
                    urli = logicaWS.Movile_Select_ContenidosFavotito_Mtd(LenguajeBase, "0", "0", EmailUsuario);

                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                if (jsonProcedimiento != "")
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    foreach (JObject item in jsArray)
                    {
                        Contenidos contenidos = new Contenidos(item.GetValue("IdContenido").ToString(), item.GetValue("Titulo").ToString(), claseBase.CapitalizeFirstLetter(item.GetValue("NombreCategoria").ToString()),
                            item.GetValue("url").ToString(), item.GetValue("ImagenPortada").ToString(), item.GetValue("ImagenesCarrusel").ToString(), claseBase.CapitalizeFirstLetter(item.GetValue("Contexto").ToString()),
                            item.GetValue("Contenido").ToString(), item.GetValue("PalabrasClave").ToString(), item.GetValue("Fav").ToString(), IdSucesoComunidad);

                        ListaContenidos.Add(contenidos);
                    }
                }
                ListaContenidosTemp = ListaContenidos.OrderBy(x => x.Contenido).ToList();
                ListaContenidos = ListaContenidos.OrderBy(x => x.Contenido).ToList();
                CantidadContenidos = ListaContenidos.Count.ToString() + " " + "Resultados";
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ContenidosVM", "BuscarContenidos_MtoAsync", "consultacontenidosuceso/consultacontenidocomunidad/consultafavoritoscontenidos");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
        }

        private async Task FiltroContenidos_MtdAsync()
        {
            try
            {
                ListaContenidosFiltro = new ObservableCollection<ListasGeneral>();
                string urli = logicaWS.Movile_Select_Categoria_succeso_Mtd(LenguajeBase);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                if (jsonProcedimiento != "")
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    foreach (JObject item in jsArray)
                    {
                        ListasGeneral listasFiltroComunidad = new ListasGeneral { Descripcion = item.GetValue("Descripcion").ToString(), Id = item.GetValue("IdCategoria").ToString() };
                        ListaContenidosFiltro.Add(listasFiltroComunidad);
                    }
                }

                if (ListaContenidosFiltro.Count() == 0)
                    ListaContenidosFiltro = new ObservableCollection<ListasGeneral>() { new ListasGeneral() { Id = "0", Descripcion = "Ninguna categoria" } };

                FlechaFiltro = true;
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ContenidosVM", "FiltroContenidos_MtdAsync", "consultacategoria");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
        }

        private async void FiltrosGenerales_MtdAsycn()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar

                ListaContenidos = ListaContenidosTemp.ToList();

                if (EquisFiltro)
                    ListaContenidos = ListaContenidos.Where(x => x.NombreCategoria.ToLower().Contains(SelectFiltroComunidad.Descripcion.ToLower().Trim())).ToList();

                if (claseBase.ValidaString(EntContenidos) != "")
                    ListaContenidos = ListaContenidos.Where(
                        x => x.Titulo.ToLower().Contains(EntContenidos.ToLower().Trim()) || x.Contenido.ToLower().Contains(EntContenidos.ToLower().Trim()) || x.Contexto.ToLower().Contains(EntContenidos.ToLower().Trim())).ToList();

                CantidadContenidos = ListaContenidos.Count.ToString() + " " + "Resultados";
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ContenidosVM", "FiltrosGenerales_MtdAsycn", null);
            }
            IsBusy = false;
        }

        private async void LimpiarFiltro_Mto()
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar

            EquisFiltro = false;
            FlechaFiltro = true;
            _ = new ObservableCollection<ListasGeneral>();
            ObservableCollection<ListasGeneral> team = ListaContenidosFiltro;
            ListaContenidosFiltro = new ObservableCollection<ListasGeneral>();
            ListaContenidosFiltro = team;

            FiltrosGenerales_MtdAsycn();
        }

        private void OrdenarContenidos_Mto()
        {
            ListaContenidos.Reverse();
            ListaContenidos = ListaContenidos.ToList();
        }

        private async void SeleccionarFav_mtd(object obj)
        {
            try
            {
                var content = obj as Contenidos;

                foreach (var item in ListaContenidos)
                {
                    if (item.IdContenido == content.IdContenido)
                    {
                        if (item.Fav == "1") /*1 Para favorito*/
                        {
                            item.ImagenFav = "ic_favorito_obscuro";
                            item.Fav = "0";
                            string urli = logicaWS.Movile_delet_Fav_Contenidos_Mtd(EmailUsuario, item.IdContenido);
                            await logicaWS.ConectionGet(urli);
                        }
                        else
                        {
                            item.ImagenFav = "ic_favortio_relleno";
                            item.Fav = "1";
                            string urli = logicaWS.Movile_Update_Fav_Contenidos_Mtd(EmailUsuario, item.IdContenido);
                            await logicaWS.ConectionGet(urli);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ContenidosVM", "SeleccionarFav_mtd", "eliminarfavoritocontenido/agregarfavoritocontenido");
            }
        }

        void EntryCompleted(object sender, EventArgs e)
        {
            FiltrosGenerales_MtdAsycn();
        }

        private async void SeleccionarModulo(Contenidos content)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                if (content != null)
                {
                    await RootNavigation.PushAsync(new DetalleContenidoView(content, null, Vista_), false);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}