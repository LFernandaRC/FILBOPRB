using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Boletas;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json;
using System.Windows.Input;
using Newtonsoft.Json.Linq;

namespace EventosCorferias.ViewModel.Boletas
{
    public class ResumenCompraVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;

        private readonly string? TipoPublico;
        private readonly string? IdTransaccion;

        private readonly TerminosCondiciones terminos;

        Pagos pagos;

        private string? _nombreFeria;
        private string? _actividad;
        private string? _categoria;
        private string? _valor;
        private string? _cantidad;
        private string? _codigoPromocional;
        private string? _AplicacionImpuestos;
        private string? _totalEntradas;
        private string? _titularFactura;
        private string? _tipoId;
        private string? _numeroId;
        private string? _correoElectronicoFactura;
        private string? _total;
        private string? _telefonoPersona;

        private string? _nombreEmpresa;
        private string? _telefonoEmpresa;
        private string? _paisEmpresa;
        private string? _ciudadEmpresa;
        private string? _direccion;
        private string? _identificacionEmpresa;

        private bool _verFormEmpresa;
        private bool _checkTerminos;

        public string? NombreEmpresa
        {
            get { return _nombreEmpresa; }
            set { _nombreEmpresa = value; OnPropertyChanged(nameof(NombreEmpresa)); }
        }

        public string? TelefonoEmpresa
        {
            get { return _telefonoEmpresa; }
            set { _telefonoEmpresa = value; OnPropertyChanged(nameof(TelefonoEmpresa)); }
        }

        public string? TelefonoPersona
        {
            get { return _telefonoPersona; }
            set { _telefonoPersona = value; OnPropertyChanged(nameof(TelefonoPersona)); }
        }

        public string? PaisEmpresa
        {
            get { return _paisEmpresa; }
            set { _paisEmpresa = value; OnPropertyChanged(nameof(PaisEmpresa)); }
        }

        public bool VerFormEmpresa
        {
            get { return _verFormEmpresa; }
            set { _verFormEmpresa = value; OnPropertyChanged(nameof(VerFormEmpresa)); }
        }

        public bool CheckTerminos
        {
            get { return _checkTerminos; }
            set { _checkTerminos = value; OnPropertyChanged(nameof(CheckTerminos)); }
        }

        public string? CiudadEmpresa
        {
            get { return _ciudadEmpresa; }
            set { _ciudadEmpresa = value; OnPropertyChanged(nameof(CiudadEmpresa)); }
        }

        public string? Direccion
        {
            get { return _direccion; }
            set { _direccion = value; OnPropertyChanged(nameof(Direccion)); }
        }

        public string? IdentificacionEmpresa
        {
            get { return _identificacionEmpresa; }
            set { _identificacionEmpresa = value; OnPropertyChanged(nameof(IdentificacionEmpresa)); }
        }

        public string? NombreFeria
        {
            get { return _nombreFeria; }
            set { _nombreFeria = value; OnPropertyChanged(nameof(NombreFeria)); }
        }

        public string? Actividad
        {
            get { return _actividad; }
            set { _actividad = value; OnPropertyChanged(nameof(Actividad)); }
        }

        public string? Categoria
        {
            get { return _categoria; }
            set { _categoria = value; OnPropertyChanged(nameof(Categoria)); }
        }

        public string? Valor
        {
            get { return _valor; }
            set { _valor = value; OnPropertyChanged(nameof(Valor)); }
        }

        public string? Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; OnPropertyChanged(nameof(Cantidad)); }
        }

        public string? CodigoPromocional
        {
            get { return _codigoPromocional; }
            set { _codigoPromocional = value; OnPropertyChanged(nameof(CodigoPromocional)); }
        }

        public string? AplicacionImpuestos
        {
            get { return _AplicacionImpuestos; }
            set { _AplicacionImpuestos = value; OnPropertyChanged(nameof(AplicacionImpuestos)); }
        }

        public string? TotalEntradas
        {
            get { return _totalEntradas; }
            set { _totalEntradas = value; OnPropertyChanged(nameof(TotalEntradas)); }
        }

        public string? TitularFactura
        {
            get { return _titularFactura; }
            set { _titularFactura = value; OnPropertyChanged(nameof(TitularFactura)); }
        }

        public string? TipoId
        {
            get { return _tipoId; }
            set { _tipoId = value; OnPropertyChanged(nameof(TipoId)); }
        }

        public string? NumeroId
        {
            get { return _numeroId; }
            set { _numeroId = value; OnPropertyChanged(nameof(NumeroId)); }
        }

        public string? CorreoElectronicoFactura
        {
            get { return _correoElectronicoFactura; }
            set { _correoElectronicoFactura = value; OnPropertyChanged(nameof(CorreoElectronicoFactura)); }
        }

        public string? Total
        {
            get { return _total; }
            set { _total = value; OnPropertyChanged(nameof(Total)); }
        }

        public ICommand BtnPagar { get; }

        public ICommand BtnEliminar { get; }

        public ICommand BtnTerminosYcondiciones { get; }

        public ResumenCompraVm(string idTransaccion, string tipoPublico)
        {
            IsBusy = true;

            TipoPublico = tipoPublico;
            IdTransaccion = idTransaccion;

            pagos = new Pagos();
            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();
            terminos = new TerminosCondiciones();

            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");

            ImagenSplash = logicaWs.ImgMenuSuperior_Mtd();

            BtnPagar = new Command(BtnPagar_Mtd);
            BtnEliminar = new Command(BtnEliminar_Mtd);
            BtnTerminosYcondiciones = new Command(async () => await pageServicio.DisplayAlert(terminos.Modulo, terminos.Texto, AppResources.cerrar));

            _ = CargarDatos();
        }

        private async Task CargarDatos()
        {
            try
            {
                string urli = logicaWs.Movile_Select_ConsultarTransaccion_Mtd("1", EmailUsuario, LenguajeBase, IdTransaccion, Preferences.Get("IdSuceso", ""));
                string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    NombreFeria = item.GetValue("NombreSuceso")?.ToString();
                    Actividad = item.GetValue("NombreActividad")?.ToString();
                    Categoria = item.GetValue("NombreCategoria")?.ToString();
                    Valor = item.GetValue("Valor")?.ToString();
                    Cantidad = item.GetValue("Cantidad")?.ToString();
                    CodigoPromocional = claseBase.ValidaString(item.GetValue("CodigoDescuento").ToString());
                    AplicacionImpuestos = item.GetValue("AplicaImpuesto")?.ToString();
                    TotalEntradas = item.GetValue("Precio")?.ToString();
                    TitularFactura = item.GetValue("TitularFacturacion")?.ToString();
                    NumeroId = item.GetValue("Identificacion")?.ToString();
                    TipoId = item.GetValue("NombreIdentificacion")?.ToString();
                    CorreoElectronicoFactura = item.GetValue("Email")?.ToString();
                    Total = item.GetValue("Precio")?.ToString();
                    TelefonoPersona = item.GetValue("Telefono")?.ToString();
                    CiudadEmpresa = item.GetValue("NombreCiudad")?.ToString();
                    PaisEmpresa = item.GetValue("NombrePais")?.ToString();

                    if (!claseBase.ValidaString(TipoPublico).Equals(""))
                    {
                        if (TipoPublico == "2" || TipoPublico == "3")
                        {
                            if (item.GetValue("Empresa")?.ToString() != null)
                            {
                                if (item.GetValue("Empresa")?.ToString() != "" && item.GetValue("Empresa")?.ToString() != " " && item.GetValue("Empresa")?.ToString() != "0" && item.GetValue("Empresa")?.ToString().ToLower() != "null")
                                    NombreEmpresa = item.GetValue("Empresa")?.ToString();
                            }

                            if (item.GetValue("Nit")?.ToString() != null)
                            {
                                if (item.GetValue("Nit")?.ToString() != "" && item.GetValue("Nit")?.ToString() != " " && item.GetValue("Nit")?.ToString() != "0" && item.GetValue("Nit")?.ToString().ToLower() != "null")
                                    IdentificacionEmpresa = item.GetValue("Nit")?.ToString();
                            }

                            if (item.GetValue("TelefonoEmpresa")?.ToString() != null)
                            {
                                if (item.GetValue("TelefonoEmpresa")?.ToString() != "" && item.GetValue("TelefonoEmpresa")?.ToString() != " " && item.GetValue("TelefonoEmpresa")?.ToString() != "0" && item.GetValue("TelefonoEmpresa")?.ToString().ToLower() != "null")
                                    TelefonoEmpresa = item.GetValue("TelefonoEmpresa")?.ToString();
                            }

                            if (item.GetValue("Direccion")?.ToString() != null)
                            {
                                if (item.GetValue("Direccion")?.ToString() != "" && item.GetValue("Direccion")?.ToString() != " " && item.GetValue("Direccion")?.ToString() != "0" && item.GetValue("Direccion")?.ToString().ToLower() != "null")
                                    Direccion = item.GetValue("Direccion")?.ToString();
                            }

                            VerFormEmpresa = true;
                        }
                    }

                    pagos.ANO = item.GetValue("Vigencia")?.ToString();
                    pagos.CHKTERMINO = item.GetValue("Acepta")?.ToString();
                    pagos.EVENTO = item.GetValue("IdEvento")?.ToString();
                    pagos.RECINTO = item.GetValue("IdRecinto")?.ToString();
                    pagos.SELTIPOIDEN = item.GetValue("NombreIdentificacion")?.ToString();
                    pagos.TXTAPELLIDO = item.GetValue("Apellidos")?.ToString();
                    pagos.TXTCANTIDAD = item.GetValue("Cantidad")?.ToString();
                    pagos.TXTCATEG = item.GetValue("Idcategoria")?.ToString();
                    pagos.TXTDESCRIPCION = item.GetValue("Descripcion")?.ToString();
                    pagos.TXTEMAIL = item.GetValue("Email")?.ToString();
                    pagos.TXTEMAIL_CONFIRMAR = item.GetValue("Email")?.ToString();
                    pagos.TXTEMPCIUDAD = item.GetValue("NombreCiudad")?.ToString();
                    pagos.TXTEMPDEPTO = item.GetValue("Departamento")?.ToString();
                    pagos.TXTEMPDIREC = item.GetValue("Direccion")?.ToString();
                    pagos.TXTEMPNOMBRE = item.GetValue("Empresa")?.ToString();
                    pagos.TXTEMPPAIS = item.GetValue("NombrePais")?.ToString();
                    pagos.TXTEMPTELEF = item.GetValue("TelefonoEmpresa")?.ToString();
                    pagos.TXTIDEEVE = item.GetValue("IdSucesoServicio")?.ToString();
                    pagos.TXTIDENTIFICACION = item.GetValue("Identificacion")?.ToString();
                    pagos.TXTIVA = item.GetValue("DetalleImpuesto")?.ToString();
                    pagos.TXTMODULO = "BOLETERIA";
                    pagos.TXTMONEDA = item.GetValue("Moneda")?.ToString();
                    pagos.TXTMONIVA = item.GetValue("DetalleImpuesto")?.ToString();
                    pagos.TXTMONTO = item.GetValue("ValorCompra")?.ToString();
                    pagos.TXTNOMBRE = item.GetValue("Nombre")?.ToString();
                    pagos.TXTNUMREFERENCIA = item.GetValue("Referencia")?.ToString();
                    pagos.TXTTELEFONO = item.GetValue("Telefono")?.ToString();
                    pagos.TXTTIPFERIA = TipoPublico;
                    pagos.TXTTOTMONT = item.GetValue("ValorCompra")?.ToString();

                }

                await ConsultaPoliticasAsync();
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, ex.Message, AppResources.cerrar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ResumenCompraBoleteriaVM", "CargarDatos", "consultatransaccionboleteriapp");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ConsultaPoliticasAsync()
        {
            try
            {
                string urli = logicaWs.Movile_Select_Terminos_Politicas_Mtd("1", "6", LenguajeBase);
                string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);

                foreach (JObject item in jsArray)
                {
                    terminos.Max = item.GetValue("Max")?.ToString();
                    terminos.Modulo = item.GetValue("Modulo")?.ToString();
                    terminos.Texto = item.GetValue("Texto")?.ToString();
                    terminos.FechaPublica = item.GetValue("FechaPublica")?.ToString();
                    terminos.IdModulo = item.GetValue("IdModulo")?.ToString();
                }
            }
            catch (Exception ex)
            {
                terminos.Max = "0";
                terminos.Modulo = AppResources.terminosYCondiciones;
                terminos.Texto = AppResources.VMEstamosCargandoInfo;
                terminos.FechaPublica = "0";
                terminos.IdModulo = "0";

                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ResumenCompraBoleteriaVM", "ConsultaPoliticasAsync", "consultaterminosaplicacion");
            }
        }

        private async Task CheckTerminosYCondiciones()
        {
            try
            {
                string urliDos = logicaWs.Movile_Insert_habeasData_Mtd("2", EmailUsuario);


                HabeasData habeasData = new HabeasData
                {
                    IpCel = await GetLocalIPAddressAsync(),
                    Acepto = "1",
                    IdModulo = "6",
                    IdTerminoPolitico = terminos.Max,
                    Navegador = "0"
                };

                if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
                {
                    habeasData.Navegador = "iOS";
                }
                else if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                {
                    habeasData.Navegador = "Android";
                }

                string json = JsonConvert.SerializeObject(habeasData);
                string? res = await logicaWs.ConectionPost(json, urliDos);

                if (!res.Equals("Datos Registrados correctamente"))
                    claseBase.InsertarLogs_Mtd("ERROR", res, "ResumenCompraBoleteriaVM", "CheckTerminosYCondiciones", "insertarhabeasdatapp");
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ResumenCompraBoleteriaVM", "CheckTerminosYCondiciones", "insertarhabeasdatapp");
            }
        }

        private async void BtnPagar_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                if (CheckTerminos)
                {
                    await CheckTerminosYCondiciones();
                    RootMainPage.Detail = new NavigationPage(new ContenidosWebPost(pagos));
                }
                else
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElCampoTerminosEsObligatorio, AppResources.VMaceptar);
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ResumenCompraBoleteriaVM", "BtnPagar_Mtd", "redireccionamiento");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void BtnEliminar_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                bool res = await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.estasSeguroDeEliminarLaFactura, AppResources.VMaceptar, AppResources.VMCancelar);
                if (res)
                {
                    string urli = logicaWs.Movile_delete_boleta_Mtd("4", IdTransaccion);
                    string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                    RootMainPage.Detail = new NavigationPage(new BoleteriaView());

                    if (jsonProcedimiento != "OK")
                    {
                        claseBase.InsertarLogs_Mtd("ERROR", jsonProcedimiento, "ResumenCompraBoleteriaVM", "BtnEliminar_Mtd", "actualizaestadoboletapp");
                    }
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ResumenCompraBoleteriaVM", "BtnEliminar_Mtd", "actualizaestadoboletapp");
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
