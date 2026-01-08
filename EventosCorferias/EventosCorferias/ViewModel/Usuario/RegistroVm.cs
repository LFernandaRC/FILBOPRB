using Newtonsoft.Json;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Usuario;
using EventosCorferias.Resources.RecursosIdioma;


namespace EventosCorferias.ViewModel.Usuario
{
    public class RegistroVM : BaseViewModel
    {
        public RegistroUsuario? registroUsuario;

        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;

        private ObservableCollection<ListasGeneral>? _listaPaisUsu;
        private ObservableCollection<ListasGeneral>? _listaTipoIdUsu;
        private ObservableCollection<ListasGeneral>? _listaPaisEmpre;
        private ObservableCollection<ListasGeneral>? _listaCiudadEmpre;
        private ObservableCollection<ListasGeneral>? _listaProfesionUsu;
        private ObservableCollection<ListasGeneral>? _listaSectorEcoUsu;
        private ObservableCollection<ListasGeneral>? _listaCiudadRecintoUsu;

        private string? origenRedSocial;

        private string? _entIdUsu;
        private string? _entIdEmpr;
        private string? _entTelEmpr;
        private string? _entClaveUsu;
        private string? _entNombreUsu;
        private string? _entCorreoUsu;
        private string? _entClaveUsu2;
        private string? _entCargoEmpr;
        private string? _entCelularUsu;
        private string? _entNombreEmpr;
        private string? _entTelPaisEmpr;
        private string? _entTelAreaEmpr;
        private string? _entDireccionEmpr;
        private string? _entCodPostalEmpr;
        private string? _entConfiCorreoUsu;

        private int _indexPais;

        private DateTime _entFechaNacimientoUsu;

        private ListasGeneral? _selectPaisUsu;
        private ListasGeneral? _selectPaisEmpr;
        private ListasGeneral? _selectCiudadUsu;
        private ListasGeneral? _selectTipoIdUsu;
        private ListasGeneral? _selectCiudadEmpre;
        private ListasGeneral? _selectProfesionUsu;
        private ListasGeneral? _selectSectorEcoUsu;

        private bool _FormEmpresa;
        private bool _FormUsuario;
        private bool _checkTerminos;
        private bool _noobligatorioApple;

        readonly TerminosCondiciones? terminos;
        readonly TerminosCondiciones? politicas;

        public int IndexPais
        {
            get { return _indexPais; }
            set { _indexPais = value; OnPropertyChanged(nameof(IndexPais)); }
        }

        public ListasGeneral? SelectPaisEmpre
        {
            get { return _selectPaisEmpr; }
            set { SetProperty(ref _selectPaisEmpr, value); OnPropertyChanged(nameof(SelectPaisEmpre)); CargarListaciudadByPais(_selectPaisEmpr.Id); }
        }

        public ListasGeneral? SelectCiudadEmpresa
        {
            get { return _selectCiudadEmpre; }
            set { SetProperty(ref _selectCiudadEmpre, value); }
        }

        public ListasGeneral? SelectPaisUsu
        {
            get { return _selectPaisUsu; }
            set { SetProperty(ref _selectPaisUsu, value); }
        }

        public ListasGeneral? SelectCiudadUsu
        {
            get { return _selectCiudadUsu; }
            set { SetProperty(ref _selectCiudadUsu, value); }
        }

        public ListasGeneral? SelectTipoIdUsu
        {
            get { return _selectTipoIdUsu; }
            set { SetProperty(ref _selectTipoIdUsu, value); }
        }

        public ListasGeneral? SelectProfesionUsu
        {
            get { return _selectProfesionUsu; }
            set { SetProperty(ref _selectProfesionUsu, value); }
        }

        public ListasGeneral? SelectSectorEcoUsu
        {
            get { return _selectSectorEcoUsu; }
            set { SetProperty(ref _selectSectorEcoUsu, value); }
        }

        public string? EntNombreUsu
        {
            get { return _entNombreUsu; }
            set { _entNombreUsu = value; OnPropertyChanged(nameof(EntNombreUsu)); }
        }

        public string? EntCorreoUsu
        {
            get { return _entCorreoUsu; }
            set { _entCorreoUsu = value; OnPropertyChanged(nameof(EntCorreoUsu)); }
        }

        public string? EntConfiCorreoUsu
        {
            get { return _entConfiCorreoUsu; }
            set { _entConfiCorreoUsu = value; OnPropertyChanged(nameof(EntConfiCorreoUsu)); }
        }

        public string? EntClaveUsu
        {
            get { return _entClaveUsu; }
            set { _entClaveUsu = value; OnPropertyChanged(nameof(EntClaveUsu)); }
        }

        public string? EntClaveUsu2
        {
            get { return _entClaveUsu2; }
            set { _entClaveUsu2 = value; OnPropertyChanged(nameof(EntClaveUsu2)); }
        }

        public bool NoobligatorioApple
        {
            get { return _noobligatorioApple; }
            set { _noobligatorioApple = value; OnPropertyChanged(nameof(NoobligatorioApple)); }
        }

        public DateTime EntFechaNacimientoUsu
        {
            get { return _entFechaNacimientoUsu; }
            set { _entFechaNacimientoUsu = value; OnPropertyChanged(nameof(EntFechaNacimientoUsu)); }
        }

        public string? EntIdUsu
        {
            get { return _entIdUsu; }
            set { _entIdUsu = value; OnPropertyChanged(nameof(EntIdUsu)); }
        }

        public string? EntCelularUsu
        {
            get { return _entCelularUsu; }
            set { _entCelularUsu = value; OnPropertyChanged(nameof(EntCelularUsu)); }
        }

        public string? EntNombreEmpr
        {
            get { return _entNombreEmpr; }
            set { _entNombreEmpr = value; OnPropertyChanged(nameof(EntNombreEmpr)); }
        }

        public string? EntIdEmpr
        {
            get { return _entIdEmpr; }
            set { _entIdEmpr = value; OnPropertyChanged(nameof(EntIdEmpr)); }
        }

        public string? EntTelEmpr
        {
            get { return _entTelEmpr; }
            set { _entTelEmpr = value; OnPropertyChanged(nameof(EntTelEmpr)); }
        }

        public string? EntTelPaisEmpr
        {
            get { return _entTelPaisEmpr; }
            set { _entTelPaisEmpr = value; OnPropertyChanged(nameof(EntTelPaisEmpr)); }
        }

        public string? EntTelAreaEmpr
        {
            get { return _entTelAreaEmpr; }
            set { _entTelAreaEmpr = value; OnPropertyChanged(nameof(EntTelAreaEmpr)); }
        }

        public string? EntDireccionEmpr
        {
            get { return _entDireccionEmpr; }
            set { _entDireccionEmpr = value; OnPropertyChanged(nameof(EntDireccionEmpr)); }
        }

        public string? EntCodPostalEmpr
        {
            get { return _entCodPostalEmpr; }
            set { _entCodPostalEmpr = value; OnPropertyChanged(nameof(EntCodPostalEmpr)); }
        }

        public string? EntCargoEmpr
        {
            get { return _entCargoEmpr; }
            set { _entCargoEmpr = value; OnPropertyChanged(nameof(EntCargoEmpr)); }
        }

        public bool FormEmpresa
        {
            get { return _FormEmpresa; }
            set { _FormEmpresa = value; OnPropertyChanged(nameof(FormEmpresa)); }
        }

        public bool FormUsuario
        {
            get { return _FormUsuario; }
            set { _FormUsuario = value; OnPropertyChanged(nameof(FormUsuario)); }
        }

        public bool CheckTerminos
        {
            get { return _checkTerminos; }
            set { _checkTerminos = value; OnPropertyChanged(nameof(CheckTerminos)); }
        }


        public ObservableCollection<ListasGeneral>? ListaPaisUsu
        {
            get { return _listaPaisUsu; }
            set { _listaPaisUsu = value; OnPropertyChanged(nameof(ListaPaisUsu)); }
        }

        public ObservableCollection<ListasGeneral>? ListaCiudadRecintoUsu
        {
            get { return _listaCiudadRecintoUsu; }
            set { _listaCiudadRecintoUsu = value; OnPropertyChanged(nameof(ListaCiudadRecintoUsu)); }
        }

        public ObservableCollection<ListasGeneral>? ListaTipoIdUsu
        {
            get { return _listaTipoIdUsu; }
            set { _listaTipoIdUsu = value; OnPropertyChanged(nameof(ListaTipoIdUsu)); }
        }

        public ObservableCollection<ListasGeneral>? ListaProfesionUsu
        {
            get { return _listaProfesionUsu; }
            set { _listaProfesionUsu = value; OnPropertyChanged(nameof(ListaProfesionUsu)); }
        }


        public ObservableCollection<ListasGeneral>? ListaSectorEcoUsu
        {
            get { return _listaSectorEcoUsu; }
            set { _listaSectorEcoUsu = value; OnPropertyChanged(nameof(ListaSectorEcoUsu)); }
        }

        public ObservableCollection<ListasGeneral>? ListaPaisEmpre
        {
            get { return _listaPaisEmpre; }
            set { _listaPaisEmpre = value; OnPropertyChanged(nameof(ListaPaisEmpre)); }
        }

        public ObservableCollection<ListasGeneral>? ListaCiudadEmpre
        {
            get { return _listaCiudadEmpre; }
            set { _listaCiudadEmpre = value; OnPropertyChanged(nameof(ListaCiudadEmpre)); }
        }

        public ICommand? Pais { get; }

        public ICommand? Regresar { get; }

        public ICommand? Terminos { get; }

        public ICommand? Politicas { get; }

        public ICommand? Registrarse { get; }

        public ICommand? Form_Empresa { get; }

        public ICommand? Form_Usuario { get; }

        public ICommand? Notificacion_clave { get; }

        public ICommand? Notificacion_correo { get; }

        public RegistroVM()
        {
            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            terminos = new TerminosCondiciones();
            politicas = new TerminosCondiciones();

            try
            {
                LenguajeBase = Preferences.Get("IdiomaDefecto", "");


#if IOS
                origenRedSocial = "apple";
#endif

#if ANDROID
                origenRedSocial = "android";
#endif

                Regresar = new Command(Regresar_Mto);
                Form_Empresa = new Command(Form_Empresa_Mto);
                Registrarse = new Command(async () => await Registro_Mto());
                Form_Usuario = new Command(async () => await Form_Usuario_Mto());

                Terminos = new Command(async () => await pageServicio.DisplayAlert(terminos.Modulo, terminos.Texto, "Cerrar"));
                Politicas = new Command(async () => await pageServicio.DisplayAlert(politicas.Modulo, politicas.Texto, "Cerrar"));
                Notificacion_clave = new Command(async () => await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMLaContrasenaDebeTenerMinimoMas, AppResources.cerrar));
                Notificacion_correo = new Command(async () => await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElCorreoElectronicoDebeMas, AppResources.cerrar));

                Inicializar();
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroVM", "RegistroVM", "Carga inicial");
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoCargaVistaRegistro, AppResources.cerrar);
            }
        }

        private async void Inicializar()
        {
            try
            {
                if (origenRedSocial == "apple")
                {
                    NoobligatorioApple = false;
                }
                else
                {
                    NoobligatorioApple = true;
                }

                IndexPais = 1;

                FormUsuario = true;
                EntFechaNacimientoUsu = DateTime.Now;

                await CargarListasUsuario();
                await ConsultaPoliticasAsync();
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroVM", "Inicializar", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoCargaVistaRegistro, AppResources.cerrar);
            }
        }

        private async Task CargarListasUsuario()
        {
            try
            {
                /*Cargar lista paises */
                ListaPaisUsu = new ObservableCollection<ListasGeneral>();
                string urli = logicaWS.Movile_select_Pais_Mtd();
                string jsonProcedimiento = await logicaWS.ConectionGet(urli) ?? "[]";
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Descripcion = item.GetValue("Nombre_Pais")?.ToString() ?? "Desconocido",
                        Id = item.GetValue("idPais")?.ToString() ?? "0"
                    };
                    ListaPaisUsu.Add(listasGeneral);
                }
                ListaPaisUsu = new ObservableCollection<ListasGeneral>(ListaPaisUsu.OrderBy(x => x.Descripcion).ToList());

                /* ciudad recinto usuario */
                ListaCiudadRecintoUsu = new ObservableCollection<ListasGeneral>();
                urli = logicaWS.Movile_select_CiudadRecinto_Mtd();
                jsonProcedimiento = await logicaWS.ConectionGet(urli) ?? "[]";
                jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Descripcion = item.GetValue("nombreCiudad")?.ToString() ?? "Desconocido",
                        Id = item.GetValue("idCiudad")?.ToString() ?? "0"
                    };
                    ListaCiudadRecintoUsu.Add(listasGeneral);
                }
                ListaCiudadRecintoUsu = new ObservableCollection<ListasGeneral>(ListaCiudadRecintoUsu.OrderBy(x => x.Descripcion).ToList());

                /*Cargar lista Tipo de identificacion */
                ListaTipoIdUsu = new ObservableCollection<ListasGeneral>();
                urli = logicaWS.Movile_select_TipoId_Mtd(); //consultarpaises
                jsonProcedimiento = await logicaWS.ConectionGet(urli) ?? "[]";
                jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Descripcion = item.GetValue("NombreIdentificacion")?.ToString() ?? "Desconocido",
                        Id = item.GetValue("idIdentificacion")?.ToString() ?? "0"
                    };
                    ListaTipoIdUsu.Add(listasGeneral);
                }
                ListaTipoIdUsu = new ObservableCollection<ListasGeneral>(ListaTipoIdUsu.OrderBy(x => x.Descripcion).ToList());
                /*Cargar lista profesion */
                ListaProfesionUsu = new ObservableCollection<ListasGeneral>();
                urli = logicaWS.Movile_select_Profesion_Mtd();
                jsonProcedimiento = await logicaWS.ConectionGet(urli) ?? "[]";
                jsArray = JArray.Parse(jsonProcedimiento);

                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Descripcion = item.GetValue("NombreProfesion")?.ToString() ?? "Desconocido",
                        Id = item.GetValue("idProfesion")?.ToString() ?? "0"
                    };
                    ListaProfesionUsu.Add(listasGeneral);
                }
                ListaProfesionUsu = new ObservableCollection<ListasGeneral>(ListaProfesionUsu.OrderBy(x => x.Descripcion).ToList());

                /*Cargar lista sector economico */
                ListaSectorEcoUsu = new ObservableCollection<ListasGeneral>();
                urli = logicaWS.Movile_select_SectoEcono_Mtd();
                jsonProcedimiento = await logicaWS.ConectionGet(urli) ?? "[]";
                jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Descripcion = item.GetValue("NombreSectorEconomico")?.ToString() ?? "Desconocido",
                        Id = item.GetValue("idSectorEconomico")?.ToString() ?? "0"
                    };
                    ListaSectorEcoUsu.Add(listasGeneral);
                }
                ListaSectorEcoUsu = new ObservableCollection<ListasGeneral>(ListaSectorEcoUsu.OrderBy(x => x.Descripcion).ToList());

                /*Cargar lista pais empresa */
                ListaPaisEmpre = new ObservableCollection<ListasGeneral>();
                urli = logicaWS.Movile_select_Pais_Mtd();
                jsonProcedimiento = await logicaWS.ConectionGet(urli) ?? "[]";
                jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Descripcion = item.GetValue("Nombre_Pais")?.ToString() ?? "Desconocido",
                        Id = item.GetValue("idPais")?.ToString() ?? "0"
                    };
                    ListaPaisEmpre.Add(listasGeneral);
                }
                ListaPaisEmpre = new ObservableCollection<ListasGeneral>(ListaPaisEmpre.OrderBy(x => x.Descripcion).ToList());
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroVM", "CargarListasUsuario", "listas varias para el registro");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoCargaVistaRegistro, AppResources.cerrar);
            }
        }

        private void Form_Empresa_Mto()
        {
            FormEmpresa = false;
            FormUsuario = true;

        }

        private async Task Form_Usuario_Mto()
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            if (await ValidarRegistro_Mto())
            {
                FormEmpresa = true;
                FormUsuario = false;
            }
            IsBusy = false;
        }

        private async void Regresar_Mto()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroVM", "Regresar_Mto", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoCargaVistaRegistro, AppResources.cerrar);
            }
        }

        private async Task<bool> ValidarRegistro_Mto()
        {
            try
            {
                char[] charsToTrim = { ' ' };
                string expreregularletras = @"^([a-zA-Z ñáéíóúÑÁÉÍÓÚ]{1,99})$";
                string expreregularcorreos = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

                /*Validar Info obligatoria de la persona */
                if (origenRedSocial == "apple")
                {
                    if (string.IsNullOrWhiteSpace(EntCorreoUsu) || string.IsNullOrWhiteSpace(EntConfiCorreoUsu)
                        || string.IsNullOrWhiteSpace(EntNombreUsu) || string.IsNullOrWhiteSpace(EntClaveUsu) || string.IsNullOrWhiteSpace(EntClaveUsu2))
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVacios, AppResources.cerrar);
                        return false;
                    }
                }
                else
                {
                    if (SelectPaisUsu == null || SelectTipoIdUsu == null || SelectTipoIdUsu == null)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVacios, AppResources.cerrar);
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(EntCorreoUsu) || string.IsNullOrWhiteSpace(EntConfiCorreoUsu) || string.IsNullOrWhiteSpace(EntClaveUsu)
                        || string.IsNullOrWhiteSpace(EntClaveUsu2) || string.IsNullOrWhiteSpace(EntNombreUsu)
                        || string.IsNullOrWhiteSpace(SelectPaisUsu.Id) || string.IsNullOrWhiteSpace(SelectTipoIdUsu.Id) || string.IsNullOrWhiteSpace(EntIdUsu) || string.IsNullOrWhiteSpace(SelectTipoIdUsu.Id))
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVacios, AppResources.cerrar);
                        return false;
                    }
                }


                /*Validar info obligatoria de la empresa */
                if (FormEmpresa)
                {

                    if (string.IsNullOrEmpty(EntNombreEmpr) || string.IsNullOrEmpty(EntIdEmpr) || string.IsNullOrEmpty(EntCodPostalEmpr) || string.IsNullOrEmpty(EntTelPaisEmpr) || string.IsNullOrEmpty(EntTelEmpr) || string.IsNullOrEmpty(EntTelAreaEmpr) || string.IsNullOrEmpty(EntDireccionEmpr) || string.IsNullOrEmpty(EntCargoEmpr))
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVaciosEmpresa, AppResources.cerrar);
                        return false;
                    }
                }

                /*Vaidar largo del nombre */
                if (EntNombreUsu.Length < 3)
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElNombreDebeTenerMinimo3Caracteres, AppResources.cerrar);
                    return false;
                }

                /* validar formato correo */
                bool resultadoCorreo = Regex.IsMatch(EntCorreoUsu.Trim(charsToTrim), expreregularcorreos, RegexOptions.IgnoreCase);
                if (!resultadoCorreo)
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoTieneFormatoCorreo, AppResources.cerrar);
                    return false;
                }

                if (EntClaveUsu != null)
                {
                    if (EntClaveUsu != "")
                    {
                        if (EntClaveUsu.Length < 6)
                        {
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMLaContrasenaDebeTenerMinimoMas, AppResources.cerrar);
                            return false;
                        }

                        /*Validar que las contraseñas sean iguales */
                        if (EntClaveUsu != EntClaveUsu2)
                        {
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMLasContrasenasNoCoinciden, AppResources.cerrar);
                            return false;
                        }
                    }
                    else
                    {
                        EntClaveUsu = "";
                    }
                }
                else
                {
                    EntClaveUsu = "";
                }


                /* validar formato Nombre completo */
                bool resultadoNombreUsu = Regex.IsMatch(EntNombreUsu.Trim(charsToTrim), expreregularletras, RegexOptions.IgnoreCase);
                if (!resultadoNombreUsu)
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoTieneFormaroElNombreDelUsuario, AppResources.cerrar);
                    return false;
                }


                if (origenRedSocial != "apple")
                {
                    /* Validar edad */
                    if (EntFechaNacimientoUsu.AddYears(18) > DateTime.Today)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMMayorDeEdad, AppResources.cerrar);
                        return false;
                    }
                }

                /*Validar que l0s correo sean iguales */
                if (EntConfiCorreoUsu.ToLower() != EntCorreoUsu.ToLower())
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMLosCorreosNoCoinciden, AppResources.cerrar);
                    return false;
                }

                if (CheckTerminos == false)
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElCampoTerminosEsObligatorio, AppResources.cerrar);
                    return false;
                }
                else
                {
                    string AuxFecha = null;
                    int AuxpaisUsu = 0;
                    int AuxCiudadUsu = 2;
                    string? CiudadUsuDesc = "Bogotá";
                    int AuxTipoIdUsu = 0;
                    int AuxProfesionUsu = 0;
                    int AuxSectorEcoUsu = 0;
                    EntCorreoUsu = EntCorreoUsu.ToLower();

                    if (origenRedSocial != "apple")
                    {
                        AuxFecha = EntFechaNacimientoUsu.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                        if (SelectPaisUsu != null)
                        {
                            if (SelectPaisUsu.Id != null && SelectPaisUsu.Id != "")
                                AuxpaisUsu = int.Parse(SelectPaisUsu.Id);
                        }

                        if (SelectCiudadUsu != null)
                        {
                            if (SelectCiudadUsu.Id != null && SelectCiudadUsu.Id != "")
                            {
                                AuxCiudadUsu = int.Parse(SelectCiudadUsu.Id);
                                CiudadUsuDesc = SelectCiudadUsu.Descripcion;
                            }
                        }

                        if (SelectTipoIdUsu != null)
                        {
                            if (SelectTipoIdUsu.Id != null && SelectTipoIdUsu.Id != "")
                                AuxTipoIdUsu = int.Parse(SelectTipoIdUsu.Id);
                        }

                        if (SelectProfesionUsu != null)
                        {
                            if (SelectProfesionUsu != null)
                                if (SelectProfesionUsu.Id != null && SelectProfesionUsu.Id != "")
                                    AuxProfesionUsu = int.Parse(SelectProfesionUsu.Id);
                        }

                        if (SelectSectorEcoUsu != null)
                        {
                            if (SelectSectorEcoUsu != null)
                                if (SelectSectorEcoUsu.Id != null && SelectSectorEcoUsu.Id != "")
                                    AuxSectorEcoUsu = int.Parse(SelectSectorEcoUsu.Id);
                        }
                    }

                    string AuxEntIdEmpr = "0";
                    if (EntIdEmpr != null && EntIdEmpr != "")
                        AuxEntIdEmpr = EntIdEmpr;

                    string AuxEntTelEmpr = "0";
                    if (EntTelEmpr != null && EntTelEmpr != "")
                        AuxEntTelEmpr = EntTelEmpr;

                    int AuxEntTelPaisEmpr = 0;
                    if (EntTelPaisEmpr != null && EntTelPaisEmpr != "")
                        AuxEntTelPaisEmpr = int.Parse(EntTelPaisEmpr);

                    int AuxEntTelAreaEmpr = 0;
                    if (EntTelAreaEmpr != null && EntTelAreaEmpr != "")
                        AuxEntTelAreaEmpr = int.Parse(EntTelAreaEmpr);

                    int AuxpaisEmpre = 0;
                    if (SelectPaisEmpre != null)
                        if (SelectPaisEmpre.Id != null && SelectPaisEmpre.Id != "")
                            AuxpaisEmpre = int.Parse(SelectPaisEmpre.Id);

                    int AuxciudadEmpre = 0;
                    if (SelectCiudadEmpresa != null)
                        if (SelectCiudadEmpresa.Id != null && SelectCiudadEmpresa.Id != "")
                            AuxciudadEmpre = int.Parse(SelectCiudadEmpresa.Id);

                    int AuxEntCodPostalEmpr = 0;
                    if (EntCodPostalEmpr != null && EntCodPostalEmpr != "")
                        AuxEntCodPostalEmpr = int.Parse(EntCodPostalEmpr);

                    string AuxCelular = "0";
                    if (EntCelularUsu != null && EntCelularUsu != "")
                        AuxCelular = EntCelularUsu;

                    string Image = "https://thumbs.dreamstime.com/b/omita-el-icono-del-perfil-avatar-placeholder-gris-de-la-foto-99724602.jpg";
                    if (Preferences.ContainsKey("Imagen"))
                        Image = Preferences.Get("Imagen", "https://thumbs.dreamstime.com/b/omita-el-icono-del-perfil-avatar-placeholder-gris-de-la-foto-99724602.jpg");

                    int idRedSocial = 0;
                    var hasKey = Preferences.ContainsKey("RedSocial");

                    if (hasKey)
                        idRedSocial = Preferences.Get("RedSocial", 0);

                    if (EntIdUsu == null)
                    {
                        EntIdUsu = "";
                    }

                    registroUsuario = new RegistroUsuario
                    {
                        NombreCompleto = EntNombreUsu,
                        Email = EntCorreoUsu,
                        Contrasena = EntClaveUsu,
                        IdPais = AuxpaisUsu,
                        IdCiudad = AuxCiudadUsu,
                        IdIdentificacion = AuxTipoIdUsu,
                        FechaNacimiento = AuxFecha,
                        Identificacion = EntIdUsu,
                        Celular = AuxCelular,
                        IdProfesion = AuxProfesionUsu,
                        IdSectorEconomico = AuxSectorEcoUsu,
                        NombreEmpresa = EntNombreEmpr,
                        IdentificacionEmpresa = AuxEntIdEmpr,
                        Telefono = AuxEntTelEmpr,
                        IndicativoPais = AuxEntTelPaisEmpr,
                        Area = AuxEntTelAreaEmpr,
                        IdPaisemp = AuxpaisEmpre,
                        IdCiudademp = AuxciudadEmpre,
                        Direccion = EntDireccionEmpr,
                        CodigoPostal = AuxEntCodPostalEmpr,
                        Cargo = EntCargoEmpr,
                        IdRedSocial = idRedSocial,
                        TokenRed = "",
                        TerminosCondiciones = 1,
                        IdSectorEconomicoemp = 0,
                        Imagen = Image
                    };

                    return true;
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroVM", "ValidarRegistro_Mto", "n/a");
                return false;
            }
        }

        private async Task Registro_Mto()
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            try
            {
                if (!StateConexion)
                {
                    if (await ValidarRegistro_Mto())
                    {
                        string Bandera;
                        if (FormEmpresa)
                            Bandera = "0"; /* Registra Todo */
                        else
                            Bandera = "1"; /* Registra sin info Empresa */

                        string urli = logicaWS.Movile_Insert_Registro_Mtd(Bandera);
                        string json = JsonConvert.SerializeObject(registroUsuario);
                        string jsonProcedimiento = (await logicaWS.ConectionPost(json, urli)) ?? "";
                        if (jsonProcedimiento.Trim() == "Datos registrados")
                        {
                            if (claseBase.ValidaString(EntCelularUsu ?? "") != "")
                                Preferences.Set("Celular", EntCelularUsu);

                            Preferences.Remove("RedSocial");
                            Preferences.Set("IdCiudadDesc", "Bogotá");
                            Preferences.Set("Email", registroUsuario.Email ?? "");
                            Preferences.Set("IdCiudad", registroUsuario.IdCiudad.ToString());
                            Preferences.Set("NombreCompleto", registroUsuario.NombreCompleto);
                            Preferences.Set("IdIdentificacion", registroUsuario.IdIdentificacion.ToString());
                            Preferences.Set("numeroIdentificacion", registroUsuario.Identificacion);

                            Preferences.Set("MantenerConectado", true);
                            Application.Current!.MainPage = new MasterHomePage();

                            await CheckTerminosYCondiciones();
                            await RelacionUsuarioApp();
                        }
                        else
                        {
                            if (jsonProcedimiento == "El email o documento registrado ya existe" || jsonProcedimiento == "El usuario ya existe")
                                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElUsuarioYaEstaRegistrado, AppResources.cerrar);
                            else
                                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento + " " + jsonProcedimiento, AppResources.cerrar);
                        }
                    }
                }
                else
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.cerrar);
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.cerrar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroVM", "Registro_Mto", "insertarusuarioApp");
            }
            finally
            {
                IsBusy = false;
            }
        }


        private async void CargarListaciudadByPais(string IdPais)
        {
            try
            {
                /*Cargar lista ciudades dependiendo del pais */
                ListaCiudadEmpre = new ObservableCollection<ListasGeneral>();
                string urli = logicaWS.Movile_select_CiudadByPais_Mtd(IdPais);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);

                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Id = item.GetValue("IdCiudad").ToString(),
                        Descripcion = item.GetValue("NombreCiudad").ToString()
                    };
                    ListaCiudadEmpre.Add(listasGeneral);
                }
                ListaCiudadEmpre = new ObservableCollection<ListasGeneral>(ListaCiudadEmpre.OrderBy(x => x.Descripcion).ToList());
                SelectCiudadEmpresa = new ListasGeneral();
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.cerrar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroVM", "CargarListaciudadByPais", "consultaciudadporpais");
            }
        }

        private async Task CheckTerminosYCondiciones()
        {
            try
            {
                string urliDos = logicaWS.Movile_Insert_habeasData_Mtd("2", registroUsuario.Email);
                HabeasData habeasData = new HabeasData
                {
                    IpCel = await GetLocalIPAddressAsync(),
                    Acepto = "1",
                    IdModulo = "50",
                    IdTerminoPolitico = terminos.Max,
                    Navegador = "0"
                };

                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        habeasData.Navegador = "iOS";
                        break;
                    case Device.Android:
                        habeasData.Navegador = "Android";
                        break;
                    default:
                        break;
                }

                string json = JsonConvert.SerializeObject(habeasData);
                string res = await logicaWS.ConectionPost(json, urliDos);

                if (!res.Equals("Datos Registrados correctamente"))
                    claseBase.InsertarLogs_Mtd("ERROR", res, "RegistroVM", "CheckTerminosYCondiciones", "insertarhabeasdatapp");

                string urliTres = logicaWS.Movile_Insert_habeasData_Mtd("2", registroUsuario.Email);
                HabeasData habeasDataTres = new HabeasData
                {
                    IpCel = await GetLocalIPAddressAsync(),
                    Acepto = "1",
                    IdModulo = "50",
                    IdTerminoPolitico = politicas.Max,
                    Navegador = "0"
                };

                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        habeasDataTres.Navegador = "iOS";
                        break;
                    case Device.Android:
                        habeasDataTres.Navegador = "Android";
                        break;
                    default:
                        break;
                }

                string jsonTres = JsonConvert.SerializeObject(habeasDataTres);
                string resTres = await logicaWS.ConectionPost(jsonTres, urliTres);

                if (!resTres.Equals("Datos Registrados correctamente"))
                    claseBase.InsertarLogs_Mtd("ERROR", resTres, "RegistroVM", "CheckTerminosYCondiciones", "insertarhabeasdatapp");
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroVM", "CheckTerminosYCondiciones2", "insertarhabeasdatapp");
            }
        }

        private async Task ConsultaPoliticasAsync()
        {
            if (!StateConexion)
            {
                try
                {
                    string urli = logicaWS.Movile_Select_Terminos_Politicas_Mtd("1", "50", LenguajeBase);
                    string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                    JArray jsArray = JArray.Parse(jsonProcedimiento);

                    foreach (JObject item in jsArray)
                    {
                        terminos.Max = item.GetValue("Max").ToString();
                        terminos.Modulo = item.GetValue("Modulo").ToString();
                        terminos.Texto = item.GetValue("Texto").ToString();
                        terminos.FechaPublica = item.GetValue("FechaPublica").ToString();
                        terminos.IdModulo = item.GetValue("IdModulo").ToString();
                    }
                }
                catch (Exception ex)
                {
                    terminos.Max = "0";
                    terminos.FechaPublica = "0";
                    terminos.IdModulo = "0";
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroVM", "ConsultaPoliticasAsync", "consultaterminosaplicacion");
                }

                try
                {

                    string urli = logicaWS.Movile_Select_Terminos_Politicas_Mtd("2", "50", LenguajeBase);
                    string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                    JArray jsArray = JArray.Parse(jsonProcedimiento);

                    foreach (JObject item in jsArray)
                    {
                        politicas.Max = item.GetValue("Max").ToString();
                        politicas.Modulo = item.GetValue("Modulo").ToString();
                        politicas.Texto = item.GetValue("Texto").ToString();
                        politicas.FechaPublica = item.GetValue("FechaPublica").ToString();
                        politicas.IdModulo = item.GetValue("IdModulo").ToString();
                    }
                }
                catch (Exception ex)
                {
                    politicas.Max = "0";
                    politicas.FechaPublica = "0";
                    politicas.IdModulo = "0";
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroVM", "ConsultaPoliticasAsync", "consultaterminosaplicacion");
                }
            }
        }

        private async Task RelacionUsuarioApp()
        {
            try
            {
                string urli = logicaWS.Movile_actualizarelacionusuario_Mtd("2");
                UsuActuApp ip = new UsuActuApp()
                {
                    Email = Preferences.Get("Email", ""),
                    IdApp = Preferences.Get("IdApp", "")
                };

                string jsonip = JsonConvert.SerializeObject(ip);
                string jsonProcedimiento = await logicaWS.ConectionPost(urli, jsonip);
            }
            catch (Exception e)
            {
                claseBase.InsertarLogs_Mtd("ERROR", e.Message, "RegistroVM", "RecuperarToken", "actualizatoken");
            }
        }
    }

    public class UsuActuApp
    {
        public string Email { get; set; }
        public string IdApp { get; set; }
    }
}
