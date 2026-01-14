using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json.Linq;
using System.Windows.Input;

namespace EventosCorferias.ViewModel.Usuario
{
    class NotificacionesVm : BaseViewModel
    {
        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;
        public readonly IPageServicio pageServicio;

        private bool auxFecha;
        private bool _equisCalendario;
        private bool _flechaCalendario;
        private string idSuceso;

        private string _cantidadAgenda;
        public string? IdCominidad_;

        private List<Notificaciones> listaAgenda;
        private List<Notificaciones> listaAgendaTem;

        private DateTime _entFechaBuscar = DateTime.Now;

        public bool FlechaCalendario
        {
            get { return _flechaCalendario; }
            set { _flechaCalendario = value; OnPropertyChanged(nameof(FlechaCalendario)); }
        }

        public bool EquisCalendario
        {
            get { return _equisCalendario; }
            set { _equisCalendario = value; OnPropertyChanged(nameof(EquisCalendario)); }
        }

        public string CantidadAgenda
        {
            get { return _cantidadAgenda; }
            set { _cantidadAgenda = value; OnPropertyChanged(nameof(CantidadAgenda)); }
        }

        public List<Notificaciones> ListaAgenda
        {
            get { return listaAgenda; }
            set { listaAgenda = value; OnPropertyChanged(); }
        }

        public DateTime EntFechaBuscar
        {
            get { return _entFechaBuscar; }
            set
            {
                SetProperty(ref _entFechaBuscar, value);
                if (value != null)
                {
                    if (auxFecha.Equals(true))
                    {
                        FlechaCalendario = false;
                        EquisCalendario = true;
                        FiltradoGeneral_MtoAsync();
                    }
                }
                OnPropertyChanged(nameof(EntFechaBuscar));
            }
        }

        public ICommand ItemSelectedCommandModulos
        {
            get
            {
                return new Command(async list =>
                {
                    try
                    {
                        Notificaciones SelectNotificacion = (Notificaciones)list;

                        if (!StateConexion)
                        {
                            bool res;
                            bool Eva = false;

                            switch (SelectNotificacion.IdEvaluacion)
                            {
                                case null:
                                    res = await pageServicio.DisplayAlert(AppResources.nombreMarca, SelectNotificacion.Texto, AppResources.eliminar, AppResources.VMaceptar);
                                    break;
                                case "":
                                    res = await pageServicio.DisplayAlert(AppResources.nombreMarca, SelectNotificacion.Texto, AppResources.eliminar, AppResources.VMaceptar);
                                    break;
                                case "0":
                                    res = await pageServicio.DisplayAlert(AppResources.nombreMarca, SelectNotificacion.Texto, AppResources.eliminar, AppResources.VMaceptar);
                                    break;
                                default:
                                    res = await pageServicio.DisplayAlertDos(AppResources.nombreMarca, SelectNotificacion.Texto, AppResources.evaluar);
                                    Eva = true;
                                    break;
                            }

                            if (res && !Eva)
                            {
                                string urli = logicaWS.Movile_Update_Notificaciones_Mtd("3", EmailUsuario, LenguajeBase, SelectNotificacion.IdNotificacion, "3", idSuceso);
                                string? jsonProcedimiento = await logicaWS.ConectionGet(urli);

                                await CargarAgenda_MtoAsync(IdCominidad_);
                            }

                            if (!res)
                            {
                                string urli = logicaWS.Movile_Update_Notificaciones_Mtd("3", EmailUsuario, LenguajeBase, SelectNotificacion.IdNotificacion, "1", idSuceso);
                                string? jsonProcedimiento = await logicaWS.ConectionGet(urli);
                                foreach (var aux in ListaAgenda)
                                {
                                    if (aux.IdNotificacion.Equals(SelectNotificacion.IdNotificacion))
                                    {
                                        aux.ImagenIcono = "ic_notificacion_relleno_tres.png";
                                    }
                                }
                            }

                            if (res && Eva)
                            {
                                string urli = logicaWS.Movile_Update_Notificaciones_Mtd("3", EmailUsuario, LenguajeBase, SelectNotificacion.IdNotificacion, "1", idSuceso);
                                string? jsonProcedimiento = await logicaWS.ConectionGet(urli);
                                //await RootMainPage.PushAsync(new EvaluacionPage(SelectNotificacion.IdEvaluacion));
                            }

                            CantidadAgenda = ListaAgenda.Count.ToString() + " " + AppResources.resultados;

                        }
                        else
                        {
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
                        }

                    }
                    catch (Exception exs)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                        claseBase.InsertarLogs_Mtd("ERROR", exs.Message, "DetalleSucesoVM", "ICommand ItemSelectedCommandModulos", "Error en el ItemSelectedCommandModulos");
                    }
                    finally
                    {
                        IsBusy = false;
                        ContadorNotificaciones_Mtd();
                    }

                });
            }
        }

        public ICommand OrdenarAgenda { get; }
        public ICommand FocusCalendario { get; }
        public ICommand LimpiarCalendario { get; }
        public ICommand BorrarNotificacion { get; }

        public NotificacionesVm(string idComunidad)
        {
            IsBusy = true;
            IdCominidad_ = idComunidad;
            Title = AppResources.notificaciones;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            idSuceso = Preferences.Get("IdSuceso", "");
            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();

            OrdenarAgenda = new Command(OrdenarAgenda_Mto);
            FocusCalendario = new Command(claseBase.FocusPickerDate_Mtd);
            LimpiarCalendario = new Command(LimpiarCalendario_Mto);
            BorrarNotificacion = new Command(BorrarNotificacionGenrales_MtoAsync);

            Inicializar(idComunidad);
            ContadorNotificaciones_Mtd();
        }

        private async void Inicializar(string idComunidad)
        {
            await CargarAgenda_MtoAsync(idComunidad);
        }

        private async Task CargarAgenda_MtoAsync(string idComunidad)
        {
            try
            {
                ListaAgenda = new List<Notificaciones>();
                listaAgendaTem = new List<Notificaciones>();

                string urli;

                if (idComunidad.Equals("0"))
                {
                    urli = logicaWS.Movile_Select_Notificaciones_Mtd(EmailUsuario, LenguajeBase, idSuceso);
                }
                else
                {
                    urli = logicaWS.Movile_Select_NotificacionesComunidades_Mtd("1", EmailUsuario, LenguajeBase, idComunidad);
                }

                string? jsonProcedimiento = await logicaWS.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);


                foreach (JObject item in jsArray)
                {

                    Notificaciones agenda = new Notificaciones
                        (
                        item.GetValue("IdNotificacion")?.ToString() ?? string.Empty,
                        item.GetValue("FechaProgramada")?.ToString() ?? string.Empty,
                        item.GetValue("Titulo")?.ToString() ?? string.Empty,
                        item.GetValue("Texto")?.ToString() ?? string.Empty,
                        item.GetValue("Estado")?.ToString() ?? string.Empty,
                        item.GetValue("IdEvaluacion")?.ToString() ?? string.Empty,
                        item.GetValue("DiaIn")?.ToString() ?? string.Empty,
                        item.GetValue("MesIn")?.ToString() ?? string.Empty,
                        item.GetValue("Vigencia")?.ToString() ?? string.Empty
                        );

                    ListaAgenda.Add(agenda);
                    listaAgendaTem.Add(agenda);
                }


                ListaAgenda = ListaAgenda.OrderBy(x => x.Fecha).ToList();
                listaAgendaTem = listaAgendaTem.OrderBy(x => x.Fecha).ToList();
                CantidadAgenda = ListaAgenda.Count.ToString() + " " + AppResources.resultados;

                auxFecha = true;
                FlechaCalendario = true;
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "NotificacionesVm", "CargarAgenda_MtoAsync", "consultaagendasuceso");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void FiltradoGeneral_MtoAsync()
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            try
            {
                ListaAgenda = listaAgendaTem.ToList();

                if (EquisCalendario)
                    ListaAgenda = ListaAgenda.Where(x => x.Fecha.Equals(EntFechaBuscar)).ToList();

                CantidadAgenda = ListaAgenda.Count.ToString() + " " + AppResources.resultados;
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "NotificacionesVm", "FiltradoGeneral_MtoAsync", "Error en el filtrado general en las notificaciones");
            }
            finally
            {
                auxFecha = true;
                IsBusy = false;
            }
        }

        private async void BorrarNotificacionGenrales_MtoAsync()
        {
            IsBusy = true;
            await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
            try
            {
                bool res = await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.estasSeguroDeEliminarLasNotificaciones, AppResources.eliminar, AppResources.VMCancelar);

                if (res)
                {
                    string urli = logicaWS.Movile_Update_Notificaciones_Mtd("2", EmailUsuario, LenguajeBase, "0", "0", idSuceso);
                    string? jsonProcedimiento = await logicaWS.ConectionGet(urli);
                    CantidadAgenda = 0 + " " + AppResources.resultados;

                    ListaAgenda = new List<Notificaciones>();
                    listaAgendaTem = new List<Notificaciones>();

                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.SeHanBorradoConExito, AppResources.VMaceptar);
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "NotificacionesInternasVM", "BorrarNotificacionGenrales_MtoAsync", "actualizanotifiaciones");
            }
            finally
            {
                IsBusy = false;
                ContadorNotificaciones_Mtd();
            }
        }

        private void LimpiarCalendario_Mto()
        {
            IsBusy = true;
            auxFecha = false;
            FlechaCalendario = true;
            EquisCalendario = false;
            EntFechaBuscar = DateTime.Now;
            FiltradoGeneral_MtoAsync();
        }

        private void OrdenarAgenda_Mto()
        {
            ListaAgenda.Reverse();
            ListaAgenda = ListaAgenda.ToList();
        }
    }
}