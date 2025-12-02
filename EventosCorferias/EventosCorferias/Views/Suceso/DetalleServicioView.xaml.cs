using EventosCorferias.Models;
using EventosCorferias.ViewModel.Suceso;

namespace EventosCorferias.Views.Suceso;

public partial class DetalleServicioView : ContentPage
{
    private bool PrimerEntrada;
    private Image[] ListPuntos;

    public DetalleServicioView(ServiciosM servicios)
    {
        InitializeComponent();
        BindingContext = new DetalleServicioVm(servicios);
        PrimerEntrada = false;
    }

    void OnCarouselViewScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        try
        {
            var vm = BindingContext as DetalleServicioVm;
            int auxIndex = (e.LastVisibleItemIndex);

            if (!PrimerEntrada)
            {
                PrimerEntrada = true;
                int cantidad = vm.ImagenesCarrusel.Count;
                ListPuntos = new Image[cantidad];
                for (int i = 0; i < cantidad; i++)
                {
                    Image image = new Image { Source = "ic_puntoClaro.png", HorizontalOptions = LayoutOptions.Center, WidthRequest = 8, ClassId = i.ToString() };
                    CaruselIndex.Children.Add(image);
                    ListPuntos[i] = image;
                }
                ListPuntos[0].Source = "ic_puntoOscuro.png";
            }
            else
            {
                foreach (var aux in ListPuntos)
                {
                    if (aux.ClassId == auxIndex.ToString())
                        aux.Source = "ic_puntoOscuro.png";
                    else
                        aux.Source = "ic_puntoClaro.png";
                }
            }
        }
        catch
        {
            Image image = new Image { Source = "ic_puntoOscuro.png", HorizontalOptions = LayoutOptions.Center, WidthRequest = 8 };
            CaruselIndex.Children.Add(image);
        }
    }
}