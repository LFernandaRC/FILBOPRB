using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Suceso;
using EventosCorferias.Resources.RecursosIdioma;

using Newtonsoft.Json.Linq;
using System.Windows.Input;

namespace EventosCorferias.ViewModel.Suceso
{
    public class ConferencistaVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        private readonly ClaseBase claseBase;
        public readonly IPageServicio pageServicio;

        private List<Conferencista> _listaConferencista;
        private List<Conferencista> listaConferencistaTemp;

        private string? _entConferencia;
        private string? _cantidadConferencia;

        private readonly string? IdSuceso;
        private readonly bool EsFavorito;

        private async void SeleccionarConferencista(Conferencista conferencista)
        {
            if (conferencista != null)
            {
                CargarConferencista(conferencista);
            }
        }

        public List<Conferencista> ListaConferencista
        {
            get { return _listaConferencista; }
            set { _listaConferencista = value; OnPropertyChanged(); }
        }

        public string? EntConferencia
        {
            get { return _entConferencia; }
            set { _entConferencia = value; OnPropertyChanged(nameof(EntConferencia)); }
        }

        public string? CantidadConferencia
        {
            get { return _cantidadConferencia; }
            set { _cantidadConferencia = value; OnPropertyChanged(nameof(CantidadConferencia)); }
        }

        public ICommand SeleccionarFavBtn { get; }

        public ICommand BuscarConferencista { get; }

        public ICommand OrdenarConferencista { get; }

        public Command<Conferencista> SeleccionarConferencistaCommand { get; }

        public ICommand ItemSelectedCommandConferencista
        {
            get
            {
                return new Command(async list =>
                {
                    Conferencista lista = (Conferencista)list;
                    CargarConferencista(lista);
                });
            }
        }

        public ConferencistaVm(string idSuce, bool esFavorito, Entry filtroTres)
        {
            IsBusy = true;
            EsFavorito = esFavorito;

            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            IdSuceso = Preferences.Get("IdSuceso", "");
            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            ImagenSplash = logicaWs.ImgMenuSuperior_Mtd();

            SeleccionarFavBtn = new Command(SeleccionarFav_Mtd);
            BuscarConferencista = new Command(() => FiltroGeneral_Mtd());
            OrdenarConferencista = new Command(() => OrdenarConferencista_Mtd());
            SeleccionarConferencistaCommand = new Command<Conferencista>(SeleccionarConferencista);

            filtroTres.Completed += EntryCompleted;

            Inicializar();
        }


        private async void Inicializar()
        {
            await CargarConferencista_MtoAsync();
        }

        private async Task CargarConferencista_MtoAsync()
        {
            try
            {
                ListaConferencista = new List<Conferencista>();
                listaConferencistaTemp = new List<Conferencista>();

                string urli;

                if (EsFavorito)
                    urli = logicaWs.Movile_select_Confenrencistas_Fav_Mtd("0", "0", "8", "0", EmailUsuario, IdSuceso, LenguajeBase);
                else
                    urli = logicaWs.Movile_select_Confenrencistas_Fav_Mtd("0", "0", "1", "0", EmailUsuario, IdSuceso, LenguajeBase);

                string jsonProcedimiento = await logicaWs.ConectionGet(urli);
                if (!claseBase.ValidaString(jsonProcedimiento).Equals(""))
                {
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    foreach (JObject item in jsArray)
                    {
                        Conferencista conferencista = new Conferencista(
                                        claseBase.ValidaString(item.GetValue("idConferencista")?.ToString() ?? string.Empty),
                                        claseBase.ValidaString(item.GetValue("NombreSuceso")?.ToString() ?? string.Empty),
                                        claseBase.ValidaString(item.GetValue("idSuceso")?.ToString() ?? string.Empty),
                                        claseBase.ValidaString(item.GetValue("IdSucesoServicio")?.ToString() ?? string.Empty),
                                        claseBase.ValidaString(item.GetValue("NombreConferencista")?.ToString() ?? string.Empty).ToUpper(),
                                        claseBase.CapitalizeFirstLetter(item.GetValue("Profesion")?.ToString() ?? string.Empty),
                                        claseBase.ValidaString(item.GetValue("Cita")?.ToString() ?? string.Empty),
                                        claseBase.ValidaString((item.GetValue("Perfil")?.ToString()?.Trim() ?? string.Empty)),
                                        claseBase.ValidaString(item.GetValue("Palabraclave")?.ToString() ?? string.Empty),
                                        claseBase.ValidaString((item.GetValue("PerfilConferencista")?.ToString()?.Trim() ?? string.Empty)),
                                        claseBase.ValidaString(item.GetValue("SitioWeb")?.ToString() ?? string.Empty),
                                        claseBase.ValidaString(item.GetValue("Fav")?.ToString() ?? string.Empty),
                                        claseBase.ValidaString(item.GetValue("Imagen")?.ToString() ?? string.Empty),
                                        claseBase.ValidaString(item.GetValue("ImagenFeria")?.ToString() ?? string.Empty),
                                        claseBase.ValidaString(item.GetValue("IdPais")?.ToString() ?? string.Empty),
                                        claseBase.ValidaString(item.GetValue("NombrePais")?.ToString() ?? string.Empty),
                                        claseBase.ValidaString(item.GetValue("IdAutor")?.ToString() ?? string.Empty));

                        ListaConferencista.Add(conferencista);

                        foreach (var x in ListaConferencista)
                        {
                            if (x.idSuceso.Equals(conferencista.idSuceso))
                            {
                                if (x.Fav.Equals("1"))
                                {
                                    x.ImagenFav = "ic_favortio_relleno.png";
                                }
                                else
                                {
                                    x.ImagenFav = "ic_favorito_obscuro.png";
                                }
                            }
                        }
                    }
                }

                ListaConferencista = ListaConferencista.OrderBy(x => x.NombreConferencista).ToList();
                listaConferencistaTemp = ListaConferencista.OrderBy(x => x.NombreConferencista).ToList();

                CantidadConferencia = ListaConferencista.Count.ToString() + " " + AppResources.resultados;
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ConferencistaVM", "CargarConferencista_MtoAsync", "consultafavoritoconferencista/consultaconferencista");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void CargarConferencista(Conferencista conferencista)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                if (conferencista != null)
                {
                    await RootNavigation.PushAsync(new DetalleConferencistaView(conferencista), false);
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ConferencistaVM", "CargarConferencista", "redireccionamiento");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, ex.Message, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void FiltroGeneral_Mtd()
        {
            IsBusy = true;

            if (EntConferencia != null)
            {
                ListaConferencista = listaConferencistaTemp.Where(x => x.NombreConferencista.ToLower().Contains(EntConferencia.ToLower().Trim())).ToList();
                CantidadConferencia = ListaConferencista.Count.ToString() + " " + AppResources.resultados;
            }
            else
            {
                pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMNoDejesCamposVaciosConferencista, AppResources.VMaceptar);
            }

            IsBusy = false;
        }

        private void OrdenarConferencista_Mtd()
        {
            ListaConferencista.Reverse();
            ListaConferencista = ListaConferencista.ToList();
        }

        private async void SeleccionarFav_Mtd(object obj)
        {
            try
            {
                var content = obj as Conferencista;

                foreach (var item in ListaConferencista)
                {
                    if (item.idConferencista.Equals(content.idConferencista))
                    {
                        if (item.Fav.Equals("1")) /*1 Para favorito*/
                        {
                            item.ImagenFav = "ic_favorito_obscuro.png";
                            item.Fav = "0";
                            string urli = logicaWs.Movile_delet_Fav_Confenrencista_Mtd(EmailUsuario, item.idConferencista);
                            await logicaWs.ConectionGet(urli);
                        }
                        else
                        {
                            item.ImagenFav = "ic_favortio_relleno.png";
                            item.Fav = "1";
                            string urli = logicaWs.Movile_Update_Fav_Confenrencista_Mtd(EmailUsuario, item.idConferencista);
                            await logicaWs.ConectionGet(urli);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ConferencistaVM", "SeleccionarFav_Mtd", "eliminarfavoritoconferencista/insertarfavoritoconferencista");
            }
        }

        void EntryCompleted(object sender, EventArgs e)
        {
            FiltroGeneral_Mtd();
        }

    }
}
