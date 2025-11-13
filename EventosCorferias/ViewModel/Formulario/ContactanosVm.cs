using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using EventosCorferias.Views.Suceso;

namespace EventosCorferias.ViewModel.Formulario
{

    public class ContactanosVm : BaseViewModel
    {
        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;
        private readonly TerminosCondiciones terminos;
        private ListasGeneral _SelectTipoContacto;

        private ContactanosInsert contactanosInsert;
        private ObservableCollection<ListasGeneral> _tipoContacto;

        private string _correoElectronico;
        private string _telefonoCelular;
        private string _asunto;
        private string _descripcion;

        private bool _checkTerminos;
        private bool _verBtnEnviarUno;
        private bool _VerBtnEnviarDos;

        public string CorreoElectronico
        {
            get { return _correoElectronico; }
            set { _correoElectronico = value; OnPropertyChanged(nameof(CorreoElectronico)); }
        }

        public string TelefonoCelular
        {
            get { return _telefonoCelular; }
            set { _telefonoCelular = value; OnPropertyChanged(nameof(TelefonoCelular)); }
        }

        public string Asunto
        {
            get { return _asunto; }
            set { _asunto = value; OnPropertyChanged(nameof(Asunto)); }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; OnPropertyChanged(nameof(Descripcion)); }
        }

        public bool CheckTerminos
        {
            get { return _checkTerminos; }
            set { _checkTerminos = value; OnPropertyChanged(nameof(CheckTerminos)); }
        }

        public bool VerBtnEnviarUno
        {
            get { return _verBtnEnviarUno; }
            set { _verBtnEnviarUno = value; OnPropertyChanged(nameof(VerBtnEnviarUno)); }
        }

        public bool VerBtnEnviarDos
        {
            get { return _VerBtnEnviarDos; }
            set { _VerBtnEnviarDos = value; OnPropertyChanged(nameof(VerBtnEnviarDos)); }
        }

        public ObservableCollection<ListasGeneral> TipoContacto
        {
            get { return _tipoContacto; }
            set { _tipoContacto = value; OnPropertyChanged(nameof(TipoContacto)); }
        }

        public ListasGeneral SelectTipoContacto
        {
            get { return _SelectTipoContacto; }
            set { _SelectTipoContacto = value; OnPropertyChanged(nameof(SelectTipoContacto)); }
        }


        public ICommand BtnEnviar { get; }

        public ICommand Politicas { get; }

        public ICommand Notificacion_clave { get; }

        public ICommand FocusCorreo { get; }

        public ICommand FocusTelefono { get; }

        public ICommand FocusMultilista { get; }

        public ICommand BtnAdvertenciaCorreo { get; }


        public ContactanosVm()
        {
            IsBusy = true;
            VerBtnEnviarUno = true;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            terminos = new TerminosCondiciones();

            EmailUsuario = Preferences.Get("Email", "");
            CorreoElectronico = Preferences.Get("Email", "");
            CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();

            if (Preferences.Get("Celular", "") != "0")
                TelefonoCelular = Preferences.Get("Celular", "");

            BtnEnviar = new Command(BtnEnviar_MtdAsync);
            FocusCorreo = new Command(FocusPickerEntry_Mtd);
            FocusTelefono = new Command(FocusPickerEntry_Mtd);
            BtnAdvertenciaCorreo = new Command(BtnAdvertenciaCorreo_MtdAsycn);

            Notificacion_clave = new Command(async () => await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElCorreoElectronicoDebeMas, AppResources.VMaceptar));
            Politicas = new Command(async () => await pageServicio.DisplayAlert(terminos.Modulo, terminos.Texto, AppResources.VMaceptar));

            Multilista_MtdAsync();
            _ = ValidarMenuInferior();
        }

        public async void Multilista_MtdAsync()
        {
            try
            {
                string urli = logicaWS.Movile_Select_Lista_TipoContenido_Contactanos_Mtd();
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                TipoContacto = new ObservableCollection<ListasGeneral>();
                foreach (JObject item in jsArray)
                {
                    ListasGeneral listasGeneral = new ListasGeneral { Id = item.GetValue("Codigo").ToString(), Descripcion = item.GetValue("TipoContacto").ToString() };
                    TipoContacto.Add(listasGeneral);
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ContactanosVM", "Multilista_MtdAsync", "listatipocontenido");
            }
            await ConsultaPoliticas_MtdAsync();

            IsBusy = false;
        }

        public async void BtnEnviar_MtdAsync()
        {
            IsBusy = true;
            VerBtnEnviarUno = false;
            VerBtnEnviarDos = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar

            try
            {
                if (await ValidarFormulario_MtdAsync())
                {
                    string urli = logicaWS.Movile_Insert_Contactanos_Mtd(EmailUsuario);
                    string json = JsonConvert.SerializeObject(contactanosInsert);
                    string jsonProcedimiento = await logicaWS.ConectionPost(json, urli);

                    if (jsonProcedimiento == "Datos Registrados Correctamente")
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.EnvioExitoso, AppResources.VMaceptar);
                        await CheckTerminosYCondiciones();
                        RootMainPage.Detail = new NavigationPage(new SucesoView());
                    }
                    else
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.EnvioNoExitoso, AppResources.VMaceptar);
                        claseBase.InsertarLogs_Mtd("ERROR", jsonProcedimiento, "ContactanosVM", "BtnEnviar_MtdAsync", "insertarcontactanosb2");
                    }
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ContactanosVM", "BtnEnviar_MtdAsync", "insertarcontactanosb2");
            }
            finally
            {
                IsBusy = false;
                VerBtnEnviarUno = true;
                VerBtnEnviarDos = false;
            }
        }

        public async Task<bool> ValidarFormulario_MtdAsync()
        {
            try
            {
                char[] charsToTrim = { ' ' };
                string expreregularcorreos = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

                if (CorreoElectronico != null && Asunto != null && Descripcion != null && SelectTipoContacto != null)
                {
                    if (CorreoElectronico == "" || Asunto == "" || Descripcion == "" ||
                        CorreoElectronico == " " || Asunto == " " || Descripcion == " ")
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.camposObligatorios, AppResources.VMaceptar);
                        return false;
                    }

                    /* validar formato correo */
                    bool resultadoCorreo = Regex.IsMatch(CorreoElectronico.Trim(charsToTrim), expreregularcorreos, RegexOptions.IgnoreCase);
                    if (!resultadoCorreo)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoTieneFormatoCorreo, AppResources.VMaceptar);
                        return false;
                    }

                    /* Maximo caracteres asunto */
                    if (Asunto.Length > 50)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.asunto50Carac, AppResources.VMaceptar);
                        return false;
                    }

                    /* Maximo caracteres Descripicon */
                    if (Asunto.Length > 500)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.Descrip50, AppResources.VMaceptar);
                        return false;
                    }

                    if (!CheckTerminos)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElCampoTerminosEsObligatorio, AppResources.VMaceptar);
                        return false;
                    }

                    if (TelefonoCelular == null)
                        TelefonoCelular = "0";

                    if (TelefonoCelular == "" || TelefonoCelular == " ")
                        TelefonoCelular = "0";

                    contactanosInsert = new ContactanosInsert(EmailUsuario, SelectTipoContacto.Id, TelefonoCelular, CorreoElectronico, Asunto, Descripcion);
                    return true;
                }
                else
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.camposObligatorios, AppResources.VMaceptar);
                    return false;
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ContactanosVM", "ValidarFormulario_MtdAsync", "n/a");
                return false;
            }
        }

        private async Task ConsultaPoliticas_MtdAsync()
        {
            try
            {
                string urli = logicaWS.Movile_Select_Terminos_Politicas_Mtd("2", "16", LenguajeBase);
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
                terminos.Modulo = AppResources.terminosYCondiciones;
                terminos.Texto = AppResources.VMEstamosCargandoInfo;
                terminos.FechaPublica = "0";
                terminos.IdModulo = "0";

                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ContactanosVm", "ConsultaPoliticasAsync", "consultaterminosaplicacion");
            }
        }

        public async void BtnAdvertenciaCorreo_MtdAsycn()
        {
            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElCorreoElectronicoDebeMas, AppResources.VMaceptar);
        }

        private void FocusPickerEntry_Mtd(object obj)
        {
            var focusEntry = (Entry)obj;
            focusEntry.Focus();
            focusEntry.Text = "";
        }

        private async Task CheckTerminosYCondiciones()
        {
            string urliDos = logicaWS.Movile_Insert_habeasData_Mtd("2", EmailUsuario);
            HabeasData habeasData = new HabeasData
            {
                IpCel = await GetLocalIPAddressAsync(),
                Acepto = "1",
                IdModulo = "16",
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
                claseBase.InsertarLogs_Mtd("ERROR", res, "ContactanosVm", "CheckTerminosYCondiciones", "insertarhabeasdatapp");
        }
    }
}