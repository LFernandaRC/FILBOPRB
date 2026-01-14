using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Usuario;
using EventosCorferias.Resources.RecursosIdioma;
using Mopups.Services;
using EventosCorferias.Views.PopUp;

namespace EventosCorferias.ViewModel.Usuario
{
    public class EditarPerfilVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        public readonly ClaseBase claseBase;
        public readonly IPageServicio pageServicio;

        private ObservableCollection<ListasGeneral> _listaPaisUsu;
        private ObservableCollection<ListasGeneral> ListaRedSocial;
        private ObservableCollection<ListasGeneral> _listaPaisEmpre;
        private ObservableCollection<ListasGeneral> _listaCiudadEmpre;
        private ObservableCollection<ListasGeneral> _listaProfesionUsu;
        private ObservableCollection<ListasGeneral> _listaSectorEcoUsu;
        private ObservableCollection<ListasGeneral> _listaCiudadRecintoUsu;
        private ObservableCollection<ListasGeneral> _listaTipoIdUsu;

        public ActualizarUsuario ActualizarUsuario;
        public ActualizarInsertaUsuario ActualizarUsuarioIOS;

        private string _entIdUsu;
        private string _entIdEmpr;
        private string _entTelEmpr;
        private string _enTipoIdUsuS;
        private string _entCargoEmpr;
        private string _entCorreoUsu;
        private string _entNombreUsu;
        private string _entCelularUsu;
        private string _entNombreEmpr;
        private string _entTelPaisEmpr;
        private string _entTelAreaEmpr;
        private string _selectIndexPais;
        private string _fechaNacimiento;
        private string _entDireccionEmpr;
        private string _entCodPostalEmpr;
        private string _selectIndexCiudad;
        private string _selectIndexSector;
        private string _selectIndexProfesion;
        private string _selectIndexPaisEmpre;
        private string _selectIndexCiudadEmpre;
        private string correoViejo;

        private bool _apple;
        private bool _google;
        private bool _facebook;
        private bool _FormEmpresa;
        private bool _FormUsuario;

        private ImageSource _fotoGaleria = "";

        private DateTime? _entFechaNacimientoUsu = DateTime.Now;

        private ListasGeneral _selectPaisUsu;
        private ListasGeneral _selectPaisEmpr;
        private ListasGeneral _selectCiudadUsu;
        private ListasGeneral _selectCiudadEmpre;
        private ListasGeneral _selectProfesionUsu;
        private ListasGeneral _selectSectorEcoUsu;

        /*Habilitar o desabilitar el actualizar cc y correo*/
        private bool _verCorreoBloq;
        private bool _verCorreoDesBloq;
        private bool _verIdentificacionBloq;
        private bool _verIdentificacionDesBloq;
        private bool _verTerminos;

        private ListasGeneral _selecttipoDocumento;
        private string _entIdentificacio;
        private string _entCorreo;

        private bool _checkTerminos;

        readonly TerminosCondiciones terminos;
        readonly TerminosCondiciones politicas;

        public bool VerTerminos
        {
            get { return _verTerminos; }
            set { _verTerminos = value; OnPropertyChanged(nameof(VerTerminos)); }
        }

        public bool CheckTerminos
        {
            get { return _checkTerminos; }
            set { _checkTerminos = value; OnPropertyChanged(nameof(CheckTerminos)); }
        }

        public bool VerCorreoBloq
        {
            get { return _verCorreoBloq; }
            set { _verCorreoBloq = value; OnPropertyChanged(nameof(VerCorreoBloq)); }
        }
        public bool VerCorreoDesBloq
        {
            get { return _verCorreoDesBloq; }
            set { _verCorreoDesBloq = value; OnPropertyChanged(nameof(VerCorreoDesBloq)); }
        }
        public bool VerIdentificacionBloq
        {
            get { return _verIdentificacionBloq; }
            set { _verIdentificacionBloq = value; OnPropertyChanged(nameof(VerIdentificacionBloq)); }
        }
        public bool VerIdentificacionDesBloq
        {
            get { return _verIdentificacionDesBloq; }
            set { _verIdentificacionDesBloq = value; OnPropertyChanged(nameof(VerIdentificacionDesBloq)); }
        }

        public ListasGeneral SelecttipoDocumento
        {
            get { return _selecttipoDocumento; }
            set
            {
                _selecttipoDocumento = value; OnPropertyChanged(nameof(SelecttipoDocumento));
                if (value != null)
                {
                    ActualizarUsuarioIOS.IdIdentificacion = _selecttipoDocumento.Id;
                }

            }
        }
        public string EntIdentificacio
        {
            get { return _entIdentificacio; }
            set { _entIdentificacio = value; OnPropertyChanged(nameof(EntIdentificacio)); }
        }
        public string EntCorreo
        {
            get { return _entCorreo; }
            set { _entCorreo = value; OnPropertyChanged(nameof(EntCorreo)); }
        }

        public string EntNombreUsu
        {
            get { return _entNombreUsu; }
            set
            {
                _entNombreUsu = value;
                OnPropertyChanged(nameof(EntNombreUsu));
            }
        }

        public string EnTipoIdUsuS
        {
            get { return _enTipoIdUsuS; }
            set
            {
                _enTipoIdUsuS = value;
                OnPropertyChanged(nameof(EnTipoIdUsuS));
            }
        }

        public DateTime? EntFechaNacimientoUsu
        {
            get { return _entFechaNacimientoUsu; }
            set
            {
                _entFechaNacimientoUsu = value;
                OnPropertyChanged(nameof(EntFechaNacimientoUsu));
            }
        }

        public string EntIdUsu
        {
            get { return _entIdUsu; }
            set
            {
                _entIdUsu = value;
                OnPropertyChanged(nameof(EntIdUsu));
            }
        }
        public string EntCelularUsu
        {
            get { return _entCelularUsu; }
            set
            {
                _entCelularUsu = value;
                OnPropertyChanged(nameof(EntCelularUsu));
            }
        }
        public string EntNombreEmpr
        {
            get { return _entNombreEmpr; }
            set
            {
                _entNombreEmpr = value;
                OnPropertyChanged(nameof(EntNombreEmpr));
            }
        }

        public string EntIdEmpr
        {
            get { return _entIdEmpr; }
            set
            {
                _entIdEmpr = value;
                OnPropertyChanged(nameof(EntIdEmpr));
            }
        }

        public string EntTelEmpr
        {
            get { return _entTelEmpr; }
            set
            {
                _entTelEmpr = value;
                OnPropertyChanged(nameof(EntTelEmpr));
            }
        }

        public string EntTelPaisEmpr
        {
            get { return _entTelPaisEmpr; }
            set
            {
                _entTelPaisEmpr = value;
                OnPropertyChanged(nameof(EntTelPaisEmpr));
            }
        }
        public string EntTelAreaEmpr
        {
            get { return _entTelAreaEmpr; }
            set
            {
                _entTelAreaEmpr = value;
                OnPropertyChanged(nameof(EntTelAreaEmpr));
            }
        }

        public string EntDireccionEmpr
        {
            get { return _entDireccionEmpr; }
            set
            {
                _entDireccionEmpr = value;
                OnPropertyChanged(nameof(EntDireccionEmpr));
            }
        }

        public string EntCodPostalEmpr
        {
            get { return _entCodPostalEmpr; }
            set
            {
                _entCodPostalEmpr = value;
                OnPropertyChanged(nameof(EntCodPostalEmpr));
            }
        }
        public string EntCargoEmpr
        {
            get { return _entCargoEmpr; }
            set
            {
                _entCargoEmpr = value;
                OnPropertyChanged(nameof(EntCargoEmpr));
            }
        }
        public string EntCorreoUsu
        {
            get { return _entCorreoUsu; }
            set
            {
                _entCorreoUsu = value;
                OnPropertyChanged(nameof(EntCorreoUsu));
            }
        }
        public string FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set
            {
                _fechaNacimiento = value;
                OnPropertyChanged(nameof(FechaNacimiento));
            }
        }
        public string SelectIndexCiudad
        {
            get { return _selectIndexCiudad; }
            set { _selectIndexCiudad = value; OnPropertyChanged(nameof(SelectIndexCiudad)); }
        }

        public string SelectIndexPais
        {
            get { return _selectIndexPais; }
            set { _selectIndexPais = value; OnPropertyChanged(nameof(SelectIndexPais)); }
        }

        public string SelectIndexSector
        {
            get { return _selectIndexSector; }
            set { _selectIndexSector = value; OnPropertyChanged(nameof(SelectIndexSector)); }
        }

        public string SelectIndexProfesion
        {
            get { return _selectIndexProfesion; }
            set { _selectIndexProfesion = value; OnPropertyChanged(nameof(SelectIndexProfesion)); }
        }

        public string SelectIndexPaisEmpre
        {
            get { return _selectIndexPaisEmpre; }
            set { _selectIndexPaisEmpre = value; OnPropertyChanged(nameof(SelectIndexPaisEmpre)); }
        }

        public string SelectIndexCiudadEmpre
        {
            get { return _selectIndexCiudadEmpre; }
            set { _selectIndexCiudadEmpre = value; OnPropertyChanged(nameof(SelectIndexCiudadEmpre)); }
        }

        public ImageSource FotoGaleria
        {
            get { return _fotoGaleria; }
            set
            {
                _fotoGaleria = value;
                OnPropertyChanged(nameof(FotoGaleria));
            }
        }
        public bool FormEmpresa
        {
            get { return _FormEmpresa; }
            set
            {
                _FormEmpresa = value;
                OnPropertyChanged(nameof(FormEmpresa));
            }
        }
        public bool FormUsuario
        {
            get { return _FormUsuario; }
            set
            {
                _FormUsuario = value;
                OnPropertyChanged(nameof(FormUsuario));
            }
        }

        public bool Facebook
        {
            get { return _facebook; }
            set
            {
                _facebook = value;
                OnPropertyChanged(nameof(Facebook));
            }
        }

        public bool Google
        {
            get { return _google; }
            set
            {
                _google = value;
                OnPropertyChanged(nameof(Google));
            }
        }

        public bool Apple
        {
            get { return _apple; }
            set
            {
                _apple = value;
                OnPropertyChanged(nameof(Apple));
            }
        }

        public ListasGeneral SelectPaisEmpre
        {
            get { return _selectPaisEmpr; }
            set
            {
                SetProperty(ref _selectPaisEmpr, value);
                ActualizarUsuario.IdPaisemp = int.Parse(_selectPaisEmpr.Id);
                _ = CargarListaciudadByPais(_selectPaisEmpr.Id);
                OnPropertyChanged(nameof(SelectPaisEmpre));

                if (value != null)
                {
                    ActualizarUsuarioIOS.IdPaisemp = int.Parse(_selectPaisEmpr.Id);
                    ActualizarUsuario.IdPaisemp = int.Parse(_selectPaisEmpr.Id);
                }

            }
        }

        public ListasGeneral SelectCiudadEmpresa
        {
            get { return _selectCiudadEmpre; }
            set
            {
                SetProperty(ref _selectCiudadEmpre, value);
                OnPropertyChanged(nameof(SelectCiudadEmpresa));
                if (value != null)
                {
                    ActualizarUsuario.IdCiudademp = int.Parse(_selectCiudadEmpre.Id);
                    ActualizarUsuarioIOS.IdCiudademp = int.Parse(_selectCiudadEmpre.Id);
                }

            }
        }

        public ListasGeneral SelectPaisUsu
        {
            get { return _selectPaisUsu; }
            set
            {
                SetProperty(ref _selectPaisUsu, value);
                if (value != null)
                {
                    ActualizarUsuario.IdPais = int.Parse(_selectPaisUsu.Id);
                    ActualizarUsuarioIOS.IdPais = int.Parse(_selectPaisUsu.Id);
                }

            }
        }
        public ListasGeneral SelectCiudadUsu
        {
            get { return _selectCiudadUsu; }
            set
            {
                SetProperty(ref _selectCiudadUsu, value);
                if (value != null)
                {
                    ActualizarUsuario.IdCiudad = int.Parse(_selectCiudadUsu.Id);
                    ActualizarUsuarioIOS.IdCiudad = int.Parse(_selectCiudadUsu.Id);
                }
            }
        }
        public ListasGeneral SelectProfesionUsu
        {
            get { return _selectProfesionUsu; }
            set
            {
                SetProperty(ref _selectProfesionUsu, value);
                if (value != null)
                {
                    ActualizarUsuario.IdProfesion = int.Parse(_selectProfesionUsu.Id);
                    ActualizarUsuarioIOS.IdProfesion = int.Parse(_selectProfesionUsu.Id);
                }
            }
        }
        public ListasGeneral SelectSectorEcoUsu
        {
            get { return _selectSectorEcoUsu; }
            set
            {
                SetProperty(ref _selectSectorEcoUsu, value);
                if (value != null)
                {
                    ActualizarUsuario.IdSectorEconomico = int.Parse(_selectSectorEcoUsu.Id);
                    ActualizarUsuarioIOS.IdSectorEconomico = int.Parse(_selectSectorEcoUsu.Id);
                }

            }
        }
        public ObservableCollection<ListasGeneral> ListaPaisUsu
        {
            get { return _listaPaisUsu; }
            set
            {
                _listaPaisUsu = value;
                OnPropertyChanged(nameof(ListaPaisUsu));
            }
        }
        public ObservableCollection<ListasGeneral> ListaCiudadRecintoUsu
        {
            get { return _listaCiudadRecintoUsu; }
            set
            {
                _listaCiudadRecintoUsu = value;
                OnPropertyChanged(nameof(ListaCiudadRecintoUsu));
            }
        }
        public ObservableCollection<ListasGeneral> ListaTipoIdUsu
        {
            get { return _listaTipoIdUsu; }
            set
            {
                _listaTipoIdUsu = value;
                OnPropertyChanged(nameof(ListaTipoIdUsu));
            }
        }
        public ObservableCollection<ListasGeneral> ListaProfesionUsu
        {
            get { return _listaProfesionUsu; }
            set
            {
                _listaProfesionUsu = value;
                OnPropertyChanged(nameof(ListaProfesionUsu));
            }
        }
        public ObservableCollection<ListasGeneral> ListaSectorEcoUsu
        {
            get { return _listaSectorEcoUsu; }
            set
            {
                _listaSectorEcoUsu = value;
                OnPropertyChanged(nameof(ListaSectorEcoUsu));
            }
        }
        public ObservableCollection<ListasGeneral> ListaPaisEmpre
        {
            get { return _listaPaisEmpre; }
            set
            {
                _listaPaisEmpre = value;
                OnPropertyChanged(nameof(ListaPaisEmpre));
            }
        }
        public ObservableCollection<ListasGeneral> ListaCiudadEmpre
        {
            get { return _listaCiudadEmpre; }
            set
            {
                _listaCiudadEmpre = value;
                OnPropertyChanged(nameof(ListaCiudadEmpre));
            }
        }

        public ICommand Notificacion_correo { get; }
        public ICommand Edit_Clave { get; }
        public ICommand Registrarse { get; }
        public ICommand Edit_google { get; }
        public ICommand Form_Empresa { get; }
        public ICommand Form_Usuario { get; }
        public ICommand Cambiar_Foto { get; }
        public ICommand Edit_facebook { get; }
        public ICommand Edit_apple { get; }
        public ICommand Terminos { get; }
        public ICommand Politicas { get; }


        public EditarPerfilVm()
        {
            IsBusy = true;

            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();
            ActualizarUsuario = new ActualizarUsuario();
            ActualizarUsuarioIOS = new ActualizarInsertaUsuario();

            EmailUsuario = Preferences.Get("Email", "");
            CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            ImagenSplash = logicaWs.ImgMenuSuperior_Mtd();

            Form_Empresa = new Command(Form_Empresa_Mto);
            Registrarse = new Command(async () => await Registro_Mto());
            Cambiar_Foto = new Command(async () => await Cambiar_Foto_Mto());
            Form_Usuario = new Command(async () => await Form_Usuario_Mto());
            Edit_Clave = new Command(async () => await Edit_Clave_MtoAsync());
            Edit_google = new Command(async () => await Edit_google_MtoAsync());
            Edit_facebook = new Command(async () => await Edit_facebook_MtoAsync());
            Edit_apple = new Command(async () => await Edit_apple_MtoAsync());
            Notificacion_correo = new Command(async () => await Notificacion_correo_Mto());

            Terminos = new Command(async () => await pageServicio.DisplayAlert(terminos.Modulo, terminos.Texto, AppResources.cerrar));
            Politicas = new Command(async () => await pageServicio.DisplayAlert(politicas.Modulo, politicas.Texto, AppResources.cerrar));

            terminos = new TerminosCondiciones();
            politicas = new TerminosCondiciones();

            Inicializar();
            ContadorNotificaciones_Mtd();
        }

        //INICIA CARGA INCIAL
        private async void Inicializar()
        {
            try
            {
                VerTerminos = false;
                FormUsuario = true;
                EntFechaNacimientoUsu = DateTime.Now;

                await CargarInfoUsuario();
                await CargarInfoRedSocial();
                await CargarListasUsuario();
            }
            catch (Exception ex)
            {
                IsBusy = false;
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "Inicializar", "n/a");
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

                    if (item.GetValue("nombrecompleto")?.ToString() != "null" && item.GetValue("nombrecompleto")?.ToString() != "" && item.GetValue("nombrecompleto")?.ToString() != "")
                    {
                        EntNombreUsu = item.GetValue("nombrecompleto").ToString();
                        ActualizarUsuario.NombreCompleto = item.GetValue("nombrecompleto")?.ToString();
                    }

                    if (item.GetValue("idtipoidentificacion")?.ToString() != null && item.GetValue("idtipoidentificacion")?.ToString() != "" && item.GetValue("idtipoidentificacion")?.ToString() != "")
                    {
                        ActualizarUsuarioIOS.IdIdentificacion = item.GetValue("idtipoidentificacion")?.ToString();
                    }


                    if (item.GetValue("tipoidentificacion")?.ToString() != "null" && item.GetValue("tipoidentificacion")?.ToString() != "" && item.GetValue("tipoidentificacion")?.ToString() != "")
                    {
                        EnTipoIdUsuS = item.GetValue("tipoidentificacion").ToString();
                    }

                    if (item.GetValue("Nidentificacion")?.ToString() != "null" && item.GetValue("Nidentificacion")?.ToString() != "" && item.GetValue("Nidentificacion")?.ToString() != "")
                    {
                        EntIdUsu = item.GetValue("Nidentificacion").ToString();
                    }

                    if (item.GetValue("Email")?.ToString() != "null" && item.GetValue("Email")?.ToString() != "" && item.GetValue("Email")?.ToString() != "")
                    {
                        EntCorreoUsu = item.GetValue("Email").ToString();
                    }

                    if (item.GetValue("celular")?.ToString() != "null" && item.GetValue("celular")?.ToString() != "" && item.GetValue("celular")?.ToString() != "" && item.GetValue("celular")?.ToString() != "0")
                    {
                        EntCelularUsu = item.GetValue("celular").ToString();
                        ActualizarUsuario.Celular = item.GetValue("celular")?.ToString();
                    }
                    else
                    {
                        ActualizarUsuario.Celular = "0";
                    }

                    if (item.GetValue("fechanacimiento")?.ToString() != "null" && item.GetValue("fechanacimiento")?.ToString() != "" && item.GetValue("fechanacimiento")?.ToString() != "")
                    {
                        FechaNacimiento = item.GetValue("fechanacimiento").ToString();
                        EntFechaNacimientoUsu = DateTime.ParseExact(FechaNacimiento, "dd/MM/yyyy", null);
                        ActualizarUsuario.FechaNacimiento = item.GetValue("fechanacimiento")?.ToString(); ;
                    }
                    else
                    {
                        ActualizarUsuario.FechaNacimiento = "0";
                    }

                    if (item.GetValue("CiudadR")?.ToString() != "null" && item.GetValue("CiudadR")?.ToString() != "" && item.GetValue("CiudadR")?.ToString() != "" && item.GetValue("CiudadR")?.ToString() != "0")
                    {
                        ActualizarUsuario.IdCiudad = int.Parse(item.GetValue("CiudadR").ToString());
                    }
                    else
                    {
                        ActualizarUsuario.IdCiudad = 0;
                    }

                    if (item.GetValue("PaisR")?.ToString() != "null" && item.GetValue("PaisR")?.ToString() != "" && item.GetValue("PaisR")?.ToString() != "" && item.GetValue("PaisR")?.ToString() != "0")
                    {
                        ActualizarUsuario.IdPais = int.Parse(item.GetValue("PaisR").ToString());
                    }
                    else
                    {
                        ActualizarUsuario.IdPais = 0;
                    }

                    if (item.GetValue("idsector")?.ToString() != "null" && item.GetValue("idsector")?.ToString() != "" && item.GetValue("idsector")?.ToString() != "" && item.GetValue("idsector")?.ToString() != "0")
                    {
                        ActualizarUsuario.IdSectorEconomico = int.Parse(item.GetValue("idsector").ToString());
                    }
                    else
                    {
                        ActualizarUsuario.IdSectorEconomico = 0;
                    }

                    if (item.GetValue("idprofesion")?.ToString() != "null" && item.GetValue("idprofesion")?.ToString() != "" && item.GetValue("idprofesion")?.ToString() != "" && item.GetValue("idprofesion")?.ToString() != "0")
                    {
                        ActualizarUsuario.IdProfesion = int.Parse(item.GetValue("idprofesion").ToString());
                    }
                    else
                    {
                        ActualizarUsuario.IdProfesion = 0;
                    }

                    if (item.GetValue("nombreempresa")?.ToString() != "null" && item.GetValue("nombreempresa")?.ToString() != "" && item.GetValue("nombreempresa")?.ToString() != "" && item.GetValue("nombreempresa")?.ToString() != "0")
                    {
                        EntNombreEmpr = item.GetValue("nombreempresa").ToString();
                        ActualizarUsuario.NombreEmpresa = item.GetValue("nombreempresa")?.ToString();
                    }
                    else
                    {
                        ActualizarUsuario.NombreEmpresa = "";
                    }

                    if (item.GetValue("identificacionempresa")?.ToString() != "null" && item.GetValue("identificacionempresa")?.ToString() != "" && item.GetValue("identificacionempresa")?.ToString() != "" && item.GetValue("identificacionempresa")?.ToString() != "0")
                    {
                        EntIdEmpr = item.GetValue("identificacionempresa").ToString();
                        ActualizarUsuario.IdentificacionEmpresa = item.GetValue("identificacionempresa")?.ToString();
                    }
                    else
                    {
                        ActualizarUsuario.IdentificacionEmpresa = "0";
                    }

                    if (item.GetValue("telefonoEmpresa")?.ToString() != "null" && item.GetValue("telefonoEmpresa")?.ToString() != "" && item.GetValue("telefonoEmpresa")?.ToString() != "" && item.GetValue("telefonoEmpresa")?.ToString() != "0")
                    {
                        EntTelEmpr = item.GetValue("telefonoEmpresa").ToString();
                        ActualizarUsuario.Telefono = item.GetValue("telefonoEmpresa")?.ToString();
                    }
                    else
                    {
                        ActualizarUsuario.Telefono = "0";
                    }

                    if (item.GetValue("indicativopais")?.ToString() != "null" && item.GetValue("indicativopais")?.ToString() != "" && item.GetValue("indicativopais")?.ToString() != "" && item.GetValue("indicativopais")?.ToString() != "0")
                    {
                        EntTelPaisEmpr = item.GetValue("indicativopais").ToString();
                        ActualizarUsuario.IndicativoPais = int.Parse(item.GetValue("indicativopais").ToString());
                    }
                    else
                    {
                        ActualizarUsuario.IndicativoPais = 0;
                    }

                    if (item.GetValue("area")?.ToString() != "null" && item.GetValue("area")?.ToString() != "" && item.GetValue("area")?.ToString() != "" && item.GetValue("area")?.ToString() != "0")
                    {
                        EntTelAreaEmpr = item.GetValue("area").ToString();
                        ActualizarUsuario.Area = int.Parse(item.GetValue("area").ToString());

                    }
                    else
                    {
                        ActualizarUsuario.Area = 0;
                    }

                    if (item.GetValue("PaisER")?.ToString() != "null" && item.GetValue("PaisER")?.ToString() != "" && item.GetValue("PaisER")?.ToString() != "" && item.GetValue("PaisER")?.ToString() != "0")
                    {
                        ActualizarUsuario.IdPaisemp = int.Parse(item.GetValue("PaisER").ToString());
                    }
                    else
                    {
                        ActualizarUsuario.IdPaisemp = 0;
                    }

                    if (item.GetValue("CiudadER")?.ToString() != "null" && item.GetValue("CiudadER")?.ToString() != "" && item.GetValue("CiudadER")?.ToString() != "0")
                    {
                        ActualizarUsuario.IdCiudademp = int.Parse(item.GetValue("CiudadER").ToString());
                    }
                    else
                    {
                        ActualizarUsuario.IdCiudademp = 0;
                    }

                    if (item.GetValue("direccionEmpresa")?.ToString() != "null" && item.GetValue("direccionEmpresa")?.ToString() != "" && item.GetValue("direccionEmpresa")?.ToString() != "" && item.GetValue("direccionEmpresa")?.ToString() != "0")
                    {
                        EntDireccionEmpr = item.GetValue("direccionEmpresa").ToString();
                        ActualizarUsuario.Direccion = item.GetValue("direccionEmpresa")?.ToString();
                    }
                    else
                    {
                        ActualizarUsuario.Direccion = "";
                    }

                    if (item.GetValue("codigopostal")?.ToString() != "null" && item.GetValue("codigopostal")?.ToString() != "" && item.GetValue("codigopostal")?.ToString() != "" && item.GetValue("codigopostal")?.ToString() != "0")
                    {
                        EntCodPostalEmpr = item.GetValue("codigopostal").ToString();
                        ActualizarUsuario.CodigoPostal = int.Parse(item.GetValue("codigopostal").ToString());
                    }
                    else
                    {
                        ActualizarUsuario.CodigoPostal = 0;
                    }

                    if (item.GetValue("cargoEmpresa")?.ToString() != "null" && item.GetValue("cargoEmpresa")?.ToString() != "" && item.GetValue("cargoEmpresa")?.ToString() != "")
                    {
                        EntCargoEmpr = item.GetValue("cargoEmpresa").ToString();
                        ActualizarUsuario.Cargo = item.GetValue("cargoEmpresa")?.ToString();
                    }
                    else
                    {
                        ActualizarUsuario.Cargo = "";
                    }

                    if (item.GetValue("imagen")?.ToString() != "null" && item.GetValue("imagen")?.ToString() != "" && item.GetValue("imagen")?.ToString() != "")
                    {
                        FotoGaleria = item.GetValue("imagen").ToString();
                        ActualizarUsuario.Imagen = item.GetValue("imagen")?.ToString();
                    }
                    else
                    {
                        ActualizarUsuario.Imagen = "";
                    }

                    ValidarCamposiOS(EntCorreoUsu, EntIdUsu);
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "CargarInfoUsuario", "consultausuario");
            }
        }

        private async Task CargarInfoRedSocial()
        {
            try
            {
                /*Cargar informacion de los perfiles */
                string urli = logicaWs.Movile_select_RedSocialUsu_Mtd(EmailUsuario);
                string jsonProcedimiento = await logicaWs.ConectionPost("", urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                ListaRedSocial = new ObservableCollection<ListasGeneral>(); // crear una nueva lista
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Descripcion = item.GetValue("NombreRed")?.ToString(),
                        Id = item.GetValue("IdRedSocial")?.ToString()
                    };
                    ListaRedSocial.Add(listasGeneral);
                }

                if (ListaRedSocial.Count > 0)
                {
                    foreach (var aux in ListaRedSocial)
                    {
                        if (aux.Id == "1")
                        {
                            Facebook = true;
                        }
                        if (aux.Id == "2")
                        {
                            Google = true;
                        }
                        if (aux.Id == "3")
                        {
                            Apple = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "CargarInfoRedSocial", "consultaredsocialusuario");
            }
        }

        private async Task CargarListasUsuario()
        {
            try
            {

                /* ciudad recinto usuario */
                ListaCiudadRecintoUsu = new ObservableCollection<ListasGeneral>();
                string urli = logicaWs.Movile_select_CiudadRecinto_Mtd();
                string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                int auxCont = 0;
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Descripcion = item.GetValue("nombreCiudad")?.ToString(),
                        Id = item.GetValue("idCiudad")?.ToString()
                    };
                    ListaCiudadRecintoUsu.Add(listasGeneral);

                }
                ListaCiudadRecintoUsu = new ObservableCollection<ListasGeneral>(ListaCiudadRecintoUsu.OrderBy(x => x.Descripcion).ToList());
                foreach (ListasGeneral item in ListaCiudadRecintoUsu)
                {
                    if (ActualizarUsuario.IdCiudad.ToString() != "0")
                    {
                        if (ActualizarUsuario.IdCiudad.ToString() == item.Id)
                        {
                            SelectIndexCiudad = auxCont.ToString();
                        }
                    }
                    auxCont += 1;
                }

                /*Cargar lista paises */
                ListaPaisUsu = new ObservableCollection<ListasGeneral>();
                urli = logicaWs.Movile_select_Pais_Mtd();
                jsonProcedimiento = await logicaWs.ConectionGet(urli);
                jsArray = JArray.Parse(jsonProcedimiento);
                auxCont = 0;
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Descripcion = item.GetValue("Nombre_Pais")?.ToString(),
                        Id = item.GetValue("idPais")?.ToString()
                    };
                    ListaPaisUsu.Add(listasGeneral);
                }
                ListaPaisUsu = new ObservableCollection<ListasGeneral>(ListaPaisUsu.OrderBy(x => x.Descripcion).ToList());
                foreach (ListasGeneral item in ListaPaisUsu)
                {
                    if (ActualizarUsuario.IdPais.ToString() != "0")
                    {
                        if (ActualizarUsuario.IdPais.ToString() == item.Id)
                        {
                            SelectIndexPais = auxCont.ToString();
                        }
                    }
                    auxCont += 1;
                }

                /*Cargar lista profesion */
                ListaProfesionUsu = new ObservableCollection<ListasGeneral>();
                urli = logicaWs.Movile_select_Profesion_Mtd();
                jsonProcedimiento = await logicaWs.ConectionGet(urli);
                jsArray = JArray.Parse(jsonProcedimiento);
                auxCont = 0;
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Descripcion = item.GetValue("NombreProfesion")?.ToString(),
                        Id = item.GetValue("idProfesion")?.ToString()
                    };
                    ListaProfesionUsu.Add(listasGeneral);

                }
                ListaProfesionUsu = new ObservableCollection<ListasGeneral>(ListaProfesionUsu.OrderBy(x => x.Descripcion).ToList());
                foreach (ListasGeneral item in ListaProfesionUsu)
                {
                    if (ActualizarUsuario.IdProfesion.ToString() != "0")
                    {
                        if (ActualizarUsuario.IdProfesion.ToString() == item.Id)
                        {
                            SelectIndexProfesion = auxCont.ToString();
                        }
                    }
                    auxCont += 1;
                }

                /*Cargar lista sector economico */
                ListaSectorEcoUsu = new ObservableCollection<ListasGeneral>();
                urli = logicaWs.Movile_select_SectoEcono_Mtd();
                jsonProcedimiento = await logicaWs.ConectionGet(urli);
                jsArray = JArray.Parse(jsonProcedimiento);
                auxCont = 0;
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Descripcion = item.GetValue("NombreSectorEconomico")?.ToString(),
                        Id = item.GetValue("idSectorEconomico")?.ToString()
                    };
                    ListaSectorEcoUsu.Add(listasGeneral);

                }

                ListaSectorEcoUsu = new ObservableCollection<ListasGeneral>(ListaSectorEcoUsu.OrderBy(x => x.Descripcion).ToList());
                foreach (ListasGeneral item in ListaSectorEcoUsu)
                {
                    if (ActualizarUsuario.IdSectorEconomico.ToString() != "0")
                    {
                        if (ActualizarUsuario.IdSectorEconomico.ToString() == item.Id)
                        {
                            SelectIndexSector = auxCont.ToString();
                        }
                    }
                    auxCont += 1;
                }

                /*Cargar lista pais empresa */
                ListaPaisEmpre = new ObservableCollection<ListasGeneral>();
                urli = logicaWs.Movile_select_Pais_Mtd();
                jsonProcedimiento = await logicaWs.ConectionGet(urli);
                jsArray = JArray.Parse(jsonProcedimiento);
                auxCont = 0;
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Descripcion = item.GetValue("Nombre_Pais")?.ToString(),
                        Id = item.GetValue("idPais")?.ToString()
                    };
                    ListaPaisEmpre.Add(listasGeneral);
                }

                ListaPaisEmpre = new ObservableCollection<ListasGeneral>(ListaPaisEmpre.OrderBy(x => x.Descripcion).ToList());
                foreach (ListasGeneral item in ListaPaisEmpre)
                {

                    if (ActualizarUsuario.IdPaisemp.ToString() != "0")
                    {
                        if (ActualizarUsuario.IdPaisemp.ToString() == item.Id)
                        {
                            SelectIndexPaisEmpre = auxCont.ToString();
                        }
                    }
                    auxCont += 1;
                }

                /*Cargar lista Tipo de identificacion */
                ListaTipoIdUsu = new ObservableCollection<ListasGeneral>();
                urli = logicaWs.Movile_select_TipoId_Mtd(); //consultarpaises
                jsonProcedimiento = await logicaWs.ConectionGet(urli);
                jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral { Descripcion = item.GetValue("NombreIdentificacion")?.ToString(), Id = item.GetValue("idIdentificacion")?.ToString() };
                    ListaTipoIdUsu.Add(listasGeneral);
                }
                ListaTipoIdUsu = new ObservableCollection<ListasGeneral>(ListaTipoIdUsu.OrderBy(x => x.Descripcion).ToList());

            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "CargarListasUsuario", "multilistas de perfil");
            }
        }

        private void ValidarCamposiOS(string Correo, string Identificacion)
        {
            try
            {
                correoViejo = Correo;

                string s2 = "corferias@ios.com";
                bool res = Correo.ToLower().Contains(s2.ToLower());

                string s3 = "appleid.com";
                bool res3 = Correo.ToLower().Contains(s3.ToLower());

                if (res || res3)
                {
                    EntCorreo = "";
                    VerTerminos = true;
                    VerCorreoBloq = false;
                    VerCorreoDesBloq = true;
                    _ = ConsultaPoliticasAsync();
                }
                else
                {
                    EntCorreo = Correo;
                    VerCorreoBloq = true;
                    VerCorreoDesBloq = false;
                }
            }
            catch (Exception ex)
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "ValidarCamposiOS", "no aplica - " + Correo);
            }

            try
            {
                long aux = long.Parse(Identificacion);
                if (aux < 1)
                {
                    EntIdentificacio = "";
                    VerTerminos = true;
                    VerIdentificacionBloq = false;
                    VerIdentificacionDesBloq = true;
                    _ = ConsultaPoliticasAsync();
                }
                else
                {
                    SelecttipoDocumento = new ListasGeneral();
                    EntIdentificacio = Identificacion;
                    SelecttipoDocumento.Id = "1";
                    VerIdentificacionBloq = true;
                    VerIdentificacionDesBloq = false;
                }
            }
            catch (Exception ex)
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "ValidarCamposiOS", "identificacion mal - " + Identificacion);
            }
        }

        private async Task ConsultaPoliticasAsync()
        {
            if (!StateConexion)
            {
                try
                {
                    string urli = logicaWs.Movile_Select_Terminos_Politicas_Mtd("1", "50", LenguajeBase);
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

                    await pageServicio.DisplayAlert("Corferias", AppResources.VMServicioEnMantenimiento, AppResources.cerrar);
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroVM", "ConsultaPoliticasAsync", "consultaterminosaplicacion");
                }

                try
                {

                    string urli = logicaWs.Movile_Select_Terminos_Politicas_Mtd("2", "50", LenguajeBase);
                    string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                    JArray jsArray = JArray.Parse(jsonProcedimiento);

                    foreach (JObject item in jsArray)
                    {
                        politicas.Max = item.GetValue("Max")?.ToString();
                        politicas.Modulo = item.GetValue("Modulo")?.ToString();
                        politicas.Texto = item.GetValue("Texto")?.ToString();
                        politicas.FechaPublica = item.GetValue("FechaPublica")?.ToString();
                        politicas.IdModulo = item.GetValue("IdModulo")?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    politicas.Max = "0";
                    politicas.Modulo = AppResources.politicaDeTratamientoDeDatos;
                    politicas.Texto = AppResources.VMEstamosCargandoInfo;
                    politicas.FechaPublica = "0";
                    politicas.IdModulo = "0";

                    await pageServicio.DisplayAlert("Corferias", AppResources.VMServicioEnMantenimiento, AppResources.cerrar);
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroVM", "ConsultaPoliticasAsync", "consultaterminosaplicacion");
                }
            }
        }

        private async Task Notificacion_correo_Mto()
        {
            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElCorreoElectronicoDebeMas, AppResources.cerrar);
        }

        //TERMINA CARGA INCIAL

        private void Form_Empresa_Mto()
        {
            FormEmpresa = false;
            FormUsuario = true;
        }

        private async Task Form_Usuario_Mto()
        {
            bool res = await ValidarActualizacion_Mto();
            if (res)
            {
                FormEmpresa = true;
                FormUsuario = false;
            }
        }

        private async Task Registro_Mto()

        {

            if (await ValidarActualizacion_Mto())
            {
                if (VerIdentificacionDesBloq || VerCorreoDesBloq)
                {
                    try
                    {
                        IsBusy = true;

                        string urli = logicaWs.Insert_Usuario_ios_Mtd("1", correoViejo);
                        string json = JsonConvert.SerializeObject(ActualizarUsuarioIOS);
                        string jsonProcedimientoDos = await logicaWs.ConectionPost(json, urli);

                        if (jsonProcedimientoDos == "No fue posible ejecutar los datos, verifique el Log para validar la inconsistencia")
                        {
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoSeHanPodidoActualizarLosDatos, AppResources.VMaceptar);
                            claseBase.InsertarLogs_Mtd("ERROR", jsonProcedimientoDos, "EditarPerfilVm", "Registro_Mto", "actualizausuario");
                        }
                        else
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, jsonProcedimientoDos, AppResources.VMaceptar);


                        urli = logicaWs.Movile_select_Perfil_Mtd(ActualizarUsuarioIOS.Email);
                        json = JsonConvert.SerializeObject(" ");
                        string jsonProcedimiento = await logicaWs.ConectionPost(json, urli);
                        JArray jsArray = JArray.Parse(jsonProcedimiento);

                        if (jsArray.Count > 0)
                        {
                            foreach (JObject item in jsArray)
                            {
                                if (!string.IsNullOrWhiteSpace(item.GetValue("Nidentificacion")?.ToString()))
                                {
                                    if (item.GetValue("Nidentificacion")?.ToString().ToLower() != "null")
                                    {

                                        string resAUX = jsonProcedimientoDos.Substring(1, jsonProcedimientoDos.Length - 2);

                                        if (resAUX == "Valide la información, este número y tipo de identificación o el correo ya se encuentran registrados")
                                        {
                                            urli = logicaWs.Movile_select_Perfil_Mtd(correoViejo);
                                            json = JsonConvert.SerializeObject(" ");
                                            jsonProcedimiento = await logicaWs.ConectionPost(json, urli);
                                            jsArray = JArray.Parse(jsonProcedimiento);

                                            Preferences.Set("NombreCompleto", item.GetValue("nombrecompleto")?.ToString());
                                            Preferences.Set("IdCiudad", item.GetValue("CiudadR")?.ToString());
                                            Preferences.Set("IdCiudadDesc", item.GetValue("ciudad")?.ToString());
                                            Preferences.Set("Celular", item.GetValue("celular")?.ToString());
                                            Preferences.Set("Email", correoViejo.Trim());
                                            Preferences.Set("IdIdentificacion", item.GetValue("tipoidentificacion")?.ToString());
                                            Preferences.Set("numeroIdentificacion", item.GetValue("Nidentificacion")?.ToString());
                                        }
                                        else
                                        {
                                            Preferences.Set("NombreCompleto", item.GetValue("nombrecompleto")?.ToString());
                                            Preferences.Set("IdCiudad", item.GetValue("CiudadR")?.ToString());
                                            Preferences.Set("IdCiudadDesc", item.GetValue("ciudad")?.ToString());
                                            Preferences.Set("Celular", item.GetValue("celular")?.ToString());
                                            Preferences.Set("Email", ActualizarUsuarioIOS.Email.Trim());
                                            Preferences.Set("IdIdentificacion", item.GetValue("tipoidentificacion")?.ToString());
                                            Preferences.Set("numeroIdentificacion", item.GetValue("Nidentificacion")?.ToString());
                                        }
                                    }
                                    else
                                    {
                                        urli = logicaWs.Movile_select_Perfil_Mtd(correoViejo);
                                        json = JsonConvert.SerializeObject(" ");
                                        jsonProcedimiento = await logicaWs.ConectionPost(json, urli);
                                        jsArray = JArray.Parse(jsonProcedimiento);

                                        foreach (JObject itemTres in jsArray)
                                        {
                                            Preferences.Set("NombreCompleto", itemTres.GetValue("nombrecompleto")?.ToString());
                                            Preferences.Set("IdCiudad", itemTres.GetValue("CiudadR")?.ToString());
                                            Preferences.Set("IdCiudadDesc", itemTres.GetValue("ciudad")?.ToString());
                                            Preferences.Set("Celular", itemTres.GetValue("celular")?.ToString());
                                            Preferences.Set("Email", correoViejo);
                                            Preferences.Set("IdIdentificacion", itemTres.GetValue("tipoidentificacion")?.ToString());
                                            Preferences.Set("numeroIdentificacion", itemTres.GetValue("Nidentificacion")?.ToString());
                                        }
                                    }

                                }
                                else
                                {
                                    urli = logicaWs.Movile_select_Perfil_Mtd(correoViejo);
                                    json = JsonConvert.SerializeObject(" ");
                                    jsonProcedimiento = await logicaWs.ConectionPost(json, urli);
                                    jsArray = JArray.Parse(jsonProcedimiento);

                                    foreach (JObject itemDos in jsArray)
                                    {
                                        Preferences.Set("NombreCompleto", itemDos.GetValue("nombrecompleto")?.ToString());
                                        Preferences.Set("IdCiudad", itemDos.GetValue("CiudadR")?.ToString());
                                        Preferences.Set("IdCiudadDesc", itemDos.GetValue("ciudad")?.ToString());
                                        Preferences.Set("Celular", itemDos.GetValue("celular")?.ToString());
                                        Preferences.Set("Email", correoViejo);
                                        Preferences.Set("IdIdentificacion", itemDos.GetValue("tipoidentificacion")?.ToString());
                                        Preferences.Set("numeroIdentificacion", itemDos.GetValue("Nidentificacion")?.ToString());
                                    }
                                }
                            }
                        }
                        else
                        {
                            urli = logicaWs.Movile_select_Perfil_Mtd(correoViejo);
                            json = JsonConvert.SerializeObject(" ");
                            jsonProcedimiento = await logicaWs.ConectionPost(json, urli);
                            jsArray = JArray.Parse(jsonProcedimiento);

                            foreach (JObject item in jsArray)
                            {
                                Preferences.Set("NombreCompleto", item.GetValue("nombrecompleto")?.ToString());
                                Preferences.Set("IdCiudad", item.GetValue("CiudadR")?.ToString());
                                Preferences.Set("IdCiudadDesc", item.GetValue("ciudad")?.ToString());
                                Preferences.Set("Celular", item.GetValue("celular")?.ToString());
                                Preferences.Set("Email", correoViejo);
                                Preferences.Set("IdIdentificacion", item.GetValue("tipoidentificacion")?.ToString());
                                Preferences.Set("numeroIdentificacion", item.GetValue("Nidentificacion")?.ToString());
                            }
                        }

                        await CheckTerminosYCondiciones();

                        await RootNavigation.Navigation.PopToRootAsync(false);
                        RootMainPage.Detail = new NavigationPage(new PerfilMenuView());

                    }
                    catch (Exception ex)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoSeHanPodidoActualizarLosDatos, AppResources.VMaceptar);
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "Registro_Mto", "actualizausuario");
                    }
                }
                else
                {
                    try
                    {
                        string urli = logicaWs.Movile_Update_Usuario_Mtd(EmailUsuario);
                        ActualizarUsuario.Imagen = ActualizarUsuario.Imagen.Replace("\"", "");
                        string json = JsonConvert.SerializeObject(ActualizarUsuario);
                        string jsonProcedimiento = await logicaWs.ConectionPost(json, urli);

                        if (jsonProcedimiento == "No fue posible ejecutar los datos, verifique el Log para validar la inconsistencia")
                        {
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoSeHanPodidoActualizarLosDatos, AppResources.VMaceptar);
                            claseBase.InsertarLogs_Mtd("ERROR", jsonProcedimiento, "EditarPerfilVm", "Registro_Mto", "actualizausuario");
                        }
                        else
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, jsonProcedimiento, AppResources.VMaceptar);


                        urli = logicaWs.Movile_select_Perfil_Mtd(EmailUsuario);
                        json = JsonConvert.SerializeObject(" ");
                        jsonProcedimiento = await logicaWs.ConectionPost(json, urli);
                        JArray jsArray = JArray.Parse(jsonProcedimiento);

                        if (jsArray.Count > 0)
                        {
                            foreach (JObject item in jsArray)
                            {
                                if (!string.IsNullOrWhiteSpace(item.GetValue("Nidentificacion")?.ToString()))
                                {
                                    if (item.GetValue("Nidentificacion")?.ToString().ToLower() != "null")
                                    {
                                        Preferences.Set("NombreCompleto", item.GetValue("nombrecompleto")?.ToString());
                                        Preferences.Set("IdCiudad", item.GetValue("CiudadR")?.ToString());
                                        Preferences.Set("IdCiudadDesc", item.GetValue("ciudad")?.ToString());
                                        Preferences.Set("Celular", item.GetValue("celular")?.ToString());
                                        Preferences.Set("Email", EmailUsuario);
                                        Preferences.Set("IdIdentificacion", item.GetValue("tipoidentificacion")?.ToString());
                                        Preferences.Set("numeroIdentificacion", item.GetValue("Nidentificacion")?.ToString());

                                        await RootNavigation.Navigation.PopToRootAsync(false);
                                        RootMainPage.Detail = new NavigationPage(new PerfilMenuView());
                                    }
                                    else
                                    {
                                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoSeHanPodidoActualizarLosDatos, AppResources.VMaceptar);
                                        claseBase.InsertarLogs_Mtd("ERROR", "no trae infromaciob jsArray" + EmailUsuario, "EditarPerfilVm", "Registro_Mto", "PRB 2");
                                    }
                                }
                                else
                                {
                                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoSeHanPodidoActualizarLosDatos, AppResources.VMaceptar);
                                    claseBase.InsertarLogs_Mtd("ERROR", "no trae infromaciob jsArray" + EmailUsuario, "EditarPerfilVm", "Registro_Mto", "PRB 2");
                                }
                            }
                        }
                        else
                        {
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoSeHanPodidoActualizarLosDatos, AppResources.VMaceptar);
                            claseBase.InsertarLogs_Mtd("ERROR", "no trae infromaciob jsArray" + EmailUsuario, "EditarPerfilVm", "Registro_Mto", "actualizausuario 1");

                        }
                    }
                    catch (Exception ex)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "Registro_Mto", "actualizausuario");
                    }
                }

            }
        }

        private async Task<bool> ValidarActualizacion_Mto()
        {
            char[] charsToTrim = { ' ' };
            string expreregularletras = @"^([a-zA-Z ñáéíóúÑÁÉÍÓÚ]{1,99})$";
            string expreregularcorreos = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            if (VerCorreoDesBloq || VerIdentificacionDesBloq)
            {
                if (string.IsNullOrWhiteSpace(EntCorreo) || string.IsNullOrWhiteSpace(EntIdentificacio) || EntFechaNacimientoUsu == null)
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVacios, AppResources.cerrar);
                    return false;
                }

                ActualizarUsuarioIOS.Identificacion = EntIdentificacio;
                ActualizarUsuarioIOS.Email = EntCorreo;

                if (SelectPaisUsu == null || SelectCiudadUsu == null)
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVacios, AppResources.cerrar);
                    return false;
                }

                if (VerIdentificacionDesBloq)
                {
                    if (SelecttipoDocumento == null)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVacios, AppResources.cerrar);
                        return false;
                    }
                }

                if (SelectProfesionUsu == null)
                {
                    ActualizarUsuarioIOS.IdProfesion = '0';
                }


                if (SelectSectorEcoUsu == null)
                {
                    ActualizarUsuarioIOS.IdSectorEconomico = '0';
                }

                ActualizarUsuarioIOS.Contrasena = "";
                ActualizarUsuarioIOS.TerminosCondiciones = "1";
                /* validar formato correo */
                bool resultadoCorreo = Regex.IsMatch(EntCorreoUsu.Trim(charsToTrim), expreregularcorreos, RegexOptions.IgnoreCase);
                if (!resultadoCorreo)
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoTieneFormatoCorreo, AppResources.cerrar);
                    return false;
                }

                string s2 = "corferias@ios.com";
                bool res = EntCorreo.ToLower().Contains(s2.ToLower());


                string s3 = "appleid.com";
                bool res3 = EntCorreo.ToLower().Contains(s3.ToLower());

                if (res || res3)
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, "El correo no ha sido modificado correctamente, intenta de nuevo", AppResources.cerrar);
                    return false;
                }

                /*Validar nuemero de identificacion*/
                try
                {
                    int aux = int.Parse(EntIdentificacio);
                    if (aux < 0)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, "El número de identificación no ha sido modificado correctamente, intenta de nuevo", AppResources.cerrar);
                        return false;
                    }
                }
                catch
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, "El número de identificación no ha sido modificado correctamente, intenta de nuevo", AppResources.cerrar);
                    return false;
                }

                if (CheckTerminos == false)
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElCampoTerminosEsObligatorio, AppResources.cerrar);
                    return false;
                }
            }


            if (EntNombreUsu == "" || EntNombreUsu == null || EntFechaNacimientoUsu == null || ActualizarUsuario.IdPais == 0 || ActualizarUsuario.IdCiudad == 0)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVacios, AppResources.VMaceptar);
                return false;
            }
            else
            {
                if (FormEmpresa)
                {
                    if (string.IsNullOrEmpty(EntNombreEmpr) || string.IsNullOrEmpty(EntIdEmpr) || string.IsNullOrEmpty(EntCodPostalEmpr) || string.IsNullOrEmpty(EntTelPaisEmpr) || string.IsNullOrEmpty(EntTelEmpr) || string.IsNullOrEmpty(EntTelAreaEmpr) || string.IsNullOrEmpty(EntDireccionEmpr) || string.IsNullOrEmpty(EntCargoEmpr))
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVaciosEmpresa, AppResources.VMaceptar);
                        return false;
                    }
                }
            }

            /* validar formato Nombre completo */
            bool resultadoNombreUsu = Regex.IsMatch(EntNombreUsu.Trim(charsToTrim), expreregularletras, RegexOptions.IgnoreCase);
            if (!resultadoNombreUsu)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoTieneFormaroElNombreDelUsuario, AppResources.VMaceptar);
                return false;
            }


            /* Validar edad */
            if (EntFechaNacimientoUsu.Value.AddYears(18) > DateTime.Today)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMMayorDeEdad, AppResources.VMaceptar);
                return false;
            }

            string AuxFecha = EntFechaNacimientoUsu.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);


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

            int AuxEntCodPostalEmpr = 0;
            if (EntCodPostalEmpr != null && EntCodPostalEmpr != "")
                AuxEntCodPostalEmpr = int.Parse(EntCodPostalEmpr);

            string AuxCelular = "0";
            if (EntCelularUsu != null && EntCelularUsu != "")
                AuxCelular = EntCelularUsu;

            ActualizarUsuario.NombreCompleto = EntNombreUsu;
            ActualizarUsuario.FechaNacimiento = AuxFecha;
            ActualizarUsuario.Celular = AuxCelular;
            ActualizarUsuario.NombreEmpresa = EntNombreEmpr;
            ActualizarUsuario.IdentificacionEmpresa = AuxEntIdEmpr;
            ActualizarUsuario.Telefono = AuxEntTelEmpr;
            ActualizarUsuario.IndicativoPais = AuxEntTelPaisEmpr;
            ActualizarUsuario.Area = AuxEntTelAreaEmpr;
            ActualizarUsuario.Direccion = EntDireccionEmpr;
            ActualizarUsuario.CodigoPostal = AuxEntCodPostalEmpr;
            ActualizarUsuario.Cargo = EntCargoEmpr;
            ActualizarUsuario.IdSectorEconomicoemp = 0;
            ActualizarUsuario.Imagen = ActualizarUsuario.Imagen;

            ActualizarUsuarioIOS.NombreCompleto = EntNombreUsu;
            ActualizarUsuarioIOS.FechaNacimiento = AuxFecha;
            ActualizarUsuarioIOS.Celular = AuxCelular;
            ActualizarUsuarioIOS.NombreEmpresa = EntNombreEmpr;
            ActualizarUsuarioIOS.IdentificacionEmpresa = AuxEntIdEmpr;
            ActualizarUsuarioIOS.Telefono = AuxEntTelEmpr;
            ActualizarUsuarioIOS.IndicativoPais = AuxEntTelPaisEmpr;
            ActualizarUsuarioIOS.Area = AuxEntTelAreaEmpr;
            ActualizarUsuarioIOS.Direccion = EntDireccionEmpr;
            ActualizarUsuarioIOS.CodigoPostal = AuxEntCodPostalEmpr;
            ActualizarUsuarioIOS.Cargo = EntCargoEmpr;
            ActualizarUsuarioIOS.IdSectorEconomicoemp = 0;
            ActualizarUsuarioIOS.Imagen = ActualizarUsuario.Imagen;

            if (ActualizarUsuarioIOS.IdIdentificacion == null)
            {
                ActualizarUsuarioIOS.IdIdentificacion = "1";
            }

            if (ActualizarUsuarioIOS.IdIdentificacion == "")
            {
                ActualizarUsuarioIOS.IdIdentificacion = "1";
            }

            return true;
        }

        private async Task CheckTerminosYCondiciones()
        {
            try
            {
                string urliDos = logicaWs.Movile_Insert_habeasData_Mtd("2", EntCorreo);
                HabeasData habeasData = new HabeasData
                {
                    IpCel = await GetLocalIPAddressAsync(),
                    Acepto = "1",
                    IdModulo = "50",
                    IdTerminoPolitico = terminos.Max,
                    Navegador = "0"
                };

                switch (DeviceInfo.Platform.ToString())
                {
                    case "iOS":
                        habeasData.Navegador = "iOS";
                        break;
                    case "Android":
                        habeasData.Navegador = "Android";
                        break;
                    default:
                        habeasData.Navegador = "Otro";
                        break;
                }

                string json = JsonConvert.SerializeObject(habeasData);
                string res = await logicaWs.ConectionPost(json, urliDos);

                if (!res.Equals("Datos Registrados correctamente"))
                    claseBase.InsertarLogs_Mtd("ERROR", res, "RegistroVM", "CheckTerminosYCondiciones", "insertarhabeasdatapp");

                string urliTres = logicaWs.Movile_Insert_habeasData_Mtd("2", EntCorreo);
                HabeasData habeasDataTres = new HabeasData
                {
                    IpCel = await GetLocalIPAddressAsync(),
                    Acepto = "1",
                    IdModulo = "50",
                    IdTerminoPolitico = politicas.Max,
                    Navegador = "0"
                };

                switch (DeviceInfo.Platform.ToString())
                {
                    case "iOS":
                        habeasDataTres.Navegador = "iOS";
                        break;
                    case "Android":
                        habeasDataTres.Navegador = "Android";
                        break;
                    default:
                        habeasDataTres.Navegador = "Otro";
                        break;
                }

                string jsonTres = JsonConvert.SerializeObject(habeasDataTres);
                string resTres = await logicaWs.ConectionPost(jsonTres, urliTres);

                if (!resTres.Equals("Datos Registrados correctamente"))
                    claseBase.InsertarLogs_Mtd("ERROR", resTres, "RegistroVM", "CheckTerminosYCondiciones", "insertarhabeasdatapp");
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "CheckTerminosYCondiciones", "insertarhabeasdatapp");
            }

        }

        private async Task CargarListaciudadByPais(string IdPais)
        {
            try
            {
                /*Cargar lista ciudades dependiendo del pais */
                ListaCiudadEmpre = new ObservableCollection<ListasGeneral>();
                string urli = logicaWs.Movile_select_CiudadByPais_Mtd(IdPais);
                string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                int auxCont = 0;
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral
                    {
                        Descripcion = item.GetValue("NombreCiudad")?.ToString(),
                        Id = item.GetValue("IdCiudad")?.ToString()
                    };
                    ListaCiudadEmpre.Add(listasGeneral);

                }

                ListaCiudadEmpre = new ObservableCollection<ListasGeneral>(ListaCiudadEmpre.OrderBy(x => x.Descripcion).ToList());
                foreach (var item in ListaCiudadEmpre)
                {

                    if (ActualizarUsuario.IdCiudademp.ToString() != "0")
                    {
                        if (ActualizarUsuario.IdCiudademp.ToString() == item.Id)
                        {
                            SelectIndexCiudadEmpre = auxCont.ToString();
                        }
                    }
                    auxCont += 1;
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "CargarListaciudadByPais", "consultaciudadporpais");
            }
        }

        private async Task Edit_facebook_MtoAsync()
        {
            bool action = await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMDesvincularFacebook, AppResources.VMaceptar, AppResources.VMCancelar); ;

            if (action == true)
            {
                await Eliminar_RedSocial("1");
            }
        }

        private async Task Edit_apple_MtoAsync()
        {
            bool action = await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMDesvincularApple, AppResources.VMaceptar, AppResources.VMCancelar); ;

            if (action == true)
            {
                await Eliminar_RedSocial("3");
            }
        }

        public async Task Eliminar_RedSocial(string idRed)
        {
            string urli = logicaWs.Movile_Delet_RedSocialUsu_Mtd(EmailUsuario, "1", idRed);
            string json = JsonConvert.SerializeObject("");
            string jsonProcedimiento = await logicaWs.ConectionPost(json, urli);
            if (jsonProcedimiento == "Datos Eliminados")
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMSehaEliminadoLaRedSocialConExito, AppResources.VMaceptar);
                if (idRed == "1")
                    Facebook = false;
                if (idRed == "2")
                    Google = false;
                if (idRed == "3")
                    Apple = false;
            }
            else
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
        }

        private async Task Edit_google_MtoAsync()
        {
            bool action = await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMDesvincularGoogle, AppResources.VMaceptar, AppResources.VMCancelar);

            if (action == true)
            {
                await Eliminar_RedSocial("2");
            }
        }

        private async Task Edit_Clave_MtoAsync()
        {
            bool resul = await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMCambiarLaClaveDeCuenta, AppResources.VMaceptar, AppResources.VMCancelar);

            if (resul == true)
            {

                string jsonProcedimiento;
                Clave clave = new Clave { Contrasena = "" };
                string urli = logicaWs.Movile_Consulta_Login_Mtd(EmailUsuario);
                string json = JsonConvert.SerializeObject(clave);
                jsonProcedimiento = await logicaWs.ConectionPost(json, urli);

                if (jsonProcedimiento != "Credenciales Incorrectos" && jsonProcedimiento != "Usuario no existe")
                {
                    try
                    {
                        JArray jsArray = JArray.Parse(jsonProcedimiento);

                        foreach (JObject item in jsArray)
                        {
                            string Email = item.GetValue("Email").ToString();
                        }
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.loginolvidecontraseña, AppResources.VMaceptar);
                    }
                    catch (Exception ex)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "Edit_Clave_MtoAsync", "validausuarioaplicacion");
                    }

                }
                else
                {
                    await MopupService.Instance.PushAsync(new CambioContrasenaPopUp());
                }
            }

        }

        //INICIA FOTOS
        private async Task Cambiar_Foto_Mto()
        {
            if (!StateConexion)
            {
                try
                {
                    string action = await pageServicio.DisplayOpcion(AppResources.seleccionar, AppResources.VMComoQuieresCambiarTuFoto, AppResources.VMCamara, AppResources.VMGaleria);
                    if (action == "2")
                        Tomar_Foto_Galeria();
                    else
                    if (action == "1")
                        Tomar_Foto_Camara();
                }
                catch (Exception ex)
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "Cambiar_Foto_Mto", "Error metodo cambiar foto");
                }
            }
            else
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
            }
        }

        private async void Tomar_Foto_Galeria()
        {
            try
            {
                await RequestCameraAndGalleryPermissions();
                IsBusy = true;
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);


            }
            catch (Exception ex)
            {
                IsBusy = false;
                await pageServicio.DisplayAlert(AppResources.nombreMarca, ex.Message, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "Tomar_Foto_Galeria", "Error metodo tomar foto galeria");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                return;
            }
            try
            {
                // save the file into local storage
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                var stream = await photo.OpenReadAsync();
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                await SubirImagenAsync(new MemoryStream(memoryStream.ToArray()));
                //FotoGaleria = ImageSource.FromStream(() => { return new MemoryStream(memoryStream.ToArray()); });
                FotoGaleria = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                memoryStream.Dispose();
                stream.Dispose();
            }
            catch (Exception ex)
            {
                IsBusy = false;
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "Tomar_Foto_Galeria", "Error metodo cargar foto async");
            }
        }

        private async Task<bool> RequestCameraAndGalleryPermissions()
        {
            var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var storageStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (cameraStatus != PermissionStatus.Granted)
                cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();

            if (storageStatus != PermissionStatus.Granted)
                storageStatus = await Permissions.RequestAsync<Permissions.StorageRead>();

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, "Se necesitan permisos de cámara y almacenamiento para continuar.", AppResources.VMaceptar);
                throw new Exception("Permisos denegados");
            }

            return true;
        }

        private async void Tomar_Foto_Camara()
        {
            try
            {
                await RequestCameraAndGalleryPermissions();
                IsBusy = true;
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    try
                    {
                        var photo = await MediaPicker.CapturePhotoAsync();
                        if (photo != null)
                        {
                            using var stream = await photo.OpenReadAsync();
                            using var memoryStream = new MemoryStream();
                            await stream.CopyToAsync(memoryStream);

                            await SubirImagenAsync(new MemoryStream(memoryStream.ToArray()));

                            FotoGaleria = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                        }
                    }
                    catch (Exception ex)
                    {
                        claseBase.InsertarLogs_Mtd("EXCEPTION", ex.Message, "EditarPerfilVm", "Tomar_Foto_Camara", "Error validar camara habilitada o no habilitada");
                    }
                }
                else
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, "Camara no habilitada", AppResources.VMaceptar);
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "EditarPerfilVm", "Tomar_Foto_Camara", "Error metodo tomar foto camara");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SubirImagenAsync(Stream stream)
        {
            if (!StateConexion)
            {
                try
                {
                    byte[] imageArray = null;
                    var buffer = new byte[16 * 1024];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int read;
                        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                        imageArray = ms.ToArray();
                    }
                    ActualizarUsuario.Imagen = await logicaWs.UploadDataAsync(imageArray);
                    ActualizarUsuarioIOS.Imagen = await logicaWs.UploadDataAsync(imageArray);
                }
                catch (Exception ex)
                {
                    claseBase.InsertarLogs_Mtd("EXCEPTION", ex.Message, "EditarPerfilVm", "SubirImagenAsync", "Error metodo subir imagenes async");
                }
            }
            else
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
            IsBusy = false;
        }
        //TERMINA FOTO
    }
}
