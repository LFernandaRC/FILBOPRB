using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Usuario;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace EventosCorferias.ViewModel.Usuario
{
    public class PerfilMenuVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        public readonly ClaseBase claseBase;
        public readonly IPageServicio pageServicio;

        private ActualizarUsuario _actualizarUsuario;

        private ImageSource _fotoGaleria = "";

        private ObservableCollection<ListasGeneral> ListaRedSocial;

        private string? _entPaisS;
        private string? _entIdUsu;
        private string? _entBarrio;
        private string? _entIdEmpr;
        private string? _entTelEmpr;
        private string? _enTipoIdUsuS;
        private string? _entLocalidad;
        private string? _entNombreUsu;
        private string? _entCargoEmpr;
        private string? _entCorreoUsu;
        private string? _entPaisempreS;
        private string? _entCiudadUsuS;
        private string? _entCelularUsu;
        private string? _entNombreEmpr;
        private string? _entTelPaisEmpr;
        private string? _entTelAreaEmpr;
        private string? _entCiudadEmpreS;
        private string? _fechaNacimiento;
        private string? _entDireccionEmpr;
        private string? _entCodPostalEmpr;
        private string? _entProfesionUsuS;
        private string? _entSectorEcoUsuS;

        private bool _apple;
        private bool _google;
        private bool _facebook;
        private bool _formEmpreMas;
        private bool _formEmpreMenos;
        private bool _terminosyPoliticas;

        public ActualizarUsuario UsuarioActualizar
        {
            get { return _actualizarUsuario; }
            set { _actualizarUsuario = value; OnPropertyChanged(nameof(RegistroUsuario)); }
        }

        public string? EntBarrio
        {
            get { return _entBarrio; }
            set { _entBarrio = value; OnPropertyChanged(nameof(EntBarrio)); }
        }

        public string? EntLocalidad
        {
            get { return _entLocalidad; }
            set { _entLocalidad = value; OnPropertyChanged(nameof(EntLocalidad)); }
        }

        public string? EntNombreUsu
        {
            get { return _entNombreUsu; }
            set { _entNombreUsu = value; OnPropertyChanged(nameof(EntNombreUsu)); }
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

        public string? EntCorreoUsu
        {
            get { return _entCorreoUsu; }
            set { _entCorreoUsu = value; OnPropertyChanged(nameof(EntCorreoUsu)); }
        }
        public string? EntPaisS
        {
            get { return _entPaisS; }
            set { _entPaisS = value; OnPropertyChanged(nameof(EntPaisS)); }
        }

        public string? EntPaisempreS
        {
            get { return _entPaisempreS; }
            set { _entPaisempreS = value; OnPropertyChanged(nameof(EntPaisempreS)); }
        }

        public string? EntCiudadEmpreS
        {
            get { return _entCiudadEmpreS; }
            set { _entCiudadEmpreS = value; OnPropertyChanged(nameof(EntCiudadEmpreS)); }
        }

        public string? EntCiudadUsuS
        {
            get { return _entCiudadUsuS; }
            set { _entCiudadUsuS = value; OnPropertyChanged(nameof(EntCiudadUsuS)); }
        }

        public string? EnTipoIdUsuS
        {
            get { return _enTipoIdUsuS; }
            set { _enTipoIdUsuS = value; OnPropertyChanged(nameof(EnTipoIdUsuS)); }
        }

        public string? EntProfesionUsuS
        {
            get { return _entProfesionUsuS; }
            set { _entProfesionUsuS = value; OnPropertyChanged(nameof(EntProfesionUsuS)); }
        }

        public string? EntSectorEcoUsuS
        {
            get { return _entSectorEcoUsuS; }
            set { _entSectorEcoUsuS = value; OnPropertyChanged(nameof(EntSectorEcoUsuS)); }
        }

        public string? FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; OnPropertyChanged(nameof(FechaNacimiento)); }
        }

        public ImageSource FotoGaleria
        {
            get { return _fotoGaleria; }
            set { _fotoGaleria = value; OnPropertyChanged(nameof(FotoGaleria)); }
        }

        public bool Facebook
        {
            get { return _facebook; }
            set { _facebook = value; OnPropertyChanged(nameof(Facebook)); }
        }

        public bool Google
        {
            get { return _google; }
            set { _google = value; OnPropertyChanged(nameof(Google)); }
        }

        public bool Apple
        {
            get { return _apple; }
            set { _apple = value; OnPropertyChanged(nameof(Apple)); }
        }
        public bool FormEmpreMas
        {
            get { return _formEmpreMas; }
            set { _formEmpreMas = value; OnPropertyChanged(nameof(FormEmpreMas)); }
        }
        public bool FormEmpreMenos
        {
            get { return _formEmpreMenos; }
            set { _formEmpreMenos = value; OnPropertyChanged(nameof(FormEmpreMenos)); }
        }

        public bool TerminosyPoliticas
        {
            get { return _terminosyPoliticas; }
            set { _terminosyPoliticas = value; OnPropertyChanged(nameof(TerminosyPoliticas)); }

        }

        public ICommand Edit_Perfil { get; }

        public ICommand Button_Form { get; }

        public ICommand LinkTerminos { get; }

        public ICommand LinkPoliticas { get; }

        public ICommand Cerrar_Sesion { get; }


        public PerfilMenuVm()
        {
            IsBusy = true;

            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();
            UsuarioActualizar = new ActualizarUsuario();

            try
            {
                Button_Form = new Command(Button_Form_Mtd);
                LinkTerminos = new Command(LinkTerminos_Mtd);
                LinkPoliticas = new Command(LinkPoliticas_Mtd);
                Edit_Perfil = new Command(async () => await Edit_Perfil_MtoAsync());
                Cerrar_Sesion = new Command(async () => await Cerrar_Sesion_MtoAsync());

                Inicializar();
                ContadorNotificaciones_Mtd();
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "PerfilMenuVm", "Inicializar", "Inicializa la vista del view PerfilMenu");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void Inicializar()
        {
            try
            {
                EmailUsuario = Preferences.Get("Email", "");
                CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
                LenguajeBase = Preferences.Get("IdiomaDefecto", "");
                NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
                ImagenSplash = logicaWs.ImgMenuSuperior_Mtd();

                FormEmpreMas = false;
                FormEmpreMenos = true;

                if (!StateConexion)
                {
                    await CargarInfoUsuario();
                    await CargarInfoRedSocial();
                }
                else
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "PerfilMenuVm", "Inicializar", "Inicializa la vista del view PerfilMenu");
            }
        }

        private async Task CargarInfoUsuario()
        {
            try
            {
                string urli = logicaWs.Movile_select_Perfil_Mtd(EmailUsuario);
                string? jsonProcedimiento = await logicaWs.ConectionPost(" ", urli);

                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    if (item.GetValue("Email")?.ToString() != "null" && item.GetValue("Email")?.ToString() != "" && item.GetValue("Email")?.ToString() != "")
                        EntCorreoUsu = item.GetValue("Email")?.ToString();

                    if (item.GetValue("nombrecompleto")?.ToString() != "null" && item.GetValue("nombrecompleto")?.ToString() != "" && item.GetValue("nombrecompleto")?.ToString() != "")
                        EntNombreUsu = item.GetValue("nombrecompleto")?.ToString();

                    if (item.GetValue("pais")?.ToString() != "null" && item.GetValue("pais")?.ToString() != "" && item.GetValue("pais")?.ToString() != "")
                        EntPaisS = item.GetValue("pais")?.ToString();

                    if (item.GetValue("PaisR")?.ToString() != "null" && item.GetValue("PaisR")?.ToString() != "" && item.GetValue("PaisR")?.ToString() != "")
                        UsuarioActualizar.IdPais = int.Parse(item.GetValue("PaisR").ToString());

                    if (item.GetValue("ciudad")?.ToString() != "null" && item.GetValue("ciudad")?.ToString() != "" && item.GetValue("ciudad")?.ToString() != "")
                        EntCiudadUsuS = item.GetValue("ciudad").ToString();

                    if (item.GetValue("CiudadR")?.ToString() != "null" && item.GetValue("CiudadR")?.ToString() != "" && item.GetValue("CiudadR")?.ToString() != "")
                        UsuarioActualizar.IdCiudad = int.Parse(item.GetValue("CiudadR").ToString());

                    if (item.GetValue("tipoidentificacion")?.ToString() != "null" && item.GetValue("tipoidentificacion")?.ToString() != "" && item.GetValue("tipoidentificacion")?.ToString() != "")
                        EnTipoIdUsuS = item.GetValue("tipoidentificacion")?.ToString();

                    if (item.GetValue("celular")?.ToString() != "null" && item.GetValue("celular")?.ToString() != "" && item.GetValue("celular")?.ToString() != "" && item.GetValue("celular")?.ToString() != "0")
                        EntCelularUsu = item.GetValue("celular")?.ToString();

                    if (item.GetValue("Nidentificacion")?.ToString() != "null" && item.GetValue("Nidentificacion")?.ToString() != "" && item.GetValue("Nidentificacion")?.ToString() != "")
                        EntIdUsu = item.GetValue("Nidentificacion")?.ToString();

                    if (item.GetValue("profesion")?.ToString() != "null" && item.GetValue("profesion")?.ToString() != "" && item.GetValue("profesion")?.ToString() != "")
                        EntProfesionUsuS = item.GetValue("profesion")?.ToString();

                    if (item.GetValue("sectoreconomico")?.ToString() != "null" && item.GetValue("sectoreconomico")?.ToString() != "" && item.GetValue("sectoreconomico")?.ToString() != "")
                        EntSectorEcoUsuS = item.GetValue("sectoreconomico")?.ToString();

                    if (item.GetValue("nombreempresa")?.ToString() != "null" && item.GetValue("nombreempresa")?.ToString() != "" && item.GetValue("nombreempresa")?.ToString() != "")
                        EntNombreEmpr = item.GetValue("nombreempresa")?.ToString();

                    if (item.GetValue("identificacionempresa")?.ToString() != "null" && item.GetValue("identificacionempresa")?.ToString() != "" && item.GetValue("identificacionempresa")?.ToString() != "" && item.GetValue("identificacionempresa").ToString() != "0")
                        EntIdEmpr = item.GetValue("identificacionempresa")?.ToString();

                    if (item.GetValue("telefonoEmpresa")?.ToString() != "null" && item.GetValue("telefonoEmpresa")?.ToString() != "" && item.GetValue("telefonoEmpresa")?.ToString() != "" && item.GetValue("telefonoEmpresa")?.ToString() != "0")
                        EntTelEmpr = item.GetValue("telefonoEmpresa")?.ToString();

                    if (item.GetValue("indicativopais")?.ToString() != "null" && item.GetValue("indicativopais")?.ToString() != "" && item.GetValue("indicativopais")?.ToString() != "" && item.GetValue("indicativopais")?.ToString() != "0")
                        EntTelPaisEmpr = item.GetValue("indicativopais")?.ToString();

                    if (item.GetValue("area")?.ToString() != "null" && item.GetValue("area")?.ToString() != "" && item.GetValue("area")?.ToString() != "" && item.GetValue("area")?.ToString() != "0")
                        EntTelAreaEmpr = item.GetValue("area")?.ToString();

                    if (item.GetValue("paisEmpresa")?.ToString() != "null" && item.GetValue("paisEmpresa")?.ToString() != "" && item.GetValue("paisEmpresa")?.ToString() != "" && item.GetValue("paisEmpresa")?.ToString() != "0")
                        EntPaisempreS = item.GetValue("paisEmpresa")?.ToString();

                    if (item.GetValue("ciudadEmpresa")?.ToString() != "null" && item.GetValue("ciudadEmpresa")?.ToString() != "" && item.GetValue("ciudadEmpresa")?.ToString() != "0")
                        EntCiudadEmpreS = item.GetValue("ciudadEmpresa")?.ToString();

                    if (item.GetValue("direccionEmpresa")?.ToString() != "null" && item.GetValue("direccionEmpresa")?.ToString() != "" && item.GetValue("direccionEmpresa")?.ToString() != "")
                        EntDireccionEmpr = item.GetValue("direccionEmpresa")?.ToString();

                    if (item.GetValue("codigopostal")?.ToString() != "null" && item.GetValue("codigopostal")?.ToString() != "" && item.GetValue("codigopostal")?.ToString() != "" && item.GetValue("codigopostal")?.ToString() != "0")
                        EntCodPostalEmpr = item.GetValue("codigopostal")?.ToString();

                    if (item.GetValue("cargoEmpresa")?.ToString() != "null" && item.GetValue("cargoEmpresa")?.ToString() != "" && item.GetValue("cargoEmpresa")?.ToString() != "")
                        EntCargoEmpr = item.GetValue("cargoEmpresa")?.ToString();

                    if (item.GetValue("fechanacimiento")?.ToString() != "null" && item.GetValue("fechanacimiento")?.ToString() != "" && item.GetValue("fechanacimiento")?.ToString() != "")
                        FechaNacimiento = item.GetValue("fechanacimiento")?.ToString();
                    UsuarioActualizar.FechaNacimiento = item.GetValue("fechanacimiento")?.ToString();

                    if (item.GetValue("Nidentificacion")?.ToString() != "null" && item.GetValue("Nidentificacion")?.ToString() != "" && item.GetValue("Nidentificacion")?.ToString() != "")
                        EntIdUsu = item.GetValue("Nidentificacion")?.ToString();

                    if (item.GetValue("idprofesion")?.ToString() != "null" && item.GetValue("idprofesion")?.ToString() != "" && item.GetValue("idprofesion")?.ToString() != "")
                        UsuarioActualizar.IdProfesion = int.Parse(item.GetValue("idprofesion").ToString());

                    if (item.GetValue("idsector")?.ToString() != "null" && item.GetValue("idsector")?.ToString() != "" && item.GetValue("idsector")?.ToString() != "")
                        UsuarioActualizar.IdSectorEconomico = int.Parse(item.GetValue("idsector").ToString());

                    if (item.GetValue("imagen")?.ToString() != "null" && item.GetValue("imagen")?.ToString() != "" && item.GetValue("imagen")?.ToString() != "")
                    {
                        FotoGaleria = item.GetValue("imagen")?.ToString();
                        UsuarioActualizar.Imagen = item.GetValue("imagen")?.ToString();
                    }
                    else
                    {
                        UsuarioActualizar.Imagen = "";
                    }

                    try
                    {
                        long auxId = long.Parse(EntIdUsu);
                        if (auxId < 0)
                        {
                            EntIdUsu = "";
                            EnTipoIdUsuS = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "PerfilMenuVm", "CargarInfoUsuario", "Identificacion mal, no aplica" + EntIdUsu);
                    }

                    try
                    {
                        string s2 = "corferias@ios.com";
                        bool res = EntCorreoUsu.ToLower().Contains(s2.ToLower());

                        string s3 = "appleid.com";
                        bool res3 = EntCorreoUsu.ToLower().Contains(s3.ToLower());

                        if (res || res3)
                        {
                            EntCorreoUsu = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "PerfilMenuVm", "CargarInfoUsuario", "Correo mal, no aplica" + EntCorreoUsu);
                    }
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "PerfilMenuVm", "CargarInfoUsuario", "consultausuario");
            }
        }

        private async Task CargarInfoRedSocial()
        {
            try
            {
                string urli = logicaWs.Movile_select_RedSocialUsu_Mtd(EmailUsuario);
                string? jsonProcedimiento = await logicaWs.ConectionPost(" ", urli);
                if (jsonProcedimiento != "")
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    ListaRedSocial = new ObservableCollection<ListasGeneral>();
                    foreach (JObject item in jsArray)
                    {
                        ListasGeneral listasGeneral = new ListasGeneral
                        {
                            Id = item.GetValue("IdRedSocial")?.ToString(),
                            Descripcion = item.GetValue("NombreRed")?.ToString()
                        };
                        ListaRedSocial.Add(listasGeneral);
                    }

                    if (ListaRedSocial.Count > 0)
                    {
                        foreach (var aux in ListaRedSocial)
                        {
                            if (aux.Id == "1")
                                Facebook = true;

                            if (aux.Id == "2")
                                Google = true;

                            if (aux.Id == "3")
                                Apple = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "PerfilMenuVm", "CargarInfoRedSocial", "consultaredsocialusuario");
            }
        }

        private void Button_Form_Mtd(object obj)
        {
            var content = obj as ImageButton;

            switch (content.ClassId)
            {
                case "1":
                    FormEmpreMas = false;
                    FormEmpreMenos = true;
                    break;
                case "2":
                    FormEmpreMas = true;
                    FormEmpreMenos = false;
                    break;
            }
        }

        private async Task Cerrar_Sesion_MtoAsync()
        {
            try
            {
                string s2 = "corferias@ios.com";
                bool res = EmailUsuario.ToLower().Contains(s2.ToLower());

                string s3 = "appleid.com";
                bool res3 = EmailUsuario.ToLower().Contains(s3.ToLower());

                if (res || res3)
                {
                    bool resulDos = await pageServicio.DisplayAlertDos(AppResources.nombreMarca, AppResources.ActualizaDatosPerfil, AppResources.VMaceptar);

                    if (resulDos)
                    {
                        bool resul = await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstasSegurpDeCerrarSesion, AppResources.VMaceptar, AppResources.VMCancelar);
                        if (resul)
                        {
                            Preferences.Remove("NombreCompleto");
                            Preferences.Remove("Email");
                            Preferences.Remove("IdCiudad");
                            Preferences.Remove("IdCiudadDesc");
                            Preferences.Remove("RedSocial");
                            await pageServicio.PushModalAsync(new EventosCorferias.Views.Usuario.Splash());
                        }
                    }
                }
                else
                {
                    bool resul = await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstasSegurpDeCerrarSesion, AppResources.VMaceptar, AppResources.VMCancelar);
                    if (resul)
                    {
                        Preferences.Remove("NombreCompleto");
                        Preferences.Remove("Email");
                        Preferences.Remove("IdCiudad");
                        Preferences.Remove("IdCiudadDesc");
                        Preferences.Remove("RedSocial");
                        await pageServicio.PushModalAsync(new EventosCorferias.Views.Usuario.Splash());
                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "PerfilMenuVM", "Cerrar_Sesion_MtoAsync", "validacion iOS correo");
            }
        }

        private async Task Edit_Perfil_MtoAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await RootNavigation.PushAsync(new EditarPerfilView());
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "PerfilMenuVM", "Edit_Perfil_MtoAsync", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, ex.Message, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void LinkTerminos_Mtd()
        {
            try
            {
                string urli = logicaWs.Movile_Select_Terminos_Politicas_Mtd("1", "13", LenguajeBase);
                string? jsonProcedimiento = await logicaWs.ConectionGet(urli);
                if (jsonProcedimiento != "")
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);

                    foreach (JObject item in jsArray)
                    {
                        await pageServicio.DisplayAlert(item.GetValue("Modulo")?.ToString() ?? string.Empty, item.GetValue("Texto")?.ToString() ?? string.Empty, AppResources.VMaceptar);
                    }

                    if (jsArray.Count == 0)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                    }
                }
                else
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "PerfilMenuVM", "LinkTerminos_Mtd", "consultaterminosaplicacion");
            }
        }

        private async void LinkPoliticas_Mtd()
        {
            try
            {
                string urli = logicaWs.Movile_Select_Terminos_Politicas_Mtd("2", "49", LenguajeBase);
                string? jsonProcedimiento = await logicaWs.ConectionGet(urli);
                if (jsonProcedimiento != "")
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);

                    foreach (JObject item in jsArray)
                    {
                        await pageServicio.DisplayAlert(item.GetValue("Modulo")?.ToString() ?? string.Empty, item.GetValue("Texto")?.ToString() ?? string.Empty, AppResources.VMaceptar);
                    }

                    if (jsArray.Count == 0)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                    }
                }
                else
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "PerfilMenuVM", "LinkPoliticas_Mtd", "consultaterminosaplicacion");
            }
        }
    }
}