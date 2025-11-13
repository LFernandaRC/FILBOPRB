using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Boletas;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace EventosCorferias.ViewModel.Boletas
{
    public class BoleteriaVentaVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;
        private readonly string? IdSuceso;

        private List<Models.Boletas> _listaBoletas;
        private List<Models.Boletas> listaBoletasTemp;

        private ObservableCollection<ListasGeneral> _listaCategoriasBoleta;

        private ListasGeneral _selectCategoriaBoletas;

        private string? _CantidadBoletas;
        private string? _nombreSuceso;
        private string? _IndexCateforia;

        public List<Models.Boletas> ListaBoletas
        {
            get { return _listaBoletas; }
            set { _listaBoletas = value; OnPropertyChanged(nameof(ListaBoletas)); }
        }

        public ObservableCollection<ListasGeneral> ListaCategoriasBoleta
        {
            get { return _listaCategoriasBoleta; }
            set { _listaCategoriasBoleta = value; OnPropertyChanged(nameof(ListaCategoriasBoleta)); }
        }

        public string? CantidadBoletas
        {
            get { return _CantidadBoletas; }
            set { SetProperty(ref _CantidadBoletas, value); OnPropertyChanged(nameof(CantidadBoletas)); }
        }

        public string? NombreSuceso
        {
            get { return _nombreSuceso; }
            set { SetProperty(ref _nombreSuceso, value); OnPropertyChanged(nameof(NombreSuceso)); }
        }

        public string? IndexCateforia
        {
            get { return _IndexCateforia; }
            set { SetProperty(ref _IndexCateforia, value); OnPropertyChanged(nameof(IndexCateforia)); }
        }

        public ListasGeneral SelectCategoriaBoletas
        {
            get { return _selectCategoriaBoletas; }
            set
            {
                SetProperty(ref _selectCategoriaBoletas, value);
                if (value != null)
                    FiltradoGeneral_MtoAsync();
                OnPropertyChanged(nameof(SelectCategoriaBoletas));
            }
        }

        public ICommand FocusPicker { get; }

        public ICommand BuscarBoletas { get; }

        public ICommand OrdenarBoletas { get; }

        public Command<Models.Boletas> SeleccionarBoletasCommand { get; }


        public BoleteriaVentaVm(string idSuceso)
        {
            IsBusy = true;
            IdSuceso = idSuceso;

            Title = AppResources.Boleteria;

            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");

            ImagenSplash = logicaWs.ImgMenuSuperior_Mtd();

            FocusPicker = new Command(claseBase.FocusPicker_Mto);
            BuscarBoletas = new Command(FiltradoGeneral_MtoAsync);
            OrdenarBoletas = new Command(OrdenarExpositor_Mto);
            SeleccionarBoletasCommand = new Command<Models.Boletas>(SeleccionarBoletas);

            Inicializar();
        }

        private async void Inicializar()
        {
            try
            {
                await CargarBoletas_MtoAsync();
                ListasFiltros_MtoAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CargarBoletas_MtoAsync()
        {
            try
            {
                string urli = logicaWs.Movile_Select_Boletas_Mtd("1", IdSuceso, "0", "0", EmailUsuario, LenguajeBase, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                ListaBoletas = new List<Models.Boletas>();
                listaBoletasTemp = new List<Models.Boletas>();
                foreach (JObject item in jsArray)
                {
                    Title = AppResources.Boleteria + " - " + item.GetValue("NombreSuceso")?.ToString();

                    Models.Boletas boletas = new Models.Boletas
                    {
                        IdCategoria = item.GetValue("IdCategoria")?.ToString(),
                        IdActividad = item.GetValue("IdActividad")?.ToString(),
                        NombreActividad = item.GetValue("NombreActividad")?.ToString(),
                        NombreCategoria = item.GetValue("NombreCategoria")?.ToString(),
                        DetalleCategoria = claseBase.CapitalizeFirstLetter(item.GetValue("DetalleCategoria").ToString()),
                        Iva = item.GetValue("Iva")?.ToString(),
                        Item = item.GetValue("Item")?.ToString(),
                        IdSuceso = item.GetValue("IdSuceso")?.ToString(),
                        NombreSuceso = item.GetValue("NombreSuceso")?.ToString(),
                        Limite = item.GetValue("Limite")?.ToString(),
                        FechaInicio = item.GetValue("FechaInicio")?.ToString(),
                        FechaFin = item.GetValue("FechaFin")?.ToString(),
                        HoraInicio = item.GetValue("HoraInicio")?.ToString(),
                        HoraFin = item.GetValue("HoraFin")?.ToString(),
                        IdBoleteria = item.GetValue("IdBoleteria")?.ToString(),
                        DiaInicio = item.GetValue("DiaInicio")?.ToString(),
                        MesInici = item.GetValue("MesInici")?.ToString(),
                        DiaFin = item.GetValue("DiaFin")?.ToString(),
                        MesFin = item.GetValue("MesFin")?.ToString(),
                        imagen = item.GetValue("imagen")?.ToString(),
                        valorSinFormato = item.GetValue("Valor")?.ToString(),
                        TipoPublico = item.GetValue("TipoPublico")?.ToString(),
                        IdSucesoServicio = item.GetValue("IdSucesoServicio")?.ToString(),
                    };
                    try
                    {
                        boletas.Valor = claseBase.FormatoPrecio_Mto(long.Parse(boletas.valorSinFormato));
                    }
                    catch (Exception ex)
                    {
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BoleteriaVentaVM", "CargarBoletas_MtoAsync", "consultaboleteriacategoriaapp");
                    }
                    ListaBoletas.Add(boletas);
                    listaBoletasTemp.Add(boletas);
                    NombreSuceso = boletas.NombreSuceso;
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BoleteriaVentaVM", "CargarBoletas_MtoAsync", "consultaboleteriacategoriaapp");
            }
        }

        private async void ListasFiltros_MtoAsync()
        {
            try
            {
                ListaCategoriasBoleta = new ObservableCollection<ListasGeneral>();
                string urli = logicaWs.Movile_Select_CategoriaBoletas_Mtd(IdSuceso, LenguajeBase);
                string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral { Descripcion = item.GetValue("NombreActividad")?.ToString(), Id = item.GetValue("IdActividad")?.ToString() };
                    ListaCategoriasBoleta.Add(listasGeneral);
                }

                if (ListaCategoriasBoleta.Count > 0)
                    IndexCateforia = "0";

            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BoleteriaVentaVM", "ListasFiltros_MtoAsync", "consultalistaactividadesbolapp");
            }
        }

        private async void FiltradoGeneral_MtoAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                ListaBoletas = listaBoletasTemp.ToList();
                ListaBoletas = ListaBoletas.Where(x => x.IdActividad.ToLower().Contains(SelectCategoriaBoletas.Id.ToLower().Trim())).ToList();
                CantidadBoletas = ListaBoletas.Count.ToString() + " " + AppResources.resultados;
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BoleteriaVentaVM", "FiltradoGeneral_MtoAsync", "Error Metodo Filtrado General");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OrdenarExpositor_Mto()
        {
            ListaBoletas.Reverse();
            ListaBoletas = ListaBoletas.ToList();
        }

        private async void SeleccionarBoletas(Models.Boletas boletas)
        {
            if (boletas != null)
            {
                try
                {
                    IsBusy = true;
                    await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                    string urli = logicaWs.Movile_validar_boleta_Mtd(boletas.IdCategoria, boletas.IdSucesoServicio);
                    string jsonProcedimiento = await logicaWs.ConectionPost("", urli);
                    //JArray jsArray = JArray.Parse("[{\"IdCategoria\":\"3158\",\"CantidadBoletas\":\"100\",\"Habilitada\":\"1\"}]");
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    bool auxBoleta = false;
                    foreach (JObject item in jsArray)
                    {
                        if (item.GetValue("Habilitada").ToString().Equals("1"))
                            if (int.Parse(item.GetValue("CantidadBoletas").ToString()) > 0)
                            {
                                boletas.cantidadMaxDos = item.GetValue("CantidadBoletas")?.ToString();
                                auxBoleta = true;
                            }
                        break;
                    }

                    if (auxBoleta)
                        await RootNavigation.PushAsync(new SeleccionBoletasView(boletas));
                    else
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.noBoleteriaEnElMomento, AppResources.VMaceptar);
                }
                catch (Exception ex)
                {
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BoleteriaVentaVM", "BoletaSeleccionada", "validaboleteria");
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

    }
}
