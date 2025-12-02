using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.PopUp;
using EventosCorferias.Views.Suceso;
using EventosCorferias.Views.Usuario;
using EventosCorferias.Resources.RecursosIdioma;

using Mopups.Services;
using Newtonsoft.Json;
using System.Windows.Input;
using Newtonsoft.Json.Linq;

namespace EventosCorferias.ViewModel.Suceso
{
    public class DetalleSitioVm : BaseViewModel
    {
        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;

        private readonly string idSitio_;
        private string idAgendaAux_;
        private readonly string NombreServicio_;

        private string _TituloSitio;
        private string _Descripcion;
        private string _Horario;
        private string _Recomendaciones;
        private string _Ira;
        private string _imgPrincipal;
        private ImageSource _imgPrincipalTitulo;

        private bool _verBtnExpo;
        private bool _VerBtnAgenda;
        private bool _verHorario;
        private bool _verDescripcion;
        private bool _verRecomendaciones;
        private bool _verDetalleSitio;

        public bool VerHorario
        {
            get { return _verHorario; }
            set { _verHorario = value; OnPropertyChanged(nameof(TituloSitio)); }
        }

        public bool VerDetalleSitio
        {
            get { return _verDetalleSitio; }
            set { _verDetalleSitio = value; OnPropertyChanged(nameof(VerDetalleSitio)); }
        }

        public bool VerBtnExpo
        {
            get { return _verBtnExpo; }
            set { _verBtnExpo = value; OnPropertyChanged(nameof(VerBtnExpo)); }
        }

        public bool VerBtnAgenda
        {
            get { return _VerBtnAgenda; }
            set { _VerBtnAgenda = value; OnPropertyChanged(nameof(VerBtnAgenda)); }
        }

        public bool VerDescripcion
        {
            get { return _verDescripcion; }
            set { _verDescripcion = value; OnPropertyChanged(nameof(VerDescripcion)); }
        }

        public bool VerRecomendaciones
        {
            get { return _verRecomendaciones; }
            set { _verRecomendaciones = value; OnPropertyChanged(nameof(VerRecomendaciones)); }
        }

        public string TituloSitio
        {
            get { return _TituloSitio; }
            set { _TituloSitio = value; OnPropertyChanged(nameof(TituloSitio)); }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; OnPropertyChanged(nameof(Descripcion)); }
        }

        public string Horario
        {
            get { return _Horario; }
            set { _Horario = value; OnPropertyChanged(nameof(Horario)); }
        }

        public string Recomendaciones
        {
            get { return _Recomendaciones; }
            set { _Recomendaciones = value; OnPropertyChanged(nameof(Recomendaciones)); }
        }

        public string Ira
        {
            get { return _Ira; }
            set { _Ira = value; OnPropertyChanged(nameof(Ira)); }
        }

        public string ImgPrincipal
        {
            get { return _imgPrincipal; }
            set { _imgPrincipal = value; OnPropertyChanged(nameof(ImgPrincipal)); }
        }

        public ImageSource ImgPrincipalTitulo
        {
            get { return _imgPrincipalTitulo; }
            set { _imgPrincipalTitulo = value; OnPropertyChanged(nameof(ImgPrincipalTitulo)); }
        }

        public Command PlayImg { get; }
        public ICommand AbrirLink { get; }
        public ICommand BtnAgenda { get; }
        public ICommand BtnExpositor { get; }


        public DetalleSitioVm(SitiosM servicios)
        {
            Title = AppResources.detalleSitio;

            IsBusy = true;
            VerDetalleSitio = true;
            idSitio_ = servicios.IdSitio;
            NombreServicio_ = servicios.NombreServicio;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();

            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "").ToString();
            ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();

            PlayImg = new Command(PlayImg_Mtd);
            AbrirLink = new Command(AbrirLink_Mtd);
            BtnAgenda = new Command(BtnAgenda_Mtd);
            BtnExpositor = new Command(BtnExpositor_Mtd);

            _ = CargaGeneralInfoAsync(servicios);
            ValidaBtnExpoAgrnda();
        }

        public async Task CargaGeneralInfoAsync(SitiosM servicios)
        {
            try
            {
                TituloSitio = claseBase.ValidaString(servicios.NombreServicio);
                Descripcion = claseBase.ValidaString(servicios.Descripcion);
                Horario = claseBase.ValidaString(servicios.Horario);
                Recomendaciones = claseBase.ValidaString(servicios.Recomendacion);
                Ira = claseBase.ValidaString(servicios.Ira);

                try
                {
                    if (!string.IsNullOrWhiteSpace(Recomendaciones))
                    {
                        VerRecomendaciones = true;
                    }
                    else
                    {
                        VerRecomendaciones = false;
                    }

                    if (!string.IsNullOrWhiteSpace(Descripcion))
                    {
                        VerDescripcion = true;
                    }
                    else
                    {
                        VerDescripcion = false;
                    }
                }
                catch { }

                try
                {
                    ImgPrincipal = servicios.iconoservicio.Split(',').ToList()[0].Trim();
                    var rutaImagen = servicios.iconoservicio.Split(',').ToList()[0].Trim();

                    if (!string.IsNullOrEmpty(rutaImagen))
                    {
                        ImgPrincipalTitulo = ImageSource.FromUri(new Uri(rutaImagen));
                        Console.WriteLine($"Ruta imagen: {rutaImagen}");
                        OnPropertyChanged(nameof(ImgPrincipalTitulo)); 
                    }
                }
                catch (Exception ex)
                {
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleSitioVm", "CargaGeneralInfoAsync", "n/a");
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleSitioVm", "CargaGeneralInfoAsync2", "n/a");
            }
        }

        private async void ValidaBtnExpoAgrnda()
        {
            try
            {
                ConsultaExpositor consultaAgenda = new ConsultaExpositor
                {
                    Idioma = "es",
                    NombreExpositor = "0",
                    correo = EmailUsuario
                };
                string urli = logicaWS.Moviel_select_consAExpositorSitioLugar_Mtd("1", "0", Preferences.Get("IdSuceso", "0"), idSitio_);
                string json = JsonConvert.SerializeObject(consultaAgenda);

                string jsonProcedimiento = await logicaWS.ConectionPost(json, urli);

                JArray jsArray = JArray.Parse(jsonProcedimiento);

                if (jsArray.Count > 0)
                {
                    VerBtnExpo = true;
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleSitioVm", "ValidaBtnExpoAgrnda", "n/a");
            }

            try
            {

                ConsultaAgenda consultaAgenda = new ConsultaAgenda
                {
                    Categoria = "0",
                    FechaInicio = "0",
                    IdAgenda = "0",
                    Lugar = "0",
                    Franja = "0"
                };
                string urli = logicaWS.Moviel_select_consultaagendasuceso_Mtd("0", "3", EmailUsuario, Preferences.Get("IdSuceso", "0"), LenguajeBase, "0", idSitio_);
                string json = JsonConvert.SerializeObject(consultaAgenda);

                string jsonProcedimiento = await logicaWS.ConectionPost(json, urli);

                JArray jsArray = JArray.Parse(jsonProcedimiento);

                if (jsArray.Count > 0)
                {
                    VerBtnAgenda = true;

                    foreach (JObject item in jsArray)
                    {
                        idAgendaAux_ = item.GetValue("IdAgenda").ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleSitioVm", "ValidaBtnExpoAgrnda2", "n/a");
            }
            finally
            {
                IsBusy = false;
            }

        }


        private async void PlayImg_Mtd(object obj)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await RootNavigation.PushAsync(new ContenidosWebView(AppResources.detalleSitio, ImgPrincipal));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleSitioVm", "PlayImg_Mtd", "n/a");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void BtnExpositor_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await MopupService.Instance.PushAsync(new ExpositoresPopUp(idSitio_, NombreServicio_));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleSitioVm", "PlayImg_Mtd", "n/a");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void BtnAgenda_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await MopupService.Instance.PushAsync(new AgendaSitiosPopUp(idSitio_, idAgendaAux_));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleSitioVm", "PlayImg_Mtd", "n/a");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void AbrirLink_Mtd(Object obj)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                if (obj.ToString().Contains("https://") || obj.ToString().Contains("http://"))
                {
                    await RootNavigation.PushAsync(new ContenidosWebView(AppResources.sitios, obj.ToString()));
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleSitioVm", "PlayImg_Mtd", "n/a");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }


    public class ConsultaExpositor
    {
        public string Idioma { get; set; }
        public string correo { get; set; }
        public string NombreExpositor { get; set; }
    }

}