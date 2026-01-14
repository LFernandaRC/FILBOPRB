using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Views.Suceso;
using EventosCorferias.Resources.RecursosIdioma;

using System.Windows.Input;
using System.Collections.ObjectModel;

namespace EventosCorferias.ViewModel.Suceso
{
    public class DetalleServicioVm : BaseViewModel
    {
        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;

        private ObservableCollection<ListasGeneral> _imagenesCarrusel;

        private string _TituloSitio;
        private string _Descripcion;
        private string _Horario;
        private string _Recomendaciones;
        private string _Ira;

        private bool _verDescripcion;
        private bool _verRecomendaciones;

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

        public ObservableCollection<ListasGeneral> ImagenesCarrusel
        {
            get { return _imagenesCarrusel; }
            set { _imagenesCarrusel = value; OnPropertyChanged(nameof(ImagenesCarrusel)); }
        }

        public Command PlayImg { get; }
        public ICommand AbrirLink { get; }

        public DetalleServicioVm(ServiciosM servicios)
        {
            Title = AppResources.detalleServicio;

            IsBusy = true;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();

            EmailUsuario = Preferences.Get("Email", "");
            ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();

            AbrirLink = new Command(AbrirLink_Mtd);
            PlayImg = new Command(PlayImg_Mtd);

            CargaGeneralInfoAsync(servicios);
            ContadorNotificaciones_Mtd();
        }

        private void CargaGeneralInfoAsync(ServiciosM servicios)
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
                catch (Exception ex)
                {
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleSitioMapaVM", "colores", "consultadetallesitiob1");
                }

                try
                {
                    ImagenesCarrusel = new ObservableCollection<ListasGeneral>();
                    foreach (var aux in servicios.iconoservicio.Split(',').ToList())
                    {
                        if (!string.IsNullOrEmpty(aux))
                        {
                            ImagenesCarrusel.Add(new ListasGeneral
                            {
                                Observacion = aux.Trim()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleSitioMapaVM", "CargaGeneralInfoAsync", "consultadetallesitiob1");
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleSitioMapaVM", "CargaGeneralInfoAsync", "consultadetallesitiob1");
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
                ListasGeneral lik = (ListasGeneral)obj;
                await RootNavigation.PushAsync(new ContenidosWebView(AppResources.detalleServicio, lik.Observacion));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleServicioVm", "PlayImg_Mtd", "n/a");
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
                    await RootNavigation.PushAsync(new ContenidosWebView(AppResources.detalleServicio, obj.ToString()));
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleServicioVm", "AbrirLink_Mtd", "n/a");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }

}
