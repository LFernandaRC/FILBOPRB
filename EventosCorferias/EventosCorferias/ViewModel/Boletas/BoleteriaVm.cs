using EventosCorferias.Models;
using EventosCorferias.Services;
using EventosCorferias.Interfaces;
using EventosCorferias.Views.Suceso;
using EventosCorferias.Views.Boletas;
using EventosCorferias.Resources.RecursosIdioma;

/*using ZXing;
using SkiaSharp;
using ZXing.Common;*/
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using Newtonsoft.Json;

namespace EventosCorferias.ViewModel.Boletas
{
    public class BoleteriaVm : BaseViewModel
    {
        private readonly LogicaWs logicaWs;
        private readonly ClaseBase claseBase;
        private readonly IPageServicio pageServicio;

        private Color _ColorFondoCompra;
        private Color _ColorTextoCompra;
        private Color _ColorFondoBoletas;
        private Color _ColorTextoBoletas;
        private Color _ColorFondoPreregistro;
        private Color _ColorTextoPreregistro;

        private bool _VerBtnComprar;
        private bool _VerListaMisBoletas;
        private bool _VerListaPreRegistro;

        private List<CompraBoleteria> listaComprar;
        private List<MisBoletas> listaMisBoletas;
        private List<PreRegistroTaquilla> listaPreRegistro;

        public bool VerListaMisBoletas
        {
            get { return _VerListaMisBoletas; }
            set { _VerListaMisBoletas = value; OnPropertyChanged(nameof(VerListaMisBoletas)); }
        }
        public bool VerListaPreRegistro
        {
            get { return _VerListaPreRegistro; }
            set { _VerListaPreRegistro = value; OnPropertyChanged(nameof(VerListaPreRegistro)); }
        }
        public bool VerBtnComprar
        {
            get { return _VerBtnComprar; }
            set { _VerBtnComprar = value; OnPropertyChanged(nameof(VerBtnComprar)); }
        }

        public Color ColorFondoBoletas
        {
            get { return _ColorFondoBoletas; }
            set { _ColorFondoBoletas = value; OnPropertyChanged(nameof(ColorFondoBoletas)); }
        }

        public Color ColorTextoBoletas
        {
            get { return _ColorTextoBoletas; }
            set { _ColorTextoBoletas = value; OnPropertyChanged(nameof(ColorTextoBoletas)); }
        }

        public Color ColorFondoPreregistro
        {
            get { return _ColorFondoPreregistro; }
            set { _ColorFondoPreregistro = value; OnPropertyChanged(nameof(ColorFondoPreregistro)); }
        }

        public Color ColorTextoPreregistro
        {
            get { return _ColorTextoPreregistro; }
            set { _ColorTextoPreregistro = value; OnPropertyChanged(nameof(ColorTextoPreregistro)); }
        }

        public Color ColorFondoCompra
        {
            get { return _ColorFondoCompra; }
            set { _ColorFondoCompra = value; OnPropertyChanged(nameof(ColorFondoCompra)); }
        }

        public Color ColorTextoCompra
        {
            get { return _ColorTextoCompra; }
            set { _ColorTextoCompra = value; OnPropertyChanged(nameof(ColorTextoCompra)); }
        }

        public List<CompraBoleteria> ListaComprar
        {
            get { return listaComprar; }
            set { listaComprar = value; OnPropertyChanged(nameof(ListaComprar)); }
        }

        public List<MisBoletas> ListaMisBoletas
        {
            get { return listaMisBoletas; }
            set { listaMisBoletas = value; OnPropertyChanged(nameof(ListaMisBoletas)); }
        }

        public List<PreRegistroTaquilla> ListaPreRegistro
        {
            get { return listaPreRegistro; }
            set { listaPreRegistro = value; OnPropertyChanged(nameof(ListaPreRegistro)); }
        }

        public ICommand BtnComprar { get; }
        public ICommand BtnMisBoletas { get; }
        public ICommand BtnPreRegistro { get; }
        public ICommand ItemSelectedCommandModulos
        {
            get
            {
                return new Command(async list =>
                {
                    try
                    {
                        IsBusy = true;
                        await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                        CompraBoleteria SelectNotificacion = (CompraBoleteria)list;
                        await RootNavigation.PushAsync(new BoleteriaVentaView(SelectNotificacion.IdSuceso.ToString()), false);
                    }
                    catch (Exception exs)
                    {
                        await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                        claseBase.InsertarLogs_Mtd("ERROR", exs.Message, "DetalleSucesoVM", "ICommand ItemSelectedCommandModulos", "n/a");
                    }
                    finally
                    {
                        IsBusy = false;
                    }

                });
            }
        }
        public Command<MisBoletas> SeleccionarModuloCommand { get; }
        public Command<PreRegistroTaquilla> SeleccionarModuloCommandDos { get; }

        public BoleteriaVm()
        {
            IsBusy = true;
            Title = AppResources.Boleteria;

            logicaWs = new LogicaWs();
            claseBase = new ClaseBase();
            pageServicio = new PageServicio();

            EmailUsuario = Preferences.Get("Email", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");

            ImagenSplash = logicaWs.ImgMenuSuperior_Mtd();

            BtnComprar = new Command(BtnComprar_Mtd);
            BtnMisBoletas = new Command(BtnMisBoletas_Mtd);
            BtnPreRegistro = new Command(BtnPreRegistro_Mtd);
            SeleccionarModuloCommand = new Command<MisBoletas>(SeleccionarModulo);
            SeleccionarModuloCommandDos = new Command<PreRegistroTaquilla>(SeleccionarModuloDos);

            Inicializar();
        }

        public void Inicializar()
        {
            try
            {
                BtnComprar_Mtd();
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BoleteriaVm", "BtnPreRegistro_Mtd", "n/a");
            }
        }


        /*SECCION COMPRAR BOLETA*/
        public void BtnComprar_Mtd()
        {
            IsBusy = true;
            VerBtnComprar = true;
            VerListaMisBoletas = false;
            VerListaPreRegistro = false;

            ColorFondoBoletas = Colors.WhiteSmoke;
            ColorTextoBoletas = Color.FromArgb("#333333");

            ColorFondoPreregistro = Colors.WhiteSmoke;
            ColorTextoPreregistro = Color.FromArgb("#333333");

            ColorFondoCompra = Color.FromArgb("#041B3B");
            ColorTextoCompra = Colors.WhiteSmoke;

            CargarListasComprar_Mtd();

        }
        public async void CargarListasComprar_Mtd()
        {
            try
            {
                ListaComprar = new List<CompraBoleteria>();

                string urli = logicaWs.Movile_Select_Comprar_Mtd("0", "0", "1", Preferences.Get("IdSuceso", ""), "0", "0", EmailUsuario, LenguajeBase);
                string? jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    CompraBoleteria compra = new CompraBoleteria(item.GetValue("FechaInicio")?.ToString() ?? string.Empty, item.GetValue("FechaFin")?.ToString() ?? string.Empty,
                        item.GetValue("MesIn")?.ToString(), item.GetValue("MesFin")?.ToString())
                    {
                        IdSuceso = item.GetValue("IdSuceso")?.ToString(),
                        NombreSuceso = item.GetValue("NombreSuceso")?.ToString(),
                        FechaInicio = item.GetValue("FechaInicio")?.ToString(),
                        FechaFin = item.GetValue("FechaFin")?.ToString(),
                        Fav = item.GetValue("Fav")?.ToString(),
                        DiaIn = item.GetValue("DiaIn")?.ToString(),
                        DiaFin = item.GetValue("DiaFin")?.ToString(),
                        MesIn = item.GetValue("MesIn")?.ToString(),
                        MesFin = item.GetValue("MesFin")?.ToString(),
                        IdCiudad = item.GetValue("IdCiudad")?.ToString(),
                        NombreCiudad = item.GetValue("NombreCiudad")?.ToString(),
                        imagen = item.GetValue("imagen")?.ToString(),
                        verdetalle = item.GetValue("verdetalle")?.ToString(),
                        IdCategoria = item.GetValue("IdCategoria")?.ToString(),
                    };
                    ListaComprar.Add(compra);
                }

                ListaComprar = ListaComprar.OrderBy(x => x.FechaFiltroIncio).ToList();
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BoleteriaVm", "CargarListasComprar_Mtd", "consultaboleteriasucesoapp");
            }
            finally
            {
                IsBusy = false;
            }
        }


        /*SECCION MIS BOLETAS*/
        public void BtnMisBoletas_Mtd()
        {
            IsBusy = true;
            VerBtnComprar = false;
            VerListaMisBoletas = true;
            VerListaPreRegistro = false;

            ColorFondoPreregistro = Colors.WhiteSmoke;
            ColorTextoPreregistro = Color.FromArgb("#333333");

            ColorFondoCompra = Colors.WhiteSmoke;
            ColorTextoCompra = Color.FromArgb("#333333");

            ColorFondoBoletas = Color.FromArgb("#041B3B");
            ColorTextoBoletas = Colors.WhiteSmoke;

            CargarListaMisBoletas_Mtd();
        }
        public async void CargarListaMisBoletas_Mtd()
        {
            try
            {
                ListaMisBoletas = new List<MisBoletas>();

                string urli = logicaWs.Movile_Select_MisBoletasr_Mtd("1", EmailUsuario, LenguajeBase, Preferences.Get("IdSuceso", ""));
                string? jsonProcedimiento = await logicaWs.ConectionGet(urli);
                JArray jsArray = JArray.Parse(jsonProcedimiento);
                foreach (JObject item in jsArray)
                {
                    MisBoletas misBoletas = new MisBoletas(item.GetValue("FechaTransaccion")?.ToString() ?? string.Empty)
                    {
                        ID = item.GetValue("ID")?.ToString(),
                        IdSuceso = item.GetValue("IdSuceso")?.ToString(),
                        NombreSuceso = item.GetValue("NombreSuceso")?.ToString().ToUpper(),
                        NombreActividad = item.GetValue("NombreActividad")?.ToString(),
                        NombreCategoria = item.GetValue("NombreCategoria")?.ToString(),
                        Valor = item.GetValue("Valor")?.ToString(),
                        Cantidad = item.GetValue("Cantidad")?.ToString(),
                        CodigoDescuento = item.GetValue("CodigoDescuento")?.ToString(),
                        AplicaImpuesto = item.GetValue("AplicaImpuesto")?.ToString(),
                        DetalleImpuesto = item.GetValue("DetalleImpuesto")?.ToString(),
                        Precio = item.GetValue("Precio")?.ToString(),
                        TitularFacturacion = item.GetValue("TitularFacturacion")?.ToString(),
                        NombreIdentificacion = item.GetValue("NombreIdentificacion")?.ToString(),
                        Identificacion = item.GetValue("Identificacion")?.ToString(),
                        IdIdentificacion = item.GetValue("IdIdentificacion")?.ToString(),
                        Email = item.GetValue("Email")?.ToString(),
                        Telefono = item.GetValue("Telefono")?.ToString(),
                        IdTransaccion = item.GetValue("IdTransaccion")?.ToString(),
                        IdBoleteria = item.GetValue("IdBoleteria")?.ToString(),
                        NoBoleta = item.GetValue("NoBoleta")?.ToString(),
                        EstadoBoleta = item.GetValue("EstadoBoleta")?.ToString(),
                        DescripcionEstado = item.GetValue("DescripcionEstado")?.ToString(),
                        FechaTransaccion = item.GetValue("FechaTransaccion")?.ToString(),
                        Qr = item.GetValue("Qr")?.ToString(),
                        FechaIngreso = item.GetValue("FechaIngreso")?.ToString(),
                        IdCiudad = item.GetValue("IdCiudad")?.ToString()
                    };

                    if (!string.IsNullOrWhiteSpace(item.GetValue("Qr")?.ToString()))
                    {
                        if (item.GetValue("Qr")?.ToString() != "0")
                        {
                            string urlWeb = item.GetValue("Qr").ToString().Trim();
                            string Var_Sub = urlWeb.Trim().Substring(urlWeb.Length - 4, 4);

                            if (Var_Sub.ToLower().Equals(".png") || Var_Sub.ToLower().Equals(".jpg"))
                            {
                                misBoletas.VerCodigoQR = false;
                                misBoletas.VerImagenFeria = true;
                                misBoletas.ImagenFeria = item.GetValue("Qr")?.ToString();
                            }
                            else
                            {
                                try
                                {
                                    misBoletas.VerCodigoQR = true;
                                    misBoletas.VerImagenFeria = false;
                                    misBoletas.ImagenFeria = item.GetValue("Qr")?.ToString();
                                    misBoletas.QrImage = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(GenerQr_Clicked(item.GetValue("Qr")?.ToString()))));
                                }
                                catch
                                {
                                    misBoletas.VerCodigoQR = false;
                                    misBoletas.VerImagenFeria = true;
                                    misBoletas.ImagenFeria = "logo_app_negro.png";
                                }
                            }
                        }
                        else
                        {
                            misBoletas.VerCodigoQR = false;
                            misBoletas.VerImagenFeria = true;
                            misBoletas.ImagenFeria = "logo_app_negro.png";
                        }
                    }
                    else
                    {
                        misBoletas.VerCodigoQR = false;
                        misBoletas.VerImagenFeria = true;
                        misBoletas.ImagenFeria = "logo_app_negro.png";
                    }

                    ListaMisBoletas.Add(misBoletas);

                    ListaMisBoletas = ListaMisBoletas.OrderByDescending(x => x.FechaIngresoFiltro).ToList();
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BoleteriaVm", "CargarListaMisBoletas_Mtd", "consultaconfirmacionboleteriapp");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private string GenerQr_Clicked(string codigo)
        {/*
            try
            {
                Console.WriteLine("Base64 QR en iOS: codigo" + codigo);
                var writer = new BarcodeWriterPixelData
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new EncodingOptions
                    {
                        Height = 450,
                        Width = 450,
                        Margin = 0
                    }
                };

                var pixelData = writer.Write(codigo);
                var imageInfo = new SKImageInfo(pixelData.Width, pixelData.Height, SKColorType.Bgra8888, SKAlphaType.Premul);

                using (var surface = SKSurface.Create(imageInfo))
                {
                    surface.Canvas.Clear(SKColors.White);

                    using (var bitmap = new SKBitmap(imageInfo))
                    {
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmap.GetPixels(), pixelData.Pixels.Length);
                        surface.Canvas.DrawBitmap(bitmap, 0, 0);
                    }

                    using (var image = surface.Snapshot())
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                    {
                        byte[] imageBytes = data.ToArray();
                        string base64String = Convert.ToBase64String(imageBytes);


                        Console.WriteLine("Base64 QR en iOS: " + base64String);

                        return base64String;
                    }
                }
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BoleteriaVm", "generQr_Clicked", "Error al Generar Codigo QR");
                Console.WriteLine("Base64 QR en iOS: " + "error");
                Console.WriteLine("Base64 QR en iOS: " + ex.Message);
                return codigo;
            }
      
        */
            return codigo;
        }


        /*SECCION PREREGISTRO MIS INVITACIONES*/
        public void BtnPreRegistro_Mtd()
        {
            IsBusy = true;
            VerBtnComprar = false;
            VerListaMisBoletas = false;
            VerListaPreRegistro = true;

            ColorFondoBoletas = Colors.WhiteSmoke;
            ColorTextoBoletas = Color.FromArgb("#333333");

            ColorFondoCompra = Colors.WhiteSmoke;
            ColorTextoCompra = Color.FromArgb("#333333");

            ColorFondoPreregistro = Color.FromArgb("#041B3B");
            ColorTextoPreregistro = Colors.WhiteSmoke;

            CargarListasPreregistro_Mtd();
        }

        public async void CargarListasPreregistro_Mtd()
        {
            try
            {
                /*Validar Info preregistro*/
                string urliTres = logicaWs.Movile_Select_preregistro_Mtd(Preferences.Get("IdIdentificacion", "").ToString(), Preferences.Get("numeroIdentificacion", "").ToString(), Preferences.Get("IdSuceso", ""), EmailUsuario);
                string jsonTres = JsonConvert.SerializeObject("");
                string jsonProcedimiento = await logicaWs.ConectionPost(jsonTres, urliTres);

                if (jsonProcedimiento != "\"Finalizo\"")
                    claseBase.InsertarLogs_Mtd("ERROR", jsonProcedimiento, "TaquillaVM", "BtnPreRegistro_Mtd", "preregistroservicio");

            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "TaquillaVM", "BtnPreRegistro_Mtd", "preregistroservicio");
            }

            try
            {
                if (!StateConexion)
                {
                    string urli = logicaWs.Movile_Select_PreRegistroConsultaGeneral_Mtd(EmailUsuario, "0", "0", Preferences.Get("IdSuceso", ""), LenguajeBase);
                    string? jsonProcedimiento = await logicaWs.ConectionGet(urli);
                    JArray jsArray = JArray.Parse(jsonProcedimiento);
                    ListaPreRegistro = new List<PreRegistroTaquilla>();
                    foreach (JObject item in jsArray)
                    {
                        PreRegistroTaquilla preRegistro = new PreRegistroTaquilla(item.GetValue("Cons")?.ToString() ?? string.Empty, item.GetValue("IdSuceso")?.ToString() ?? string.Empty,
                            item.GetValue("NombreSuceso")?.ToString() ?? string.Empty, item.GetValue("CodigoInvitacion")?.ToString() ?? string.Empty, item.GetValue("TieneInvitacion")?.ToString() ?? string.Empty,
                            item.GetValue("EstadoSolicitud")?.ToString() ?? string.Empty, item.GetValue("CodigoQR")?.ToString() ?? string.Empty, item.GetValue("Vigencia")?.ToString() ?? string.Empty,
                            item.GetValue("NombreCompleto")?.ToString() ?? string.Empty, item.GetValue("Identificacion")?.ToString() ?? string.Empty, item.GetValue("NombreIdentificacion")?.ToString() ?? string.Empty,
                            item.GetValue("NombreProfesion")?.ToString() ?? string.Empty, item.GetValue("NombreRecinto")?.ToString() ?? string.Empty, item.GetValue("DiaIn")?.ToString() ?? string.Empty,
                            item.GetValue("MesIn")?.ToString() ?? string.Empty);


                        if (!string.IsNullOrWhiteSpace(item.GetValue("Qr")?.ToString()))
                        {
                            if (item.GetValue("Qr")?.ToString() != "0")
                            {
                                string urlWeb = item.GetValue("CodigoQR")?.ToString() ?? string.Empty.Trim();
                                string Var_Sub = urlWeb.Trim().Substring(urlWeb.Length - 4, 4);

                                if (Var_Sub.ToLower().Equals(".png") || Var_Sub.ToLower().Equals(".jpg"))
                                {
                                    preRegistro.VerCodigoQR = false;
                                    preRegistro.VerImagenFeria = true;
                                    preRegistro.ImagenFeria = item.GetValue("CodigoQR")?.ToString();
                                }
                                else
                                {
                                    try
                                    {
                                        preRegistro.VerCodigoQR = true;
                                        preRegistro.VerImagenFeria = false;
                                        preRegistro.ImagenFeria = item.GetValue("CodigoQR")?.ToString();
                                        preRegistro.QrImage = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(GenerQr_Clicked(item.GetValue("CodigoQR")?.ToString()))));
                                    }
                                    catch
                                    {
                                        preRegistro.VerCodigoQR = false;
                                        preRegistro.VerImagenFeria = true;
                                        preRegistro.ImagenFeria = "logo_app_negro.png";
                                    }
                                }
                            }
                            else
                            {
                                preRegistro.VerCodigoQR = false;
                                preRegistro.VerImagenFeria = true;
                                preRegistro.ImagenFeria = "logo_app_negro.png";
                            }
                        }
                        else
                        {
                            preRegistro.VerCodigoQR = false;
                            preRegistro.VerImagenFeria = true;
                            preRegistro.ImagenFeria = "logo_app_negro.png";
                        }

                        ListaPreRegistro.Add(preRegistro);
                    }

                    ListaPreRegistro = ListaPreRegistro.OrderBy(x => x.Vigencia).ToList();
                }
                else
                {
                    await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMRevisaTuConexion, AppResources.VMaceptar);
                }
            }
            catch (Exception ex)
            {
                await pageServicio.DisplayAlert(AppResources.nombreMarca, AppResources.VMServicioEnMantenimiento, AppResources.VMaceptar);
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "BoleteriaVm", "CargarListasPreregistro_Mtd", "consutlapreregistroappb1");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void SeleccionarModulo(MisBoletas lista)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                if (lista != null)
                {
                    await RootNavigation.PushAsync(new CodigoQr(lista.QrImage));
                }
            }
            catch
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void SeleccionarModuloDos(PreRegistroTaquilla lista)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100); // Da tiempo a la UI para actualizarse antes de navegar
                if (lista != null)
                {
                    await RootNavigation.PushAsync(new CodigoQr(lista.QrImage));
                }
            }
            catch
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
