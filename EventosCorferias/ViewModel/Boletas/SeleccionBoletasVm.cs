using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Boletas;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace EventosCorferias.ViewModel.Boletas
{
    public class SeleccionBoletasVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        public readonly ClaseBase claseBase;
        public readonly IPageServicio pageServicio;
        public readonly Models.Boletas boletas1;

        private ObservableCollection<ListasGeneral> _listaTipoID;
        private ObservableCollection<ListasGeneral> _listaPaisEmpresa;
        private ObservableCollection<ListasGeneral> _listaCiudadEmpresa;

        private string? _entValor;
        private string? _soruceImagen;
        private string? _entIva;
        private string? _entNombre;
        private string? _entNumeroIdentificacion;
        private string? _entCelular;
        private string? _entCorreoElectronico;
        private string? _entCorreoElectronicoDos;
        private string? _entCodigoPromocional;
        private string? _indexIdentificacion;

        private string? _entNombreEmpresa;
        private string? _entIdentificacionempresa;
        private string? _entTelefonoEmpresa;
        private string? _entDireccionEmpr;
        private string? _indexIdPaisEmpr;
        private string? _IndexIdCiudadEmpre;
        private string? _AuxIdCiudadEmpre;
        private string? _AuxIdPaisEmpr;

        private string? EnTipoIdUsuS;

        private string? _nombreSuceso;
        private string? _actividad;
        private string? _diaini;
        private string? _mesini;
        private string? _diafin;
        private string? _mesfin;
        private string? _descripcion;

        private int descuentoCodigo;
        private string? paisEmpresa = "0";
        private string? ciudadEmpresa = "0";

        private int MaxCantidad;
        private string? _cantidad;

        private string? _valorTotalSinIva;

        private bool _verInfoEmpresa;
        private bool _obligatorioEmpresaSi;
        private bool _obligatorioEmpresaNo;

        private ListasGeneral _selectPaisEmpresa;
        private ListasGeneral _selectCiudadEmpresa;
        private ListasGeneral _selectIdentificacion;

        public ObservableCollection<ListasGeneral> ListaTipoID
        {
            get { return _listaTipoID; }
            set { _listaTipoID = value; OnPropertyChanged(nameof(ListaTipoID)); }
        }

        public ObservableCollection<ListasGeneral> ListaPaisEmpresa
        {
            get { return _listaPaisEmpresa; }
            set { _listaPaisEmpresa = value; OnPropertyChanged(nameof(ListaPaisEmpresa)); }
        }

        public ObservableCollection<ListasGeneral> ListaCiudadEmpresa
        {
            get { return _listaCiudadEmpresa; }
            set { _listaCiudadEmpresa = value; OnPropertyChanged(nameof(ListaCiudadEmpresa)); }
        }

        public ListasGeneral SelectIdentificacion
        {
            get { return _selectIdentificacion; }
            set { SetProperty(ref _selectIdentificacion, value); }
        }

        public ListasGeneral SelectPaisEmpresa
        {
            get { return _selectPaisEmpresa; }
            set { SetProperty(ref _selectPaisEmpresa, value); if (value != null) { CargarListaciudadByPais(value.Id); paisEmpresa = value.Id; } }
        }

        public ListasGeneral SelectCiudadEmpresa
        {
            get { return _selectCiudadEmpresa; }
            set { SetProperty(ref _selectCiudadEmpresa, value); if (value != null) ciudadEmpresa = value.Id; }
        }

        public string? SoruceImagen
        {
            get { return _soruceImagen; }
            set { _soruceImagen = value; OnPropertyChanged(nameof(SoruceImagen)); }
        }

        public string? Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; OnPropertyChanged(nameof(Descripcion)); }
        }

        public string? ValorTotalSinIva
        {
            get { return _valorTotalSinIva; }
            set { _valorTotalSinIva = value; OnPropertyChanged(nameof(ValorTotalSinIva)); }
        }

        public string? EntDireccionEmpr
        {
            get { return _entDireccionEmpr; }
            set { _entDireccionEmpr = value; OnPropertyChanged(nameof(EntDireccionEmpr)); }
        }

        public string? EntValor
        {
            get { return _entValor; }
            set { _entValor = value; OnPropertyChanged(nameof(EntValor)); }
        }

        public string? EntIva
        {
            get { return _entIva; }
            set { _entIva = value; OnPropertyChanged(nameof(EntIva)); }
        }

        public string? NombreSuceso
        {
            get { return _nombreSuceso; }
            set { _nombreSuceso = value; OnPropertyChanged(nameof(NombreSuceso)); }
        }

        public string? Actividad
        {
            get { return _actividad; }
            set { _actividad = value; OnPropertyChanged(nameof(Actividad)); }
        }

        public string? Diaini
        {
            get { return _diaini; }
            set { _diaini = value; OnPropertyChanged(nameof(Diaini)); }
        }

        public string? Mesini
        {
            get { return _mesini; }
            set { _mesini = value; OnPropertyChanged(nameof(Mesini)); }
        }

        public string? Diafin
        {
            get { return _diafin; }
            set { _diafin = value; OnPropertyChanged(nameof(Diafin)); }
        }

        public string? Mesfin
        {
            get { return _mesfin; }
            set { _mesfin = value; OnPropertyChanged(nameof(Mesfin)); }
        }

        public string? Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; OnPropertyChanged(nameof(Cantidad)); ValidarEntrada(); }
        }

        public string? IndexIdentificacion
        {
            get { return _indexIdentificacion; }
            set
            { _indexIdentificacion = value; OnPropertyChanged(nameof(IndexIdentificacion)); }
        }

        public string? EntNombre
        {
            get { return _entNombre; }
            set { _entNombre = value; OnPropertyChanged(nameof(EntNombre)); }
        }

        public string? EntNumeroIdentificacion
        {
            get { return _entNumeroIdentificacion; }
            set { _entNumeroIdentificacion = value; OnPropertyChanged(nameof(EntNumeroIdentificacion)); }
        }

        public string? EntCelular
        {
            get { return _entCelular; }
            set { _entCelular = value; OnPropertyChanged(nameof(EntCelular)); }
        }

        public string? EntCorreoElectronico
        {
            get { return _entCorreoElectronico; }
            set { _entCorreoElectronico = value; OnPropertyChanged(nameof(EntCorreoElectronico)); }
        }

        public string? EntCorreoElectronicoDos
        {
            get { return _entCorreoElectronicoDos; }
            set { _entCorreoElectronicoDos = value; OnPropertyChanged(nameof(EntCorreoElectronicoDos)); }
        }

        public string? EntCodigoPromocional
        {
            get { return _entCodigoPromocional; }
            set { _entCodigoPromocional = value; OnPropertyChanged(nameof(EntCodigoPromocional)); }
        }

        public string? EntNombreEmpresa
        {
            get { return _entNombreEmpresa; }
            set { _entNombreEmpresa = value; OnPropertyChanged(nameof(EntNombreEmpresa)); }
        }

        public string? EntIdentificacionempresa
        {
            get { return _entIdentificacionempresa; }
            set { _entIdentificacionempresa = value; OnPropertyChanged(nameof(EntIdentificacionempresa)); }
        }

        public string? EntTelefonoEmpresa
        {
            get { return _entTelefonoEmpresa; }
            set { _entTelefonoEmpresa = value; OnPropertyChanged(nameof(EntTelefonoEmpresa)); }
        }

        public string? IndexIdPaisEmpr
        {
            get { return _indexIdPaisEmpr; }
            set { _indexIdPaisEmpr = value; OnPropertyChanged(nameof(IndexIdPaisEmpr)); }
        }

        public string? IndexIdCiudadEmpre
        {
            get { return _IndexIdCiudadEmpre; }
            set { _IndexIdCiudadEmpre = value; OnPropertyChanged(nameof(IndexIdCiudadEmpre)); }
        }

        public bool VerInfoEmpresa
        {
            get { return _verInfoEmpresa; }
            set { _verInfoEmpresa = value; OnPropertyChanged(nameof(VerInfoEmpresa)); }
        }

        public bool ObligatorioEmpresaSi
        {
            get { return _obligatorioEmpresaSi; }
            set { _obligatorioEmpresaSi = value; OnPropertyChanged(nameof(ObligatorioEmpresaSi)); }
        }

        public bool ObligatorioEmpresaNo
        {
            get { return _obligatorioEmpresaNo; }
            set { _obligatorioEmpresaNo = value; OnPropertyChanged(nameof(ObligatorioEmpresaNo)); }
        }


        public ICommand BtnMas { get; }
        public ICommand BtnMenos { get; }
        public ICommand BtnContinuar { get; }
        public ICommand Notificacion_correo { get; }

        public SeleccionBoletasVm(Models.Boletas boletas)
        {

            IsBusy = true;
            boletas1 = boletas;
            Title = AppResources.datosFacturacion + " - " + boletas1.NombreSuceso;

            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");

            ImagenSplash = logicaWs.ImgMenuSuperior_Mtd();

            BtnMas = new Command(BtnMas_Mto);
            BtnMenos = new Command(NBtnMenos_Mto);
            BtnContinuar = new Command(async () => await BtnContinuar_Mto());
            Notificacion_correo = new Command(async () => await Notificacion_correo_Mto());

            _ = Inicializar();
        }

        private async Task Inicializar()
        {
            try
            {
                await CargarInfoUsuario();
                await CargarInfoTarjeta();
                await CargarListasUsuario();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CargarInfoUsuario()
        {
            try
            {
                /*Cargar informacion del usuario */
                string urli = logicaWs.Movile_select_Perfil_Mtd(EmailUsuario);
                string json = JsonConvert.SerializeObject(" ");
                string jsonProcedimiento = await logicaWs.ConectionPost(json, urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {

                    if (item.GetValue("nombrecompleto")?.ToString() != null)
                    {
                        if (item.GetValue("nombrecompleto")?.ToString() != "" && item.GetValue("nombrecompleto")?.ToString() != " " && item.GetValue("nombrecompleto")?.ToString() != "0" && item.GetValue("nombrecompleto")?.ToString().ToLower() != "null")
                            EntNombre = item.GetValue("nombrecompleto")?.ToString();
                    }


                    if (item.GetValue("idtipoidentificacion")?.ToString() != null)
                    {
                        if (item.GetValue("idtipoidentificacion")?.ToString() != "" && item.GetValue("idtipoidentificacion")?.ToString() != " " && item.GetValue("idtipoidentificacion")?.ToString() != "0" && item.GetValue("idtipoidentificacion")?.ToString().ToLower() != "null")
                            EnTipoIdUsuS = item.GetValue("idtipoidentificacion")?.ToString();
                    }

                    if (item.GetValue("Nidentificacion")?.ToString() != null)
                    {
                        if (item.GetValue("Nidentificacion")?.ToString() != "" && item.GetValue("Nidentificacion")?.ToString() != " " && item.GetValue("Nidentificacion")?.ToString() != "0" && item.GetValue("Nidentificacion")?.ToString().ToLower() != "null")
                            EntNumeroIdentificacion = item.GetValue("Nidentificacion")?.ToString();
                    }

                    if (item.GetValue("Email")?.ToString() != null)
                    {
                        if (item.GetValue("Email")?.ToString() != "" && item.GetValue("Email")?.ToString() != " " && item.GetValue("Email")?.ToString() != "0" && item.GetValue("Email")?.ToString().ToLower() != "null")
                            EntCorreoElectronico = item.GetValue("Email")?.ToString();
                    }

                    if (item.GetValue("celular")?.ToString() != null)
                    {
                        if (item.GetValue("celular")?.ToString() != "" && item.GetValue("celular")?.ToString() != " " && item.GetValue("celular")?.ToString() != "0" && item.GetValue("celular")?.ToString().ToLower() != "null")
                            EntCelular = item.GetValue("celular")?.ToString();
                    }

                    if (item.GetValue("nombreempresa")?.ToString() != null)
                    {
                        if (item.GetValue("nombreempresa")?.ToString() != "" && item.GetValue("nombreempresa")?.ToString() != " " && item.GetValue("nombreempresa")?.ToString() != "0" && item.GetValue("nombreempresa")?.ToString().ToLower() != "null")
                            EntNombreEmpresa = item.GetValue("nombreempresa")?.ToString();
                    }

                    if (item.GetValue("identificacionempresa")?.ToString() != null)
                    {
                        if (item.GetValue("identificacionempresa")?.ToString() != "" && item.GetValue("identificacionempresa")?.ToString() != " " && item.GetValue("identificacionempresa")?.ToString() != "0" && item.GetValue("identificacionempresa")?.ToString().ToLower() != "null")
                            EntIdentificacionempresa = item.GetValue("identificacionempresa")?.ToString();
                    }

                    if (item.GetValue("telefonoEmpresa")?.ToString() != null)
                    {
                        if (item.GetValue("telefonoEmpresa")?.ToString() != "" && item.GetValue("telefonoEmpresa")?.ToString() != " " && item.GetValue("telefonoEmpresa")?.ToString() != "0" && item.GetValue("telefonoEmpresa")?.ToString().ToLower() != "null")
                            EntTelefonoEmpresa = item.GetValue("telefonoEmpresa")?.ToString();
                    }

                    if (item.GetValue("PaisER")?.ToString() != null)
                    {
                        if (item.GetValue("PaisER")?.ToString() != "" && item.GetValue("PaisER")?.ToString() != " " && item.GetValue("PaisER")?.ToString() != "0" && item.GetValue("PaisER")?.ToString().ToLower() != "null")
                            _AuxIdPaisEmpr = item.GetValue("PaisER")?.ToString();
                    }

                    if (item.GetValue("CiudadER")?.ToString() != null)
                    {
                        if (item.GetValue("CiudadER")?.ToString() != "" && item.GetValue("CiudadER")?.ToString() != " " && item.GetValue("CiudadER")?.ToString() != "0" && item.GetValue("CiudadER")?.ToString().ToLower() != "null")
                            _AuxIdCiudadEmpre = item.GetValue("CiudadER")?.ToString();
                    }
                }

                if (!claseBase.ValidaString(boletas1.TipoPublico).Equals(""))
                {
                    if (boletas1.TipoPublico.Equals("1"))
                    {
                        VerInfoEmpresa = false;
                        ObligatorioEmpresaSi = false;
                        ObligatorioEmpresaNo = true;
                    }

                    if (boletas1.TipoPublico.Equals("2"))
                    {
                        VerInfoEmpresa = true;
                        ObligatorioEmpresaSi = true;
                        ObligatorioEmpresaNo = false;
                    }

                    if (boletas1.TipoPublico.Equals("3"))
                    {
                        VerInfoEmpresa = true;
                        ObligatorioEmpresaSi = false;
                        ObligatorioEmpresaNo = true;
                    }
                }
                else
                {
                    VerInfoEmpresa = false;
                    ObligatorioEmpresaSi = false;
                    ObligatorioEmpresaNo = true;
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SeccionBoletasVM", "CargarInfoUsuario", "consultausuario");
            }
        }

        private async Task CargarInfoTarjeta()
        {
            try
            {
                NombreSuceso = boletas1.NombreSuceso;
                Actividad = boletas1.NombreActividad;
                Diaini = boletas1.DiaInicio;
                Mesini = boletas1.MesInici;
                Diafin = boletas1.DiaFin;
                Mesfin = boletas1.MesFin;
                Descripcion = boletas1.DetalleCategoria;
                Cantidad = "1";
                EntValor = boletas1.Valor;
                SoruceImagen = boletas1.imagen;

                try
                {
                    EntIva = claseBase.FormatoPrecio_Mto(long.Parse(boletas1.Iva));
                }
                catch
                {
                    EntIva = boletas1.Iva;
                }

                try
                {
                    int auxUno = int.Parse(boletas1.Limite);
                    int ausDos = int.Parse(boletas1.cantidadMaxDos);

                    if (auxUno <= ausDos)
                        MaxCantidad = auxUno;
                    else
                        MaxCantidad = ausDos;
                }
                catch (Exception ex)
                {
                    MaxCantidad = 0;
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SeccionBoletasVM", "CargarInfoTarjeta", "n/a");
                }
            }
            catch
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
        }

        private async Task CargarListasUsuario()
        {
            try
            {
                /*Tipo identificacion*/
                ListaTipoID = new ObservableCollection<ListasGeneral>();
                string urli = logicaWs.Movile_select_TipoId_Mtd();
                string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                int auxCont = 0;
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral { Descripcion = item.GetValue("NombreIdentificacion")?.ToString(), Id = item.GetValue("idIdentificacion")?.ToString() };
                    ListaTipoID.Add(listasGeneral);
                }
                ListaTipoID = new ObservableCollection<ListasGeneral>(ListaTipoID.OrderBy(x => x.Descripcion).ToList());
                foreach (ListasGeneral item in ListaTipoID)
                {
                    if (EnTipoIdUsuS == item.Id)
                    {
                        IndexIdentificacion = auxCont.ToString();
                        break;
                    }
                    auxCont += 1;
                }
                if (!boletas1.TipoPublico.Equals("1"))
                {
                    /*pais empresa*/
                    ListaPaisEmpresa = new ObservableCollection<ListasGeneral>();
                    urli = logicaWs.Movile_select_Pais_Mtd();
                    jsonProcedimiento = await logicaWs.ConectionGet(urli);
                    jsArray = JArray.Parse(jsonProcedimiento);
                    auxCont = 0;
                    foreach (JObject item in jsArray)
                    {
                        ListasGeneral listasGeneral = new ListasGeneral { Descripcion = item.GetValue("Nombre_Pais")?.ToString(), Id = item.GetValue("idPais")?.ToString() };
                        ListaPaisEmpresa.Add(listasGeneral);
                    }
                    ListaPaisEmpresa = new ObservableCollection<ListasGeneral>(ListaPaisEmpresa.OrderBy(x => x.Descripcion).ToList());
                    foreach (ListasGeneral item in ListaPaisEmpresa)
                    {
                        if (!claseBase.ValidaString(_AuxIdPaisEmpr).Equals(""))
                        {
                            if (_AuxIdPaisEmpr == item.Id)
                            {
                                IndexIdPaisEmpr = auxCont.ToString();
                                break;
                            }
                            auxCont += 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                    try
                    {
                        /*Ciudad Empresa*/
                        ListaCiudadEmpresa = new ObservableCollection<ListasGeneral>();
                        urli = logicaWs.Movile_select_CiudadByPais_Mtd(_AuxIdPaisEmpr);
                        jsonProcedimiento = await logicaWs.ConectionGet(urli);
                        jsArray = JArray.Parse(jsonProcedimiento);
                        auxCont = 0;
                        foreach (JObject item in jsArray)
                        {
                            ListasGeneral listasGeneral = new ListasGeneral { Id = item.GetValue("IdCiudad")?.ToString(), Descripcion = item.GetValue("NombreCiudad")?.ToString() };
                            ListaCiudadEmpresa.Add(listasGeneral);
                        }
                        ListaCiudadEmpresa = new ObservableCollection<ListasGeneral>(ListaCiudadEmpresa.OrderBy(x => x.Descripcion).ToList());
                        foreach (ListasGeneral item in ListaCiudadEmpresa)
                        {
                            if (!claseBase.ValidaString(_AuxIdCiudadEmpre).Equals(""))
                            {
                                if (_AuxIdCiudadEmpre == item.Id)
                                {
                                    IndexIdCiudadEmpre = auxCont.ToString();
                                    break;
                                }
                                auxCont += 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SeccionBoletasVM", "CargarListasUsuario", "consultatipoid");
            }
            IsBusy = false;
        }

        private async Task<bool> ValidarFormulario()
        {
            try
            {
                /*Validar cantidad de boletas disponibles*/
                string urli = logicaWs.Movile_validar_boleta_Mtd(boletas1.IdCategoria, boletas1.IdSucesoServicio);
                string jsonProcedimiento = await logicaWs.ConectionPost("", urli);
                //JArray jsArray = JArray.Parse("[{\"IdCategoria\":\"1\",\"CantidadBoletas\":\"100\",\"Habilitada\":\"1\"}]");
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    if (item.GetValue("Habilitada").ToString().Equals("1"))
                    {
                        if (int.Parse(item.GetValue("CantidadBoletas").ToString()) > 0)
                        {
                            if (int.Parse(item.GetValue("CantidadBoletas").ToString()) <= int.Parse(Cantidad))
                            {
                                bool res = await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.contamosCon + item.GetValue("CantidadBoletas")?.ToString() + AppResources.boletasDeseasRealizarLaCompra, AppResources.VMaceptar, AppResources.VMCancelar);
                                if (res)
                                    Cantidad = item.GetValue("CantidadBoletas")?.ToString();
                                else
                                    return false;
                            }
                        }
                        else
                        {
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.noBoleteriaEnElMomento, AppResources.VMaceptar);
                            return false;
                        }
                    }
                    else
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.noBoleteriaEnElMomento, AppResources.VMaceptar);
                        return false;
                    }
                    break;
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BoleteriaVentaVM", "BoletaSeleccionada", "validaboleteria");
                return false;
            }


            /*Validar si el codigo promocional es valido*/
            if (!string.IsNullOrWhiteSpace(EntCodigoPromocional))
            {
                try
                {
                    string urli = logicaWs.Movile_validar_CodigoPromocional_Mtd(boletas1.IdSuceso, EntCodigoPromocional, boletas1.IdCategoria);
                    string? jsonProcedimiento = await logicaWs.ConectionGet(urli);

                    if (jsonProcedimiento.Equals("Código inválido. Verifique la información."))
                    {
                        descuentoCodigo = 0;
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.codigoPromocionalInvalido, AppResources.VMaceptar);
                        return false;
                    }
                    else
                    {
                        string prueba;
                        int aux = jsonProcedimiento.Length;
                        prueba = jsonProcedimiento.Substring(1, aux - 2);
                        descuentoCodigo = int.Parse(prueba);
                    }
                }
                catch (Exception ex)
                {
                    descuentoCodigo = 0;
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.codigoPromocionalInvalido, AppResources.VMaceptar);
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SeccionBoletasVM", "ValidarFormulario", "consultarcodigopromocionapp");
                    return false;
                }
            }

            char[] charsToTrim = { ' ' };
            string expreregularcorreos = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            if (VerInfoEmpresa && ObligatorioEmpresaSi)
            {
                if (!string.IsNullOrWhiteSpace(EntNombre) && SelectIdentificacion != null && !string.IsNullOrWhiteSpace(EntNumeroIdentificacion)
                     && !string.IsNullOrWhiteSpace(EntCorreoElectronico) && !string.IsNullOrWhiteSpace(EntCorreoElectronicoDos) && !string.IsNullOrWhiteSpace(EntCelular)
                     && !string.IsNullOrWhiteSpace(EntNombreEmpresa) && !string.IsNullOrWhiteSpace(EntNumeroIdentificacion) && !string.IsNullOrWhiteSpace(EntTelefonoEmpresa)
                     && !string.IsNullOrWhiteSpace(EntDireccionEmpr))
                {

                    if (SelectPaisEmpresa == null || SelectCiudadEmpresa == null)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVacios, AppResources.cerrar);
                        return false;
                    }

                    if (EntNombre.Length < 3)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElNombreDebeTenerMinimo3Caracteres, AppResources.cerrar);
                        return false;
                    }

                    if (EntCorreoElectronico.Trim() != EntCorreoElectronicoDos.Trim())
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMLosCorreosNoCoinciden, AppResources.cerrar);
                        return false;
                    }

                    bool resultadoCorreo = Regex.IsMatch(EntCorreoElectronico.Trim(charsToTrim), expreregularcorreos, RegexOptions.IgnoreCase);
                    if (!resultadoCorreo)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoTieneFormatoCorreo, AppResources.cerrar);
                        return false;
                    }

                    return true;
                }
                else
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVacios, AppResources.cerrar);
                    return false;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(EntNombre) && SelectIdentificacion != null && !string.IsNullOrWhiteSpace(EntNumeroIdentificacion)
                   && !string.IsNullOrWhiteSpace(EntCorreoElectronico) && !string.IsNullOrWhiteSpace(EntCorreoElectronicoDos) && !string.IsNullOrWhiteSpace(EntCelular))
                {
                    if (EntNombre.Length < 3)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElNombreDebeTenerMinimo3Caracteres, AppResources.cerrar);
                        return false;
                    }

                    if (EntCorreoElectronico.Trim().ToLower() != EntCorreoElectronicoDos.Trim().ToLower())
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMLosCorreosNoCoinciden, AppResources.cerrar);
                        return false;
                    }

                    bool resultadoCorreo = Regex.IsMatch(EntCorreoElectronico.Trim(charsToTrim), expreregularcorreos, RegexOptions.IgnoreCase);
                    if (!resultadoCorreo)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoTieneFormatoCorreo, AppResources.cerrar);
                        return false;
                    }

                    return true;
                }
                else
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVacios, AppResources.cerrar);
                    return false;
                }
            }
        }

        private async Task BtnContinuar_Mto()
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            if (await ValidarFormulario())
            {
                try
                {
                    int aux = int.Parse(Cantidad);
                    if (aux > 0 && aux <= MaxCantidad)
                    {
                        if (string.IsNullOrWhiteSpace(EntCodigoPromocional))
                            EntCodigoPromocional = "0";

                        InsertBoletas insertBoletas;

                        if (ObligatorioEmpresaSi)
                        {
                            insertBoletas = new InsertBoletas
                            {
                                IdSuceso = boletas1.IdSuceso,
                                Cantidad = Cantidad,
                                TitularFacturacion = EntNombre,
                                Telefono = EntCelular,
                                IdIdentificacion = SelectIdentificacion.Id,
                                Identificacion = EntNumeroIdentificacion,
                                IdBoleteria = boletas1.IdBoleteria,
                                CodigoDescuento = EntCodigoPromocional,
                                Vigencia = Diaini + " de " + Mesini + " al " + Diafin + " de " + Mesfin,
                                Email = EntCorreoElectronico,
                                Precio = boletas1.valorSinFormato,
                                AplicaImpuesto = "0",
                                DetalleImpuesto = boletas1.Iva,
                                IpApp = await GetLocalIPAddressAsync(),
                                Navegador = "No se encontro",
                                Descuento = descuentoCodigo.ToString(),
                                Empresa = EntNombreEmpresa,
                                Nit = EntIdentificacionempresa,
                                TelefonoEmpresa = EntTelefonoEmpresa,
                                Pais = paisEmpresa,
                                Ciudad = ciudadEmpresa,
                                Direccion = EntDireccionEmpr,
                                NombreArchivo = "",
                                DocumentoArchivo = "",
                                TipoArchivo = "",
                                Tamano = ""
                            };

                        }
                        else if (VerInfoEmpresa && ObligatorioEmpresaNo)
                        {
                            insertBoletas = new InsertBoletas
                            {
                                IdSuceso = boletas1.IdSuceso,
                                Cantidad = Cantidad,
                                TitularFacturacion = EntNombre,
                                Telefono = EntCelular,
                                IdIdentificacion = SelectIdentificacion.Id,
                                Identificacion = EntNumeroIdentificacion,
                                IdBoleteria = boletas1.IdBoleteria,
                                CodigoDescuento = EntCodigoPromocional,
                                Vigencia = Diaini + " de " + Mesini + " al " + Diafin + " de " + Mesfin,
                                Email = EntCorreoElectronico,
                                Precio = boletas1.valorSinFormato,
                                AplicaImpuesto = "0",
                                DetalleImpuesto = boletas1.Iva,
                                IpApp = await GetLocalIPAddressAsync(),
                                Navegador = "No se encontro",
                                Descuento = descuentoCodigo.ToString(),
                                Empresa = EntNombreEmpresa,
                                Nit = EntIdentificacionempresa,
                                TelefonoEmpresa = EntTelefonoEmpresa,
                                Pais = paisEmpresa,
                                Ciudad = ciudadEmpresa,
                                Direccion = EntDireccionEmpr,
                                NombreArchivo = "",
                                DocumentoArchivo = "",
                                TipoArchivo = "",
                                Tamano = ""
                            };
                        }
                        else
                        {
                            insertBoletas = new InsertBoletas
                            {
                                IdSuceso = boletas1.IdSuceso,
                                Cantidad = Cantidad,
                                TitularFacturacion = EntNombre,
                                Telefono = EntCelular,
                                IdIdentificacion = SelectIdentificacion.Id,
                                Identificacion = EntNumeroIdentificacion,
                                IdBoleteria = boletas1.IdBoleteria,
                                CodigoDescuento = EntCodigoPromocional,
                                Vigencia = Diaini + " de " + Mesini + " al " + Diafin + " de " + Mesfin,
                                Email = EntCorreoElectronico,
                                Precio = boletas1.valorSinFormato,
                                AplicaImpuesto = "0",
                                DetalleImpuesto = boletas1.Iva,
                                IpApp = await GetLocalIPAddressAsync(),
                                Navegador = "No se encontro",
                                Descuento = descuentoCodigo.ToString(),
                                Empresa = null,
                                Nit = null,
                                TelefonoEmpresa = null,
                                Pais = "0",
                                Ciudad = "0",
                                Direccion = null,
                                NombreArchivo = "",
                                DocumentoArchivo = "",
                                TipoArchivo = "",
                                Tamano = ""
                            };
                        }

                        if (EntCodigoPromocional.Equals("0"))
                            EntCodigoPromocional = "";

                        if (boletas1.Iva != "0")
                            insertBoletas.AplicaImpuesto = "1";

                        string urli = logicaWs.Movile_Insert_Boleteria_Mtd("2", boletas1.IdCategoria, boletas1.IdActividad, EmailUsuario, LenguajeBase, "0");
                        string json = JsonConvert.SerializeObject(insertBoletas);
                        string jsonProcedimiento = await logicaWs.ConectionPost(json, urli);
                        var auxP = jsonProcedimiento.ToLower().Contains("timestamp");
                        if (auxP)
                        {
                            claseBase.InsertarLogs_Mtd("ERROR", jsonProcedimiento, "SeccionBoletasVM", "BtnContinuar_Mto3", "insertarboleteriacategoriapp");
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, "En el momento no se puede realizar la compra de esta boleta.", AppResources.cerrar);
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(jsonProcedimiento))
                                await RootNavigation.PushAsync(new ResumenCompraView(jsonProcedimiento, boletas1.TipoPublico));
                            else
                            {
                                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.NoSePuedoRealizarLaCompra, AppResources.cerrar);
                                claseBase.InsertarLogs_Mtd("ERROR", "jsonProcedimiento llego vacio", "SeccionBoletasVM", "BtnContinuar_Mto2", "insertarboleteriacategoriapp");
                            }
                        }
                    }
                    else
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.CantidadMinimaUna + " " + MaxCantidad.ToString(), AppResources.cerrar);
                    }
                }
                catch (Exception ex)
                {
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "SeccionBoletasVM", "BtnContinuar_Mto1", "Error Metodo Comprar Boleteria");
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.CantidadMinimaUna + " " + MaxCantidad.ToString(), AppResources.cerrar);
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                IsBusy = false;
            }
        }

        private async void CargarListaciudadByPais(string IdPais)
        {
            try
            {
                /*Cargar lista ciudades dependiendo del pais */
                ListaCiudadEmpresa = new ObservableCollection<ListasGeneral>();
                string urli = logicaWs.Movile_select_CiudadByPais_Mtd(IdPais);
                string jsonProcedimiento = await logicaWs.ConectionGet(urli);

                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral { Id = item.GetValue("IdCiudad")?.ToString(), Descripcion = item.GetValue("NombreCiudad")?.ToString() };
                    ListaCiudadEmpresa.Add(listasGeneral);
                }
                ListaCiudadEmpresa = new ObservableCollection<ListasGeneral>(ListaCiudadEmpresa.OrderBy(x => x.Descripcion).ToList());
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.cerrar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroVM", "CargarListaciudadByPais", "consultaciudadporpais");
            }
        }

        private async Task Notificacion_correo_Mto()
        {
            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElCorreoElectronicoDebeMas, AppResources.cerrar);
        }

        private async void BtnMas_Mto()
        {
            try
            {
                int cantAux = int.Parse(Cantidad);
                if (cantAux > 0)
                {
                    if (MaxCantidad == 0)
                    {
                        cantAux += 1;
                        Cantidad = cantAux.ToString();
                    }
                    else if (cantAux < MaxCantidad)
                    {
                        cantAux += 1;
                        Cantidad = cantAux.ToString();
                    }
                    else
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.CantidadMinimaUna + " " + MaxCantidad, AppResources.cerrar);
                    }
                }
            }
            catch (Exception ex)
            {
                Cantidad = "1";
                claseBase.InsertarLogs_Mtd("EXCEPTION", ex.Message, "SeccionBoletasVM", "BtnMas_Mto", "Errore Metodo Notificacion Correo");
            }
        }

        private void NBtnMenos_Mto()
        {
            try
            {
                int cantAux = int.Parse(Cantidad);
                if (cantAux > 1)
                {
                    cantAux -= 1;
                    Cantidad = cantAux.ToString();
                }
            }
            catch (Exception ex)
            {
                Cantidad = "1";
                claseBase.InsertarLogs_Mtd("EXCEPTION", ex.Message, "SeccionBoletasVM", "NBtnMenos_Mto", "Error Metdo Botonos Mostrar Menos");
            }
        }

        private void ValidarEntrada()
        {
            try
            {
                long valors = long.Parse(Cantidad) * long.Parse(boletas1.valorSinFormato);
                ValorTotalSinIva = claseBase.FormatoPrecio_Mto(valors);
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("EXCEPTION", ex.Message, "SeccionBoletasVM", "ValidarEntrada", "Error Validar Entrada");
            }
        }

    }
}
