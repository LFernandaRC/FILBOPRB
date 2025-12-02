using EventosCorferias.Interfaces;
using EventosCorferias.Models;
using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.Services;
using EventosCorferias.Views.Suceso;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EventosCorferias.ViewModel.Suceso
{
    public class CatalogoProductoVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;

        private List<Productos> _listaProductos;
        private List<Productos> ListaProductosTemp;

        private readonly string? idExpositor;

        private string? urlTienda;
        private string? _entProducto;
        private string? _cantidadProducto;

        private bool _verUrlTienda;

        public ICommand BuscarProducto { get; }

        public ICommand OrdenarProducto { get; }

        public Command TiendaVirtual_Link { get; }

        public List<Productos> ListaProductos
        {
            get { return _listaProductos; }
            set { _listaProductos = value; OnPropertyChanged(nameof(ListaProductos)); }
        }

        public string? EntProducto
        {
            get { return _entProducto; }
            set { _entProducto = value; OnPropertyChanged(nameof(EntProducto)); }
        }

        public bool VerUrlTienda
        {
            get { return _verUrlTienda; }
            set { _verUrlTienda = value; OnPropertyChanged(nameof(VerUrlTienda)); }
        }

        public string? CantidadProducto
        {
            get { return _cantidadProducto; }
            set { _cantidadProducto = value; OnPropertyChanged(nameof(CantidadProducto)); }
        }

        public CatalogoProductoVm(string idExpo, Entry FiltroEntry)
        {
            Title = AppResources.catalogoDeProductos;

            idExpositor = idExpo;

            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();


            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            ImagenSplash = logicaWs.ImgMenuSuperior_Mtd();

            BuscarProducto = new Command(() => FiltradoGeneral());
            OrdenarProducto = new Command(() => OrdenarProducto_MtoA());
          
            //TiendaVirtual_Link = new Command(irTienda_Mtd);

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            FiltroEntry.Completed += (sender, e) => EntryCompleted(sender, e);

            _ = BuscarProducto_MtoAsync();

            string ValidarIphad = DeviceInfo.Name;
            bool res = ValidarIphad.ToLower().Contains("ipad");
            if (res)
                VerChat = false;
            else
                VerChat = true;
        }

        void EntryCompleted(object sender, EventArgs e)
        {
            FiltradoGeneral();
        }

        private async Task BuscarProducto_MtoAsync()
        {
            try
            {
                if (!StateConexion)
                {
                    ListaProductos = new List<Productos>();
                    ListaProductosTemp = new List<Productos>();
                    string urli = logicaWs.Movile_Select_catalogoProductos_Mtd(LenguajeBase, idExpositor, "0");
                    string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    foreach (JObject item in jsArray)
                    {
                        Productos producto = new Productos(
                            claseBase.ValidaString(item.GetValue("IdProducto")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("NombreProducto")?.ToString() ?? string.Empty).ToUpper(),
                            claseBase.CapitalizeFirstLetter(item.GetValue("DescripcionProducto")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("Imagen")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("Estado")?.ToString() ?? string.Empty),
                            claseBase.ValidaString(item.GetValue("Precio")?.ToString() ?? string.Empty),
                            claseBase.CapitalizeFirstLetter(item.GetValue("Categoria")?.ToString() ?? string.Empty));

                        urlTienda = claseBase.ValidaString(item.GetValue("Tienda")?.ToString() ?? string.Empty);

                        if (urlTienda != "")
                            VerUrlTienda = true;

                        ListaProductos.Add(producto);
                    }

                    ListaProductosTemp = ListaProductos.OrderBy(x => x.NombreProducto).ToList();
                    ListaProductos = ListaProductos.OrderBy(x => x.NombreProducto).ToList();
                    CantidadProducto = ListaProductos.Count.ToString() + " " + AppResources.resultados;
                }
                else
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "CatalogoProductoVM", "BuscarProducto_MtoAsync", "consultacatalogoproducto");
            }
        }

        private void FiltradoGeneral()
        {
            ListaProductos = ListaProductosTemp.ToList();

            if (claseBase.ValidaString(EntProducto) != "")
                ListaProductos = ListaProductos.Where(x => x.NombreProducto.ToLower().Contains(EntProducto.ToLower().Trim())).ToList();

            CantidadProducto = ListaProductos.Count.ToString() + " " + AppResources.resultados;
        }

        private void OrdenarProducto_MtoA()
        {
            ListaProductos.Reverse();
            ListaProductos = ListaProductos.ToList();
        }

        private async void irTienda_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await RootNavigation.PushAsync(new ContenidosWebView(AppResources.catalogoDeProductos, UrlWeb: urlTienda));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "CatalogoProductoVm", "irTienda_Mtd", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
