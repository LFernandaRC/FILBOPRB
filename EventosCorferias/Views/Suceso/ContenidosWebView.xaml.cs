using EventosCorferias.Interfaces;
using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.Services;

namespace EventosCorferias.Views.Suceso;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ContenidosWebView : ContentPage
{
    public string TituloContenido
    {
        get;
        set;
    }
    public string Url
    {
        get;
        set;
    }

    public ContenidosWebView(string Titulo, string UrlWeb)
    {
        TituloContenido = Titulo;
        Url = Validateurl(UrlWeb);
        InitializeComponent();
        ValidarImagen(UrlWeb);
    }

    private string Validateurl(string urlWeb)
    {
        try
        {
            if (!urlWeb.Contains("http://"))
            {
                if (!urlWeb.Contains("https://"))
                {
                    urlWeb = "http://" + urlWeb;
                }
            }
            return urlWeb;
        }
        catch
        {
            return urlWeb;
        }
    }

    void WebviewNavigating(object sender, WebNavigatingEventArgs e)
    {
        labelLoading.IsVisible = true;
    }

    void WebviewNavigated(object sender, WebNavigatedEventArgs e)
    {
        labelLoading.IsVisible = false;
    }

    private bool ValidarImagen(string urlWeb)
    {
        try
        {
            string Var_Sub = urlWeb.Trim().Substring(urlWeb.Trim().Length - 4, 4);

            if (Var_Sub.ToLower().Equals(".jpg") || Var_Sub.ToLower().Equals(".png"))
            {
                contenIme.IsVisible = true;
                contenWeb.IsVisible = false;
                return true;
            }
            else
            {
                contenIme.IsVisible = false;
                contenWeb.IsVisible = true;
                return false;
            }
        }
        catch (Exception ex)
        {
            contenIme.IsVisible = false;
            contenWeb.IsVisible = true;
            return false;
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        IPageServicio pageServicio = new PageServicio();
        await pageServicio.DisplayAlert(AppResources.nombreMarca,AppResources.ayudaZoom, AppResources.VMaceptar);
    }

}
