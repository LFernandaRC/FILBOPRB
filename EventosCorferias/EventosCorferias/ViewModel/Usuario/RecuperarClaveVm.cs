using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Usuario;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace EventosCorferias.ViewModel.Usuario
{
    class RecuperarClaveVm : BaseViewModel
    {

        private readonly LogicaWs logicaWS;
        public readonly ClaseBase claseBase;
        public readonly IPageServicio pageServicio;

        UsuarioLogin usuarioLogin;

        private string Codigo;
        private string _entCod1;
        private string _entCod2;
        private string _entCod3;
        private string _entCod4;
        private string _entCorreo;

        private bool _error = false;
        private bool _enviado = false;
        private bool _enviado2 = true;
        private bool _ingresaCorreo = true;

        public string EntCod1
        {
            get { return _entCod1; }
            set { _entCod1 = value; OnPropertyChanged(nameof(EntCod1)); }
        }

        public string EntCod2
        {
            get { return _entCod2; }
            set { _entCod2 = value; OnPropertyChanged(nameof(EntCod2)); }
        }

        public string EntCod3
        {
            get { return _entCod3; }
            set { _entCod3 = value; OnPropertyChanged(nameof(EntCod3)); }
        }

        public string EntCod4
        {
            get { return _entCod4; }
            set { _entCod4 = value; OnPropertyChanged(nameof(EntCod4)); }
        }

        public string EntCorreo
        {
            get { return _entCorreo; }
            set { _entCorreo = value; OnPropertyChanged(nameof(EntCorreo)); }
        }

        public bool Enviado
        {
            get { return _enviado; }
            set { _enviado = value; OnPropertyChanged(nameof(Enviado)); }
        }

        public bool Enviado2
        {
            get { return _enviado2; }
            set { _enviado2 = value; OnPropertyChanged(nameof(Enviado2)); }
        }

        public bool IngresaCorreo
        {
            get { return _ingresaCorreo; }
            set { _ingresaCorreo = value; OnPropertyChanged(nameof(IngresaCorreo)); }
        }

        public bool Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged(nameof(Error)); }
        }

        public ICommand Enviar { get; }

        public ICommand Regresar { get; }

        public ICommand Comprobar { get; }

        public ICommand GenerarNuevo { get; }

        public RecuperarClaveVm()
        {
            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            Regresar = new Command(Regresar_Mto);
            Comprobar = new Command(Comprobar_Mto);
            Enviar = new Command(async () => await GenerarCodigo_MtoAsync());
            GenerarNuevo = new Command(async () => await GenerarNuevo_MtoAsync());
        }

        private async Task GenerarNuevo_MtoAsync()
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            try
            {
                Error = false;

                EntCod1 = "";
                EntCod2 = "";
                EntCod3 = "";
                EntCod4 = "";

                if (ValidarCorreoAsync())
                {
                    try
                    {
                        string urli = logicaWS.Movile_Generar_Codigo_Mtd();
                        UsuarioMensaje usuarioMensaje = new UsuarioMensaje
                        {
                            email = EntCorreo.ToLower(),
                            content = AppResources.BienvenidoCodigoConfirmacion_,
                            subject = AppResources.CodigoMarca_
                        };

                        string json = JsonConvert.SerializeObject(usuarioMensaje);
                        var jsonProcedimiento = await logicaWS.ConectionPost(json, urli);

                        List<ResponseCode> responseCode1 = JsonConvert.DeserializeObject<List<ResponseCode>>(jsonProcedimiento);

                        if (!responseCode1[0].codigo.Equals(""))
                        {
                            Codigo = responseCode1[0].codigo;
                            Enviado = true;
                            Enviado2 = false;

                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.seAcaboDeReenviarElCodigo, AppResources.VMaceptar);
                        }
                        else
                        {
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElUsuarioNoSeEncontro, AppResources.VMaceptar);
                        }
                    }
                    catch (Exception ex)
                    {
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RecuperarClaveVM", "GenerarNuevo_MtoAsync1", "send");
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);

                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RecuperarClaveVM", "GenerarNuevo_MtoAsync2", "send");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GenerarCodigo_MtoAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                Enviado2 = false;

                if (ValidarCorreoAsync())
                {
                    try
                    {
                        usuarioLogin = new UsuarioLogin("0");
                        string urliUno = logicaWS.Movile_Consulta_Login_Mtd(EntCorreo.ToLower());
                        string jsonUno = JsonConvert.SerializeObject(usuarioLogin);
                        string jsonProcedimientoUno = await logicaWS.ConectionPost(jsonUno, urliUno);

                        if (jsonProcedimientoUno == "Usuario No Existe")
                        {
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEsteUsuaraioNoExisteMas, AppResources.VMaceptar);
                            Enviado2 = true;
                        }
                        else
                        {
                            string urli = logicaWS.Movile_Generar_Codigo_Mtd();
                            UsuarioMensaje usuarioMensaje = new UsuarioMensaje
                            {
                                email = EntCorreo.ToLower(),
                                content = AppResources.BienvenidoCodigoConfirmacion_,
                                subject = AppResources.CodigoMarca_
                            };

                            string json = JsonConvert.SerializeObject(usuarioMensaje);
                            var jsonProcedimiento = await logicaWS.ConectionPost(json, urli);

                            List<ResponseCode> responseCode1 = JsonConvert.DeserializeObject<List<ResponseCode>>(jsonProcedimiento);

                            if (!responseCode1[0].codigo.Equals(""))
                            {
                                Codigo = responseCode1[0].codigo;
                                Enviado = true;
                                Enviado2 = false;
                                IngresaCorreo = false;
                            }
                            else
                            {
                                Enviado2 = true;
                                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMElUsuarioNoSeEncontro, AppResources.VMaceptar);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Enviado2 = true;
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RecuperarClaveVM", "GenerarCodigo_MtoAsync1", "validausuarioaplicacion");
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                    }
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RecuperarClaveVM", "GenerarCodigo_MtoAsync2", "validausuarioaplicacion");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void Comprobar_Mto()
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            Error = false;
            if (string.IsNullOrEmpty(EntCod1) || string.IsNullOrEmpty(EntCod2) || string.IsNullOrEmpty(EntCod3) || string.IsNullOrEmpty(EntCod4))
            {
                Error = true;
            }
            else
            {
                if ((EntCod1 + EntCod2 + EntCod3 + EntCod4).Equals(Codigo))
                {
                    Preferences.Set("CorreoConfirmado", EntCorreo.ToLower());
                    await Application.Current.MainPage.Navigation.PushModalAsync(new ConfirmarClaveView());
                }
                else
                {
                    Error = true;
                }
            }
            IsBusy = false;
        }

        private bool ValidarCorreoAsync()
        {
            try
            {
                char[] charsToTrim = { ' ' };
                string expreregularcorreos = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                if (string.IsNullOrEmpty(EntCorreo))
                {
                    Enviado = false;
                    Enviado2 = true;
                    pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesElCampoVacio, AppResources.VMaceptar);
                    return false;
                }
                else
                {
                    bool resultadoCorreo = Regex.IsMatch(EntCorreo.Trim(charsToTrim), expreregularcorreos, RegexOptions.IgnoreCase);

                    if (!resultadoCorreo)
                    {
                        Enviado = false;
                        Enviado2 = true;
                        pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoTieneFormatoCorreo, AppResources.VMaceptar);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RecuperarClaveVM", "ValidarCorreoAsync", "Valida Correo Electronico Usuario");
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                return false;
            }
        }

        private async void Regresar_Mto()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RecuperarClaveVm", "Regresar_Mto", "n/a");
            }
        }
    }

    public class UsuarioMensaje
    {
        public string? email { get; set; }
        public string? content { get; set; }
        public string? subject { get; set; }
    }

    public class ResponseCode
    {
        public string? codigo { get; set; }
        public string? fecha { get; set; }
    }
}