using EventosCorferias.Interfaces;
using EventosCorferias.Models;
using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.Services;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EventosCorferias.ViewModel.Suceso
{
    public class CuponesVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        private readonly ClaseBase claseBase;
        public readonly IPageServicio pageServicio;

        private List<Cuponera> _listaCupones;
        private List<Cuponera> ListaCuponesTemp;

        private ListasGeneral _SelectFiltroCategoria;

        private ObservableCollection<ListasGeneral> _listaCategoriasFiltro;

        private readonly bool Favorito;
        private readonly string IdCupon;
        private readonly string IdExpositor;

        private string Bandera;   /* 1 favorito -- 7 cuponera*/
        private string _filtroSuceso;
        private string _cantidadCupones;
        private string _filtroExpositor;

        private bool _filtros1;
        private bool _filtros2;
        private bool _equisFiltro1;
        private bool _flechaFiltro1;

        public ICommand BuscarCupon { get; }

        public ICommand FocusFiltro { get; }

        public ICommand AvisoGeneral { get; }

        public ICommand OrdenarCupones { get; }

        public ICommand LimpiarFiltro1 { get; }

        public ICommand PlusButtonCommand { get; }

        public ListasGeneral SelectFiltroCuponera
        {
            get { return _SelectFiltroCategoria; }
            set
            {
                _SelectFiltroCategoria = value;
                if (value != null)
                {
                    FlechaFiltro1 = false;
                    EquisFiltro1 = true;
                    FiltradoGeneral();
                }
                OnPropertyChanged(nameof(SelectFiltroCuponera));
            }
        }

        public List<Cuponera> ListaCupones
        {
            get { return _listaCupones; }
            set { _listaCupones = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ListasGeneral> ListaCategoriasFiltro
        {
            get { return _listaCategoriasFiltro; }
            set { _listaCategoriasFiltro = value; OnPropertyChanged(); }
        }

        public string CantidadCupones
        {
            get { return _cantidadCupones; }
            set { _cantidadCupones = value; OnPropertyChanged(nameof(CantidadCupones)); }
        }

        public string FiltroExpositor
        {
            get { return _filtroExpositor; }
            set { _filtroExpositor = value; OnPropertyChanged(nameof(FiltroExpositor)); }
        }

        public string FiltroSuceso
        {
            get { return _filtroSuceso; }
            set { _filtroSuceso = value; OnPropertyChanged(nameof(FiltroSuceso)); }
        }

        public bool EquisFiltro1
        {
            get { return _equisFiltro1; }
            set { _equisFiltro1 = value; OnPropertyChanged(nameof(EquisFiltro1)); }
        }

        public bool FlechaFiltro1
        {
            get { return _flechaFiltro1; }
            set { _flechaFiltro1 = value; OnPropertyChanged(nameof(FlechaFiltro1)); }
        }

        public bool Filtros1
        {
            get { return _filtros1; }
            set { _filtros1 = value; OnPropertyChanged(nameof(Filtros1)); }
        }

        public bool Filtros2
        {
            get { return _filtros2; }
            set { _filtros2 = value; OnPropertyChanged(nameof(Filtros2)); }
        }

        public CuponesVm(bool favorito, string idExpositor, string idCupon)
        {
            Title = AppResources.cuponera;

            IdCupon = idCupon;
            Favorito = favorito;
            IdExpositor = idExpositor;

            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            ImagenSplash = logicaWs.ImgMenuSuperior_Mtd();

            FocusFiltro = new Command(claseBase.FocusPicker_Mto);
            BuscarCupon = new Command(() => FiltradoGeneral());
            PlusButtonCommand = new Command(SeleccionarFav_MtdAsycn);
            OrdenarCupones = new Command(() => OrdenarComunidad_Mto());
            LimpiarFiltro1 = new Command(() => LimpiarFiltro1_Mto());
            AvisoGeneral = new Command(async () => await AvisoGeneral_MtoAsync());

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;


            Inicializar_MtdAsync();
            ContadorNotificaciones_Mtd();
        }


        private async void Inicializar_MtdAsync()
        {
            if (Favorito)
            {
                Filtros1 = true;
                Bandera = "1";
            }
            else if (IdCupon.Equals("0"))
            {
                Filtros2 = true;
                Bandera = "7";
            }
            else
                Bandera = "7";

            if (!StateConexion)
            {
                if (IdCupon.Equals("0"))
                    await FiltroCategoria_MtoAsync();

                await BuscarCupones_MtoAsync();
            }
            else
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
        }

        private async Task FiltroCategoria_MtoAsync()
        {
            try
            {
                ListaCategoriasFiltro = new ObservableCollection<ListasGeneral>();
                string urli = logicaWs.Movile_select_filtroCategoria_Mtd(LenguajeBase, IdExpositor);
                string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral { Descripcion = item.GetValue("NombreCategoria")?.ToString(), Id = item.GetValue("IdCategoriaP")?.ToString() };
                    ListaCategoriasFiltro.Add(listasGeneral);
                }
                FlechaFiltro1 = true;
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "CuponeraVM", "FiltroCategoria_MtoAsync", "filtrocategoriaproducto");
            }
        }

        private async Task BuscarCupones_MtoAsync()
        {
            try
            {
                ListaCupones = new List<Cuponera>();
                ListaCuponesTemp = new List<Cuponera>();

                string urli = logicaWs.Movile_select_Cupones_Mtd(Bandera, LenguajeBase, EmailUsuario, IdCupon, IdExpositor, "0", "0", "0", "0", "0");
                string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    Cuponera cuponera = new Cuponera(
                        item.GetValue("IdExpositor")?.ToString() ?? string.Empty, item.GetValue("IdCupon")?.ToString() ?? string.Empty,
                        item.GetValue("Imagen")?.ToString() ?? string.Empty,
                        claseBase.CapitalizeFirstLetter(item.GetValue("NombreCategoria")?.ToString() ?? string.Empty),
                        claseBase.CapitalizeFirstLetter(item.GetValue("NombreSubcategoria")?.ToString() ?? string.Empty),
                        item.GetValue("Precio")?.ToString() ?? string.Empty, item.GetValue("Fav")?.ToString() ?? string.Empty,
                        claseBase.CapitalizeFirstLetter(item.GetValue("DescripcionCupon")?.ToString() ?? string.Empty),
                        claseBase.CapitalizeFirstLetter(item.GetValue("ParteLegal")?.ToString() ?? string.Empty),
                        claseBase.CapitalizeFirstLetter(item.GetValue("Vigencia")?.ToString() ?? string.Empty),
                        item.GetValue("NombreCupon")?.ToString().ToUpper() ?? string.Empty,
                        item.GetValue("IdCategoriaP")?.ToString() ?? string.Empty,
                        item.GetValue("Expositor")?.ToString().ToUpper() ?? string.Empty, item.GetValue("Feria")?.ToString().ToUpper() ?? string.Empty);

                    ListaCupones.Add(cuponera);
                }

                ListaCupones = ListaCupones.OrderBy(x => x.NombreCupon).ToList();
                ListaCuponesTemp = ListaCupones.OrderBy(x => x.NombreCupon).ToList();
                CantidadCupones = ListaCupones.Count.ToString() + " " + AppResources.resultados;
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "CuponeraVM", "BuscarCupones_MtoAsync", "consultacuponera");
            }
        }

        private async void FiltradoGeneral()
        {
            IsBusy = true;

            try
            {
                ListaCupones = ListaCuponesTemp.ToList();

                if (EquisFiltro1)
                    ListaCupones = ListaCupones.Where(x => x.IdCategoriaP.Trim() == SelectFiltroCuponera.Id.Trim()).ToList();

                if (claseBase.ValidaString(FiltroSuceso) != "")
                    ListaCupones = ListaCupones.Where(x => x.Feria.ToLower().Contains(FiltroSuceso.ToLower().Trim())).ToList();

                if (claseBase.ValidaString(FiltroExpositor) != "")
                    ListaCupones = ListaCupones.Where(x => x.Expositor.ToLower().Contains(FiltroExpositor.ToLower().Trim())).ToList();

                CantidadCupones = ListaCupones.Count.ToString() + " " + AppResources.resultados;
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "CuponeraVM", "FiltradoGeneral", "n/a");
            }
            IsBusy = false;
        }

        private void LimpiarFiltro1_Mto()
        {
            try
            {
                IsBusy = true;
                EquisFiltro1 = false;
                FlechaFiltro1 = true;
                _ = new ObservableCollection<ListasGeneral>();
                ObservableCollection<ListasGeneral> team = ListaCategoriasFiltro;
                ListaCategoriasFiltro = new ObservableCollection<ListasGeneral>();
                ListaCategoriasFiltro = team;

                FiltradoGeneral();
            }
            catch (Exception ex)
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "CuponeraVM", "LimpiarFiltro1_Mto", "n/a");
            }
        }

        private async void SeleccionarFav_MtdAsycn(object obj)
        {
            try
            {
                if (!StateConexion)
                {
                    var content = obj as Cuponera;

                    foreach (var item in ListaCupones)
                    {
                        if (item.IdCupon == content.IdCupon)
                        {
                            if (item.Fav == "1") /*1 Para favorito*/
                            {
                                item.ImagenFav = "ic_favorito_obscuro.png";
                                item.Fav = "0";
                                string urli = logicaWs.Movile_delet_Fav_Cupones_Mtd(EmailUsuario, item.IdCupon);
                                await logicaWs.ConectionGet(urli);
                            }
                            else
                            {
                                item.ImagenFav = "ic_favortio_relleno.png";
                                item.Fav = "1";
                                string urli = logicaWs.Movile_Update_Fav_Cupones_Mtd(EmailUsuario, item.IdCupon);
                                await logicaWs.ConectionGet(urli);
                            }
                        }
                    }
                }
                else
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "CuponeraVM", "SeleccionarFav_MtdAsycn", "eliminarusuariocuponera/insertarusuariocuponera");
            }
        }

        private void OrdenarComunidad_Mto()
        {
            try
            {
                ListaCupones.Reverse();
                ListaCupones = ListaCupones.ToList();
            }
            catch (Exception ex)
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "CuponeraVM", "OrdenarComunidad_Mto", "n/a");
            }
        }

        private async Task AvisoGeneral_MtoAsync()
        {
            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.DebesTenerEnCuentaQueLosCuponesSonLimitado, AppResources.VMaceptar);
        }
    }

}
