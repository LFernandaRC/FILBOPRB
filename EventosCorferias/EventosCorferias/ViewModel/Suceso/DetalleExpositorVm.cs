using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Suceso;
using EventosCorferias.Resources.RecursosIdioma;

using System.Windows.Input;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace EventosCorferias.ViewModel.Suceso
{
    public class DetalleExpositorVm : BaseViewModel
    {
        private readonly LogicaWs logicaWS;
        private readonly ClaseBase claseBase;
        private readonly Expositores Expositor;
        private readonly IPageServicio pageServicio;

        public ObservableCollection<Images> _imagenesRedExpositor;

        public ListasGeneral _SelectedItem;
        public Images _SelectedItemRedSocial;

        private string _url;
        private string _pabellon;
        private string _imagenFav;
        private string _ImagenLogo;
        private string _imagenExpositor;
        private string _nombreExpositor;
        private string _correoExpositor;
        private string _perfilExpositor;
        private string _telefonoExpositor;
        private string _direccionExpositor;
        private string _descripcionExpositor;

        private bool _iconUrl;
        private bool _iconStan;
        private bool _siguenosEn;
        private bool _iconCorreo;
        private bool _icontelefono;
        private bool _iconUbicacion;

        public Images SelectedRedSocial
        {
            get { return _SelectedItemRedSocial; }
            set { _SelectedItemRedSocial = null; ItemSelectedRedSocialCommand.Execute(value); OnPropertyChanged(nameof(SelectedRedSocial)); }
        }

        public ObservableCollection<Images> ImagenesRedSuceso
        {
            get { return _imagenesRedExpositor; }
            set { _imagenesRedExpositor = value; OnPropertyChanged(nameof(ImagenesRedSuceso)); }
        }

        public ListasGeneral SelectedModulo
        {
            get { return _SelectedItem; }
            set { _SelectedItem = null; ItemSelectedCommand.Execute(value); OnPropertyChanged(nameof(SelectedModulo)); }
        }

        public string UrlTextExpo
        {
            get { return _url; }
            set { _url = value; OnPropertyChanged(nameof(UrlTextExpo)); }
        }

        public string Pabellon
        {
            get { return _pabellon; }
            set { _pabellon = value; OnPropertyChanged(nameof(Pabellon)); }
        }

        public string TelefonoExpositor
        {
            get { return _telefonoExpositor; }
            set { _telefonoExpositor = value; OnPropertyChanged(nameof(TelefonoExpositor)); }
        }

        public string CorreoExpositor
        {
            get { return _correoExpositor; }
            set { _correoExpositor = value; OnPropertyChanged(nameof(CorreoExpositor)); }
        }

        public string DireccionExpositor
        {
            get { return _direccionExpositor; }
            set { _direccionExpositor = value; OnPropertyChanged(nameof(DireccionExpositor)); }
        }

        public string DescripcionExpositor
        {
            get { return _descripcionExpositor; }
            set { _descripcionExpositor = value; OnPropertyChanged(nameof(DescripcionExpositor)); }
        }

        public string ImagenExpositor
        {
            get { return _imagenExpositor; }
            set { _imagenExpositor = value; OnPropertyChanged(nameof(ImagenExpositor)); }
        }

        public string PerfilExpositor
        {
            get { return _perfilExpositor; }
            set { _perfilExpositor = value; OnPropertyChanged(nameof(PerfilExpositor)); }
        }

        public string ImagenFav
        {
            get { return _imagenFav; }
            set { _imagenFav = value; OnPropertyChanged(nameof(ImagenFav)); }
        }

        public string ImagenLogo
        {
            get { return _ImagenLogo; }
            set { _ImagenLogo = value; OnPropertyChanged(nameof(ImagenLogo)); }
        }

        public string NombreExpositor
        {
            get { return _nombreExpositor; }
            set { _nombreExpositor = value; OnPropertyChanged(nameof(NombreExpositor)); }
        }

        public bool SiguenosEn
        {
            get { return _siguenosEn; }
            set { _siguenosEn = value; OnPropertyChanged(nameof(SiguenosEn)); }
        }

        public bool IconStan
        {
            get { return _iconStan; }
            set { _iconStan = value; OnPropertyChanged(nameof(IconStan)); }
        }

        public bool IconUbicacion
        {
            get { return _iconUbicacion; }
            set { _iconUbicacion = value; OnPropertyChanged(nameof(IconUbicacion)); }
        }

        public bool Icontelefono
        {
            get { return _icontelefono; }
            set { _icontelefono = value; OnPropertyChanged(nameof(Icontelefono)); }
        }

        public bool IconUrl
        {
            get { return _iconUrl; }
            set { _iconUrl = value; OnPropertyChanged(nameof(IconUrl)); }
        }

        public bool IconCorreo
        {
            get { return _iconCorreo; }
            set { _iconCorreo = value; OnPropertyChanged(nameof(IconCorreo)); }
        }

        public Command PlayImg { get; set; }
        public Command BtnCompartir { get; set; }
        public Command FavActualizar { get; set; }
        public Command UrlExpositorLink { get; set; }
        public ICommand ItemSelectedCommand
        {
            get
            {
                return new Command(async list =>
                {
                    try
                    {
                        if (!StateConexion)
                        {
                            ListasGeneral lista = (ListasGeneral)list;
                            /*CUPONES REDIRECCIONA */
                            if (lista.Id.Equals("1"))
                            {
                                string urli = logicaWS.Movile_select_Cupones_Mtd("7", LenguajeBase, EmailUsuario, "0", Expositor.IdExpositor, "0", "0", "0", "0", "0");
                                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                                if (!jsonProcedimiento.Equals(""))
                                {
                                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                                    /*   if (jsArray.Count() > 0)
                                           await pageServicio.PushAsync(new CuponeraPage(false, Expositor.IdExpositor, "0"));
                                       else
                                           await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);*/
                                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                                }
                                else
                                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                            }

                            /*MAPA REDIRECCIONA */
                            if (lista.Id.Equals("2"))
                                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                            // await Application.Current.MainPage.Navigation.PushPopupAsync(new SitiosPage(Expositor.IdSuceso, "2", "2"));

                            /*CATALOGO PRODUCTOS REDIRECCIONA */
                            if (lista.Id.Equals("3"))
                            {
                                string urli = logicaWS.Movile_Select_catalogoProductos_Mtd(LenguajeBase, Expositor.IdExpositor, "0");
                                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                                if (!jsonProcedimiento.Equals(""))
                                {
                                    /*  JArray jsArray = JArray.Parse(jsonProcedimiento);
                                      if (jsArray.Count() > 0)
                                          await pageServicio.PushAsync(new CatalogoProductoPage(Expositor.IdExpositor));
                                      else
                                          await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);*/
                                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                                }
                                else
                                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMEstamosCargandoInfo, AppResources.VMaceptar);
                            }


                        }
                        else
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
                    }
                    catch (Exception ex)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleExpositorVM", "ICommand ItemSelectedCommand", "consultacuponera/consultacatalogoproducto");
                    }
                });
            }
        }
        public ICommand ItemSelectedRedSocialCommand
        {
            get
            {
                return new Command(async list =>
                {
                    try
                    {
                        if (!StateConexion)
                        {
                            Images lista = (Images)list;
                            await RootNavigation.PushAsync(new ContenidosWebView(AppResources.redSocial, lista.Link));
                        }
                        else
                            await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
                    }
                    catch (Exception ex)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                        claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleExpositorVM", "ICommand ItemSelectedRedSocialCommand", null);
                    }
                });
            }
        }

        public DetalleExpositorVm(Expositores expositor)
        {
            Title = AppResources.detalleExpositor;

            Expositor = expositor;

            logicaWS = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaApp", "");
            ImagenSplash = logicaWS.ImgMenuSuperior_Mtd();

            PlayImg = new Command(PlayImg_Mtd);
            BtnCompartir = new Command(BtnCompartir_Mtd);
            FavActualizar = new Command(async () => await FavActualizar_MtoAsync());
            UrlExpositorLink = new Command(async () => await UrlExpositorLink_MtdAsync());

            InfoExpositorSuceso();
        }

        public async void InfoExpositorSuceso()
        {
            try
            {
                /*Informacion General*/
                NombreExpositor = Expositor.NombreExpositor.ToUpper();
                DescripcionExpositor = claseBase.ValidaString(Expositor.PerfilExpositor);
                ImagenLogo = Expositor.LogoDetalle;
                ImagenFav = Expositor.ImagenFav;

                if (!string.IsNullOrWhiteSpace(Expositor.Pabellon))
                {
                    IconStan = true;
                    Pabellon = claseBase.CapitalizeFirstLetter(Expositor.Pabellon);
                }

                if (!string.IsNullOrWhiteSpace(Expositor.Url))
                {
                    UrlTextExpo = Expositor.Url.ToString();
                    IconUrl = true;
                }

                if (!string.IsNullOrWhiteSpace(Expositor.ImagenDetalle))
                {
                    ImagenExpositor = ImagenLogo;
                    ImagenExpositor = Expositor.ImagenDetalle;
                }

                if (!string.IsNullOrWhiteSpace(Expositor.email))
                {
                    IconCorreo = true;
                    CorreoExpositor = Expositor.email.ToLower();
                }

                if (!string.IsNullOrWhiteSpace(Expositor.telefono))
                {
                    Icontelefono = true;
                    TelefonoExpositor = Expositor.telefono.ToLower();
                }

                if (!string.IsNullOrWhiteSpace(Expositor.Direccion))
                {
                    IconUbicacion = true;
                    DireccionExpositor = Expositor.Direccion.ToLower();
                }

            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleExpositorVM", "InfoExpositorSuceso", "consultaredesexpositor");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }

            try
            {
                /*Redes Sociales */
                ImagenesRedSuceso = new ObservableCollection<Images>();
                string urli = logicaWS.Movile_Redes_Expo_Mtd(Expositor.IdExpositor);
                string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    ImagenesRedSuceso.Add(new Images() { Url = item.GetValue("Imagen").ToString(), Link = item.GetValue("Url").ToString() });
                }

                if (ImagenesRedSuceso.Count() > 0)
                    SiguenosEn = true;

            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleExpositorVM", "InfoExpositorSuceso2", "consultaredesexpositor");
            }
        }

        private async void PlayImg_Mtd()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await RootNavigation.PushAsync(new ContenidosWebView(AppResources.Expositores, ImagenLogo));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleExpositorVm", "PlayImg_Mtd", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void BtnCompartir_Mtd()
        {
            try
            {
                await Share.RequestAsync(new ShareTextRequest()
                {
                    Text = NombreExpositor + " " + DescripcionExpositor,
                    Uri = "https://feriadellibro.com/es/catalogo-expositores"
                }); ;
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleExpositorVm", "BtnCompartir_Mtd", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
        }

        private async Task UrlExpositorLink_MtdAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                await RootNavigation.PushAsync(new ContenidosWebView(AppResources.detalleExpositor, UrlTextExpo));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleExpositorVm", "BtnCompartir_Mtd", "n/a");
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task FavActualizar_MtoAsync()
        {
            try
            {
                if (ImagenFav.Equals("ic_favorito_obscuro"))
                {
                    ImagenFav = "ic_favortio_relleno";
                    string urli = logicaWS.Movile_Update_Fav_Expo_Mtd(LenguajeBase, EmailUsuario, Expositor.IdExpositor);
                    await logicaWS.ConectionGet(urli);
                }
                else
                {
                    ImagenFav = "ic_favorito_obscuro";
                    string urli = logicaWS.Movile_Delet_Fav_Expo_Mtd(LenguajeBase, EmailUsuario, Expositor.IdExpositor);
                    await logicaWS.ConectionDelete(urli);
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "DetalleExpositorVM", "FavActualizar_MtoAsync", "insertarusuarioexpositor/eliminarusuarioexpositor");
            }
        }

    }
}