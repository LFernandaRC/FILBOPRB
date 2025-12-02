using EventosCorferias.Interfaces;
using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Views.Suceso;
using Mopups.Services;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace EventosCorferias.ViewModel.PopUp
{
    public class ServiciosPopUpVm : BaseViewModel
    {
        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;

        private ObservableCollection<ServiciosM> _listaSitios;

        private JArray GeneralSitios;

        private readonly string Origen;
        private readonly string idSuceso;

        public Command<ServiciosM> SeleccionarModuloCommand { get; }
        public ObservableCollection<ServiciosM> ListaServicios
        {
            get { return _listaSitios; }
            set { _listaSitios = value; OnPropertyChanged(nameof(ListaServicios)); }
        }

        public ServiciosPopUpVm(string Id, string origen)
        {
            IsBusy = true;

            idSuceso = Id;
            Origen = origen;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();

            pageServicio = new PageServicio();
            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();

            SeleccionarModuloCommand = new Command<ServiciosM>(SeleccionarModulo);

            _ = CargarInfoServicios(Id, origen);
        }

        private async Task CargarInfoServicios(string id, string origen)
        {
            try
            {
                string urli = logicaWS.Movile_Select_consAServiciosEvento_Mtd("1", id, origen, LenguajeBase, "15");
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                JArray GeneralServicios = JArray.Parse(jsonProcedimiento);
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
                        iconoservicio = item.GetValue("iconoservicio").ToString().Trim(),
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
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ServiciosPopUpVm", "CargarInfoServicios", "consultasitiosmapaserviciosb2");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void SeleccionarModulo(ServiciosM lista)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                if (lista != null)
                {
                    await RootNavigation.PushAsync(new DetalleServicioView(lista));
                    await MopupService.Instance.PopAsync();
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ServiciosPopUpVm", "SeleccionarModulo", "redireccionamiento");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}