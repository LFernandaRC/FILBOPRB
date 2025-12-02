using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Usuario;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json;
using System.Windows.Input;

namespace EventosCorferias.ViewModel.Usuario
{
    class ConfirmarClaveVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio PageServicio;

        public string _entClaveUsu;
        public string _entClaveUsu2;

        public string EntClaveUsu
        {
            get { return _entClaveUsu; }
            set { _entClaveUsu = value; OnPropertyChanged(nameof(EntClaveUsu)); }
        }

        public string EntClaveUsu2
        {
            get { return _entClaveUsu2; }
            set { _entClaveUsu2 = value; OnPropertyChanged(nameof(EntClaveUsu2)); }
        }

        public ICommand CambiarClave { get; }
        public ICommand Notificacion_clave { get; }

        public ConfirmarClaveVm()
        {
            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            PageServicio = new PageServicio();

            CambiarClave = new Command(async () => await CambiarClave_MtoAsync());
            Notificacion_clave = new Command(async () => await Notificacion_clave_Mto());
        }

        private async Task Notificacion_clave_Mto()
        {
            await PageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMLaContrasenaDebeTenerMinimoMas, AppResources.VMaceptar);
        }

        private async Task<bool> ValidarRegistro_Mto()
        {
            char[] charsToTrim = { ' ' };

            if (claseBase.ValidaString(EntClaveUsu).Equals("") || claseBase.ValidaString(EntClaveUsu2).Equals(""))
            {
                await PageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVacios, AppResources.VMaceptar);
                return false;
            }
            else
            {
                if (EntClaveUsu.Length < 6)
                {
                    await PageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMLaContrasenaDebeTenerMinimoMas, AppResources.VMaceptar);
                    return false;
                }
                else
                {
                    if (!EntClaveUsu.Equals(EntClaveUsu2))
                    {
                        await PageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMLasContrasenasNoCoinciden, AppResources.VMaceptar);
                        return false;
                    }
                    else
                        return true;
                }
            }
        }

        private async Task CambiarClave_MtoAsync()
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar

            if (await ValidarRegistro_Mto())
            {
                try
                {
                    string mail = Preferences.Get("CorreoConfirmado", "0");
                    string urli = logicaWs.Movile_Cambio_Clave_Mtd(mail);
                    Clave clave = new Clave { Contrasena = EntClaveUsu };
                    string json = JsonConvert.SerializeObject(clave);
                    string jsonProcedimiento = await logicaWs.ConectionPost(json, urli);

                    if (jsonProcedimiento.Equals("Valores actualizados"))
                    {
                        Preferences.Remove("CorreoConfirmado");
                        await PageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMLaContrasenaHaSidoActualizada, AppResources.VMaceptar);
                        Application.Current!.MainPage = new Splash();
                    }
                    else
                    {
                        Preferences.Remove("CorreoConfirmado");
                        claseBase.InsertarLogs_Mtd("ERROR", jsonProcedimiento, "ConfirmarClaveMV", "CambiarClave_MtoAsync", "ActualizarContrasena");
                        await PageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMLaContrasenaNoSeHaSidoActualizada, AppResources.VMaceptar);
                        Application.Current!.MainPage = new Splash();
                    }

                }
                catch (Exception ex)
                {
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ConfirmarClaveMV", "CambiarClave_MtoAsync", "ActualizarContrasena");
                    await PageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
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
    }
}